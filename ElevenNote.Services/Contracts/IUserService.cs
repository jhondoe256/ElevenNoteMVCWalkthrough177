using ElevenNote.Models.Notes;
using ElevenNote.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services.Contracts
{
    public interface IUserService
    {
        Task<UserDetail> GetUserAsync();
    }
}
