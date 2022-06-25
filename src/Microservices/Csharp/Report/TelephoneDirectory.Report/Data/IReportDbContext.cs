using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.Report.Entities;

namespace TelephoneDirectory.Report.Data
{
    public interface IReportDbContext
    {
        DbSet<ReportOutput> Reports { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
