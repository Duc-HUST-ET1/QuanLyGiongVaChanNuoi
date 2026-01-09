using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure;
using QuanLyGiongChanNuoi.Infrastructure.Models;
using QuanLyGiongChanNuoi.Infrastructure.Data;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class MenuController : Controller
    {
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

        public MenuController(QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _context = context;
        }

        // 1. DANH SÁCH MENU
        public async Task<IActionResult> Index()
        {
            // Sắp xếp theo Thứ tự
            var listMenu = await _context.Menus.OrderBy(m => m.ThuTu).ToListAsync();
            return View(listMenu);
        }

        // 2. TẠO MỚI
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Menu menu)
        {
            // Set giá trị mặc định nếu null
            if (menu.HienThi == null) menu.HienThi = true;
            if (menu.TrangThai == false) menu.TrangThai = true; // DB bạn yêu cầu not null
            if (menu.CapDo == 0) menu.CapDo = 1;

            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm Menu thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // 3. CHỈNH SỬA
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null) return NotFound();
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Menu menu)
        {
            if (id != menu.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Menus.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // 4. XÓA
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa Menu!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}