using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
    [Table("ToChucNguonGen")]
    public partial class ToChucNguonGen
    {
        [Key]
        public int Id { get; set; }

        [Column("ToChucCaNhanID")]
        [Display(Name = "Tổ chức/Cá nhân")]
        public int ToChucCaNhanId { get; set; }

        [Column("NguonGenID")]
        [Display(Name = "Nguồn gen giống")]
        public int NguonGenId { get; set; }

        [Display(Name = "Khu vực")]
        [StringLength(40)]
        public string? KhuVuc { get; set; }

        [Display(Name = "Ngày thực hiện")]
        [Column(TypeName = "datetime")]
        public DateTime? NgayThuThap { get; set; }

        [Display(Name = "Hoạt động")]
        [StringLength(50)]
        public string HoatDong { get; set; } = null!; // "Thu thập", "Bảo tồn", "Khai thác"

        // Quan hệ
        [ForeignKey("NguonGenId")]
        public virtual NguonGen? NguonGen { get; set; }

        [ForeignKey("ToChucCaNhanId")]
        public virtual ToChucCaNhan? ToChucCaNhan { get; set; }
    }
}