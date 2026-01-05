using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class HoaChatCam
{
    public int Id { get; set; }

    public string TenHoaChat { get; set; } = null!;

    public string? LyDoCam { get; set; }

    public virtual ICollection<CoSoHoaChatCam> CoSoHoaChatCams { get; set; } = new List<CoSoHoaChatCam>();
}
