using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly ILogger<TagService> _logger;

        public TagService(ITagRepository repository, ILogger<TagService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            _logger.LogInformation("Creating tag {TagName}.", tag.TagName);
            return await _repository.CreateAsync(tag);
        }

        public async Task<bool> DeleteAsync(int tagId)
        {
            return await _repository.DeleteAsync(tagId);
        }

        public List<string> Validate(Tag tag)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(tag.TagName) || tag.TagName.Length > 50)
                errors.Add("TagName is required and must be at most 50 characters.");

            return errors;
        }
    }
}
