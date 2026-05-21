using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;

namespace Relevantz.Api.CustomerDetailsWithDB.Interfaces
{
    public interface ICustomerAddressService
    {
        Task<IEnumerable<CustomerAddress>> GetByCustomerIdAsync(int customerId);
        Task<CustomerAddress?> GetByIdAsync(int customerId, int addressId);
        Task<CustomerAddress> CreateAsync(int customerId, CustomerAddress address);
        Task<CustomerAddress?> UpdateAsync(int customerId, int addressId, CustomerAddress address);
        Task<bool> DeleteAsync(int customerId, int addressId);
        List<string> Validate(CustomerAddress address);
    }
}
