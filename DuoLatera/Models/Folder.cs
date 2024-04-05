using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuoLatera.Models
{
    public class Folder
    {
        [Required, Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Access { get; set; }
        [StringLength(50, MinimumLength = 0)]
        public string? Description { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public IdentityUser User { get; set; }
        public string? ImageURL { get; set; }

    }
}
