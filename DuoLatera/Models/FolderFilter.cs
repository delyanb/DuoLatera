using DuoLatera.Repository.IRepository;

namespace DuoLatera.Models
{
    public class FolderFilter : Filter
    {
        private bool showMyFolders { get; set; } = true;
        private bool showPublicFolders { get; set; } = false;
        private bool searchFilter { get; set; } = false;
        private string searchQuery { get; set; }
        private string userId { get; set; }
        public IEnumerable<Folder> filteredFolders { get; set; } = new List<Folder>();
        private FolderFilter() { }
        public class Builder
        {
            private readonly FolderFilter _folderFilter = new FolderFilter();
            private IEnumerable<Folder> _startfolders;
            private List<Folder> _endfolders = new List<Folder>();

            public Builder ForUser(string userId)
            {
                _folderFilter.userId = userId;
                return this;
            }
            public Builder WithFolders(IEnumerable<Folder> folders)
            {
                _startfolders = folders;
                return this;
            }
            public Builder ShowMyFolders(bool value)
            {
                _folderFilter.showMyFolders = value;
                return this;
            }
            public Builder ShowPublicFolders(bool value = true)
            {
                _folderFilter.showPublicFolders = value;
                return this;
            }

            public Builder EnableSearch(string query)
            {
                _folderFilter.searchFilter = !string.IsNullOrEmpty(query);
                _folderFilter.searchQuery = query;
                return this;
            }
            public FolderFilter Build()
            {
                if (_folderFilter.userId == string.Empty)
                {
                    //to-add
                }
                if(_folderFilter.showMyFolders)
                {
                    _endfolders.AddRange(_startfolders.Where(f => f.UserId== _folderFilter.userId));
                }
                if (_folderFilter.showPublicFolders)
                {
                    _endfolders.AddRange(_startfolders.Where(f => f.UserId!= _folderFilter.userId)); 
                }
                if(_folderFilter.searchFilter)
                {
                    _endfolders = _endfolders
                    .Where(f => f.Name.Contains(_folderFilter.searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                f.Description.Contains(_folderFilter.searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                }
                _folderFilter.filteredFolders = _endfolders;
                return _folderFilter;


            }
           
            
        }

    }
}
