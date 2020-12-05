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
                var persons = await _context.Persons.Include(p => p.Contacts).ToListAsync();
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
                var person = await _context.Persons.Include(p => p.Contacts).FirstOrDefaultAsync(p => p.Id == id);

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
                await _context.SaveChangesAsync();

                return Ok(person.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Person personData)
        {
            try
            {
                if (personData == null)
                {
                    throw new ArgumentException(nameof(personData));
                }

                var person = await _context.Persons.Include(p => p.Contacts).FirstOrDefaultAsync(p => p.Id == personData.Id);

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Person person)
        {
            try
            {
                if (person == null)
                {
                    throw new ArgumentException(nameof(person));
                }
                _context.Persons.Add(person);
                int result = await _context.SaveChangesAsync();

                if (result > 0)
                {
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

    }
}
