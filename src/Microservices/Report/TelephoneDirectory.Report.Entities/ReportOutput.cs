﻿using TelephoneDirectory.Global.Entities;

namespace TelephoneDirectory.Report.Entities
{
    public class ReportOutput : BaseEntity
    {
        public ReportStatus ReportStatus { get; set; }
    }

    public enum ReportStatus
    {
        Preparing = 1,
        Completed
    }
}
