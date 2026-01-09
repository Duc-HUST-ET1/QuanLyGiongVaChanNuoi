using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
    [Table("lich_su_truy_cap")] // Ánh xạ đúng tên bảng trong SQL
    public partial class LichSuTruyCap
    {
        [Key]
        public int Id { get; set; }

        [Column("NguoiDungID")]
        public int? NguoiDungId { get; set; }

        public DateTime? ThoiGian { get; set; }

        [Column("mota")] // Map cột 'mota' trong DB thành 'HanhDong' trong C# cho dễ hiểu
        public string? HanhDong { get; set; }

        [Column("IP_address")] // Map cột IP
        public string? DiaChiIP { get; set; }

        public string? TrinhDuyet { get; set; } // Cột mới thêm

        [ForeignKey("NguoiDungId")]
        public virtual NguoiDung? NguoiDung { get; set; }
    }
}