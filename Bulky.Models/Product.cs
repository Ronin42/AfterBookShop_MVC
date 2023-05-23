using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bulky.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("ชื่อหนังสือ")]
        public string? Title { get; set; }
        public string? Description { get; set; }

        [Required]
        [DisplayName("ISBN (เลขมาตรฐานสากลประจำหนังสือ)")]
        public string? ISBN { get; set; } //รหัสที่กำหนดขึ้นให้ใช้กับสิ่งพิมพ์ประเภทหนังสือทั่วไป 

        [Required]
        [DisplayName("ชื่อผู้แต่ง")]
        public string? Author { get; set; }

        [Required]
        [DisplayName("List Price")]
        [Range(1,1000)]
        public float ListPrice { get; set; }

        [Required]
        [DisplayName("Price for 1-50")]
        [Range(1, 1000)]
        public float Price { get; set; }

        [Required]
        [DisplayName("Price for 50+")]
        [Range(1, 1000)]
        public float Price50 { get; set; }

        [Required]
        [DisplayName("Price for 100+")]
        [Range(1, 1000)]
        public float Price100 { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

    }
}
