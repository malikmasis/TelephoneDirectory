using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using TelephoneDirectory.Report.Controllers;
using Xunit;

namespace TelephoneDirectory.Report.UnitTest.Controllers;

public class ExampleControllerTest
{
	private readonly ILogger<ExampleController> _logger;

	private readonly ExampleController _openTelemetryController;

	public ExampleControllerTest()
	{
		_logger = Mock.Of<ILogger<ExampleController>>();
		_openTelemetryController = new ExampleController(_logger);
	}

	[Fact]
	public void LogTest()
	{
		var exception = Record.ExceptionAsync(() => _openTelemetryController.Log());
		Assert.Null(exception.Result);
	}

	[Fact]
	public async Task GivenInitialRequest_WhenTracingCalled()
	{
		// Arrange
		var activitiesStarted = SetupActivityListener();

		// Act
		await _openTelemetryController.Tracing();

		// Assert
		Assert.Single(activitiesStarted);
		Assert.Contains("Example", activitiesStarted.Select(a => a.DisplayName));
		Assert.NotNull(activitiesStarted.Select(a => a.Tags));
	}

	[Fact]
	public void MetricTest()
	{
		// Act
		var counter = _openTelemetryController.Metric();
		Assert.NotNull(counter);
		Assert.Contains("RequestsOfCounter", counter.Name);

		// Act
		var meter = SetupMeterListener();

		// Assert
		Assert.NotNull(meter);
		Assert.Equal("Metrics.NET", meter.Name);
	}

	private static List<Activity> SetupActivityListener()
	{
		var activitiesStarted = new List<Activity>();
		var activityListener = new ActivityListener
		{
			ShouldListenTo = s => true,
			SampleUsingParentId =
				(ref ActivityCreationOptions<string> activityOptions) => ActivitySamplingResult.AllData,
			Sample = (ref ActivityCreationOptions<ActivityContext> activityOptions)
				=> ActivitySamplingResult.AllData,
			ActivityStarted = activitiesStarted.Add
		};
		ActivitySource.AddActivityListener(activityListener);

		return activitiesStarted;
	}

	private Meter SetupMeterListener()
	{
		Meter meter = null;
		var meterListener = new MeterListener()
		{
			InstrumentPublished = (instrument, listener) =>
			{
				if (instrument.Meter.Name == "Metrics.NET")
				{
					listener.EnableMeasurementEvents(instrument);
					meter = instrument.Meter;
				}
			}
		};
		meterListener.SetMeasurementEventCallback<int>(OnMeasurementRecorded);
		meterListener.Start();

		return meter;
	}

	static void OnMeasurementRecorded<T>(Instrument instrument, T measurement,
	                                     ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
	{
		Console.WriteLine($"{instrument.Name} recorded measurement {measurement}");
	}
}