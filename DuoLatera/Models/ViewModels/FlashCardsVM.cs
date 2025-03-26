namespace DuoLatera.Models.ViewModels
{
    public class FlashCardsVM
    {
        public List<FlashCard> FlashCards { get; set; }
        public CardSet CardSet { get; set; }
        public string CardData { get; set; }
        public bool IsMine { get; set; }  
    }
}
