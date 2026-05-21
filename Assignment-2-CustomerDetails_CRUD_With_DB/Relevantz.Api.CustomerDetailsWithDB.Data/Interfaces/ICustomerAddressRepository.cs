using System.Collections.Generic;
using System.Threading.Tasks;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces
{
    public interface ICustomerAddressRepository
    {
        Task<IEnumerable<CustomerAddress>> GetByCustomerIdAsync(int customerId);
        Task<CustomerAddress?> GetByIdAsync(int addressId);
        Task<CustomerAddress> CreateAsync(CustomerAddress address);
        Task<CustomerAddress?> UpdateAsync(CustomerAddress address);
        Task<bool> DeleteAsync(int addressId);
    }
}
