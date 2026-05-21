using System.Collections.Generic;
using System.Threading.Tasks;
using Relevantz.API.CustomerDetails.Models;

namespace Relevantz.API.CustomerDetails.Interfaces
{
    /// <summary>
    /// Defines the contract for customer business-logic operations.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>Gets all customers.</summary>
        /// <returns>A list of all customers.</returns>
        Task<IEnumerable<Customer>> GetAllAsync();

        /// <summary>Gets a customer by ID.</summary>
        /// <param name="id">The customer ID.</param>
        /// <returns>The customer, or <c>null</c> if not found.</returns>
        Task<Customer?> GetByIdAsync(int id);

        /// <summary>Searches customers by a query string.</summary>
        /// <param name="query">The search term.</param>
        /// <returns>A list of matching customers.</returns>
        Task<IEnumerable<Customer>> SearchAsync(string query);

        /// <summary>Creates a new customer.</summary>
        /// <param name="customer">The customer to create.</param>
        /// <returns>The created customer.</returns>
        Task<Customer> CreateAsync(Customer customer);

        /// <summary>Updates an existing customer.</summary>
        /// <param name="customer">The customer with updated values.</param>
        /// <returns>The updated customer, or <c>null</c> if not found.</returns>
        Task<Customer?> UpdateAsync(Customer customer);

        /// <summary>Deletes a customer by ID.</summary>
        /// <param name="id">The customer ID.</param>
        /// <returns><c>true</c> if deleted; otherwise <c>false</c>.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
