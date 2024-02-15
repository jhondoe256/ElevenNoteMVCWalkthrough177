using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data.Data
{
    public class NoteEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]

        public string Title { get; set; }= string.Empty;
        
        [Required]
        [MaxLength(8000)]
        public string NoteContent { get; set; } = string.Empty;
        
        public DateTimeOffset CreatedUtc{ get; set; }
        
        public DateTimeOffset ModifiedUtc{ get; set; }

        public int CategoryEntityId { get; set; }

        [ForeignKey(nameof(CategoryEntityId))]
        public CategoryEntity CategoryEntity { get; set; }

        public string UserEntityId { get; set; }

        [ForeignKey(nameof(UserEntityId))]
        public UserEntity UserEntity { get; set; }
    }
}
