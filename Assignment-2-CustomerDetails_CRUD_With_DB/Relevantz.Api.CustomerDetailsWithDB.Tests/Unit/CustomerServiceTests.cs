using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;
using Relevantz.Api.CustomerDetailsWithDB.Services;

namespace Relevantz.Api.CustomerDetailsWithDB.Tests.Unit
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepo;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _mockRepo = new Mock<ICustomerRepository>();
            var logger = new Mock<ILogger<CustomerService>>();
            _service = new CustomerService(_mockRepo.Object, logger.Object);
        }

        [Fact]
        public void Validate_ValidCustomer_ReturnsNoErrors()
        {
            var customer = new Customer { FirstName = "John", LastName = "Doe", Email = "john@example.com", Status = "Active", CustomerType = "B2C" };

            var errors = _service.Validate(customer);

            Assert.Empty(errors);
        }

        [Fact]
        public void Validate_ShortFirstName_ReturnsError()
        {
            var customer = new Customer { FirstName = "A", LastName = "Doe", Email = "a@b.com", Status = "Active", CustomerType = "B2C" };

            var errors = _service.Validate(customer);

            Assert.Contains(errors, e => e.Contains("FirstName"));
        }

        [Fact]
        public void Validate_InvalidEmail_ReturnsError()
        {
            var customer = new Customer { FirstName = "John", LastName = "Doe", Email = "invalid", Status = "Active", CustomerType = "B2C" };

            var errors = _service.Validate(customer);

            Assert.Contains(errors, e => e.Contains("email"));
        }

        [Fact]
        public void Validate_InvalidStatus_ReturnsError()
        {
            var customer = new Customer { FirstName = "John", LastName = "Doe", Email = "j@b.com", Status = "Unknown", CustomerType = "B2C" };

            var errors = _service.Validate(customer);

            Assert.Contains(errors, e => e.Contains("Status"));
        }

        [Fact]
        public void Validate_InvalidCustomerType_ReturnsError()
        {
            var customer = new Customer { FirstName = "John", LastName = "Doe", Email = "j@b.com", Status = "Active", CustomerType = "C2C" };

            var errors = _service.Validate(customer);

            Assert.Contains(errors, e => e.Contains("CustomerType"));
        }

        [Fact]
        public async Task GetAllAsync_CallsRepository()
        {
            var customers = new List<Customer> { new Customer { CustomerId = 1, FirstName = "Test", LastName = "User", Email = "t@u.com", Status = "Active", CustomerType = "B2C" } };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(customers);

            var result = await _service.GetAllAsync();

            Assert.Single(result);
            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_CallsRepository()
        {
            var customer = new Customer { FirstName = "New", LastName = "Customer", Email = "n@c.com", Status = "Active", CustomerType = "B2C" };
            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(customer);

            var result = await _service.CreateAsync(customer);

            Assert.Equal("New", result.FirstName);
            _mockRepo.Verify(r => r.CreateAsync(customer), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_SetsIdAndCallsRepository()
        {
            var customer = new Customer { FirstName = "Updated", LastName = "Name", Email = "u@n.com", Status = "Active", CustomerType = "B2B" };
            _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Customer>())).ReturnsAsync(customer);

            var result = await _service.UpdateAsync(5, customer);

            Assert.Equal(5, customer.CustomerId);
            _mockRepo.Verify(r => r.UpdateAsync(customer), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepository()
        {
            _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _service.DeleteAsync(1);

            Assert.True(result);
            _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
        }
    }
}
