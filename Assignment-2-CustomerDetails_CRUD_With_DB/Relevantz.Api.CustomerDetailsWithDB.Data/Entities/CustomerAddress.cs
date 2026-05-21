using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Entities
{
    [Table("customer_addresses")]
    public class CustomerAddress
    {
        [Key]
        [Column("address_id")]
        public int AddressId { get; set; }

        [Required]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Required]
        [Column("address_type")]
        [StringLength(20)]
        public string AddressType { get; set; } = "Billing";

        [Required]
        [Column("address_line1")]
        [StringLength(255)]
        public string AddressLine1 { get; set; } = string.Empty;

        [Column("address_line2")]
        [StringLength(255)]
        public string? AddressLine2 { get; set; }

        [Required]
        [Column("city")]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Column("state_province")]
        [StringLength(100)]
        public string? StateProvince { get; set; }

        [Required]
        [Column("postal_code")]
        [StringLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [Column("country_code")]
        [StringLength(3)]
        public string CountryCode { get; set; } = string.Empty;

        [Column("is_primary")]
        public bool IsPrimary { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;
    }
}
