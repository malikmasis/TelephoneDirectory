using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TelephoneDirectory.Guide.Entities;

namespace TelephoneDirectory.Guide.Data;

public interface IGuideDbContext
{
    DbSet<Person> Persons { get; set; }
    DbSet<Contact> Contacts { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
