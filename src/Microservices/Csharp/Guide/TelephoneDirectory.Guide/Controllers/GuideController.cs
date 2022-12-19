using Dapr.Client;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TelephoneDirectory.Contracts;
using TelephoneDirectory.Guide.Data;
using TelephoneDirectory.Guide.Entities;
using TelephoneDirectory.Guide.Models;

namespace TelephoneDirectory.Guide.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuideController : ControllerBase
    {
        private readonly ILogger<GuideController> _logger;
        private readonly IBus _bus;
        private readonly IGuideDbContext _context;
        private readonly DaprClient _daprClient;

        public GuideController(IGuideDbContext context, IBus bus, DaprClient daprClient, ILogger<GuideController> logger)
        {
            _context = context;
            _bus = bus;
            _daprClient = daprClient;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var persons = await _context.Persons.Include(p => p.Contacts).ToListAsync();
                if (persons == null)
                {
                    return NoContent();
                }
                return Ok(persons);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var person = await _context.Persons.Include(p => p.Contacts).FirstOrDefaultAsync(p => p.Id == id);

                if (person == null)
                {
                    return NoContent();
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var person = await _context.Persons.FindAsync(new object[] { id });

                if (person == null)
                {
                    return NoContent();
                }
                _context.Persons.Remove(person);
                int result = await _context.SaveChangesAsync();
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
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Person personData)
        {
            try
            {
                if (personData == null)
                {
                    await Task.CompletedTask;
                    throw new ArgumentException(nameof(personData));
                }

                var person = await _context.Persons.Include(p => p.Contacts).FirstOrDefaultAsync(p => p.Id == personData.Id);

                if (person == null)
                {
                    return NoContent();
                }
                else
                {
                    person.Name = personData.Name;
                    person.Surname = personData.Surname;
                    person.Company = personData.Company;
                    person.Latitude = personData.Latitude;
                    person.Longitude = personData.Longitude;
                    person.Contacts = personData.Contacts;

                    int result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        return Ok(person.Id);
                    }

                    return BadRequest("Cannot update properly");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Person person)
        {
            try
            {
                if (person == null)
                {
                    await Task.CompletedTask;
                    throw new ArgumentNullException(nameof(person));
                }
                _context.Persons.Add(person);
                int result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    PersonDto personDto = new PersonDto()
                    {
                        Id = person.Id
                    };
                    await _bus.Publish(personDto);
                    return Ok(person.Id);
                }
                return BadRequest("Cannot save properly");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("saga")]
        public async Task<IActionResult> SagaPatternExample(string reportId)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
