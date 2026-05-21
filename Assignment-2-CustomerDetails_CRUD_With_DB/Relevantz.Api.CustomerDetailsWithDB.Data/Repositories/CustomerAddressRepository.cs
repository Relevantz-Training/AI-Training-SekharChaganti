using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Relevantz.Api.CustomerDetailsWithDB.Data.Context;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Repositories
{
    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        private readonly CustomerDbContext _context;

        public CustomerAddressRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerAddress>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.CustomerAddresses
                .Where(a => a.CustomerId == customerId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CustomerAddress?> GetByIdAsync(int addressId)
        {
            return await _context.CustomerAddresses.FindAsync(addressId);
        }

        public async Task<CustomerAddress> CreateAsync(CustomerAddress address)
        {
            _context.CustomerAddresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<CustomerAddress?> UpdateAsync(CustomerAddress address)
        {
            var existing = await _context.CustomerAddresses.FindAsync(address.AddressId);
            if (existing == null) return null;

            existing.AddressType = address.AddressType;
            existing.AddressLine1 = address.AddressLine1;
            existing.AddressLine2 = address.AddressLine2;
            existing.City = address.City;
            existing.StateProvince = address.StateProvince;
            existing.PostalCode = address.PostalCode;
            existing.CountryCode = address.CountryCode;
            existing.IsPrimary = address.IsPrimary;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int addressId)
        {
            var address = await _context.CustomerAddresses.FindAsync(addressId);
            if (address == null) return false;

            _context.CustomerAddresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
