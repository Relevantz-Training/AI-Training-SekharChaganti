using System.Threading.Tasks;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces
{
    public interface ICustomerBusinessProfileRepository
    {
        Task<CustomerBusinessProfile?> GetByCustomerIdAsync(int customerId);
        Task<CustomerBusinessProfile> CreateAsync(CustomerBusinessProfile profile);
        Task<CustomerBusinessProfile?> UpdateAsync(CustomerBusinessProfile profile);
        Task<bool> DeleteByCustomerIdAsync(int customerId);
    }
}
