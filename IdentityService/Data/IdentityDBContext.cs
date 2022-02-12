using IdentityService.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data
{
    public class IdentityDBContext: DbContext
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> opt) : base(opt) { }

        public DbSet<Userinfo> User { get; set; }
    }
}
