using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Relevantz.Api.CustomerDetailsWithDB.Controllers;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Tests.Unit
{
    public class CustomersControllerTests
    {
        private readonly Mock<ICustomerService> _mockService;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mockService = new Mock<ICustomerService>();
            _controller = new CustomersController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", Status = "Active", CustomerType = "B2C" }
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(customers);

            var result = await _controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsAssignableFrom<IEnumerable<Customer>>(ok.Value);
            Assert.Single(returned);
        }

        [Fact]
        public async Task GetById_NotFound_Returns404()
        {
            _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((Customer?)null);

            var result = await _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_Found_ReturnsOk()
        {
            var customer = new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", Status = "Active", CustomerType = "B2C" };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(customer);

            var result = await _controller.GetById(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, ((Customer)ok.Value!).CustomerId);
        }

        [Fact]
        public async Task Create_InvalidFirstName_ReturnsBadRequest()
        {
            var customer = new Customer { FirstName = "A", LastName = "Doe", Email = "a@b.com", Status = "Active", CustomerType = "B2C" };
            _mockService.Setup(s => s.Validate(It.IsAny<Customer>())).Returns(new List<string> { "FirstName must be between 2 and 100 characters." });

            var result = await _controller.Create(customer);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_InvalidEmail_ReturnsBadRequest()
        {
            var customer = new Customer { FirstName = "John", LastName = "Doe", Email = "invalid", Status = "Active", CustomerType = "B2C" };
            _mockService.Setup(s => s.Validate(It.IsAny<Customer>())).Returns(new List<string> { "A valid email address is required." });

            var result = await _controller.Create(customer);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_InvalidStatus_ReturnsBadRequest()
        {
            var customer = new Customer { FirstName = "John", LastName = "Doe", Email = "j@b.com", Status = "Unknown", CustomerType = "B2C" };
            _mockService.Setup(s => s.Validate(It.IsAny<Customer>())).Returns(new List<string> { "Status must be one of: Active, Inactive, Archived, Pending." });

            var result = await _controller.Create(customer);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_Valid_ReturnsCreated()
        {
            var customer = new Customer { FirstName = "John", LastName = "Doe", Email = "john@example.com", Status = "Active", CustomerType = "B2C" };
            _mockService.Setup(s => s.Validate(It.IsAny<Customer>())).Returns(new List<string>());
            _mockService.Setup(s => s.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", Status = "Active", CustomerType = "B2C" });

            var result = await _controller.Create(customer);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Delete_NotFound_Returns404()
        {
            _mockService.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

            var result = await _controller.Delete(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Found_ReturnsNoContent()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Search_EmptyQuery_ReturnsBadRequest()
        {
            var result = await _controller.Search("");

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
