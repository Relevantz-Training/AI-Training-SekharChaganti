using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Relevantz.API.CustomerDetails.Interfaces;
using Relevantz.API.CustomerDetails.MockData;
using Relevantz.API.CustomerDetails.Models;

namespace Relevantz.API.CustomerDetails.Repositories
{
    /// <summary>
    /// Provides in-memory CRUD operations for <see cref="Customer"/> entities.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers;
        private int _nextId;

        /// <summary>
        /// Initialises a new instance of <see cref="CustomerRepository"/> with seeded mock data.
        /// </summary>
        public CustomerRepository()
        {
            _customers = CustomerMockData.GetSeedData();
            _nextId = _customers.Max(c => c.Id) + 1;
        }

        /// <inheritdoc />
        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Customer>>(_customers.ToList());
        }

        /// <inheritdoc />
        public Task<Customer?> GetByIdAsync(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(customer);
        }

        /// <inheritdoc />
        public Task<IEnumerable<Customer>> SearchAsync(string query)
        {
            var lower = query.ToLowerInvariant();
            var results = _customers.Where(c =>
                c.FirstName.ToLowerInvariant().Contains(lower) ||
                c.LastName.ToLowerInvariant().Contains(lower) ||
                c.Email.ToLowerInvariant().Contains(lower) ||
                c.PhoneNumber.ToLowerInvariant().Contains(lower));

            return Task.FromResult<IEnumerable<Customer>>(results.ToList());
        }

        /// <inheritdoc />
        public Task<Customer> CreateAsync(Customer customer)
        {
            customer.Id = _nextId++;
            customer.CreatedDate = DateTime.UtcNow;
            customer.UpdatedDate = DateTime.UtcNow;
            _customers.Add(customer);
            return Task.FromResult(customer);
        }

        /// <inheritdoc />
        public Task<Customer?> UpdateAsync(Customer customer)
        {
            var existing = _customers.FirstOrDefault(c => c.Id == customer.Id);
            if (existing is null)
                return Task.FromResult<Customer?>(null);

            existing.FirstName = customer.FirstName;
            existing.LastName = customer.LastName;
            existing.Email = customer.Email;
            existing.PhoneNumber = customer.PhoneNumber;
            existing.Address = customer.Address;
            existing.UpdatedDate = DateTime.UtcNow;

            return Task.FromResult<Customer?>(existing);
        }

        /// <inheritdoc />
        public Task<bool> DeleteAsync(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer is null)
                return Task.FromResult(false);

            _customers.Remove(customer);
            return Task.FromResult(true);
        }
    }
}
