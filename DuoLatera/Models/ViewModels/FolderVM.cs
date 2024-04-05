using Microsoft.AspNetCore.Mvc.Rendering;

namespace DuoLatera.Models.ViewModels
{
    public class FolderVM
    {
        public FolderVM() 
        {
            Folder = new Folder();
            options =
            [
                new SelectListItem { Text = "Public", Value = "Public" },
                new SelectListItem { Text = "Private", Value = "Private" },
            ];
        }
        public Folder Folder { get; set; }
        public List<SelectListItem> options { get; set; } 
    }
}
