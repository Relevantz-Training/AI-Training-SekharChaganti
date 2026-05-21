using Microsoft.AspNetCore.Mvc;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/tags")]
    public class CustomerTagsController : ControllerBase
    {
        private readonly ICustomerTagService _service;

        public CustomerTagsController(ICustomerTagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTags(int customerId)
        {
            var tags = await _service.GetTagsByCustomerIdAsync(customerId);
            return Ok(tags);
        }

        [HttpPost("{tagId}")]
        public async Task<IActionResult> AddTag(int customerId, int tagId)
        {
            var result = await _service.AddTagToCustomerAsync(customerId, tagId);
            if (!result) return Conflict("Tag already assigned to customer.");
            return Ok();
        }

        [HttpDelete("{tagId}")]
        public async Task<IActionResult> RemoveTag(int customerId, int tagId)
        {
            var result = await _service.RemoveTagFromCustomerAsync(customerId, tagId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

