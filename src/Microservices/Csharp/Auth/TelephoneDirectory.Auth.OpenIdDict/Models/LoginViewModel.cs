using System.ComponentModel.DataAnnotations;

namespace TelephoneDirectory.Auth.OpenIdDict.Models;

public sealed record LoginViewModel
{
	[Required] public string Username { get; set; }

	[Required] public string Password { get; set; }

	public string ReturnUrl { get; set; }
}