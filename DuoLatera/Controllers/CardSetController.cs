using DuoLatera.Models;
using DuoLatera.Models.ViewModels;
using DuoLatera.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DuoLatera.Controllers
{
    public class CardSetController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CardSetController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int id)
        {
            var cardSets = _unitOfWork.Sets.GetAll(s => s.FolderId == id);
            CardSetVM cardSetVM = new CardSetVM
            {
                cardSets = cardSets,
                FolderId = id,
            };
            return View(cardSetVM);
        }
        public IActionResult UpSert(int? id, int folderId)
        {
            if(id == null)
            {
                return View(new CardSet
                {
                    FolderId = folderId,
                });
            }
            else
            {
                var set = _unitOfWork.Sets.Get(s => s.Id == id);
                set.FolderId = folderId;
                return View(set);
            }
        }
        [HttpPost]
        [ActionName("UpSert")]
        [Authorize]
        public IActionResult UpSertGet(CardSet obj)
        {

            if (obj.Id == 0)
            {
                
                obj.CreateDate = DateTime.Now;
                _unitOfWork.Sets.Add(obj);
            }
            else
            {
                obj.CreateDate = DateTime.Now;
                _unitOfWork.Sets.Update(obj);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index", new { id = obj.FolderId });
        }
        public IActionResult Delete(int id)
        {
            CardSet set = _unitOfWork.Sets.Get(s => s.Id == id);
            _unitOfWork.Sets.Remove(set);
            _unitOfWork.Save();
            return RedirectToAction("Index", new {id= set.FolderId});
        }
    }
}
