using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var reports = await _context.Reports.ToListAsync();
                if (reports == null)
                {
                    return NoContent();
                }
                return Ok(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(long id)
        {

            try
            {
                var report = await _context.Reports.FindAsync(new object[] { id });

                if (report == null)
                {
                    return NoContent();
                }
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

    }
}
