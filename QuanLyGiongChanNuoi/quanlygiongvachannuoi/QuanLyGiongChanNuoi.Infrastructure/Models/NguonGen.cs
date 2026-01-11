using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
    [Table("NguonGen")]
    public partial class NguonGen
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên nguồn gen")]
        public string TenNguonGen { get; set; } = null!;

        [StringLength(200)]
        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        // Quan hệ ngược: Một nguồn gen có thể được thu thập bởi nhiều tổ chức
        [InverseProperty("NguonGen")]
        public virtual ICollection<ToChucNguonGen> ToChucNguonGens { get; set; } = new List<ToChucNguonGen>();
    }
}