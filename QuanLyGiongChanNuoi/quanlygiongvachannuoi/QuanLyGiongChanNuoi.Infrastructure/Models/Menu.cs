using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
    [Table("Menu")]
    public partial class Menu
    {
        public Menu()
        {
            MenuCons = new HashSet<Menu>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên menu không được để trống")]
        public string TenMenu { get; set; } = null!;

        public string? LienKet { get; set; } // Giữ lại để tương thích dữ liệu cũ

        [Column("MenuChaID")]
        public int? MenuChaId { get; set; }

        public int CapDo { get; set; }

        public bool TrangThai { get; set; }

        // --- CÁC CỘT MỚI (BẮT BUỘC PHẢI CÓ) ---
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? Icon { get; set; }
        public int? ThuTu { get; set; }
        public bool HienThi { get; set; }
        // --------------------------------------

        [ForeignKey("MenuChaId")]
        public virtual Menu? MenuCha { get; set; }
        public virtual ICollection<Menu> MenuCons { get; set; }
    }
}