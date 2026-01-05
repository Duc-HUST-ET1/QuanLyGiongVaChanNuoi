using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class GiayChungNhan
{
    public int Id { get; set; }

    public int CoSoThucAnId { get; set; }

    public string SoGiayChungNhan { get; set; } = null!;

    public DateOnly NgayCap { get; set; }

    public DateOnly? NgayHetHan { get; set; }

    public string? NoiCap { get; set; }

    public string? MoTa { get; set; }

    public virtual CoSoThucAn CoSoThucAn { get; set; } = null!;
}
