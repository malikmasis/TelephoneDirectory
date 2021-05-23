using System;

namespace TelephoneDirectory.Contracts
{
    public interface TokenRejected : BaseContract
    {
        string Token { get; }
        string Reason { get; }
    }
}
