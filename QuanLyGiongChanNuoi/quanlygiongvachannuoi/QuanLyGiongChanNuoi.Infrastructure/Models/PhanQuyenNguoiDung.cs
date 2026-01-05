using System;
using System.Collections.Generic;

namespace QuanLyGiongChanNuoi.Infrastructure.Models
{
	// Bảng trung gian nối Người dùng và Quyền
	public partial class PhanQuyenNguoiDung
	{
		public int NguoiDungId { get; set; }
		public string MaQuyen { get; set; } = null!;

		public virtual PhanQuyen MaQuyenNavigation { get; set; } = null!;
		public virtual NguoiDung NguoiDung { get; set; } = null!;
	}
}