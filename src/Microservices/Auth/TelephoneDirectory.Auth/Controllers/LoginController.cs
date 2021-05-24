using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TelephoneDirectory.Auth.Handler;
using TelephoneDirectory.Auth.Models;

namespace TelephoneDirectory.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            var jWTHandler = new JwtHandler(_config);

            var user = jWTHandler.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = jWTHandler.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
    }
}
