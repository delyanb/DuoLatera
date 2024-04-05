using DuoLatera.Models;
using DuoLatera.Repository.IRepository;

namespace DuoLatera.Repository
{
    public class CardSetRepository : Repository<CardSet>, ICardSetRepository
    {
        private ApplicationDbContext _db;
        public CardSetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }





        public void Update(CardSet obj)
        {
            _db.CardSets.Update(obj);
        }


    }
}
