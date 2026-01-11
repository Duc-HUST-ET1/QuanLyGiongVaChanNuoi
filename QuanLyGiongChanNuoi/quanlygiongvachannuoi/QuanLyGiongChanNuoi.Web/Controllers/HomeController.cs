using Microsoft.AspNetCore.Mvc;
using QuanLyGiongChanNuoi.Infrastructure.Data; // Thêm dòng này
using QuanLyGiongChanNuoi.Infrastructure.Models;
using System.Linq; // Thêm dòng này

namespace QuanLyGiongChanNuoi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context; // Khai báo Context

        // Tiêm Context vào Constructor
        public HomeController(ILogger<HomeController> logger, QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Nếu chưa đăng nhập thì hiện trang giới thiệu tĩnh
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            // Nếu đã đăng nhập, lấy số liệu thống kê
            // 1. Tổng số giống vật nuôi
            ViewBag.CountGiong = _context.GiongVatNuois.Count();

            // 2. Tổng số Cơ sở thức ăn
            ViewBag.CountCoSo = _context.CoSoThucAns.Count();

            // 3. Số giống đang cần bảo tồn
            ViewBag.CountBaoTon = _context.GiongCanBaoTons.Count(x => x.Loai == "Bảo tồn" && x.TrangThai == true);

            // 4. Số hóa chất cấm
            ViewBag.CountCam = _context.HoaChatCams.Count();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // ... các hàm Error giữ nguyên
    }
}