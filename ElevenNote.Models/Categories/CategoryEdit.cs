using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models.Categories
{
    public class CategoryEdit
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryTitle { get; set; }
      
    }
}
