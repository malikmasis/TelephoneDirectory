using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TelephoneDirectory.Report.Command;
using TelephoneDirectory.Report.Data;
using TelephoneDirectory.Report.Entities;

namespace TelephoneDirectory.Report.Handler
{
    public class GetOrderCommandHandler : IRequestHandler<GetReportOutputCommand, ReportOutput>
    {
        private readonly IReportDbContext _context;

        public GetOrderCommandHandler(IReportDbContext context)
        {
            _context = context;
        }

        public async Task<ReportOutput> Handle(GetReportOutputCommand request, CancellationToken cancellationToken)
        {
            return await _context.Reports.FindAsync(new object[] { request.Id });
        }
    }
}
