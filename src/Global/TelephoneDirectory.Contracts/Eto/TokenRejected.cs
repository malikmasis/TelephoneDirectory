using TelephoneDirectory.Contracts.Base;

namespace TelephoneDirectory.Contracts.Eto;

public record TokenRejected : BaseContract
{
    public string Token { get; }
    public string Reason { get; }
}
