using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
    [Table("Nhom")]
    public partial class Nhom
    {
        public Nhom()
        {
            PhanQuyenNhoms = new HashSet<PhanQuyenNhom>();
            ThanhVienNhoms = new HashSet<ThanhVienNhom>();
        }

        [Key]
        public int Id { get; set; }

        public string? Ten { get; set; }

        [Column("mota")]
        public string? Mota { get; set; }

        public DateTime? NgayTao { get; set; }

        public bool? TrangThai { get; set; } // Cột này chúng ta đã thêm ở bài trước

        public virtual ICollection<PhanQuyenNhom> PhanQuyenNhoms { get; set; }
        public virtual ICollection<ThanhVienNhom> ThanhVienNhoms { get; set; }
    }
}