using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
    [Table("CoSoThucAn")]
    public partial class CoSoThucAn
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên cơ sở")]
        public string TenCoSo { get; set; } = null!;

        [StringLength(200)]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Loại hình")]
        public string LoaiCoSo { get; set; } = null!;

        [Column("ToChucCaNhanID")]
        [Display(Name = "Chủ cơ sở")]
        public int ToChucCaNhanId { get; set; }

        [Column("ThucAnChanNuoiID")]
        [Display(Name = "Sản phẩm chính")]
        public int ThucAnChanNuoiId { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Ngày cập nhật")]
        public DateTime? NgayCapNhat { get; set; }

        // --- CÁC QUAN HỆ (Foreign Keys) ---

        [ForeignKey("ToChucCaNhanId")]
        public virtual ToChucCaNhan? ToChucCaNhan { get; set; }

        [ForeignKey("ThucAnChanNuoiId")]
        public virtual ThucAnChanNuoi? ThucAnChanNuoi { get; set; }

        // Quan hệ 1-N với Giấy chứng nhận
        [InverseProperty("CoSoThucAn")]
        public virtual ICollection<GiayChungNhan> GiayChungNhans { get; set; } = new List<GiayChungNhan>();

        // === THÊM LẠI 2 DÒNG NÀY ĐỂ HẾT LỖI CONTEXT ===
        [InverseProperty("CoSoThucAn")]
        public virtual ICollection<CoSoHoaChatCam> CoSoHoaChatCams { get; set; } = new List<CoSoHoaChatCam>();

        [InverseProperty("CoSoThucAn")]
        public virtual ICollection<CoSoNguyenLieuChoPhep> CoSoNguyenLieuChoPheps { get; set; } = new List<CoSoNguyenLieuChoPhep>();
    }
}