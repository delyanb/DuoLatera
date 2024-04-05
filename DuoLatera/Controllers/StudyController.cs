using DuoLatera.Models;
using DuoLatera.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DuoLatera.Controllers
{
    public class StudyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Turning(int setId)
        {
            IEnumerable<FlashCard> flashCards = _unitOfWork.Cards.GetAll(c=>c.CardSetId==setId);
            string json = JsonConvert.SerializeObject(flashCards);
            ViewBag.FlashCards=json;
            return View();
        }
    }
}
