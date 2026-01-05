using System;

namespace QuanLyGiongChanNuoi.Web.Models
{
	public class PhanQuyenViewModel
	{
		public string MaQuyen { get; set; }
		public string TenQuyen { get; set; }

		// Controller đang gọi 'Mota' (chữ t thường), nên ở đây phải để là Mota
		public string? Mota { get; set; }

		// Controller đang gọi 'CoQuyen', nên ở đây phải có CoQuyen
		public bool CoQuyen { get; set; }

		public bool IsSelected { get; set; } // Giữ lại đề phòng view cũ dùng
	}
}