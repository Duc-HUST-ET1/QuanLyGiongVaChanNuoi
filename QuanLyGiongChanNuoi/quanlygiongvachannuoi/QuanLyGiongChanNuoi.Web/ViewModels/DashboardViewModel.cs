namespace QuanLyGiongChanNuoi.Web.ViewModels
{
    public class DashboardViewModel
    {
        // 1. Thống kê tổng hợp (KPIs)
        public int TongNguoiDung { get; set; }
        public int NguoiDungMoiThangNay { get; set; }
        public int LuotTruyCapHomNay { get; set; }
        public int TacDongHeThongHomNay { get; set; }

        // 2. Biểu đồ tròn: Tỷ lệ người dùng theo Đơn vị hành chính
        public List<string> LabelDonVi { get; set; }
        public List<int> DataDonVi { get; set; }

        // 3. Biểu đồ đường: Lượt truy cập 7 ngày gần nhất
        public List<string> LabelNgayTruyCap { get; set; }
        public List<int> DataLuotTruyCap { get; set; }

        // 4. Biểu đồ cột: Tác động hệ thống theo loại hành động (Thêm/Sửa/Xóa)
        public int SoLuongThem { get; set; }
        public int SoLuongSua { get; set; }
        public int SoLuongXoa { get; set; }
    }
}