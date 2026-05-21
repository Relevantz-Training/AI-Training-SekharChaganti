using Microsoft.AspNetCore.Mvc;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _service.GetAllAsync();
            return Ok(tags);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tag tag)
        {
            var errors = _service.Validate(tag);
            if (errors.Count > 0) return BadRequest(new { Errors = errors });

            var created = await _service.CreateAsync(tag);
            return CreatedAtAction(nameof(GetAll), new { id = created.TagId }, created);
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

