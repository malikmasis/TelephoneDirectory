﻿using System.Threading;
using System.Threading.Tasks;
using TelephoneDirectory.Report.Data;
using TelephoneDirectory.Report.Entities;
using TelephoneDirectory.Report.Interfaces;

namespace TelephoneDirectory.Report.Services;

public sealed class ReportService : IReportService
{
    private readonly IReportDbContext _reportDbContext;
    public ReportService(IReportDbContext reportDbContext)
    {
        _reportDbContext = reportDbContext;
    }

    public async Task Save(CancellationToken cancellationToken = default)
    {
        _reportDbContext.Reports.Add(new ReportOutput().SetReportCompleted());
        await _reportDbContext.SaveChangesAsync(cancellationToken);
    }
}
