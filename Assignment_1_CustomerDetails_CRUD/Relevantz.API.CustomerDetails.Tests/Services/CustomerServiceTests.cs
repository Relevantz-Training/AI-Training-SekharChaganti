using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Relevantz.API.CustomerDetails.Interfaces;
using Relevantz.API.CustomerDetails.Models;
using Relevantz.API.CustomerDetails.Services;
using Xunit;

namespace Relevantz.API.CustomerDetails.Tests.Services
{
    /// <summary>
    /// Unit tests for <see cref="CustomerService"/>.
    /// </summary>
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepo;
        private readonly CustomerService _service;

        /// <summary>Initializes test dependencies.</summary>
        public CustomerServiceTests()
        {
            _mockRepo = new Mock<ICustomerRepository>();
            _service = new CustomerService(_mockRepo.Object);
        }

        /// <summary>GetAllAsync delegates to the repository and returns all customers.</summary>
        [Fact]
        public async Task GetAllAsync_DelegatesToRepository()
        {
            var expected = new List<Customer> { new Customer { Id = 1 } };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(expected);

            var result = await _service.GetAllAsync();

            Assert.Equal(expected, result);
            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        /// <summary>GetByIdAsync returns the customer when found.</summary>
        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsCustomer()
        {
            var customer = new Customer { Id = 1 };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(customer);

            var result = await _service.GetByIdAsync(1);

            Assert.Equal(customer, result);
        }

        /// <summary>GetByIdAsync returns null when customer not found.</summary>
        [Fact]
        public async Task GetByIdAsync_MissingId_ReturnsNull()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Customer?)null);

            var result = await _service.GetByIdAsync(99);

            Assert.Null(result);
        }

        /// <summary>SearchAsync delegates to the repository with the query term.</summary>
        [Fact]
        public async Task SearchAsync_DelegatesToRepository()
        {
            var expected = new List<Customer> { new Customer { Id = 1, FirstName = "Alice" } };
            _mockRepo.Setup(r => r.SearchAsync("Alice")).ReturnsAsync(expected);

            var result = await _service.SearchAsync("Alice");

            Assert.Equal(expected, result);
            _mockRepo.Verify(r => r.SearchAsync("Alice"), Times.Once);
        }

        /// <summary>CreateAsync delegates to the repository and returns the created customer.</summary>
        [Fact]
        public async Task CreateAsync_DelegatesToRepository()
        {
            var input = new Customer { FirstName = "Alice" };
            var created = new Customer { Id = 1, FirstName = "Alice" };
            _mockRepo.Setup(r => r.CreateAsync(input)).ReturnsAsync(created);

            var result = await _service.CreateAsync(input);

            Assert.Equal(created, result);
        }

        /// <summary>UpdateAsync delegates to the repository and returns the updated customer.</summary>
        [Fact]
        public async Task UpdateAsync_ExistingCustomer_ReturnsUpdated()
        {
            var customer = new Customer { Id = 1, FirstName = "Updated" };
            _mockRepo.Setup(r => r.UpdateAsync(customer)).ReturnsAsync(customer);

            var result = await _service.UpdateAsync(customer);

            Assert.Equal(customer, result);
        }

        /// <summary>DeleteAsync returns true when the customer is deleted.</summary>
        [Fact]
        public async Task DeleteAsync_ExistingId_ReturnsTrue()
        {
            _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _service.DeleteAsync(1);

            Assert.True(result);
        }

        /// <summary>DeleteAsync returns false when the customer is not found.</summary>
        [Fact]
        public async Task DeleteAsync_MissingId_ReturnsFalse()
        {
            _mockRepo.Setup(r => r.DeleteAsync(99)).ReturnsAsync(false);

            var result = await _service.DeleteAsync(99);

            Assert.False(result);
        }
    }
}
