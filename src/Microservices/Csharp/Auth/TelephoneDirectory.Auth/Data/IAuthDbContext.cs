using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TelephoneDirectory.Auth.Entities;

namespace TelephoneDirectory.Auth.Data;

public interface IAuthDbContext
{
    DbSet<UserAccount> UserAccounts { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
