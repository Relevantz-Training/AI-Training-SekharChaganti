using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Relevantz.Api.CustomerDetailsWithDB.Data.Context;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.Addresses)
                .Include(c => c.BusinessProfile)
                .Include(c => c.CustomerTags).ThenInclude(ct => ct.Tag)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Addresses)
                .Include(c => c.BusinessProfile)
                .Include(c => c.CustomerTags).ThenInclude(ct => ct.Tag)
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<IEnumerable<Customer>> SearchAsync(string query)
        {
            var lower = query.ToLowerInvariant();
            return await _context.Customers
                .Where(c =>
                    c.FirstName.ToLower().Contains(lower) ||
                    c.LastName.ToLower().Contains(lower) ||
                    c.Email.ToLower().Contains(lower) ||
                    (c.Phone != null && c.Phone.Contains(query)))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            customer.CreatedAt = DateTime.UtcNow;
            customer.UpdatedAt = DateTime.UtcNow;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> UpdateAsync(Customer customer)
        {
            var existing = await _context.Customers.FindAsync(customer.CustomerId);
            if (existing == null) return null;

            existing.FirstName = customer.FirstName;
            existing.LastName = customer.LastName;
            existing.Email = customer.Email;
            existing.Phone = customer.Phone;
            existing.Status = customer.Status;
            existing.CustomerType = customer.CustomerType;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
