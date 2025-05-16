using AutoMapper;
using Core.DTOs.CardActions;
using Core.DTOs.CardDTOs;
using Core.DTOs.Transfer;
using Core.Models.CardModels;
using Core.Services.CardServices;
using Domain.Entites;
using System.Linq;
using System.Web.Mvc;
using Web.Attributes;

namespace Web.Controllers
{
    [RequireLogin]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
            _mapper = DependencyResolver.Current.GetService<IMapper>();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var cards = _cardService.GetAll();
            return View(cards);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UpsertCardModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userDTO = _mapper.Map<CardCreateDTO>(model);
            _cardService.Create(userDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var card = _cardService.GetById(id);
            if (card == null)
                return HttpNotFound();

            var model = _mapper.Map<UpsertCardModel>(card);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UpsertCardModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var cardDto = _mapper.Map<CardUpdateDTO>(model);
            _cardService.Update(cardDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Card/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var card = _cardService.GetById(id);
            if (card == null)
            {
                return HttpNotFound();
            }

            _cardService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ExternalTransfer()
        {
            var cards = _cardService.GetAll();
            var model = new ExternalTransferDTO
            {
                Cards = cards.Select(c => new Card
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult LocalTransfer()
        {
            var cards = _cardService.GetAll();
            var model = new LocalTransferDTO
            {
                Cards = cards.Select(c => new Card
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult ExternalTransfer(ExternalTransferDTO model)
        {
            if (!ModelState.IsValid)
            {
                var cards = _cardService.GetAll();
                model.Cards = cards.Select(c => new Card { Id = c.Id, Name = c.Name }).ToList();
                return View(model);
            }

            var transferResult = _cardService.TransferToAnotherPerson(model);
            if (!transferResult.Success)
            {
                var cards = _cardService.GetAll();
                model.Cards = cards.Select(c => new Card { Id = c.Id, Name = c.Name }).ToList();
                ModelState.AddModelError("", transferResult.Message);
                return View(model);
            }

            return RedirectToAction("ExternalTransferSuccess");
        }


        [HttpPost]
        public ActionResult LocalTransfer(LocalTransferDTO model)
        {
            if (!ModelState.IsValid)
            {
                var cards = _cardService.GetAll();
                ViewBag.Cards = new SelectList(cards, "Id", "Name");
                return View(model);
            }

            var transferResult = _cardService.TransferToLocalCard(model);
            if (!transferResult.Success)
            {
                var cards = _cardService.GetAll();
                ViewBag.Cards = new SelectList(cards, "Id", "Name");
                ModelState.AddModelError("", transferResult.Message);
                return View(model);
            }

            return RedirectToAction("LocalTransferSuccess");
        }


        [HttpGet]
        public ActionResult Withdraw()
        {
            var cards = _cardService.GetAll().ToList();
            var model = new DepositTransferDTO
            {
                Cards = cards
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Withdraw(DepositTransferDTO model)
        {
            if (!ModelState.IsValid)
            {
                model.Cards = _cardService.GetAll().ToList();
                return View(model);
            }

            var withdrawResult = _cardService.Withdraw(model);

            if (!withdrawResult.Success)
            {
                model.Cards = _cardService.GetAll().ToList();
                ModelState.AddModelError("", withdrawResult.Message);
                return View(model);
            }

            return RedirectToAction("WithdrawSuccess");
        }

        [HttpGet]
        public ActionResult Deposit()
        {
            var cards = _cardService.GetAll();
            var model = new DepositTransferDTO
            {
                Cards = cards.Select(c => new Card
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Deposit(DepositTransferDTO model)
        {
            if (!ModelState.IsValid)
            {
                var cards = _cardService.GetAll();
                model.Cards = cards.Select(c => new Card { Id = c.Id, Name = c.Name }).ToList();
                return View(model);
            }

            var result = _cardService.Deposit(model);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                var cards = _cardService.GetAll();
                model.Cards = cards.Select(c => new Card { Id = c.Id, Name = c.Name }).ToList();
                return View(model);
            }

            return RedirectToAction("DepositSuccess");
        }

        [HttpGet]
        public ActionResult History()
        {
            var history = _cardService.GetHistory();

            if (history == null || !history.Any())
            {
                return RedirectToAction("Index");
            }

            return View(history);
        }

        [HttpGet]
        public ActionResult LocalTransferSuccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ExternalTransferSuccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult WithdrawSuccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DepositSuccess()
        {
            return View();
        }
    }
}