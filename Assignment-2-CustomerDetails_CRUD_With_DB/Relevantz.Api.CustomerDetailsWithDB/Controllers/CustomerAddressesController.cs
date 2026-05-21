using Microsoft.AspNetCore.Mvc;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/addresses")]
    public class CustomerAddressesController : ControllerBase
    {
        private readonly ICustomerAddressService _service;

        public CustomerAddressesController(ICustomerAddressService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            var addresses = await _service.GetByCustomerIdAsync(customerId);
            return Ok(addresses);
        }

        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetById(int customerId, int addressId)
        {
            var address = await _service.GetByIdAsync(customerId, addressId);
            if (address == null) return NotFound();
            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int customerId, [FromBody] CustomerAddress address)
        {
            var errors = _service.Validate(address);
            if (errors.Count > 0) return BadRequest(new { Errors = errors });

            var created = await _service.CreateAsync(customerId, address);
            return CreatedAtAction(nameof(GetById), new { customerId, addressId = created.AddressId }, created);
        }

        [HttpPut("{addressId}")]
        public async Task<IActionResult> Update(int customerId, int addressId, [FromBody] CustomerAddress address)
        {
            var errors = _service.Validate(address);
            if (errors.Count > 0) return BadRequest(new { Errors = errors });

            var updated = await _service.UpdateAsync(customerId, addressId, address);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> Delete(int customerId, int addressId)
        {
            var result = await _service.DeleteAsync(customerId, addressId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

