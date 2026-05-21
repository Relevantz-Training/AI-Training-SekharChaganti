using System;

namespace Relevantz.API.CustomerDetails.Models
{
    /// <summary>
    /// Represents a customer entity.
    /// </summary>
    public class Customer
    {
        /// <summary>Gets or sets the unique identifier.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>Gets or sets the last name.</summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>Gets or sets the email address.</summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>Gets or sets the phone number.</summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>Gets or sets the address.</summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>Gets or sets the date the record was created.</summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>Gets or sets the date the record was last updated.</summary>
        public DateTime UpdatedDate { get; set; }
    }
}
