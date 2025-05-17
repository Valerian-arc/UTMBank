using AizenBankV1.Web.Extensions;
using AutoMapper;
using Core.DTOs.CardActions;
using Core.DTOs.CardDTOs;
using Core.DTOs.Transfer;
using Core.Models;
using Domain.Entites;
using Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Services.CardServices
{
    public class CardService : ICardService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Card> _cardRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<HistoryLog> _historyLogRepository;

        public CardService(
            IMapper mapper, 
            IGenericRepository<Card> cardRepository,
            IGenericRepository<User> userRepository,
            IGenericRepository<HistoryLog> historyLogRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
            _userRepository = userRepository;
            _historyLogRepository = historyLogRepository;
        }

        public void Create(CardCreateDTO card)
        {
            var cardEntity = _mapper.Map<Card>(card);
            cardEntity.UserId = HttpContext.Current.GetMySessionObject().Id;
            _cardRepository.Add(cardEntity);
        }

        public void Update(CardUpdateDTO card)
        {
            var existingCard = _cardRepository.GetById(card.Id);
            if (existingCard != null)
            {
                _mapper.Map(card, existingCard);
                _cardRepository.Update(existingCard);
            }
        }

        public void Delete(int cardId)
        {
            if (cardId == null)
            {
                return;
            }
            _cardRepository.Delete(cardId);
        }

        public IEnumerable<Card> GetAll()
        {
            var userId = HttpContext.Current.GetMySessionObject().Id;

            return _cardRepository.GetAll().Where(c => c.UserId == userId);
        }

        public Card GetById(int id)
        {
            return _cardRepository.GetById(id);
        }

        public TransferResult TransferToAnotherPerson(ExternalTransferDTO model)
        {
            var recipient = _userRepository.GetAll().FirstOrDefault(user => user.Email == model.RecipientEmail);
            if (recipient == null)
            {
                return new TransferResult
                {
                    Success = false,
                    Message = "Recipient not found."
                };
            }

            var recipientCard = _cardRepository.GetAll().FirstOrDefault(card => card.UserId == recipient.Id);
            if (recipientCard == null)
            {
                return new TransferResult
                {
                    Success = false,
                    Message = "Recipient does not have a card."
                };
            }

            var sourceCard = _cardRepository.GetById(model.SourceCardId);
            if (sourceCard == null)
            {
                return new TransferResult
                {
                    Success = false,
                    Message = "Source card not found."
                };
            }

            if (sourceCard.Amount < model.Amount)
            {
                return new TransferResult
                {
                    Success = false,
                    Message = "Insufficient balance in the source card."
                };
            }

            recipientCard.Amount += model.Amount;
            sourceCard.Amount -= model.Amount;

            _cardRepository.Update(recipientCard);
            _cardRepository.Update(sourceCard);

            var historyLog = new HistoryLog
            {
                Source = HttpContext.Current.GetMySessionObject().Email + "(User)",
                Destination = model.RecipientEmail + "(User)",
                Amount = model.Amount,
                Type = "External Transfer"
            };
            historyLog.SourceUser = HttpContext.Current.GetMySessionObject().Id;
            historyLog.DestinationUser = recipient.Id;

            _historyLogRepository.Add(historyLog);

            return new TransferResult
            {
                Success = true,
                Message = string.Empty
            };
        }

        public TransferResult TransferToLocalCard(LocalTransferDTO model)
        {
            var sourceCard = _cardRepository.GetById(model.SourceCardId);
            if (sourceCard == null)
            {
                return new TransferResult
                {
                    Success = false,
                    Message = "Source card not found."
                };
            }

            var targetCard = _cardRepository.GetById(model.TargetCardId);
            if (targetCard == null)
            {
                return new TransferResult
                {
                    Success = false,
                    Message = "Target card not found."
                };
            }

            if (sourceCard.Amount < model.Amount)
            {
                return new TransferResult
                {
                    Success = false,
                    Message = "Insufficient balance in the source card."
                };
            }

            sourceCard.Amount -= model.Amount;
            targetCard.Amount += model.Amount;

            _cardRepository.Update(sourceCard);
            _cardRepository.Update(targetCard);

            var historyLog = new HistoryLog
            {
                Source = sourceCard.Name + "(Card)",
                Destination = targetCard.Name + "(Card)",
                Amount = model.Amount,
                Type = "Local Transfer"
            };
            historyLog.SourceUser = HttpContext.Current.GetMySessionObject().Id;

            _historyLogRepository.Add(historyLog);

            return new TransferResult
            {
                Success = true,
                Message = string.Empty
            };
        }

        public TransferResult Deposit(DepositTransferDTO model)
        {
            var card = _cardRepository.GetById(model.CardId);
            if (card == null)
            {
                return new TransferResult
                {
                    Success = false,
                    Message = "Card not found."
                };
            }

            card.Amount += model.Amount;

            _cardRepository.Update(card);

            var historyLog = new HistoryLog
            {
                Source = "External Card",
                Destination = card.Name + "(Card)",
                Amount = model.Amount,
                Type = "Deposit"
            };
            historyLog.SourceUser = HttpContext.Current.GetMySessionObject().Id;

            _historyLogRepository.Add(historyLog);

            return new TransferResult
            {
                Success = true,
                Message = "Deposit successful."
            };
        }

        public TransferResult Withdraw(DepositTransferDTO model)
        {
            var card = _cardRepository.GetById(model.CardId);
            if (card == null)
            {
                return new TransferResult
                {
                    Success = false,
                    Message = "Card not found."
                };
            }

            if (card.Amount < model.Amount)
            {
                return new TransferResult
                {
                    Success = false,
                    Message = "Insufficient funds."
                };
            }

            card.Amount -= model.Amount;

            _cardRepository.Update(card);

            var historyLog = new HistoryLog
            {
                Source = card.Name + "(Card)",
                Destination = "External destination",
                Amount = model.Amount,
                Type = "Withdraw"
            };
            historyLog.SourceUser = HttpContext.Current.GetMySessionObject().Id;

            _historyLogRepository.Add(historyLog);

            return new TransferResult
            {
                Success = true,
                Message = "Withdrawal successful."
            };
        }

        public List<HistoryLog> GetHistory()
        {
            var user = HttpContext.Current.GetMySessionObject();

            var history = _historyLogRepository.GetAll()
                .Where(h => h.DestinationUser == user.Id || h.SourceUser == user.Id)
                .GroupBy(h => new { h.SourceUser, h.DestinationUser, h.Amount, h.Type })
                .Select(g => g.First())
                .ToList();

            var mergedHistory = history
                .GroupBy(h => new { h.Amount, h.Type })
                .Select(g => g.First())
                .ToList();

            return mergedHistory;
        }

    }
}
