namespace TelephoneDirectory.Auth.Entities;

public sealed class UserAccount : BaseEntity
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
