using DuoLatera.Models;
using Microsoft.AspNetCore.Identity;

namespace DuoLatera.Repository.IRepository
{
    
        public interface IFolderRepository : IRepository<Folder>
        {
            void Update(Folder obj);


        }
    
}
