using Microsoft.AspNetCore.Mvc;

namespace TelephoneDirectory.Auth.OpenIdDict.Controllers;

public class HomeController : Controller
{
	public IActionResult Index()
	{
		return Ok();
	}
}