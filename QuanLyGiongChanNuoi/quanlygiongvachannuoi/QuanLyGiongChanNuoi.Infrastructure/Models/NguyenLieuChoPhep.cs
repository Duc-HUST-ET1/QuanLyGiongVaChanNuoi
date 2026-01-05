using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class NguyenLieuChoPhep
{
    public int Id { get; set; }

    public string TenNguyenLieu { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<CoSoNguyenLieuChoPhep> CoSoNguyenLieuChoPheps { get; set; } = new List<CoSoNguyenLieuChoPhep>();
}
