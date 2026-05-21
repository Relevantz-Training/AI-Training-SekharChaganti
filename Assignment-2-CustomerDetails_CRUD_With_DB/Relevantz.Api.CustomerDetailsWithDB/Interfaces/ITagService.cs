using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;

namespace Relevantz.Api.CustomerDetailsWithDB.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> CreateAsync(Tag tag);
        Task<bool> DeleteAsync(int tagId);
        List<string> Validate(Tag tag);
    }
}
