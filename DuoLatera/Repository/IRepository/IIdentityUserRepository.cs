
using Microsoft.AspNetCore.Identity;

namespace DuoLatera.Repository.IRepository
{
    public interface IIdentityUserRepository : IRepository<IdentityUser>
    {
        void Update(IdentityUser obj);
       

    }
}
