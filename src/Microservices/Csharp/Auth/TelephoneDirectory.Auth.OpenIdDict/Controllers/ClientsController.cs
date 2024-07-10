using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using TelephoneDirectory.Auth.OpenIdDict.Models;

namespace TelephoneDirectory.Auth.OpenIdDict.Controllers;

public class ClientsController : Controller
{
	readonly IOpenIddictApplicationManager _openIddictApplicationManager;

	public ClientsController(IOpenIddictApplicationManager openIddictApplicationManager)
	{
		_openIddictApplicationManager = openIddictApplicationManager;
	}

	[HttpGet]
	public IActionResult CreateClient()
	{
		return Ok();
	}

	[HttpPost]
	public async Task<IActionResult> CreateClient(ClientModel model)
	{
		var client = await _openIddictApplicationManager.FindByClientIdAsync(model.ClientId);

		if (client is null)
		{
			await _openIddictApplicationManager.CreateAsync(new OpenIddictApplicationDescriptor
			{
				ClientId = model.ClientId,
				ClientSecret = model.ClientSecret,
				DisplayName = model.DisplayName,
				RedirectUris = { new("https://localhost:7226/callback/login/local") },
				PostLogoutRedirectUris = { new("https://localhost:7226/callback/logout/local") },
				Permissions =
				{
					OpenIddictConstants.Permissions.Endpoints.Token,
					OpenIddictConstants.Permissions.Endpoints.Authorization,

					OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
					OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,

					OpenIddictConstants.Permissions.Prefixes.Scope + "read",
					OpenIddictConstants.Permissions.Prefixes.Scope + "write",

					OpenIddictConstants.Permissions.ResponseTypes.Code,
				}
			});

			return Ok("Created client");
		}

		return BadRequest("Already create the client");
	}
}