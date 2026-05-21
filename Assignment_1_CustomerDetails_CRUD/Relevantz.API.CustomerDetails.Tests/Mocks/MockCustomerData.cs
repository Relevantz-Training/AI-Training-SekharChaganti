using System.Collections.Generic;
using Relevantz.API.CustomerDetails.Models;

namespace Relevantz.API.CustomerDetails.Tests.Mocks
{
    /// <summary>
    /// Shared mock data helpers for unit tests.
    /// </summary>
    public static class MockCustomerData
    {
        /// <summary>Returns a list of sample customers for testing.</summary>
        public static List<Customer> GetSampleCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com", PhoneNumber = "555-0101" },
                new Customer { Id = 2, FirstName = "Bob", LastName = "Smith", Email = "bob@example.com", PhoneNumber = "555-0102" }
            };
        }
    }
}
