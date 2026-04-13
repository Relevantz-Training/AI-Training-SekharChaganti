using System;
using System.Collections.Generic;
using Relevantz.API.CustomerDetails.Models;

namespace Relevantz.API.CustomerDetails.MockData
{
    /// <summary>
    /// Provides seeded in-memory mock data for the Customer repository.
    /// </summary>
    public static class CustomerMockData
    {
        /// <summary>
        /// Returns a seeded list of sample <see cref="Customer"/> objects.
        /// </summary>
        /// <returns>A list of sample customers.</returns>
        public static List<Customer> GetSeedData()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Email = "alice.johnson@example.com",
                    PhoneNumber = "555-0101",
                    Address = "123 Maple Street, Springfield, IL 62701",
                    CreatedDate = new DateTime(2024, 1, 10, 9, 0, 0, DateTimeKind.Utc),
                    UpdatedDate = new DateTime(2024, 1, 10, 9, 0, 0, DateTimeKind.Utc)
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Bob",
                    LastName = "Smith",
                    Email = "bob.smith@example.com",
                    PhoneNumber = "555-0102",
                    Address = "456 Oak Avenue, Columbus, OH 43004",
                    CreatedDate = new DateTime(2024, 2, 15, 11, 30, 0, DateTimeKind.Utc),
                    UpdatedDate = new DateTime(2024, 2, 15, 11, 30, 0, DateTimeKind.Utc)
                },
                new Customer
                {
                    Id = 3,
                    FirstName = "Carol",
                    LastName = "Williams",
                    Email = "carol.williams@example.com",
                    PhoneNumber = "555-0103",
                    Address = "789 Pine Road, Austin, TX 73301",
                    CreatedDate = new DateTime(2024, 3, 20, 14, 0, 0, DateTimeKind.Utc),
                    UpdatedDate = new DateTime(2024, 3, 20, 14, 0, 0, DateTimeKind.Utc)
                },
                new Customer
                {
                    Id = 4,
                    FirstName = "David",
                    LastName = "Brown",
                    Email = "david.brown@example.com",
                    PhoneNumber = "555-0104",
                    Address = "321 Elm Street, Denver, CO 80201",
                    CreatedDate = new DateTime(2024, 4, 5, 8, 0, 0, DateTimeKind.Utc),
                    UpdatedDate = new DateTime(2024, 4, 5, 8, 0, 0, DateTimeKind.Utc)
                },
                new Customer
                {
                    Id = 5,
                    FirstName = "Eva",
                    LastName = "Davis",
                    Email = "eva.davis@example.com",
                    PhoneNumber = "555-0105",
                    Address = "654 Cedar Lane, Seattle, WA 98101",
                    CreatedDate = new DateTime(2024, 5, 12, 10, 15, 0, DateTimeKind.Utc),
                    UpdatedDate = new DateTime(2024, 5, 12, 10, 15, 0, DateTimeKind.Utc)
                }
            };
        }
    }
}
