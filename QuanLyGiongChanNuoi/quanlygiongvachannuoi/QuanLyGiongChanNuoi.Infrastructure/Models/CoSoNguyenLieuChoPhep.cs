using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class CoSoNguyenLieuChoPhep
{
    public int Id { get; set; }

    public int CoSoThucAnId { get; set; }

    public int NguyenLieuId { get; set; }

    public double? SoLuong { get; set; }

    public DateOnly? NgayCapNhat { get; set; }

    public virtual CoSoThucAn CoSoThucAn { get; set; } = null!;

    public virtual NguyenLieuChoPhep NguyenLieu { get; set; } = null!;
}
