using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class Nhom
{
    public int Id { get; set; }

    public string? Ten { get; set; }

    public string? Mota { get; set; }

    public DateTime? NgayTao { get; set; }

    public virtual ICollection<ThanhVienNhom> ThanhVienNhoms { get; set; } = new List<ThanhVienNhom>();

    public virtual ICollection<PhanQuyenNhom> PhanQuyenNhoms { get; set; } = new List<PhanQuyenNhom>();
}
