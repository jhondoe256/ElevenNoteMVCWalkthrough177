using AutoMapper;
using ElevenNote.Data.Data;
using ElevenNote.Models.Notes;
using ElevenNote.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services.Notes
{
    public class MappedNoteService : INoteService
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
       
        public MappedNoteService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddNoteAsync(NoteCreate model)
        {
            var entity = _mapper.Map<NoteEntity>(model);
            entity.CreatedUtc = DateTime.UtcNow;
            await _context.Notes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNoteAsync(Guid id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
            if (note == null) return false;

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditNoteAsync(NoteEdit model)
        {
            var note = await
               _context
               .Notes
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (note == null) return false;

            var conversion = _mapper.Map(model, note);
            conversion.ModifiedUtc = DateTimeOffset.UtcNow;

            _context.Notes.Update(conversion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<NoteDetail> GetNoteAsync(Guid id)
        {
            return _mapper.Map<NoteDetail>(await 
                                            _context
                                            .Notes
                                            .Include(n => n.CategoryEntity)
                                            .FirstOrDefaultAsync(x=>x.Id ==id));
        }

        public async Task<List<NoteListItem>> GetNotesAsync()
        {
            return _mapper.Map<List<NoteListItem>>(await 
                                                    _context
                                                    .Notes.
                                                    Include(n => n.CategoryEntity)
                                                    .ToListAsync());
        }
    }
}
