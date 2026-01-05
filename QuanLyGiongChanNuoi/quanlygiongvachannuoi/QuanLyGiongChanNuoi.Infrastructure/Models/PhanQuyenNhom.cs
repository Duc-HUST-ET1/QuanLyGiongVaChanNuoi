using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
	// Bảng trung gian nối Nhóm và Quyền
	public partial class PhanQuyenNhom
	{
		public int NhomId { get; set; }
		public string MaQuyen { get; set; } = null!;

		public virtual PhanQuyen MaQuyenNavigation { get; set; } = null!;
		public virtual Nhom Nhom { get; set; } = null!;
	}
}