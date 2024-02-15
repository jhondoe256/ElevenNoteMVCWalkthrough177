using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models.Notes
{
    public class NoteCreate
    {
 
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(8000)]
        public string NoteContent { get; set; } = string.Empty;

        [Required]
        public int CategoryEntityId { get; set; }

        public string UserEntityId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();

    }
}
