using MediatR;
using System.Collections.Generic;
using TelephoneDirectory.Report.Entities;

namespace TelephoneDirectory.Report.Command;

public sealed class GetListReportOutputCommand : IRequest<List<ReportOutput>>
{
    public List<ReportOutput> ReportOutputs { get; set; }
}
