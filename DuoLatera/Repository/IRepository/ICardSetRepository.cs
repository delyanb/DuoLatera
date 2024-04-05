using DuoLatera.Models;

namespace DuoLatera.Repository.IRepository
{
    public interface ICardSetRepository : IRepository<CardSet>
    {
        void Update(CardSet obj);
    }
}
