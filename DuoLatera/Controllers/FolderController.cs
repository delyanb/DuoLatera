using DuoLatera.Managers;
using DuoLatera.Models;
using DuoLatera.Models.ViewModels;
using DuoLatera.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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
        public IActionResult Index(bool allfolders, bool? myfolders, string searchQuery)
        {
            if (!myfolders.HasValue)
            {
                myfolders = true;
            }

            var userId = _loginManager.GetLoggedUserId();

            var filter = new FolderFilter.Builder()
                .ForUser(userId)
                .WithFolders(_unitOfWork.Folders.GetAll()) // Get folders from the database
                .ShowMyFolders((bool)myfolders)
                .ShowPublicFolders(allfolders)
                .EnableSearch(searchQuery)
                .Build();

            var folderVM = new FoldersVM()
            {
                folders = filter.filteredFolders,
                userId = userId
            };

            return View(folderVM);

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
