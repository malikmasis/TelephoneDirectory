using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TelephoneDirectory.Auth.Interfaces;
using TelephoneDirectory.Auth.Models;

namespace TelephoneDirectory.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly ILoginService _loginService;
    private readonly IJwtHandler _jwtHandler;

    public LoginController(ILogger<LoginController> logger, ILoginService loginService, IJwtHandler jwtHandler)
    {
        _logger = logger;
        _loginService = loginService;
        _jwtHandler = jwtHandler;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserModel login)
    {
        IActionResult response = Unauthorized();

        if (_loginService.IsAuth(login))
        {
            var tokenString = _jwtHandler.GenerateJSONWebToken();
            response = Ok(new { token = tokenString });
        }

        _logger.LogError("Couldn't log in");
        return response;
    }
}
