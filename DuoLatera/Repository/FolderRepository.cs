
using DuoLatera.Models;
using DuoLatera.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace DuoLatera.Repository
{
    public class FolderRepository : Repository<Folder>, IFolderRepository
    {
        private ApplicationDbContext _db;
        public FolderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
        public void Update(Folder obj)
        {
            
            _db.Folders.Update(obj);
        }
    }
}
