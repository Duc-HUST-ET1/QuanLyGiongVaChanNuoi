using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class ThanhVienNhom
{
    public int NguoiDungId { get; set; }

    public int NhomId { get; set; }

    public bool? LaTruongNhom { get; set; }

    public DateTime? NgayThamGia { get; set; }

    public virtual NguoiDung NguoiDung { get; set; } = null!;

    public virtual Nhom Nhom { get; set; } = null!;
}
