using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Relevantz.API.CustomerDetails.Constants;
using Relevantz.API.CustomerDetails.Interfaces;
using Relevantz.API.CustomerDetails.Models;

namespace Relevantz.API.CustomerDetails.Controllers
{
    /// <summary>
    /// Provides RESTful API endpoints for managing customer details.
    /// </summary>
    [ApiController]
    [Route(CustomerConstants.CustomersRoute)]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        /// <summary>
        /// Initializes a new instance of <see cref="CustomerController"/>.
        /// </summary>
        /// <param name="service">The customer service.</param>
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        /// <response code="200">Returns the list of customers.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _service.GetAllAsync();
            return Ok(customers);
        }

        /// <summary>
        /// Retrieves a single customer by ID.
        /// </summary>
        /// <param name="id">The customer ID.</param>
        /// <returns>The matching customer.</returns>
        /// <response code="200">Returns the customer.</response>
        /// <response code="404">If the customer is not found.</response>
        [HttpGet(CustomerConstants.IdRoute)]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _service.GetByIdAsync(id);
            if (customer is null)
                return NotFound(CustomerConstants.CustomerNotFound);

            return Ok(customer);
        }

        /// <summary>
        /// Searches customers by name, email, or phone number (partial, case-insensitive).
        /// </summary>
        /// <param name="query">The search term.</param>
        /// <returns>A list of matching customers.</returns>
        /// <response code="200">Returns the list of matching customers (may be empty).</response>
        /// <response code="400">If the query parameter is missing or empty.</response>
        [HttpGet(CustomerConstants.SearchRoute)]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Search([FromQuery] string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(CustomerConstants.SearchQueryRequired);

            var results = await _service.SearchAsync(query);
            return Ok(results);
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">The customer data.</param>
        /// <returns>The newly created customer.</returns>
        /// <response code="201">Returns the created customer.</response>
        /// <response code="400">If the request body is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Customer), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(CustomerConstants.InvalidRequest);

            var created = await _service.CreateAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The customer ID.</param>
        /// <param name="customer">The updated customer data.</param>
        /// <returns>The updated customer.</returns>
        /// <response code="200">Returns the updated customer.</response>
        /// <response code="400">If the ID in the URL does not match the body.</response>
        /// <response code="404">If the customer is not found.</response>
        [HttpPut(CustomerConstants.IdRoute)]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest(CustomerConstants.IdMismatch);

            var updated = await _service.UpdateAsync(customer);
            if (updated is null)
                return NotFound(CustomerConstants.CustomerNotFound);

            return Ok(updated);
        }

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="id">The customer ID.</param>
        /// <returns>No content on success.</returns>
        /// <response code="204">Customer deleted successfully.</response>
        /// <response code="404">If the customer is not found.</response>
        [HttpDelete(CustomerConstants.IdRoute)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(CustomerConstants.CustomerNotFound);

            return NoContent();
        }
    }
}
