using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Relevantz.Api.CustomerDetailsWithDB.Data.Context;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Repositories
{
    public class CustomerBusinessProfileRepository : ICustomerBusinessProfileRepository
    {
        private readonly CustomerDbContext _context;

        public CustomerBusinessProfileRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerBusinessProfile?> GetByCustomerIdAsync(int customerId)
        {
            return await _context.CustomerBusinessProfiles
                .FirstOrDefaultAsync(bp => bp.CustomerId == customerId);
        }

        public async Task<CustomerBusinessProfile> CreateAsync(CustomerBusinessProfile profile)
        {
            _context.CustomerBusinessProfiles.Add(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<CustomerBusinessProfile?> UpdateAsync(CustomerBusinessProfile profile)
        {
            var existing = await _context.CustomerBusinessProfiles
                .FirstOrDefaultAsync(bp => bp.CustomerId == profile.CustomerId);
            if (existing == null) return null;

            existing.CompanyName = profile.CompanyName;
            existing.JobTitle = profile.JobTitle;
            existing.LeadSource = profile.LeadSource;
            existing.LifecycleStage = profile.LifecycleStage;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteByCustomerIdAsync(int customerId)
        {
            var profile = await _context.CustomerBusinessProfiles
                .FirstOrDefaultAsync(bp => bp.CustomerId == customerId);
            if (profile == null) return false;

            _context.CustomerBusinessProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
