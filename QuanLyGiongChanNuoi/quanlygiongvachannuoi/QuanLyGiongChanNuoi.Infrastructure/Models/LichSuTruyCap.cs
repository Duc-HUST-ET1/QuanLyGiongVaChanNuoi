using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class LichSuTruyCap
{
    public int Id { get; set; }

    public int? NguoiDungId { get; set; }

    public DateTime? ThoiGian { get; set; }

    public string? Mota { get; set; }

    public string? IpAddress { get; set; }

    public virtual NguoiDung? NguoiDung { get; set; }
}
