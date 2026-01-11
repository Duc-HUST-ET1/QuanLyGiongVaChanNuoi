using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using QuanLyGiongChanNuoi.Infrastructure.Models;
using QuanLyGiongChanNuoi.Infrastructure;
using QuanLyGiongChanNuoi.Infrastructure.Data;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

        public AccountController(QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string tenDn, string matKhau)
        {
            if (string.IsNullOrEmpty(tenDn) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }

            // 1. Tìm người dùng trong DB
            var user = await _context.NguoiDungs
                .Include(u => u.ChucVu)
                .FirstOrDefaultAsync(u => u.TenDn == tenDn && u.MatKhau == matKhau);

            if (user == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
                return View();
            }

            if (user.TrangThai == false)
            {
                ViewBag.Error = "Tài khoản này đang bị khóa!";
                return View();
            }

            // 2. Cấu hình Claims (Thông tin lưu vào Cookie)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TenDn),
                new Claim("FullName", user.HoTen ?? "Người dùng"),
                new Claim("UserId", user.Id.ToString()),
            };

            // Phân quyền (Role)
            if (user.ChucVu != null)
            {
                // Lưu ý: Đảm bảo user.ChucVu.TenChucVu là tên thuộc tính đúng trong Model ChucVu của bạn
                claims.Add(new Claim(ClaimTypes.Role, user.ChucVu.TenChucVu));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "NhanVien"));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // 3. Ghi Cookie (Đăng nhập thành công)
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            // ===================================================
            // 4. GHI LỊCH SỬ TRUY CẬP (Thêm mới đoạn này vào đây)
            // ===================================================
            try
            {
                var ls = new QuanLyGiongChanNuoi.Infrastructure.Models.LichSuTruyCap
                {
                    NguoiDungId = user.Id,
                    ThoiGian = DateTime.Now,
                    HanhDong = "Đăng nhập",
                    DiaChiIP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    TrinhDuyet = Request.Headers["User-Agent"].ToString()
                };

                _context.LichSuTruyCaps.Add(ls);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ra cửa sổ Output để debug nếu cần, không làm crash app
                Console.WriteLine("Lỗi ghi lịch sử: " + ex.Message);
            }
            // ===================================================

            return RedirectToAction("Index", "Home");
        }

        // Đăng xuất
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        // --- PHẦN QUÊN MẬT KHẨU ---

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string tenDn, string email)
        {
            var user = await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.TenDn == tenDn && u.Email == email);

            if (user == null)
            {
                ViewBag.Error = "Thông tin không chính xác hoặc tài khoản không tồn tại!";
                return View();
            }

            TempData["ResetUserId"] = user.Id;
            return RedirectToAction("ResetPassword");
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            if (TempData["ResetUserId"] == null)
            {
                return RedirectToAction("Login");
            }
            TempData.Keep("ResetUserId");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string newPassword, string confirmPassword)
        {
            if (TempData["ResetUserId"] == null)
            {
                return RedirectToAction("ForgotPassword");
            }

            int userId = (int)TempData["ResetUserId"];

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp!";
                TempData.Keep("ResetUserId");
                return View();
            }

            var user = await _context.NguoiDungs.FindAsync(userId);
            if (user != null)
            {
                user.MatKhau = newPassword;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đổi mật khẩu thành công! Vui lòng đăng nhập lại.";
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}