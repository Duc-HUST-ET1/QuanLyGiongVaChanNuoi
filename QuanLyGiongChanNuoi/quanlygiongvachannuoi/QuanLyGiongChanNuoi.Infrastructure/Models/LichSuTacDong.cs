using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class LichSuTacDong
{
    public int Id { get; set; }

    public int NguoiDungId { get; set; }

    public DateTime? ThoiGian { get; set; }

    public string HoatDong { get; set; } = null!;

    public string? BangLienQuan { get; set; }

    public string? KhoaChinh { get; set; }

    public string? MoTaThem { get; set; }

    public virtual NguoiDung NguoiDung { get; set; } = null!;
}
