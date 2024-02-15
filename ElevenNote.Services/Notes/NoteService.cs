using ElevenNote.Data.Data;
using ElevenNote.Models.Categories;
using ElevenNote.Models.Notes;
using ElevenNote.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services.Notes
{
    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private string _userId;
        
        public NoteService(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {

            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> AddNoteAsync(NoteCreate model)
        {
            ProcessUserInfo();
            var entity = new NoteEntity
            {
                Title = model.Title,
                NoteContent = model.NoteContent,
                CategoryEntityId = model.CategoryEntityId,
                CreatedUtc = DateTimeOffset.UtcNow,
                UserEntityId = _userId
            };

            await _context.Notes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNoteAsync(Guid id)
        {
            ProcessUserInfo();
            var note = await _context.Notes.Where(n => n.UserEntityId == _userId)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (note == null) return false;
            
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditNoteAsync(NoteEdit model)
        {
            ProcessUserInfo();

            var note = 
                await 
                _context
                .Notes
                .Where(n => n.UserEntityId == _userId)
                .FirstOrDefaultAsync(x=>x.Id == model.Id);

            if (note == null) return false;

            note.Title = model.Title;
            note.NoteContent = model.NoteContent;
            note.CategoryEntityId = model.CategoryEntityId;
            note.ModifiedUtc = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<NoteDetail> GetNoteAsync(Guid id)
        {
            ProcessUserInfo();

            var note = 
                await 
                _context
                .Notes
                .Where(n => n.UserEntityId == _userId)
                .Include(n=>n.CategoryEntity)
                .FirstOrDefaultAsync(x=>x.Id==id);
            
            if (note == null) return null;

            return new NoteDetail
            {
                Id = note.Id,
                Title = note.Title,
                NoteContent = note.NoteContent,
                CategoryEntity = new CategoryListItem
                {
                    Id= note.CategoryEntity.Id,
                    CategoryTitle = note.CategoryEntity.CategoryTitle,
                    CreatedUtc = note.CategoryEntity.CreatedUtc
                },
                CreatedUtc= note.CreatedUtc,
                ModifiedUtc = note.ModifiedUtc
            };
        }

        public async Task<List<NoteListItem>> GetNotesAsync()
        {
            ProcessUserInfo();
            return 
                await
                _context.Notes
                .Where(n=>n.UserEntityId == _userId)
                .Include(n=>n.CategoryEntity)
                .Select(n=>new NoteListItem 
            {
                Id = n.Id,
                Title = n.Title,
                CategoryTitle= n.CategoryEntity.CategoryTitle,
                CreatedUtc = n.CreatedUtc
            }).ToListAsync();
        }

        private void ProcessUserInfo()
        {
            var claims = _contextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
            
            var value = claims!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            
            if (value == null) throw new Exception("Unable to process credentials.");
            
            _userId = value;
        }
    }
}
