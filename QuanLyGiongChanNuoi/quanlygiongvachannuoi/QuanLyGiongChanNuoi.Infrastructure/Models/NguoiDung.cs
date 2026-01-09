using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class NguoiDung
{
    public int Id { get; set; }

    public string HoTen { get; set; } = null!;

    public string TenDn { get; set; } = null!;

    public string? MatKhau { get; set; }

    public string? Email { get; set; }

    public bool? TrangThai { get; set; }

    public int? ChucVuId { get; set; }

    public int? DonViHcId { get; set; }

    public virtual ChucVu? ChucVu { get; set; }

    public virtual DonViHc? DonViHc { get; set; }

    public virtual ICollection<LichSuTacDong> LichSuTacDongs { get; set; } = new List<LichSuTacDong>();

    public virtual ICollection<ThanhVienNhom> ThanhVienNhoms { get; set; } = new List<ThanhVienNhom>();

    public virtual ICollection<PhanQuyenNguoiDung> PhanQuyenNguoiDungs { get; set; } = new List<PhanQuyenNguoiDung>();
}
