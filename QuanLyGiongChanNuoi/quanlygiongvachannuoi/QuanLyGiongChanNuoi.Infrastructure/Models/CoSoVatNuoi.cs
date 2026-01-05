using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class CoSoVatNuoi
{
    public int Id { get; set; }

    public string TenCoSo { get; set; } = null!;

    public string? DiaChi { get; set; }

    public bool? Trangthai { get; set; }

    public int ToChucCaNhanId { get; set; }

    public string LoaiCoSo { get; set; } = null!;

    public int GiongVatNuoiId { get; set; }

    public virtual GiongVatNuoi GiongVatNuoi { get; set; } = null!;

    public virtual ToChucCaNhan ToChucCaNhan { get; set; } = null!;
}
