using DuoLatera.Managers;
using DuoLatera.Models;
using DuoLatera.Models.ViewModels;
using DuoLatera.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static NuGet.Packaging.PackagingConstants;

namespace DuoLatera.Controllers
{
    public class FolderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginManager _loginManager;
        public FolderController(IUnitOfWork unitOfWork, ILoginManager loginManager)
        {
            _loginManager = loginManager;
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult Index(bool browse)
        {

            var userId = _loginManager.GetLoggedUserId();

            IEnumerable<Folder> _folderList;

            if(!browse)
            _folderList = _unitOfWork.Folders.GetAll(f => f.UserId == userId).ToList();
            else
            _folderList = _unitOfWork.Folders.GetAll(f => f.UserId != userId).Where(f => f.Access == "Public");

            var foldersVM = new FoldersVM()
            {
                folders = _folderList,
                browse = browse
            };

            return View(foldersVM);

        }
        [Authorize]
        public IActionResult UpSert(int? id)
        {
            if (id==null)
            {

            return View(new FolderVM());
            }
            else
            {
                if (!_loginManager.CheckFolderOwnership((int)id)) return RedirectToAction("Home", "Index");
                FolderVM folderVM = new FolderVM();
                folderVM.Folder = _unitOfWork.Folders.Get(f => f.Id == id);
                return View(folderVM);
            }
        }
        [HttpPost]
        [ActionName("UpSert")]
        [Authorize]
        public IActionResult UpSertGet(FolderVM obj) 
        { 
            if(obj.Folder.Id==0)
            {
                obj.Folder.CreateDate = DateTime.Now;
                var userId = _loginManager.GetLoggedUserId();
                obj.Folder.UserId = userId;
                _unitOfWork.Folders.Add(obj.Folder);
            }
            else
            {
                var userId = _loginManager.GetLoggedUserId();
                obj.Folder.UserId = userId;
                _unitOfWork.Folders.Update(obj.Folder);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index","Folder"); 
        }
        public IActionResult Delete(int id)
        {
            if (!_loginManager.CheckFolderOwnership(id)) return RedirectToAction("Home", "Index");
            Folder folder = _unitOfWork.Folders.Get(f => f.Id == id);
            _unitOfWork.Folders.Remove(folder);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Folder");
        }
        public IActionResult UpdateFolder() { return View(); }
    }
}
