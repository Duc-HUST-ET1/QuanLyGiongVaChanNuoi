using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Models; // Chứa các Model
using QuanLyGiongChanNuoi.Infrastructure;       // Chứa Context (QuanLyGiongVaThucAnChanNuoiAContext)
using QuanLyGiongChanNuoi.Infrastructure.Data;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
    public class LichSuTacDongController : Controller
    {
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

        public LichSuTacDongController(QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _context = context;
        }

        // GET: LichSuTacDong
        public async Task<IActionResult> Index(string tuKhoa, DateTime? tuNgay, DateTime? denNgay)
        {
            // 1. Lấy dữ liệu và Include bảng NguoiDung để lấy tên người làm
            var query = _context.LichSuTacDongs
                .Include(l => l.NguoiDung)
                .AsQueryable();

            // 2. Xử lý bộ lọc (Filter)
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                // Tìm theo Tên người dùng, Hoạt động hoặc Bảng liên quan
                query = query.Where(x => x.NguoiDung.HoTen.Contains(tuKhoa)
                                      || x.HoatDong.Contains(tuKhoa)
                                      || x.BangLienQuan.Contains(tuKhoa));
            }

            if (tuNgay.HasValue)
            {
                query = query.Where(x => x.ThoiGian >= tuNgay.Value);
            }

            if (denNgay.HasValue)
            {
                // Cộng thêm 1 ngày để lấy trọn vẹn ngày kết thúc
                query = query.Where(x => x.ThoiGian < denNgay.Value.AddDays(1));
            }

            // 3. Sắp xếp mới nhất lên đầu
            var data = await query.OrderByDescending(x => x.ThoiGian).ToListAsync();

            // Lưu lại giá trị tìm kiếm để hiển thị lại trên View
            ViewBag.TuKhoa = tuKhoa;
            ViewBag.TuNgay = tuNgay?.ToString("yyyy-MM-dd");
            ViewBag.DenNgay = denNgay?.ToString("yyyy-MM-dd");

            return View(data);
        }
    }
}