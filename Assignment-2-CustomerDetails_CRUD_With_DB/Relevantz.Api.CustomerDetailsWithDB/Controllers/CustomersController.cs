using Microsoft.AspNetCore.Mvc;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _service.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _service.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Search query is required.");

            var results = await _service.SearchAsync(query);
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            var errors = _service.Validate(customer);
            if (errors.Count > 0)
                return BadRequest(new { Errors = errors });

            var created = await _service.CreateAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = created.CustomerId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
        {
            var errors = _service.Validate(customer);
            if (errors.Count > 0)
                return BadRequest(new { Errors = errors });

            var updated = await _service.UpdateAsync(id, customer);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

