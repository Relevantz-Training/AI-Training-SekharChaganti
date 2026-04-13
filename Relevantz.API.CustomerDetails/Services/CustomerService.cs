using System.Collections.Generic;
using System.Threading.Tasks;
using Relevantz.API.CustomerDetails.Interfaces;
using Relevantz.API.CustomerDetails.Models;

namespace Relevantz.API.CustomerDetails.Services
{
    /// <summary>
    /// Implements business-logic operations for customers by delegating to <see cref="ICustomerRepository"/>.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        /// <summary>
        /// Initializes a new instance of <see cref="CustomerService"/>.
        /// </summary>
        /// <param name="repository">The customer repository.</param>
        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public Task<IEnumerable<Customer>> GetAllAsync() => _repository.GetAllAsync();

        /// <inheritdoc />
        public Task<Customer?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        /// <inheritdoc />
        public Task<IEnumerable<Customer>> SearchAsync(string query) => _repository.SearchAsync(query);

        /// <inheritdoc />
        public Task<Customer> CreateAsync(Customer customer) => _repository.CreateAsync(customer);

        /// <inheritdoc />
        public Task<Customer?> UpdateAsync(Customer customer) => _repository.UpdateAsync(customer);

        /// <inheritdoc />
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
