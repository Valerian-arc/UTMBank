using Core.DTOs.CardActions;
using Core.DTOs.CardDTOs;
using Core.DTOs.Transfer;
using Core.Models;
using Domain.Entites;
using System.Collections.Generic;

namespace Core.Services.CardServices
{
    public interface ICardService
    {
        void Create(CardCreateDTO card);
        void Update(CardUpdateDTO card);
        void Delete(int cardId);
        IEnumerable<Card> GetAll();
        Card GetById(int id);
        TransferResult TransferToAnotherPerson(ExternalTransferDTO model);
        TransferResult TransferToLocalCard(LocalTransferDTO model);
        TransferResult Deposit(DepositTransferDTO model);
        TransferResult Withdraw(DepositTransferDTO model);
        List<HistoryLog> GetHistory();
    }
}
