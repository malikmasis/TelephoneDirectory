using System.Collections.Generic;
using MediatR;
using TelephoneDirectory.Report.Entities;

namespace TelephoneDirectory.Report.Command
{
    public class GetListReportOutputCommand : IRequest<List<ReportOutput>>
    {
        public List<ReportOutput> ReportOutputs { get; set; }
    }
}
