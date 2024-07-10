namespace TelephoneDirectory.Auth.OpenIdDict.Models;

public sealed record ClientModel
{
	public string ClientId { get; init; }

	public string ClientSecret { get; init; }

	public string DisplayName { get; init; }
}