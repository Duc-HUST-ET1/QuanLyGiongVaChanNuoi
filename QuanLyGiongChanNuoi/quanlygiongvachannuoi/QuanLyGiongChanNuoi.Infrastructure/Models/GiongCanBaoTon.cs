using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class GiongCanBaoTon
{
    public int Id { get; set; }

    public int GiongId { get; set; }

    public string Loai { get; set; } = null!;

    public string? LyDo { get; set; }

    public DateOnly? NgayBaoTon { get; set; }

    public bool? TrangThai { get; set; }

    public virtual GiongVatNuoi Giong { get; set; } = null!;
}
