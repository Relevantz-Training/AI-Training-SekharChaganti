using System.Collections.Generic;
using System.Threading.Tasks;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(int tagId);
        Task<Tag> CreateAsync(Tag tag);
        Task<bool> DeleteAsync(int tagId);
    }
}
