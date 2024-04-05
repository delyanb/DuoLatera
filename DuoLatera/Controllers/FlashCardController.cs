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
        public FlashCardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int setId)
        {
            var cards = _unitOfWork.Cards.GetAll(c=>c.CardSetId == setId).ToList();
            string json = JsonConvert.SerializeObject(cards);
            var cardVM = new FlashCardsVM { FlashCards = cards, 
                CardSet= _unitOfWork.Sets.Get(s=>s.Id == setId),
                CardData = json };
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
            return View(new FlashCardVM { FlashCard = new FlashCard(),setId=setId});
            
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePOST(FlashCardVM viewModel)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
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
            _unitOfWork.Cards.Remove(card);
            _unitOfWork.Save();
            return RedirectToAction("Index", new { setId = card.CardSetId });
        }
        public IActionResult Edit(int setId,int cardId)
        {
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
