using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data.Data
{
    public class UserEntity: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<NoteEntity> UserNotes { get; set; } = new List<NoteEntity>();
    }
}
