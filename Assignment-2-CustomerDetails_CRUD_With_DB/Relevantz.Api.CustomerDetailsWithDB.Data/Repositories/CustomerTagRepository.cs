using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Relevantz.Api.CustomerDetailsWithDB.Data.Context;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Repositories
{
    public class CustomerTagRepository : ICustomerTagRepository
    {
        private readonly CustomerDbContext _context;

        public CustomerTagRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetTagsByCustomerIdAsync(int customerId)
        {
            return await _context.CustomerTags
                .Where(ct => ct.CustomerId == customerId)
                .Select(ct => ct.Tag)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> AddTagToCustomerAsync(int customerId, int tagId)
        {
            var exists = await _context.CustomerTags
                .AnyAsync(ct => ct.CustomerId == customerId && ct.TagId == tagId);
            if (exists) return false;

            _context.CustomerTags.Add(new CustomerTag { CustomerId = customerId, TagId = tagId });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveTagFromCustomerAsync(int customerId, int tagId)
        {
            var ct = await _context.CustomerTags
                .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.TagId == tagId);
            if (ct == null) return false;

            _context.CustomerTags.Remove(ct);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
