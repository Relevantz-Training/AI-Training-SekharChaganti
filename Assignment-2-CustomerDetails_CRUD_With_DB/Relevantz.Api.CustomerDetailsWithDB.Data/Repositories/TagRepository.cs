using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Relevantz.Api.CustomerDetailsWithDB.Data.Context;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly CustomerDbContext _context;

        public TagRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.AsNoTracking().ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int tagId)
        {
            return await _context.Tags.FindAsync(tagId);
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<bool> DeleteAsync(int tagId)
        {
            var tag = await _context.Tags.FindAsync(tagId);
            if (tag == null) return false;

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
