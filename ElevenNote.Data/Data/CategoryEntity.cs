using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Data.Data
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string CategoryTitle { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset ModifiedUtc { get; set; }

        public List<NoteEntity> Notes { get; set; } = new List<NoteEntity>();
    }
}