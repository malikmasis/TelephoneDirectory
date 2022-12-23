using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TelephoneDirectory.Contracts.Eto;

namespace TelephoneDirectory.Guide.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly IRequestClient<SubmitToken> _submitTokenRequestClient;
    private readonly ILogger<GuideController> _logger;

    public TokenController(IRequestClient<SubmitToken> submitTokenRequestClient, ILogger<GuideController> logger)
    {
        _submitTokenRequestClient = submitTokenRequestClient;
        _logger = logger;
    }

    [HttpGet("validate")]
    public async Task<IActionResult> Validate([FromQuery] string token)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (accepted, rejected) = await _submitTokenRequestClient.GetResponse<TokenAccepted, TokenRejected>(new
        {
            EventId = NewId.NextGuid(),
            InVar.Timestamp,
            token
        });

        if (accepted.IsCompletedSuccessfully)
        {
            var response = await accepted;
            return Accepted(response);
        }

        if (accepted.IsCompleted)
        {
            await accepted;
            _logger.LogError("Token was not accepted");
            return Problem("Token was not accepted");
        }
        else
        {
            var response = await rejected;
            _logger.LogError(response.Message.Reason);
            return BadRequest(response.Message);
        }
    }
}
