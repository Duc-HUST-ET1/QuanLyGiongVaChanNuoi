using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class LoaiHoatDong
{
    public int Id { get; set; }

    public string TenHoatDong { get; set; } = null!;

    public string? Mota { get; set; }
}
