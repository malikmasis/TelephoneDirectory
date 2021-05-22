using System.Threading.Tasks;
using TelephoneDirectory.Report.Data;
using TelephoneDirectory.Report.Interfaces;

namespace TelephoneDirectory.Report.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportDbContext _reportDbContext;
        public ReportService(IReportDbContext reportDbContext)
        {
            _reportDbContext = reportDbContext;
        }

        public async Task Save()
        {
            _reportDbContext.Reports.Add(new Entities.ReportOutput() { ReportStatus = Entities.ReportStatus.Completed });
            await _reportDbContext.SaveChangesAsync();
        }
    }
}
