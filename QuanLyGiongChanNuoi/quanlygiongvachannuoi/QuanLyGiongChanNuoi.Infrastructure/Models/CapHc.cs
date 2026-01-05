using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class CapHc
{
    public int Id { get; set; }

    public string Ten { get; set; } = null!;

    public int Cap { get; set; }

    public virtual ICollection<DonViHc> DonViHcs { get; set; } = new List<DonViHc>();
}
