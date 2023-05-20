using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("ชื่อหมวดหมู่")]
        public string Name { get; set; }

        [DisplayName("ลำดับการแสดงผล")]
        [Range(1, 100, ErrorMessage = "Must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
