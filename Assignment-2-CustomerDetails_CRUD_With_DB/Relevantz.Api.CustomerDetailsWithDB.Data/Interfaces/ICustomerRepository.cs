using System.Collections.Generic;
using System.Threading.Tasks;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> SearchAsync(string query);
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer?> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(int id);
    }
}
