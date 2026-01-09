using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure;
using QuanLyGiongChanNuoi.Infrastructure.Models;
using QuanLyGiongChanNuoi.Infrastructure.Data;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class LichSuTruyCapController : Controller
    {
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

        public LichSuTruyCapController(QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, DateTime? tuNgay, DateTime? denNgay)
        {
            var query = _context.LichSuTruyCaps.Include(x => x.NguoiDung).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.NguoiDung.TenDn.Contains(searchString) || x.NguoiDung.HoTen.Contains(searchString));
            }

            if (tuNgay.HasValue)
            {
                query = query.Where(x => x.ThoiGian >= tuNgay.Value);
            }

            if (denNgay.HasValue)
            {
                // Cộng thêm 1 ngày để lấy trọn vẹn ngày cuối
                query = query.Where(x => x.ThoiGian < denNgay.Value.AddDays(1));
            }

            // Lấy 100 dòng mới nhất
            var data = await query.OrderByDescending(x => x.ThoiGian).Take(100).ToListAsync();

            ViewData["SearchString"] = searchString;
            ViewData["TuNgay"] = tuNgay?.ToString("yyyy-MM-dd");
            ViewData["DenNgay"] = denNgay?.ToString("yyyy-MM-dd");

            return View(data);
        }
    }
}