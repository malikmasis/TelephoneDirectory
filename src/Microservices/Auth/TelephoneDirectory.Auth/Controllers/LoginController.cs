using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelephoneDirectory.Auth.Interfaces;
using TelephoneDirectory.Auth.Models;

namespace TelephoneDirectory.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IJwtHandler _jwtHandler;

        public LoginController(ILoginService loginService, IJwtHandler jwtHandler)
        {
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

            return response;
        }
    }
}
