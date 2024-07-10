using Microsoft.EntityFrameworkCore;

namespace TelephoneDirectory.Auth.OpenIdDict.Data;

public interface IOpenIdDictDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
