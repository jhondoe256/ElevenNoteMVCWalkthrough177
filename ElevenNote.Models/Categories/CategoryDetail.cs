using ElevenNote.Models.Notes;

namespace ElevenNote.Models.Categories
{
    public class CategoryDetail
    {
     
        public int Id { get; set; }

        public string CategoryTitle { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset ModifiedUtc { get; set; }

        public List<NoteListItem> Notes { get; set; } = new List<NoteListItem>();
    }
}
