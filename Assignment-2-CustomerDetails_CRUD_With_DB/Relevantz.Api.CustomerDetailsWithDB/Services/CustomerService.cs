using System.ComponentModel.DataAnnotations;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all customers.");
            return await _repository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Customer>> SearchAsync(string query)
        {
            return await _repository.SearchAsync(query);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            _logger.LogInformation("Creating customer with email {Email}.", customer.Email);
            return await _repository.CreateAsync(customer);
        }

        public async Task<Customer?> UpdateAsync(int id, Customer customer)
        {
            _logger.LogInformation("Updating customer {CustomerId}.", id);
            customer.CustomerId = id;
            return await _repository.UpdateAsync(customer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting customer {CustomerId}.", id);
            return await _repository.DeleteAsync(id);
        }

        public List<string> Validate(Customer customer)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(customer.FirstName) || customer.FirstName.Length < 2 || customer.FirstName.Length > 100)
                errors.Add("FirstName must be between 2 and 100 characters.");

            if (string.IsNullOrWhiteSpace(customer.LastName) || customer.LastName.Length < 2 || customer.LastName.Length > 100)
                errors.Add("LastName must be between 2 and 100 characters.");

            if (string.IsNullOrWhiteSpace(customer.Email) || !new EmailAddressAttribute().IsValid(customer.Email))
                errors.Add("A valid email address is required.");

            var validStatuses = new[] { "Active", "Inactive", "Archived", "Pending" };
            if (!validStatuses.Contains(customer.Status))
                errors.Add("Status must be one of: Active, Inactive, Archived, Pending.");

            var validTypes = new[] { "B2C", "B2B" };
            if (!validTypes.Contains(customer.CustomerType))
                errors.Add("CustomerType must be B2C or B2B.");

            return errors;
        }
    }
}
