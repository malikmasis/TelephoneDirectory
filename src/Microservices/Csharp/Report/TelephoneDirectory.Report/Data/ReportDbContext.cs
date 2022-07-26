using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TelephoneDirectory.Report.Entities;

namespace TelephoneDirectory.Report.Data
{
    public class ReportDbContext : DbContext, IReportDbContext
    {

        public ReportDbContext(DbContextOptions<ReportDbContext> options)
            : base(options)
        {
        }

        public DbSet<ReportOutput> Reports { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
