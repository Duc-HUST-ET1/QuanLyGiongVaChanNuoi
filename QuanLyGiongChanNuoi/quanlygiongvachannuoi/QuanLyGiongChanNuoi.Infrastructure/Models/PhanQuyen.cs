using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class PhanQuyen
{
    public string MaQuyen { get; set; } = null!;

    public string? TenQuyen { get; set; }

    public string? Mota { get; set; }

    // --- SỬA ĐOẠN NÀY ---
    // XÓA: public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    // XÓA: public virtual ICollection<Nhom> Nhoms { get; set; }

    // THAY BẰNG:
    public virtual ICollection<PhanQuyenNguoiDung> PhanQuyenNguoiDungs { get; set; } = new List<PhanQuyenNguoiDung>();
    public virtual ICollection<PhanQuyenNhom> PhanQuyenNhoms { get; set; } = new List<PhanQuyenNhom>();
}