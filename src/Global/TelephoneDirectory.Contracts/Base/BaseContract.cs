using System;

namespace TelephoneDirectory.Contracts.Base
{
    public record BaseContract
    {
        public Guid EventId { get; }
        public DateTime Timestamp { get; }
    }
}
