using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuoLatera.Models
{
    public class FlashCard
    {
        [Required,Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Text1 { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Text2 { get; set; }
        public int? CardSetId { get; set; }
        [ForeignKey("CardSetId")]
        [ValidateNever]
        public CardSet CardSet { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public IdentityUser User { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
