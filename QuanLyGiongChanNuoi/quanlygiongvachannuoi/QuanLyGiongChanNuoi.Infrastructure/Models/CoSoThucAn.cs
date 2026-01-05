using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class CoSoThucAn
{
    public int Id { get; set; }

    public string TenCoSo { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string LoaiCoSo { get; set; } = null!;

    public int ToChucCaNhanId { get; set; }

    public int ThucAnChanNuoiId { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public virtual ICollection<CoSoHoaChatCam> CoSoHoaChatCams { get; set; } = new List<CoSoHoaChatCam>();

    public virtual ICollection<CoSoNguyenLieuChoPhep> CoSoNguyenLieuChoPheps { get; set; } = new List<CoSoNguyenLieuChoPhep>();

    public virtual ICollection<GiayChungNhan> GiayChungNhans { get; set; } = new List<GiayChungNhan>();

    public virtual ThucAnChanNuoi ThucAnChanNuoi { get; set; } = null!;

    public virtual ToChucCaNhan ToChucCaNhan { get; set; } = null!;
}
