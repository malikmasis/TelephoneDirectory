namespace TelephoneDirectory.Guide.Entities;

public sealed class Contact : BaseEntity
{
    public long PersonId { get; private set; }
    public InfoType InfoType { get; private set; }
    public string Info { get; private set; }
}

public enum InfoType
{
    None = 0,
    Phone = 1,
    Email = 2,
    Location = 3
}