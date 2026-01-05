using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class DonViHc
{
    public int Id { get; set; }

    public string Ten { get; set; } = null!;

    public int Cap { get; set; }

    public int? TrucThuoc { get; set; }

    public int CapHcId { get; set; }

    public string? MaBuuDien { get; set; }

    public virtual CapHc CapHc { get; set; } = null!;

    public virtual ICollection<DonViHc> InverseTrucThuocNavigation { get; set; } = new List<DonViHc>();

    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();

    public virtual DonViHc? TrucThuocNavigation { get; set; }
}
