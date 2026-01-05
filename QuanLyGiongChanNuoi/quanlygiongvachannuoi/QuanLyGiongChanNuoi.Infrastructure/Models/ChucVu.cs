using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class ChucVu
{
    public int Id { get; set; }

    public string TenChucVu { get; set; } = null!;

    public string? Mota { get; set; }

    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
}
