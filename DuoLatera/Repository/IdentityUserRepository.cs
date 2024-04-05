using DuoLatera.Repository.IRepository;
using DuoLatera.Models;
using Microsoft.AspNetCore.Identity;

namespace DuoLatera.Repository
{
    public class IdentityUserRepository :Repository<IdentityUser>, IIdentityUserRepository
    {
        private ApplicationDbContext _db;
        public IdentityUserRepository(ApplicationDbContext db) : base(db)
        {
            _db= db;
            
        }



    

        public void Update(IdentityUser obj)
        {
            _db.Users.Update(obj);
        }

        
    }
}
