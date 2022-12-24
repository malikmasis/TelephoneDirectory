using TelephoneDirectory.Contracts.Base;

namespace TelephoneDirectory.Contracts.Eto;

public sealed record SubmitToken : BaseContract
{
    public string Token { get; }
}
