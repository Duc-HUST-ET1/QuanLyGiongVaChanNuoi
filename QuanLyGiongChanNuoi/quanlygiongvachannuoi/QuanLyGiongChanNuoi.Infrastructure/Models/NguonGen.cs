using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class NguonGen
{
    public int Id { get; set; }

    public string TenNguonGen { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<ToChucNguonGen> ToChucNguonGens { get; set; } = new List<ToChucNguonGen>();
}
