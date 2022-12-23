using System;

namespace TelephoneDirectory.Contracts.Abstraction;

public interface IGuideRequestCommand
{
    string ReportId { get; set; }
    DateTime RequestTime { get; set; }
}
