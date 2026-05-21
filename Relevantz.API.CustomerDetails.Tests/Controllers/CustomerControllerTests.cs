using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Relevantz.API.CustomerDetails.Constants;
using Relevantz.API.CustomerDetails.Controllers;
using Relevantz.API.CustomerDetails.Interfaces;
using Relevantz.API.CustomerDetails.Models;
using Xunit;

namespace Relevantz.API.CustomerDetails.Tests.Controllers
{
    /// <summary>
    /// Unit tests for <see cref="CustomerController"/>.
    /// </summary>
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockService;
        private readonly CustomerController _controller;

        /// <summary>Initializes test dependencies.</summary>
        public CustomerControllerTests()
        {
            _mockService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockService.Object);
        }

        // ── GetAll ───────────────────────────────────────────────────────────────

        /// <summary>GetAll returns 200 with a list of customers.</summary>
        [Fact]
        public async Task GetAll_ReturnsOkWithCustomers()
        {
            var customers = new List<Customer> { new Customer { Id = 1, FirstName = "Alice" } };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(customers);

            var result = await _controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(customers, ok.Value);
        }

        // ── GetById ──────────────────────────────────────────────────────────────

        /// <summary>GetById returns 200 when the customer exists.</summary>
        [Fact]
        public async Task GetById_ExistingId_ReturnsOk()
        {
            var customer = new Customer { Id = 1, FirstName = "Alice" };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(customer);

            var result = await _controller.GetById(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(customer, ok.Value);
        }

        /// <summary>GetById returns 404 when the customer does not exist.</summary>
        [Fact]
        public async Task GetById_NonExistingId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((Customer?)null);

            var result = await _controller.GetById(99);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(CustomerConstants.CustomerNotFound, notFound.Value);
        }

        // ── Search ───────────────────────────────────────────────────────────────

        /// <summary>Search returns 200 with matching customers.</summary>
        [Fact]
        public async Task Search_ValidQuery_ReturnsOkWithResults()
        {
            var customers = new List<Customer> { new Customer { Id = 1, FirstName = "Alice" } };
            _mockService.Setup(s => s.SearchAsync("Alice")).ReturnsAsync(customers);

            var result = await _controller.Search("Alice");

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(customers, ok.Value);
        }

        /// <summary>Search returns 200 with empty list when no matches found.</summary>
        [Fact]
        public async Task Search_NoMatches_ReturnsOkWithEmptyList()
        {
            _mockService.Setup(s => s.SearchAsync("xyz")).ReturnsAsync(new List<Customer>());

            var result = await _controller.Search("xyz");

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<Customer>>(ok.Value);
            Assert.Empty(list);
        }

        /// <summary>Search returns 400 when the query parameter is null or empty.</summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task Search_MissingOrEmptyQuery_ReturnsBadRequest(string? query)
        {
            var result = await _controller.Search(query);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(CustomerConstants.SearchQueryRequired, bad.Value);
        }

        // ── Create ───────────────────────────────────────────────────────────────

        /// <summary>Create returns 201 with the newly created customer.</summary>
        [Fact]
        public async Task Create_ValidCustomer_ReturnsCreated()
        {
            var input = new Customer { FirstName = "Alice", LastName = "Johnson", Email = "a@example.com" };
            var created = new Customer { Id = 10, FirstName = "Alice", LastName = "Johnson", Email = "a@example.com" };
            _mockService.Setup(s => s.CreateAsync(input)).ReturnsAsync(created);

            var result = await _controller.Create(input);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(created, createdResult.Value);
        }

        // ── Update ───────────────────────────────────────────────────────────────

        /// <summary>Update returns 200 with the updated customer.</summary>
        [Fact]
        public async Task Update_ExistingCustomer_ReturnsOk()
        {
            var customer = new Customer { Id = 1, FirstName = "Updated" };
            _mockService.Setup(s => s.UpdateAsync(customer)).ReturnsAsync(customer);

            var result = await _controller.Update(1, customer);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(customer, ok.Value);
        }

        /// <summary>Update returns 404 when the customer does not exist.</summary>
        [Fact]
        public async Task Update_NonExistingCustomer_ReturnsNotFound()
        {
            var customer = new Customer { Id = 99 };
            _mockService.Setup(s => s.UpdateAsync(customer)).ReturnsAsync((Customer?)null);

            var result = await _controller.Update(99, customer);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>Update returns 400 when the URL ID does not match the body ID.</summary>
        [Fact]
        public async Task Update_IdMismatch_ReturnsBadRequest()
        {
            var customer = new Customer { Id = 5 };

            var result = await _controller.Update(1, customer);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(CustomerConstants.IdMismatch, bad.Value);
        }

        // ── Delete ───────────────────────────────────────────────────────────────

        /// <summary>Delete returns 204 when the customer is deleted successfully.</summary>
        [Fact]
        public async Task Delete_ExistingCustomer_ReturnsNoContent()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>Delete returns 404 when the customer does not exist.</summary>
        [Fact]
        public async Task Delete_NonExistingCustomer_ReturnsNotFound()
        {
            _mockService.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

            var result = await _controller.Delete(99);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
