namespace DuoLatera.Models.ViewModels
{
    public class CardSetVM
    {
        public IEnumerable<CardSet> cardSets { get; set; }
        public int FolderId { get; set; }

        public bool IsMine { get; set; }
    }
}
