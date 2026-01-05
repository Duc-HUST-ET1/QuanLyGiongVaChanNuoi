using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class GiongVatNuoi
{
    public int Id { get; set; }

    public string TenGiong { get; set; } = null!;

    public string? MoTa { get; set; }

    public string Loai { get; set; } = null!;

    public virtual ICollection<CoSoVatNuoi> CoSoVatNuois { get; set; } = new List<CoSoVatNuoi>();

    public virtual ICollection<GiongCanBaoTon> GiongCanBaoTons { get; set; } = new List<GiongCanBaoTon>();
}
