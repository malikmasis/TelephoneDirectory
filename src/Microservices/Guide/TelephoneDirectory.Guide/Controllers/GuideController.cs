using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TelephoneDirectory.Guide.Data;
using TelephoneDirectory.Guide.Entities;

namespace TelephoneDirectory.Guide.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuideController : ControllerBase
    {
        private readonly ILogger<GuideController> _logger;
        private readonly IGuideDbContext _context;

        public GuideController(IGuideDbContext context, ILogger<GuideController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var persons = await _context.Persons.ToListAsync();
                if (persons == null)
                {
                    return NotFound();
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
                var person = await _context.Persons.FindAsync(new object[] { id });

                if (person == null)
                {
                    return NotFound();
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
                    return NotFound();
                }
                _context.Persons.Remove(person);
                await _context.SaveChanges();

                return Ok(person.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, Person personData)
        {
            try
            {
                var person = await _context.Persons.FindAsync(new object[] { id });

                if (person == null)
                {
                    return NotFound();
                }
                else
                {
                    person.Name = personData.Name;
                    person.Surname = personData.Surname;
                    person.Company = personData.Company;
                    person.Contacts = personData.Contacts;
                    await _context.SaveChanges();

                    return Ok(person.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Person person)
        {
            try
            {
                _context.Persons.Add(person);
                await _context.SaveChanges();

                return Ok(person.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
