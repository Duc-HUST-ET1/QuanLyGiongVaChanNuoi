using System;

namespace QuanLyGiongChanNuoi.Web.Models // <--- Phải đúng namespace này
{
	public class KetQuaPhanQuyenViewModel
	{
		public string MaQuyen { get; set; }
		public string TenQuyen { get; set; }
		public string NguonQuyen { get; set; } // Ví dụ: "Từ nhóm Admin" hoặc "Cấp riêng"
	}
}