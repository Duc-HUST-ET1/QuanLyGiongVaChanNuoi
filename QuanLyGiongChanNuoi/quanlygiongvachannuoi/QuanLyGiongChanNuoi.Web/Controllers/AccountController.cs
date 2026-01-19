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
            // 1. Tìm user theo TÊN ĐĂNG NHẬP trước (Chưa check pass vội)
            var user = await _context.NguoiDungs
                                     .Include(u => u.ChucVu)
                                     .FirstOrDefaultAsync(u => u.TenDn == tenDn);

            // TRƯỜNG HỢP 1: Không tìm thấy tên đăng nhập
            if (user == null)
            {
                ViewBag.Error = $"Không tìm thấy tài khoản có tên: {tenDn}";
                return View();
            }

            // 2. Tính toán mã hóa mật khẩu nhập vào
            string passNhapVao = matKhau;
            string passMaHoa = GetMD5(matKhau); // Code tính ra cái này

            // 3. So sánh (Debug)
            // Pass trong DB: user.MatKhau
            // Pass tính toán: passMaHoa

            if (user.MatKhau != passMaHoa)
            {
                // IN LỖI CHI TIẾT RA MÀN HÌNH ĐỂ SOI
                ViewBag.Error = "SAI MẬT KHẨU! " ;
                return View();
            }

            // TRƯỜNG HỢP 3: Bị khóa
            if (user.TrangThai == false)
            {
                ViewBag.Error = "Tài khoản đang bị khóa (TrangThai = false)";
                return View();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.TenDn),
        new Claim("FullName", user.HoTen ?? "User"),
        new Claim("UserId", user.Id.ToString())
    };

            if (user.ChucVu != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.ChucVu.TenChucVu));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "NhanVien"));
            }

            // 1. Tạo Identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // 2. Tạo Principal (BẠN ĐANG THIẾU DÒNG NÀY NÊN NÓ BÁO LỖI)
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // 3. Thực hiện đăng nhập
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

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
        private string GetMD5(string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bHash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            System.Text.StringBuilder sbHash = new System.Text.StringBuilder();
            foreach (byte b in bHash)
            {
                sbHash.Append(String.Format("{0:x2}", b));
            }
            return sbHash.ToString();
        }
    }
}