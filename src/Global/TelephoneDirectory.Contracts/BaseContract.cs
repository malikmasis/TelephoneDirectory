using System;

namespace TelephoneDirectory.Contracts
{
    public interface BaseContract
    {
        Guid EventId { get; }
        DateTime Timestamp { get; }
    }
}
