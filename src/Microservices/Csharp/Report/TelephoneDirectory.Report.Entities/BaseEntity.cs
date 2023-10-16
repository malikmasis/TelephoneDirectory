using System;

namespace TelephoneDirectory.Report.Entities;

public abstract class BaseEntity
{
    public long Id { get; private set; }
    public DateTime? CreatedDate { get; private set; } = DateTime.UtcNow;
    public long? CreatedUser { get; private set; }
    public bool IsDeleted { get; private set; } = false;
}
