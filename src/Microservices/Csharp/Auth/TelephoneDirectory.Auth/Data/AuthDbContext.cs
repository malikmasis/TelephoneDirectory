using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TelephoneDirectory.Auth.Entities;

namespace TelephoneDirectory.Auth.Data;

public sealed class AuthDbContext : DbContext, IAuthDbContext
{

    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserAccount> UserAccounts { get; set; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>().HasData(new UserAccount[] { new UserAccount { Id = 1, UserName = "admin", Password = "admin" } });
    }
}
