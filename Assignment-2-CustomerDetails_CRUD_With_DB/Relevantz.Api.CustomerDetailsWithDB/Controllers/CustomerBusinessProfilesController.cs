using Microsoft.AspNetCore.Mvc;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/profile")]
    public class CustomerBusinessProfilesController : ControllerBase
    {
        private readonly ICustomerBusinessProfileService _service;

        public CustomerBusinessProfilesController(ICustomerBusinessProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int customerId)
        {
            var profile = await _service.GetByCustomerIdAsync(customerId);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int customerId, [FromBody] CustomerBusinessProfile profile)
        {
            var errors = _service.Validate(profile);
            if (errors.Count > 0) return BadRequest(new { Errors = errors });

            var created = await _service.CreateAsync(customerId, profile);
            return CreatedAtAction(nameof(Get), new { customerId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int customerId, [FromBody] CustomerBusinessProfile profile)
        {
            var errors = _service.Validate(profile);
            if (errors.Count > 0) return BadRequest(new { Errors = errors });

            var updated = await _service.UpdateAsync(customerId, profile);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int customerId)
        {
            var result = await _service.DeleteAsync(customerId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

