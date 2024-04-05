

using DuoLatera.Repository.IRepository;

namespace DuoLatera.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _context;
        public IIdentityUserRepository Users { get; set; }
        public IFolderRepository Folders { get; set; }
        public ICardSetRepository Sets { get; set; }
        public IFlashCardRepository Cards { get; set; }



        public UnitOfWork(ApplicationDbContext db)
        {
            _context = db;
            Users = new IdentityUserRepository(_context);
            Folders = new FolderRepository(_context);
            Sets = new CardSetRepository(_context);
            Cards    = new FlashCardRepository(_context);
            
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
