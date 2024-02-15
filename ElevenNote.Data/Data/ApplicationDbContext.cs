using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElevenNote.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NoteEntity> Notes { get; set; }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }
        public DbSet<UserEntity> AppUsers { get; set; }

    }
}
