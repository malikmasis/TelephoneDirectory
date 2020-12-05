using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TelephoneDirectory.Report.Entities;

namespace TelephoneDirectory.Report.Data
{
    public interface IReportDbContext
    {
        DbSet<ReportOutput> Reports { get; set; }
        Task<int> SaveChangesAsync();
    }
}
