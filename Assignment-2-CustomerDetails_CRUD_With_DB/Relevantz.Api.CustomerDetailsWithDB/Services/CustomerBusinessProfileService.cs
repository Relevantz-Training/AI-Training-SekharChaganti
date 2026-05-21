using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Services
{
    public class CustomerBusinessProfileService : ICustomerBusinessProfileService
    {
        private readonly ICustomerBusinessProfileRepository _repository;
        private readonly ILogger<CustomerBusinessProfileService> _logger;

        public CustomerBusinessProfileService(ICustomerBusinessProfileRepository repository, ILogger<CustomerBusinessProfileService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CustomerBusinessProfile?> GetByCustomerIdAsync(int customerId)
        {
            return await _repository.GetByCustomerIdAsync(customerId);
        }

        public async Task<CustomerBusinessProfile> CreateAsync(int customerId, CustomerBusinessProfile profile)
        {
            _logger.LogInformation("Creating business profile for customer {CustomerId}.", customerId);
            profile.CustomerId = customerId;
            return await _repository.CreateAsync(profile);
        }

        public async Task<CustomerBusinessProfile?> UpdateAsync(int customerId, CustomerBusinessProfile profile)
        {
            profile.CustomerId = customerId;
            return await _repository.UpdateAsync(profile);
        }

        public async Task<bool> DeleteAsync(int customerId)
        {
            return await _repository.DeleteByCustomerIdAsync(customerId);
        }

        public List<string> Validate(CustomerBusinessProfile profile)
        {
            var errors = new List<string>();
            var validStages = new[] { "Lead", "Opportunity", "Customer", "Churned" };

            if (!validStages.Contains(profile.LifecycleStage))
                errors.Add("LifecycleStage must be Lead, Opportunity, Customer, or Churned.");

            return errors;
        }
    }
}
