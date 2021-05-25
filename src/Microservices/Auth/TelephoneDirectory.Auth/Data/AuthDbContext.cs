using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.Entities;

namespace TelephoneDirectory.Auth.Data
{
    public class AuthDbContext : DbContext, IAuthDbContext
    {

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
