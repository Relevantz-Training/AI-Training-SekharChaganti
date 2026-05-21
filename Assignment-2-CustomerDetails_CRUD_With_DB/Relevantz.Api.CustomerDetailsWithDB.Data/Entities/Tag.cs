using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Entities
{
    [Table("tags")]
    public class Tag
    {
        [Key]
        [Column("tag_id")]
        public int TagId { get; set; }

        [Required]
        [Column("tag_name")]
        [StringLength(50)]
        public string TagName { get; set; } = string.Empty;

        public ICollection<CustomerTag> CustomerTags { get; set; } = new List<CustomerTag>();
    }
}
