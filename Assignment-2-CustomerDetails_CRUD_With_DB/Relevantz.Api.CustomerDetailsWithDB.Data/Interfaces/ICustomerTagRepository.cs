using System.Collections.Generic;
using System.Threading.Tasks;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces
{
    public interface ICustomerTagRepository
    {
        Task<IEnumerable<Tag>> GetTagsByCustomerIdAsync(int customerId);
        Task<bool> AddTagToCustomerAsync(int customerId, int tagId);
        Task<bool> RemoveTagFromCustomerAsync(int customerId, int tagId);
    }
}
