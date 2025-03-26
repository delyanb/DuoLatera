using DuoLatera.Models;
using DuoLatera.Models.ViewModels;
using DuoLatera.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DuoLatera.Managers
{
    public class LoginManager : ILoginManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public LoginManager(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        //to fix
        public bool CheckFolderOwnership(int id)
        {
            string folderOwnerId = _unitOfWork.Folders.Get(f => f.Id == id).UserId;
            if (folderOwnerId == GetLoggedUserId()) return true;
            return false;
        }

        //to fix
        public bool CheckSetOwnership(int id)
        {
            int folderId = _unitOfWork.Sets.Get(s=>s.Id ==id).FolderId;
            string folderOwnerId = _unitOfWork.Folders.Get(f => f.Id == folderId).UserId;
            if (folderOwnerId == GetLoggedUserId()) return true;
            return false;
        }

        public string GetLoggedUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null) return null;

            var claimsIdentity = (ClaimsIdentity)user.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
