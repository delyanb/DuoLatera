using DuoLatera.Models;

namespace DuoLatera.Repository.IRepository
{
    public interface IFlashCardRepository : IRepository<FlashCard>
    {
        void Update(FlashCard obj);
    }
}
