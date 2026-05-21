using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Entities
{
    [Table("customer_business_profiles")]
    public class CustomerBusinessProfile
    {
        [Key]
        [Column("business_profile_id")]
        public int BusinessProfileId { get; set; }

        [Required]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("company_name")]
        [StringLength(200)]
        public string? CompanyName { get; set; }

        [Column("job_title")]
        [StringLength(100)]
        public string? JobTitle { get; set; }

        [Column("lead_source")]
        [StringLength(100)]
        public string? LeadSource { get; set; }

        [Required]
        [Column("lifecycle_stage")]
        [StringLength(20)]
        public string LifecycleStage { get; set; } = "Lead";

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;
    }
}
