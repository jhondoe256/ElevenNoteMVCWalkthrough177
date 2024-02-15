using ElevenNote.Models.Categories;
using ElevenNote.Models.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services.Contracts
{
    public interface INoteService
    {
        Task<bool> AddNoteAsync(NoteCreate model);
        Task<bool> EditNoteAsync(NoteEdit model);
        Task<bool> DeleteNoteAsync(Guid id);
        Task<NoteDetail> GetNoteAsync(Guid id);
        Task<List<NoteListItem>> GetNotesAsync();
    }
}
