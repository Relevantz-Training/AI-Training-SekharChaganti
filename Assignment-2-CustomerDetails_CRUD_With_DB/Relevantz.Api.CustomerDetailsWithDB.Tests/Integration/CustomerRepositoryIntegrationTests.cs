using Xunit;
using Microsoft.EntityFrameworkCore;
using Relevantz.Api.CustomerDetailsWithDB.Data.Context;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Repositories;

namespace Relevantz.Api.CustomerDetailsWithDB.Tests.Integration
{
    public class CustomerRepositoryIntegrationTests : IDisposable
    {
        private readonly CustomerDbContext _context;
        private readonly CustomerRepository _repository;

        public CustomerRepositoryIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

            _context = new CustomerDbContext(options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new CustomerRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.CloseConnection();
            _context.Dispose();
        }

        [Fact]
        public async Task CreateAndGetById_ReturnsCustomer()
        {
            var customer = new Customer
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane@example.com",
                Status = "Active",
                CustomerType = "B2C"
            };

            var created = await _repository.CreateAsync(customer);
            var fetched = await _repository.GetByIdAsync(created.CustomerId);

            Assert.NotNull(fetched);
            Assert.Equal("Jane", fetched!.FirstName);
            Assert.Equal("jane@example.com", fetched.Email);
        }

        [Fact]
        public async Task GetAll_ReturnsAllCustomers()
        {
            _context.Customers.Add(new Customer { FirstName = "AA", LastName = "BB", Email = "a@b.com", Status = "Active", CustomerType = "B2C" });
            _context.Customers.Add(new Customer { FirstName = "CC", LastName = "DD", Email = "c@d.com", Status = "Inactive", CustomerType = "B2B" });
            await _context.SaveChangesAsync();

            var all = await _repository.GetAllAsync();

            Assert.Equal(2, all.Count());
        }

        [Fact]
        public async Task Update_ModifiesCustomer()
        {
            var customer = new Customer { FirstName = "Old", LastName = "Name", Email = "old@e.com", Status = "Active", CustomerType = "B2C" };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customer.FirstName = "New";
            var updated = await _repository.UpdateAsync(customer);

            Assert.NotNull(updated);
            Assert.Equal("New", updated!.FirstName);
        }

        [Fact]
        public async Task Delete_RemovesCustomer()
        {
            var customer = new Customer { FirstName = "Del", LastName = "Ete", Email = "del@e.com", Status = "Active", CustomerType = "B2C" };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var result = await _repository.DeleteAsync(customer.CustomerId);

            Assert.True(result);
            Assert.Null(await _context.Customers.FindAsync(customer.CustomerId));
        }

        [Fact]
        public async Task Search_FindsByName()
        {
            _context.Customers.Add(new Customer { FirstName = "Alice", LastName = "Wonder", Email = "alice@w.com", Status = "Active", CustomerType = "B2C" });
            await _context.SaveChangesAsync();

            var results = await _repository.SearchAsync("alice");

            Assert.Single(results);
        }

        [Fact]
        public async Task Delete_NonExistent_ReturnsFalse()
        {
            var result = await _repository.DeleteAsync(999);
            Assert.False(result);
        }
    }
}
