using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;

namespace Relevantz.Api.CustomerDetailsWithDB.Interfaces
{
    public interface ICustomerBusinessProfileService
    {
        Task<CustomerBusinessProfile?> GetByCustomerIdAsync(int customerId);
        Task<CustomerBusinessProfile> CreateAsync(int customerId, CustomerBusinessProfile profile);
        Task<CustomerBusinessProfile?> UpdateAsync(int customerId, CustomerBusinessProfile profile);
        Task<bool> DeleteAsync(int customerId);
        List<string> Validate(CustomerBusinessProfile profile);
    }
}
