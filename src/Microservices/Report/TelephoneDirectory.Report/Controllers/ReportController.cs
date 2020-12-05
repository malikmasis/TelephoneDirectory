using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TelephoneDirectory.Report.Data;

namespace TelephoneDirectory.Report.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportDbContext _context;

        public ReportController(IReportDbContext context, ILogger<ReportController> logger)
        {
            _logger = logger;
            _context = context;
        }

    }
}
