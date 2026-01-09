using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Data;
using QuanLyGiongChanNuoi.Web.ViewModels;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
    [Authorize] // Yêu cầu đăng nhập mới xem được
    public class BaoCaoController : Controller
    {
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

        public BaoCaoController(QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel();
            var today = DateTime.Now.Date;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            // 1. TÍNH KPI TỔNG HỢP
            model.TongNguoiDung = await _context.NguoiDungs.CountAsync();

            // Người dùng mới trong tháng (giả sử bạn có cột NgayTao, nếu không có thì bỏ dòng này hoặc set = 0)
            // model.NguoiDungMoiThangNay = await _context.NguoiDungs.CountAsync(u => u.NgayTao >= startOfMonth);
            model.NguoiDungMoiThangNay = 0; // Tạm để 0 nếu chưa có cột NgayTao

            model.LuotTruyCapHomNay = await _context.LichSuTruyCaps
                                            .CountAsync(x => x.ThoiGian >= today);

            model.TacDongHeThongHomNay = await _context.LichSuTacDongs
                                            .CountAsync(x => x.ThoiGian >= today);

            // 2. DỮ LIỆU BIỂU ĐỒ TRÒN (User theo Đơn vị)
            var statsDonVi = await _context.NguoiDungs
                .Include(u => u.DonViHc)
                .GroupBy(u => u.DonViHc != null ? u.DonViHc.Ten : "Chưa phân bổ")
                .Select(g => new { TenDonVi = g.Key, SoLuong = g.Count() })
                .ToListAsync();

            model.LabelDonVi = statsDonVi.Select(x => x.TenDonVi).ToList();
            model.DataDonVi = statsDonVi.Select(x => x.SoLuong).ToList();

            // 3. DỮ LIỆU BIỂU ĐỒ ĐƯỜNG (Truy cập 7 ngày qua)
            var sevenDaysAgo = today.AddDays(-6);
            var statsTruyCap = await _context.LichSuTruyCaps
                .Where(x => x.ThoiGian >= sevenDaysAgo)
                .GroupBy(x => x.ThoiGian.Value.Date) // Group theo ngày
                .Select(g => new { Ngay = g.Key, SoLuong = g.Count() })
                .ToListAsync();

            // Chuẩn hóa dữ liệu 7 ngày (để ngày nào không có thì hiện số 0)
            model.LabelNgayTruyCap = new List<string>();
            model.DataLuotTruyCap = new List<int>();

            for (int i = 0; i < 7; i++)
            {
                var date = sevenDaysAgo.AddDays(i);
                var label = date.ToString("dd/MM");
                var count = statsTruyCap.FirstOrDefault(x => x.Ngay == date)?.SoLuong ?? 0;

                model.LabelNgayTruyCap.Add(label);
                model.DataLuotTruyCap.Add(count);
            }

            // 4. DỮ LIỆU BIỂU ĐỒ CỘT (Tác động Thêm/Sửa/Xóa)
            // Lưu ý: Logic này dựa vào chuỗi "Thêm mới", "Cập nhật", "Xóa" bạn đã lưu trong DbContext
            var statsTacDong = await _context.LichSuTacDongs
                .GroupBy(x => x.HoatDong)
                .Select(g => new { HanhDong = g.Key, SoLuong = g.Count() })
                .ToListAsync();

            model.SoLuongThem = statsTacDong.FirstOrDefault(x => x.HanhDong.Contains("Thêm"))?.SoLuong ?? 0;
            model.SoLuongSua = statsTacDong.FirstOrDefault(x => x.HanhDong.Contains("Cập nhật") || x.HanhDong.Contains("Sửa"))?.SoLuong ?? 0;
            model.SoLuongXoa = statsTacDong.FirstOrDefault(x => x.HanhDong.Contains("Xóa"))?.SoLuong ?? 0;

            return View(model);
        }
    }
}