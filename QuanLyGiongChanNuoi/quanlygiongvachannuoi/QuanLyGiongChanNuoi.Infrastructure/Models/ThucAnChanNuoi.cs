using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class ThucAnChanNuoi
{
    public int Id { get; set; }

    public string TenThucAn { get; set; } = null!;

    public string? MoTa { get; set; }

    public string LoaiThucAn { get; set; } = null!;

    public virtual ICollection<CoSoThucAn> CoSoThucAns { get; set; } = new List<CoSoThucAn>();
}
