using Dapr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TelephoneDirectory.Contracts.Dto;
using TelephoneDirectory.Report.Command;

namespace TelephoneDirectory.Report.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
    private readonly IMediator _mediator;

    public ReportController(ILogger<ReportController> logger, IMediator mediator)
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

    [Topic("pubsub", "PersonDeleted")]
    [HttpPost("PersonDeleted")]
    public ActionResult AddProduct(PersonDto personDto)
    {
        Console.WriteLine($"Deleted Person Id: {personDto.Id}");

        return Ok();
    }
}
