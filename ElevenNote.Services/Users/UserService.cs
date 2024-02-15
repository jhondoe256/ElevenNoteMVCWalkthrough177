using ElevenNote.Data.Data;
using ElevenNote.Models.Notes;
using ElevenNote.Models.Users;
using ElevenNote.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private string _userId;

        public UserService(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<UserDetail> GetUserAsync()
        {
            ProcessUserInfo();
            var user =  await _context.AppUsers.Where(u=>u.Id == _userId).Include(a=>a.UserNotes)
                                .ThenInclude(n=>n.CategoryEntity)
                                .FirstOrDefaultAsync(x => x.Id == _userId.ToString());

            return new UserDetail
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserNotes = user.UserNotes.Select(u=>new NoteListItem 
                {
                    Id = u.Id,
                    Title = u.Title,
                    CategoryTitle = u.CategoryEntity.CategoryTitle,
                    CreatedUtc = DateTime.UtcNow,
                }).ToList()

            };
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
