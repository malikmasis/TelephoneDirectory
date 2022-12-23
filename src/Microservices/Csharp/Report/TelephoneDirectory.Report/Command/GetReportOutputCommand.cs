using MediatR;
using TelephoneDirectory.Report.Entities;

namespace TelephoneDirectory.Report.Command;

public class GetReportOutputCommand : IRequest<ReportOutput>
{
    public long Id { get; set; }
}
