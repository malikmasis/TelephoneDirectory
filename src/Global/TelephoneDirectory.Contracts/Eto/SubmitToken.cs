using TelephoneDirectory.Contracts.Base;

namespace TelephoneDirectory.Contracts.Eto;

public record SubmitToken : BaseContract
{
    public string Token { get; }
}
