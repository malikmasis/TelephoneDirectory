namespace TelephoneDirectory.Report.Entities;

public sealed class ReportOutput : BaseEntity
{
	public ReportStatus ReportStatus { get; private set; }

	public ReportOutput SetReportCompleted()
	{
		ReportStatus = ReportStatus.Completed;

		return this;
	}
}

public enum ReportStatus
{
	None = 0,
	Preparing = 1,
	Completed = 2
}