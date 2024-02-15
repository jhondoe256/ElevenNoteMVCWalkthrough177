using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models.Notes
{
    public class NoteListItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string CategoryTitle { get; set; } = string.Empty;

        public DateTimeOffset CreatedUtc { get; set; }
    }
}
