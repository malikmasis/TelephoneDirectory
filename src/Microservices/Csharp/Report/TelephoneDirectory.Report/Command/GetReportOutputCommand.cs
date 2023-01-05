using MediatR;
using TelephoneDirectory.Report.Entities;

namespace TelephoneDirectory.Report.Command;

public sealed class GetReportOutputCommand : IRequest<ReportOutput>
{
    public long Id { get; }

    public GetReportOutputCommand(long id)
    {
        Id = id;
    }
}
