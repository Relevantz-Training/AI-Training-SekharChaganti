using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Services
{
    public class CustomerTagService : ICustomerTagService
    {
        private readonly ICustomerTagRepository _repository;
        private readonly ILogger<CustomerTagService> _logger;

        public CustomerTagService(ICustomerTagRepository repository, ILogger<CustomerTagService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Tag>> GetTagsByCustomerIdAsync(int customerId)
        {
            return await _repository.GetTagsByCustomerIdAsync(customerId);
        }

        public async Task<bool> AddTagToCustomerAsync(int customerId, int tagId)
        {
            _logger.LogInformation("Adding tag {TagId} to customer {CustomerId}.", tagId, customerId);
            return await _repository.AddTagToCustomerAsync(customerId, tagId);
        }

        public async Task<bool> RemoveTagFromCustomerAsync(int customerId, int tagId)
        {
            return await _repository.RemoveTagFromCustomerAsync(customerId, tagId);
        }
    }
}
