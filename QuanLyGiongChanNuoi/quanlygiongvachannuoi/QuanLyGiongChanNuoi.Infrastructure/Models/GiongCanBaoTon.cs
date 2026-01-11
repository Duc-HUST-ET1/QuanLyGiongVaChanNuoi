using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
    [Table("GiongCanBaoTon")]
    public partial class GiongCanBaoTon
    {
        [Key]
        public int Id { get; set; }

        [Column("GiongID")]
        [Display(Name = "Tên giống")]
        public int GiongId { get; set; } // Liên kết với bảng GiongVatNuoi

        [Display(Name = "Phân loại")]
        public string Loai { get; set; } = null!; // "Bảo tồn" hoặc "Cấm xuất khẩu"

        [Display(Name = "Lý do bảo tồn")]
        public string? LyDo { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        public DateTime? NgayBaoTon { get; set; }

        [Column("TrangThai")]
        [Display(Name = "Đang hoạt động")]
        public bool? TrangThai { get; set; }

        // Quan hệ (Foreign Key)
        [ForeignKey("GiongId")]
        public virtual GiongVatNuoi? GiongVatNuoi { get; set; }
    }
}