using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure;
using QuanLyGiongChanNuoi.Infrastructure.Models;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class QuanLyQuyenController : Controller
    {
        private readonly QuanLyGiongVaThucAnChanNuoiAContext _context;

        public QuanLyQuyenController(QuanLyGiongVaThucAnChanNuoiAContext context)
        {
            _context = context;
        }

        // 1. DANH SÁCH QUYỀN
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhanQuyens.OrderBy(p => p.MaQuyen).ToListAsync());
        }

        // 2. THÊM QUYỀN MỚI
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhanQuyen model)
        {
            // Kiểm tra trùng mã quyền (Vì mã quyền là Khóa chính)
            if (await _context.PhanQuyens.AnyAsync(x => x.MaQuyen == model.MaQuyen))
            {
                ModelState.AddModelError("MaQuyen", "Mã quyền này đã tồn tại, vui lòng chọn mã khác.");
            }

            if (ModelState.IsValid)
            {
                // Viết hoa mã quyền cho chuẩn (VD: user_add -> USER_ADD)
                model.MaQuyen = model.MaQuyen.ToUpper();

                _context.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã thêm quyền mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // 3. SỬA QUYỀN
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var quyen = await _context.PhanQuyens.FindAsync(id);
            if (quyen == null) return NotFound();

            return View(quyen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PhanQuyen model)
        {
            if (id != model.MaQuyen) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var quyenCu = await _context.PhanQuyens.FindAsync(id);
                    if (quyenCu != null)
                    {
                        quyenCu.TenQuyen = model.TenQuyen;
                        quyenCu.Mota = model.Mota;
                        // Lưu ý: Không cho sửa MaQuyen vì nó là Khóa chính

                        _context.Update(quyenCu);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Cập nhật quyền thành công!";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PhanQuyens.Any(e => e.MaQuyen == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // 4. XÓA QUYỀN
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var quyen = await _context.PhanQuyens.FindAsync(id);
            if (quyen != null)
            {
                // KIỂM TRA RÀNG BUỘC:
                // Nếu quyền này đang được cấp cho Nhóm hoặc Người dùng nào đó thì KHÔNG ĐƯỢC XÓA
                bool dangSuDung = await _context.PhanQuyenNhoms.AnyAsync(x => x.MaQuyen == id)
                               || await _context.PhanQuyenNguoiDungs.AnyAsync(x => x.MaQuyen == id);

                if (dangSuDung)
                {
                    TempData["ErrorMessage"] = "Không thể xóa quyền này vì đang được sử dụng (Cấp cho nhóm hoặc user).";
                    return RedirectToAction(nameof(Index));
                }

                _context.PhanQuyens.Remove(quyen);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa quyền thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}