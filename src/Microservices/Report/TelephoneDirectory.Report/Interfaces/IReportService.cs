using System.Threading;
using System.Threading.Tasks;

namespace TelephoneDirectory.Report.Interfaces
{
    public interface IReportService
    {
        Task Save(CancellationToken cancellationToken);
    }
}
