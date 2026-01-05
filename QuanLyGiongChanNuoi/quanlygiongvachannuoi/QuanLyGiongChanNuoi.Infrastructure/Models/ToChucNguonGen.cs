using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class ToChucNguonGen
{
    public int Id { get; set; }

    public int ToChucCaNhanId { get; set; }

    public int NguonGenId { get; set; }

    public string? KhuVuc { get; set; }

    public DateTime? NgayThuThap { get; set; }

    public string HoatDong { get; set; } = null!;

    public virtual NguonGen NguonGen { get; set; } = null!;

    public virtual ToChucCaNhan ToChucCaNhan { get; set; } = null!;
}
