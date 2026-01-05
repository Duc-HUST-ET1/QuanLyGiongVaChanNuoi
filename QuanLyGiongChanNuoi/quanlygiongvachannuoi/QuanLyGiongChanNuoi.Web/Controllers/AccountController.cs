using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using QuanLyGiongChanNuoi.Infrastructure.Models; // Namespace chứa NguoiDung
using QuanLyGiongChanNuoi.Infrastructure;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
	public class AccountController : Controller
	{
		// Thay 'AppDbContext' bằng tên class DbContext thực tế trong dự án của bạn 
		private readonly QuanLyGiongVaThucAnChanNuoiAContext _context;

		public AccountController(QuanLyGiongVaThucAnChanNuoiAContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Login()
		{
			// Nếu đã đăng nhập rồi thì đá về trang chủ, không cần hiện form login nữa
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
			// Dùng .Include(u => u.ChucVu) để lấy luôn tên chức vụ (Admin/Staff)
			var user = await _context.NguoiDungs
				.Include(u => u.ChucVu)
				.FirstOrDefaultAsync(u => u.TenDn == tenDn && u.MatKhau == matKhau);

			// Lưu ý: Hiện tại đang so sánh mật khẩu thô (plain text). 
			// Nếu DB lưu mật khẩu mã hóa (MD5/BCrypt) thì cần hàm verify ở đây.

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

			// 2. Cấu hình các thông tin lưu vào Cookie (Claims)
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.TenDn), // Lưu Username
                new Claim("FullName", user.HoTen ?? "Người dùng"), // Lưu Họ tên
                new Claim("UserId", user.Id.ToString()), // Lưu ID để tiện truy vấn sau này
            };

			// Nếu có chức vụ thì lưu thêm Role
			if (user.ChucVu != null)
			{
				// Giả sử bảng ChucVu có trường 'TenChucVu' hoặc 'MaChucVu'
				// Bạn sửa lại 'TenChucVu' cho đúng với model ChucVu nhé
				claims.Add(new Claim(ClaimTypes.Role, user.ChucVu.TenChucVu));
			}
			else
			{
				claims.Add(new Claim(ClaimTypes.Role, "NhanVien")); // Mặc định
			}

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

			// 3. Ghi Cookie (Đăng nhập thành công)
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

			return RedirectToAction("Index", "Home");
		}

		// Đăng xuất
		public async Task<IActionResult> Logout()
		{
			// 1. Xóa Cookie xác thực (Quan trọng nhất)
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// 2. Xóa Session (Nếu bạn có lưu dữ liệu tạm trong session)
			HttpContext.Session.Clear();

			// 3. Chuyển hướng về trang Đăng nhập
			return RedirectToAction("Login", "Account");
		}
        // --- PHẦN QUÊN MẬT KHẨU ---

        // 1. Hiển thị trang nhập thông tin xác minh
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // 2. Xử lý kiểm tra thông tin
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string tenDn, string email)
        {
            // Kiểm tra xem có user nào khớp cả Tên đăng nhập VÀ Email không
            var user = await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.TenDn == tenDn && u.Email == email);

            if (user == null)
            {
                ViewBag.Error = "Thông tin không chính xác hoặc tài khoản không tồn tại!";
                return View();
            }

            // Nếu đúng, lưu tạm ID người dùng vào TempData để chuyển sang trang đổi pass
            // (Lưu ý: TempData chỉ tồn tại trong 1 lần request, giúp bảo mật hơn)
            TempData["ResetUserId"] = user.Id;

            return RedirectToAction("ResetPassword");
        }

        // 3. Hiển thị trang đặt mật khẩu mới
        [HttpGet]
        public IActionResult ResetPassword()
        {
            // Kiểm tra nếu không có ID (người dùng cố tình gõ link) thì đá về Login
            if (TempData["ResetUserId"] == null)
            {
                return RedirectToAction("Login");
            }

            // Cần giữ lại TempData cho lần post tiếp theo (vì đọc xong nó sẽ tự mất)
            TempData.Keep("ResetUserId");
            return View();
        }

        // 4. Lưu mật khẩu mới
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string newPassword, string confirmPassword)
        {
            // Lấy lại ID từ TempData
            if (TempData["ResetUserId"] == null)
            {
                return RedirectToAction("ForgotPassword");
            }

            int userId = (int)TempData["ResetUserId"];

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp!";
                TempData.Keep("ResetUserId"); // Giữ lại ID để không bị mất
                return View();
            }

            var user = await _context.NguoiDungs.FindAsync(userId);
            if (user != null)
            {
                user.MatKhau = newPassword; // Cập nhật mật khẩu mới
                                            // user.MatKhau = HashPassword(newPassword); // Nếu sau này bạn dùng mã hóa

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đổi mật khẩu thành công! Vui lòng đăng nhập lại.";
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login");
        }
    }
}