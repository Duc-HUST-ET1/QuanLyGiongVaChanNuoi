using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
    [Table("CoSoVatNuoi")]
    public partial class CoSoVatNuoi
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên Cơ sở")]
        public string TenCoSo { get; set; } = null!;

        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }

        // Ánh xạ cột 'trangthai' (viết thường trong DB) sang 'TrangThai' (viết hoa trong code)
        [Column("trangthai")]
        public bool? TrangThai { get; set; }

        [Column("ToChucCaNhanID")] // Đảm bảo khớp tên cột SQL
        [Display(Name = "Chủ sở hữu")]
        public int ToChucCaNhanID { get; set; } // Viết hoa chữ D để khớp với Controller

        [Display(Name = "Loại cơ sở")]
        public string LoaiCoSo { get; set; } = null!;

        [Column("GiongVatNuoiID")] // Đảm bảo khớp tên cột SQL
        [Display(Name = "Giống vật nuôi")]
        public int GiongVatNuoiID { get; set; } // Viết hoa chữ D để khớp với Controller

        // Quan hệ bảng (Foreign Keys)
        [ForeignKey("ToChucCaNhanID")]
        [InverseProperty("CoSoVatNuois")]
        public virtual ToChucCaNhan? ToChucCaNhan { get; set; }

        [ForeignKey("GiongVatNuoiID")]
        public virtual GiongVatNuoi? GiongVatNuoi { get; set; }
    }
}