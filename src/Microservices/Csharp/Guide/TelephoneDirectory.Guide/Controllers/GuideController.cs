using Dapr.Client;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TelephoneDirectory.Contracts.Abstraction;
using TelephoneDirectory.Contracts.Dto;
using TelephoneDirectory.Guide.Data;
using TelephoneDirectory.Guide.Entities;
using TelephoneDirectory.Guide.Models;

namespace TelephoneDirectory.Guide.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class GuideController : ControllerBase
{
	private readonly ILogger<GuideController> _logger;

	private readonly IBus _bus;

	private readonly IGuideDbContext _context;

	private readonly DaprClient _daprClient;

	public GuideController(
		IGuideDbContext context,
		IBus bus,
		DaprClient daprClient,
		ILogger<GuideController> logger)
	{
		_context = context;
		_bus = bus;
		_daprClient = daprClient;
		_logger = logger;
	}

	[Authorize]
	[HttpGet("getall")]
	public async Task<IActionResult> GetAll(CancellationToken cancelToken = default)
	{
		var persons = await _context
		                    .Persons
		                    .Include(p => p.Contacts)
		                    .ToListAsync(cancelToken);

		return Ok(persons);
	}

	[HttpGet("get/{id}")]
	public async Task<IActionResult> GetById(long id, CancellationToken cancelToken = default)
	{
		var person = await _context
		                   .Persons
		                   .Include(p => p.Contacts)
		                   .SingleOrDefaultAsync(p => p.Id == id, cancelToken);

		if (person == null)
		{
			return NoContent();
		}

		return Ok(person);
	}

	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(long id, CancellationToken cancelToken = default)
	{
		var person = await _context.Persons.FindAsync(new long[] { id });

		if (person == null)
		{
			return NoContent();
		}

		_context.Persons.Remove(person);
		int result = await _context.SaveChangesAsync(cancelToken);

		if (result > 0)
		{
			PersonDto personDto = new()
			{
				Id = person.Id
			};
			await _daprClient.PublishEventAsync("pubsub", "PersonDeleted", personDto);

			return Ok(person.Id);
		}

		return BadRequest("Cannot delete properly");
	}

	[HttpPut("update")]
	public async Task<IActionResult> Update(Person personData, CancellationToken cancellationToken = default)
	{
		if (personData == null)
		{
			await Task.CompletedTask;

			throw new ArgumentNullException(nameof(personData));
		}

		var person = await _context
		                   .Persons.Include(p => p.Contacts)
		                   .SingleOrDefaultAsync(p => p.Id == personData.Id, cancellationToken);

		if (person is null)
		{
			return NoContent();
		}
		else
		{
			person
				.SetName(personData.Name)
				.SetSurname(personData.Surname)
				.SetCompany(personData.Company)
				.SetLatitude(personData.Latitude)
				.SetLongitude(personData.Longitude);

			person.Contacts = personData.Contacts;

			int result = await _context.SaveChangesAsync(cancellationToken);

			if (result > 0)
			{
				return Ok(person.Id);
			}

			return BadRequest("Cannot update properly");
		}
	}

	[HttpPost("create")]
	public async Task<IActionResult> Create([FromBody] Person person, CancellationToken cancellationToken = default)
	{
		if (person == null)
		{
			await Task.CompletedTask;

			throw new ArgumentNullException(nameof(person));
		}

		_context.Persons.Add(person);

		int result = await _context.SaveChangesAsync(cancellationToken);

		if (result > 0)
		{
			PersonDto personDto = new()
			{
				Id = person.Id
			};
			await _bus.Publish(personDto, cancellationToken);

			return Ok(person.Id);
		}

		return BadRequest("Cannot save properly");
	}

	[HttpPost("saga")]
	public async Task<IActionResult> SagaPatternExample(string reportId)
	{
		var sendToUri = new Uri($"rabbitmq://localhost/saga.service");
		var endPoint = await _bus.GetSendEndpoint(sendToUri);
		await endPoint.Send<IGuideRequestCommand>(new GuideRequestCommand
		{
			ReportId = reportId,
			RequestTime = DateTime.UtcNow
		});

		return Ok();
	}

	[HttpGet("consumedbygo")]
	public async Task SendEventByDapr()
	{
		
			string PUBSUB_NAME = "pubsub";
			string TOPIC_NAME = "neworder";
			CancellationTokenSource source = new();
			CancellationToken cancellationToken = source.Token;
			await _daprClient.PublishEventAsync(PUBSUB_NAME, TOPIC_NAME, new { Email = "malik.masis@gmail.com" }, cancellationToken);
			_logger.LogInformation("This example event should consume by the golang app");
		
	}

	[HttpGet("getperson/{id}")]
	public IActionResult Get(long id)
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