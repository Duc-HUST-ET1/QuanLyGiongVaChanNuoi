using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure; // Hoặc .Data tùy namespace của bạn
using QuanLyGiongChanNuoi.Infrastructure.Models;
using System.Security.Cryptography;
using System.Text;
using QuanLyGiongChanNuoi.Infrastructure.Data;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
    [Authorize(Roles = "Quản trị viên")] // Chỉ Admin mới được vào
    public class NguoiDungController : Controller
    {
        // Thay tên Context cho đúng với cái bạn đang dùng (AContext hoặc Context)
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

        public NguoiDungController(QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _context = context;
        }

        // 1. DANH SÁCH (INDEX)
        public async Task<IActionResult> Index(string searchString)
        {
            var query = _context.NguoiDungs
                .Include(n => n.ChucVu)
                .Include(n => n.DonViHc)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.TenDn.Contains(searchString) || s.HoTen.Contains(searchString));
                ViewData["CurrentFilter"] = searchString;
            }

            return View(await query.ToListAsync());
        }

        // 2. THÊM MỚI (CREATE)
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ChucVuId"] = new SelectList(_context.ChucVus, "Id", "TenChucVu");
            ViewData["DonViHcId"] = new SelectList(_context.DonViHcs, "Id", "Ten");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NguoiDung nguoiDung)
        {
            // Kiểm tra trùng tên đăng nhập
            if (await _context.NguoiDungs.AnyAsync(u => u.TenDn == nguoiDung.TenDn))
            {
                ModelState.AddModelError("TenDn", "Tên đăng nhập này đã tồn tại!");
            }

            if (ModelState.IsValid)
            {
                // Mã hóa mật khẩu trước khi lưu (MD5)
                nguoiDung.MatKhau = GetMD5(nguoiDung.MatKhau);

                nguoiDung.TrangThai = true; // Mặc định là Hoạt động
                _context.Add(nguoiDung);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thêm người dùng thành công!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["ChucVuId"] = new SelectList(_context.ChucVus, "Id", "TenChucVu", nguoiDung.ChucVuId);
            ViewData["DonViHcId"] = new SelectList(_context.DonViHcs, "Id", "Ten", nguoiDung.DonViHcId);
            return View(nguoiDung);
        }

        // 3. CHỈNH SỬA (EDIT)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null) return NotFound();

            // Khi sửa, không load mật khẩu cũ ra để bảo mật
            nguoiDung.MatKhau = "";

            ViewData["ChucVuId"] = new SelectList(_context.ChucVus, "Id", "TenChucVu", nguoiDung.ChucVuId);
            ViewData["DonViHcId"] = new SelectList(_context.DonViHcs, "Id", "Ten", nguoiDung.DonViHcId);
            return View(nguoiDung);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NguoiDung nguoiDung, string? NewPassword)
        {
            if (id != nguoiDung.Id) return NotFound();

            // --- KHU VỰC SỬA LỖI ---
            // 1. Bỏ qua lỗi Mật khẩu (vì edit không bắt buộc nhập pass)
            ModelState.Remove("MatKhau");

            // 2. Bỏ qua lỗi Tên đăng nhập (VÌ NÓ BỊ DISABLED TRÊN FORM NÊN KHÔNG GỬI VỀ)
            // Bạn kiểm tra kỹ xem trong Model tên biến là "TenDangNhap" hay "UserName" nhé
            ModelState.Remove("TenDangNhap");

            // 3. Bỏ qua các trường tự sinh khác nếu có (Ngày tạo, Người tạo...)
            ModelState.Remove("NgayTao");
            ModelState.Remove("NguoiTao");
            // -----------------------

            if (ModelState.IsValid)
            {
                try
                {
                    var userToUpdate = await _context.NguoiDungs.FindAsync(id);

                    // Cập nhật các thông tin
                    userToUpdate.HoTen = nguoiDung.HoTen;
                    userToUpdate.Email = nguoiDung.Email;

                    // Cập nhật Chức vụ
                    userToUpdate.ChucVuId = nguoiDung.ChucVuId;

                    userToUpdate.DonViHcId = nguoiDung.DonViHcId;

                    // Chỉ đổi mật khẩu khi có nhập mới
                    if (!string.IsNullOrEmpty(NewPassword))
                    {
                        userToUpdate.MatKhau = GetMD5(NewPassword);
                    }

                    _context.Update(userToUpdate);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Cập nhật thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.NguoiDungs.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
            }

            // --- DEBUG LỖI (NẾU VẪN KHÔNG ĐƯỢC THÌ LÀM BƯỚC NAY) ---
            // Đặt breakpoint tại đây hoặc in lỗi ra để xem nó đang kêu cái gì
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var err in errors)
            {
                Console.WriteLine(err.ErrorMessage); // Xem trong cửa sổ Output
            }

            ViewData["ChucVuId"] = new SelectList(_context.ChucVus, "Id", "TenChucVu", nguoiDung.ChucVuId);
            ViewData["DonViHcId"] = new SelectList(_context.DonViHcs, "Id", "Ten", nguoiDung.DonViHcId);
            return View(nguoiDung);
        }

        // 4. XÓA (DELETE)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung != null)
            {
                // Bước 1: Xóa các quyền liên quan trước (Tránh lỗi Foreign Key)
                var permissions = _context.PhanQuyenNguoiDungs.Where(p => p.NguoiDungId == id);
                _context.PhanQuyenNguoiDungs.RemoveRange(permissions);

                // Bước 2: Xóa khỏi các nhóm
                var groups = _context.ThanhVienNhoms.Where(p => p.NguoiDungId == id);
                _context.ThanhVienNhoms.RemoveRange(groups);

                // Bước 3: Xóa User
                _context.NguoiDungs.Remove(nguoiDung);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa người dùng vĩnh viễn.";
            }
            return RedirectToAction(nameof(Index));
        }

        // 5. KHÓA / MỞ KHÓA TÀI KHOẢN (Nút trạng thái)
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var user = await _context.NguoiDungs.FindAsync(id);
            if (user != null)
            {
                user.TrangThai = !user.TrangThai; // Đảo ngược trạng thái
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã cập nhật trạng thái của {user.TenDn}";
            }
            return RedirectToAction(nameof(Index));
        }

        // Hàm phụ trợ: Mã hóa MD5
        private string GetMD5(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            using (MD5 md5 = MD5.Create())
            {
                byte[] fromData = Encoding.UTF8.GetBytes(str);
                byte[] targetData = md5.ComputeHash(fromData);
                StringBuilder byte2String = new StringBuilder();
                for (int i = 0; i < targetData.Length; i++)
                {
                    byte2String.Append(targetData[i].ToString("x2"));
                }
                return byte2String.ToString();
            }
        }
    }
}