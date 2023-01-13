using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Provider.Controllers;

[Route("api/[controller]")]
public class ProviderController : Controller
{
    private IConfiguration _Configuration { get; }

    public ProviderController(IConfiguration configuration)
    {
        this._Configuration = configuration;
    }

    // GET api/provider?validDateTime=[DateTime String]
    [HttpGet]
    public IActionResult Get(string validDateTime)
    {
        if (String.IsNullOrEmpty(validDateTime))
        {
            return BadRequest(new { message = "validDateTime is required" });
        }

        if (this.DataMissing())
        {
            return NotFound();
        }

        DateTime parsedDateTime;
        try
        {
            parsedDateTime = DateTime.Parse(validDateTime, CultureInfo.GetCultureInfo("en-AU").DateTimeFormat);
        }
        catch (Exception)
        {
            return BadRequest(new { message = "validDateTime is not a date or time" });
        }

        return new JsonResult(new
        {
            test = "NO",
            validDateTime = parsedDateTime.ToString("dd-MM-yyyy HH:mm:ss")
        });
    }

    private bool DataMissing()
    {
        string path = Path.Combine(Path.GetTempPath(), "data");
        string pathWithFile = Path.Combine(path, "somedata.txt");

        return !System.IO.File.Exists(pathWithFile);
    }
}
