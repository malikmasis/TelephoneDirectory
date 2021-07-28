using System;

namespace TelephoneDirectory.Contracts
{
    public interface IGuideRequestCommand
    {
        string ReportId { get; set; }
        DateTime RequestTime { get; set; }
    }
}
