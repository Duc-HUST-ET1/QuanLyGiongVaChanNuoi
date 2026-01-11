using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
    [Table("GiayChungNhan")]
    public partial class GiayChungNhan
    {
        [Key]
        public int Id { get; set; }

        [Column("CoSoThucAnID")]
        public int CoSoThucAnId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Số giấy chứng nhận")]
        public string SoGiayChungNhan { get; set; } = null!;

        [Column(TypeName = "date")]
        [Display(Name = "Ngày cấp")]
        public DateTime NgayCap { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày hết hạn")]
        public DateTime? NgayHetHan { get; set; }

        [StringLength(50)]
        [Display(Name = "Nơi cấp")]
        public string? NoiCap { get; set; }

        // === THÊM LẠI DÒNG NÀY ĐỂ HẾT LỖI ===
        [StringLength(200)]
        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        [ForeignKey("CoSoThucAnId")]
        [InverseProperty("GiayChungNhans")]
        public virtual CoSoThucAn? CoSoThucAn { get; set; }
    }
}