using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuoLatera.Models
{
    public class CardSet
    {
        [Required,Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        public string? Subject { get; set; }
        
        public DateTime CreateDate { get; set; }
        public int FolderId { get; set; }
        [ForeignKey("FolderId")]
        [ValidateNever]
        public Folder Folder { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<FlashCard> FlashCards { get; set; }
    }
}
