using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class ToChucCaNhan
{
    public int Id { get; set; }

    public string Ten { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? LoaiHinh { get; set; }

    public string LoaiHoatDong { get; set; } = null!;

    public virtual ICollection<CoSoThucAn> CoSoThucAns { get; set; } = new List<CoSoThucAn>();

    public virtual ICollection<CoSoVatNuoi> CoSoVatNuois { get; set; } = new List<CoSoVatNuoi>();

    public virtual ICollection<ToChucNguonGen> ToChucNguonGens { get; set; } = new List<ToChucNguonGen>();
}
