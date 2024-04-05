namespace DuoLatera.Repository.IRepository
{
    public interface IUnitOfWork
    {

        public IIdentityUserRepository Users { get; set; }
        public IFolderRepository Folders { get; set; }
        public ICardSetRepository Sets { get; set; }
        public IFlashCardRepository Cards { get; set; }
        void Save();
    }
}
