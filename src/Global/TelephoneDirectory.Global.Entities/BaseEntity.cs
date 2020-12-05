using System;

namespace TelephoneDirectory.Global.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public long? CreatedUser { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
