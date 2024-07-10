using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TelephoneDirectory.Auth.OpenIdDict.Controllers;

public sealed class AccountController : Controller
{
	[HttpGet]
	[AllowAnonymous]
	public IActionResult Login(string returnUrl = null)
	{
		return Ok(returnUrl);
	}

	[HttpPost]
	[AllowAnonymous]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login()
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, "admin")
		};

		var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

		await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

		return RedirectToAction();

		return Ok();
	}

	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync();

		return RedirectToAction();
	}

	public async Task<IActionResult> Trying()
	{
		if (User.Identity.IsAuthenticated)
		{
			var authenticationResult = await HttpContext.AuthenticateAsync();

			var issued = authenticationResult.Properties.Items[".issued"];
			var expires = authenticationResult.Properties.Items[".expires"];

			return Ok(issued);
		}

		return Unauthorized("Not signed");
	}
}