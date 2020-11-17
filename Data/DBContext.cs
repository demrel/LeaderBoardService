using LeaderBoardService.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaderBoardService.Data
{
    public class DBContext: IdentityDbContext<AppUser>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<LeaderBoard> LeaderBoard { get; set; }
    
        public DbSet<User> User { get; set; }

    }
}
