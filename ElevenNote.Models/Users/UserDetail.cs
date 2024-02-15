using ElevenNote.Data.Data;
using ElevenNote.Models.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.Users
{
    public class UserDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<NoteListItem> UserNotes { get; set; } = new List<NoteListItem>();
    }
}
