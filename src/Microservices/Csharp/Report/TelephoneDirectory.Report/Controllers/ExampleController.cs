using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TelephoneDirectory.Report.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ExampleController : ControllerBase
{
	private static readonly ActivitySource ActivitySource = new("Tracing.NET");

	private readonly ILogger<ExampleController> _logger;

	public ExampleController(ILogger<ExampleController> logger)
	{
		_logger = logger;
	}

	[HttpGet("log")]
	public void Log()
	{
		_logger.LogInformation("Hello! It is {Time}", DateTime.UtcNow);
		_logger.LogError("Test Error");
	}

	[HttpGet("tracing")]
	public async Task Tracing()
	{
		using var activity = ActivitySource.StartActivity("Example");

		// note that "sampleActivity" can be null here if nobody listen events generated
		// by the "SampleActivitySource" activity source.
		activity?.AddTag("UserId", "AnyUser");

		// Simulate a long running operation
		await Task.Delay(500);
	}

	[HttpGet("metric")]
	public Counter<int> Metric()
	{
		//Metric
		var meter = new Meter("Metrics.NET");

		var counter = meter.CreateCounter<int>("RequestsOfCounter", "ms", "Example request");
		meter.CreateObservableGauge("ThreadCount",
		                            () => new[] { new Measurement<int>(ThreadPool.ThreadCount) });

		// Measure the number of requests
		counter.Add(1, KeyValuePair.Create<string, object>("name", "Türkiye"));

		return counter;
	}
}