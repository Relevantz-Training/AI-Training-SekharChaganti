using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;

namespace Relevantz.Api.CustomerDetailsWithDB.Interfaces
{
    public interface ICustomerTagService
    {
        Task<IEnumerable<Tag>> GetTagsByCustomerIdAsync(int customerId);
        Task<bool> AddTagToCustomerAsync(int customerId, int tagId);
        Task<bool> RemoveTagFromCustomerAsync(int customerId, int tagId);
    }
}
