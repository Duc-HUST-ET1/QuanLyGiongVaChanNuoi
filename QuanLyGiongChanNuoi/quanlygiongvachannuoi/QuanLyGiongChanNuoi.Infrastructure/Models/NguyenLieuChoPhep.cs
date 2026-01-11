using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
    [Table("NguyenLieuChoPhep")]
    public partial class NguyenLieuChoPhep
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên nguyên liệu")]
        [StringLength(100)]
        [Display(Name = "Tên nguyên liệu")]
        public string TenNguyenLieu { get; set; } = null!;

        [StringLength(200)]
        [Display(Name = "Mô tả / Thành phần")]
        public string? MoTa { get; set; }

        // === THÊM DÒNG NÀY ĐỂ HẾT LỖI ===
        [InverseProperty("NguyenLieu")]
        public virtual ICollection<CoSoNguyenLieuChoPhep> CoSoNguyenLieuChoPheps { get; set; } = new List<CoSoNguyenLieuChoPhep>();
    }
}