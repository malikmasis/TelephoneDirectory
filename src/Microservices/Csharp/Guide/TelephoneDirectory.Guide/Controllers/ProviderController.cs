using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TelephoneDirectory.Contracts.Dto;

namespace Provider.Controllers;

[Route("api/[controller]")]
public class ProviderController : Controller
{
    private IConfiguration _Configuration { get; }

    public ProviderController(IConfiguration configuration)
    {
        this._Configuration = configuration;
    }

    // GET api/provider?id=1
    [HttpGet]
    public IActionResult Get(int id)
    {
        if (id > 0)
        {
            return new JsonResult(new PersonDto
            {
                Id = 1
            });
        }

        return NoContent();
    }
}
