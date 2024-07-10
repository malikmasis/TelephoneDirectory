using Microsoft.EntityFrameworkCore;

namespace TelephoneDirectory.Auth.OpenIdDict.Data;

public sealed class OpenIdDictDbContext : DbContext, IOpenIdDictDbContext
{
	public OpenIdDictDbContext(DbContextOptions<OpenIdDictDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	}
}