using ElevenNote.Models.Categories;

namespace ElevenNote.Models.Notes
{
    public class NoteDetail
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string NoteContent { get; set; } = string.Empty;

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset ModifiedUtc { get; set; }

        public CategoryListItem CategoryEntity { get; set; }
    }
}
