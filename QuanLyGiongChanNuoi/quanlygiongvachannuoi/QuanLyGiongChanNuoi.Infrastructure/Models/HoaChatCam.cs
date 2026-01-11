using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
    [Table("HoaChatCam")]
    public partial class HoaChatCam
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên hóa chất")]
        [StringLength(100)]
        [Display(Name = "Tên hóa chất / Kháng sinh")]
        public string TenHoaChat { get; set; } = null!;

        [StringLength(200)]
        [Display(Name = "Lý do cấm / Ghi chú")]
        public string? LyDoCam { get; set; }

        // === THÊM DÒNG NÀY ĐỂ HẾT LỖI ===
        [InverseProperty("HoaChatCam")]
        public virtual ICollection<CoSoHoaChatCam> CoSoHoaChatCams { get; set; } = new List<CoSoHoaChatCam>();
    }
}