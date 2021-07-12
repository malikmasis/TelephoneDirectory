using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TelephoneDirectory.Report.Command;
using TelephoneDirectory.Report.Data;

namespace TelephoneDirectory.Report.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IMediator _mediator;

        public ReportController(IReportDbContext context, ILogger<ReportController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var reports = await _mediator.Send(new GetListReportOutputCommand());
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
                var report = await _mediator.Send(new GetReportOutputCommand
                {
                    Id = id
                });

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
