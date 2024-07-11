using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace TelephoneDirectory.Auth.OpenIdDict.Data;

public class TestData : IHostedService
{
	private readonly IServiceProvider _serviceProvider;

	public TestData(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<OpenIdDictDbContext>();
		await context.Database.EnsureCreatedAsync(cancellationToken);

		var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

		if (await manager.FindByClientIdAsync("postman", cancellationToken) is null)
		{
			await manager.CreateAsync(new OpenIddictApplicationDescriptor
			{
				ClientId = "postman",
				ClientSecret = "postman-secret",
				DisplayName = "Postman",
				Permissions =
				{
					OpenIddictConstants.Permissions.Endpoints.Token,
					OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,

					OpenIddictConstants.Permissions.Prefixes.Scope + "api"
				}
			}, cancellationToken);
		}
	}

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}