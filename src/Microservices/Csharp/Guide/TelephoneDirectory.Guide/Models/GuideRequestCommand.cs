using System;
using TelephoneDirectory.Contracts.Abstraction;

namespace TelephoneDirectory.Guide.Models
{
    public class GuideRequestCommand : IGuideRequestCommand
    {
        public string ReportId { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
