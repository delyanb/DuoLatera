using DuoLatera.Models;
using DuoLatera.Repository.IRepository;

namespace DuoLatera.Repository
{
    public class FlashCardRepository : Repository<FlashCard>, IFlashCardRepository
    {
        private ApplicationDbContext _db;
        public FlashCardRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }





        public void Update(FlashCard obj)
        {
            _db.FlashCards.Update(obj);
        }

    }
}
