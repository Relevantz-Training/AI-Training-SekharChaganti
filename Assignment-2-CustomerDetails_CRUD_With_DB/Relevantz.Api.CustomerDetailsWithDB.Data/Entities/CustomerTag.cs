using System.ComponentModel.DataAnnotations.Schema;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Entities
{
    [Table("customer_tags")]
    public class CustomerTag
    {
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("tag_id")]
        public int TagId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; } = null!;
    }
}
