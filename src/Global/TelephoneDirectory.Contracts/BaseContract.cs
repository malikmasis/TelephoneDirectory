using System;

namespace TelephoneDirectory.Contracts
{
    public record BaseContract
    {
        public Guid EventId { get; }
        public DateTime Timestamp { get; }
    }
}
