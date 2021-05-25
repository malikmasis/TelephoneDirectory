using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.Entities;

namespace TelephoneDirectory.Auth.Data
{
    public interface IAuthDbContext
    {
        DbSet<UserAccount> UserAccounts { get; set; }
        Task<int> SaveChangesAsync();
    }
}
