using TelephoneDirectory.Contracts.Base;

namespace TelephoneDirectory.Contracts.Eto;

public sealed record TokenRejected : BaseContract
{
    public string Token { get; }
    public string Reason { get; }
}
