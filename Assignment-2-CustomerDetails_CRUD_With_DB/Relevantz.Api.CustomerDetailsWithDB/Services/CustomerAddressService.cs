using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Services
{
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly ICustomerAddressRepository _repository;
        private readonly ILogger<CustomerAddressService> _logger;

        public CustomerAddressService(ICustomerAddressRepository repository, ILogger<CustomerAddressService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<CustomerAddress>> GetByCustomerIdAsync(int customerId)
        {
            return await _repository.GetByCustomerIdAsync(customerId);
        }

        public async Task<CustomerAddress?> GetByIdAsync(int customerId, int addressId)
        {
            var address = await _repository.GetByIdAsync(addressId);
            if (address == null || address.CustomerId != customerId) return null;
            return address;
        }

        public async Task<CustomerAddress> CreateAsync(int customerId, CustomerAddress address)
        {
            _logger.LogInformation("Creating address for customer {CustomerId}.", customerId);
            address.CustomerId = customerId;
            return await _repository.CreateAsync(address);
        }

        public async Task<CustomerAddress?> UpdateAsync(int customerId, int addressId, CustomerAddress address)
        {
            address.AddressId = addressId;
            address.CustomerId = customerId;
            return await _repository.UpdateAsync(address);
        }

        public async Task<bool> DeleteAsync(int customerId, int addressId)
        {
            var address = await _repository.GetByIdAsync(addressId);
            if (address == null || address.CustomerId != customerId) return false;
            return await _repository.DeleteAsync(addressId);
        }

        public List<string> Validate(CustomerAddress address)
        {
            var errors = new List<string>();
            var validTypes = new[] { "Billing", "Shipping", "Office" };

            if (!validTypes.Contains(address.AddressType))
                errors.Add("AddressType must be Billing, Shipping, or Office.");

            if (string.IsNullOrWhiteSpace(address.AddressLine1))
                errors.Add("AddressLine1 is required.");

            if (string.IsNullOrWhiteSpace(address.City))
                errors.Add("City is required.");

            if (string.IsNullOrWhiteSpace(address.PostalCode))
                errors.Add("PostalCode is required.");

            if (string.IsNullOrWhiteSpace(address.CountryCode) || address.CountryCode.Length > 3)
                errors.Add("CountryCode is required and must be at most 3 characters.");

            return errors;
        }
    }
}
