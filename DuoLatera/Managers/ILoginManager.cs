namespace DuoLatera.Managers
{
    public interface ILoginManager
    {
        string GetLoggedUserId();
        bool CheckSetOwnership(int id);
        bool CheckFolderOwnership(int id);
        //returns true if the current folder or cardset is of the user
    }
}
