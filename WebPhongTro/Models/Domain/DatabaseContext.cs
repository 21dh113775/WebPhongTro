using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebPhongTro.Models.Domain
{
    public class DatabaseContext:IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<RoomGenre> RoomGenre { get; set; }
        public DbSet<Phong> Phong { get; set; }
    }
}
