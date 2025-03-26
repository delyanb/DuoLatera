using DuoLatera.Managers;
using DuoLatera.Models;
using DuoLatera.Models.ViewModels;
using DuoLatera.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace DuoLatera.Controllers
{
    public class FlashCardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginManager _loginManager;
        public FlashCardController(IUnitOfWork unitOfWork, ILoginManager loginManager)
        {
            _loginManager = loginManager;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int setId)
        {
            bool isMine = false;

            if(_loginManager.CheckSetOwnership(setId)) isMine = true;

            var cards = _unitOfWork.Cards.GetAll(c=>c.CardSetId == setId).ToList();
            string json = JsonConvert.SerializeObject(cards);
            var cardVM = new FlashCardsVM { FlashCards = cards, 
                CardSet= _unitOfWork.Sets.Get(s=>s.Id == setId),
                CardData = json,
                IsMine = isMine};
            return View(cardVM);
        }
        public IActionResult SelectStudyMethod(int setId)
        {
            var cards = _unitOfWork.Cards.GetAll(c => c.CardSetId == setId).ToList();
            if(cards.Count==0)
            {
                TempData["Error"] = "Card Set Is Empty";
                return RedirectToAction("Index",new { setId = setId});
            }
            return View(setId);
        }
        public IActionResult Create(int setId)
        {
            if (!_loginManager.CheckSetOwnership(setId)) return RedirectToAction("Home", "Index");
            return View(new FlashCardVM { FlashCard = new FlashCard(),setId=setId});
            
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePOST(FlashCardVM viewModel)
        {

            var userId = _loginManager.GetLoggedUserId();
            viewModel.FlashCard.UserId= userId;
            viewModel.FlashCard.CreateDate = DateTime.Now;
            viewModel.FlashCard.CardSetId = viewModel.setId;
            _unitOfWork.Cards.Add(viewModel.FlashCard);
            _unitOfWork.Save();
            return RedirectToAction("Index", new { setId = viewModel.setId });

        }
        public IActionResult Delete(int id)
        {
            FlashCard card = _unitOfWork.Cards.Get(C => C.Id == id);
            if (!_loginManager.CheckSetOwnership((int)card.CardSetId)) return RedirectToAction("Home", "Index");
            _unitOfWork.Cards.Remove(card);
            _unitOfWork.Save();
            return RedirectToAction("Index", new { setId = card.CardSetId });
        }
        public IActionResult Edit(int setId,int cardId)
        {
            if (!_loginManager.CheckSetOwnership(setId)) return RedirectToAction("Home", "Index");
            var card = _unitOfWork.Cards.Get(c => c.Id == cardId);
            return View(new FlashCardVM { FlashCard=card,setId=setId});
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPOST(FlashCardVM obj)
        {
            _unitOfWork.Cards.Update(obj.FlashCard);
            _unitOfWork.Save();
            return RedirectToAction("Index", new {setId=obj.setId});
        }
    }
}
