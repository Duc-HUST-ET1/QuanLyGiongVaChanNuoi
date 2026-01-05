using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string TenMenu { get; set; } = null!;

    public string? LienKet { get; set; }

    public int? MenuChaId { get; set; }

    public int CapDo { get; set; }

    public bool TrangThai { get; set; }

    public virtual ICollection<Menu> InverseMenuCha { get; set; } = new List<Menu>();

    public virtual Menu? MenuCha { get; set; }
}
