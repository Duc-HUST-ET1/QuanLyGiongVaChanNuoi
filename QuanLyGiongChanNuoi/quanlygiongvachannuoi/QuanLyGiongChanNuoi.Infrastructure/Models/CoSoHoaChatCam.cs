using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class CoSoHoaChatCam
{
    public int Id { get; set; }

    public int CoSoThucAnId { get; set; }

    public int HoaChatCamId { get; set; }

    public DateOnly? NgayPhatHien { get; set; }

    public string? LyDoSuDung { get; set; }

    public virtual CoSoThucAn CoSoThucAn { get; set; } = null!;

    public virtual HoaChatCam HoaChatCam { get; set; } = null!;
}
