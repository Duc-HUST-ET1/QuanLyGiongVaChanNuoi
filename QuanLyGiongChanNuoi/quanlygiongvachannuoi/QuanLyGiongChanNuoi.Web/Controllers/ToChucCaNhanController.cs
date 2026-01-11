using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Models; // Namespace model của bạn
using System.Linq;
using System.Threading.Tasks;
using QuanLyGiongChanNuoi.Infrastructure.Data; // Sửa namespace cho đúng project bạn

namespace QuanLyGiongChanNuoi.Controllers
{
    public class ToChucCaNhanController : Controller
    {
        // Thay YourDbContext bằng tên class DbContext thực tế của bạn
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

        public ToChucCaNhanController(QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _context = context;
        }

        // 1. GET: Danh sách & Tìm kiếm (Index)
        public async Task<IActionResult> Index(string searchString, string searchLoaiHoatDong)
        {
            var query = _context.ToChucCaNhans.AsQueryable();

            // Lọc theo từ khóa (Tên hoặc SĐT hoặc Email)
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(t => t.Ten.Contains(searchString)
                                      || t.SoDienThoai.Contains(searchString)
                                      || t.Email.Contains(searchString));
            }

            // Lọc theo Loại hoạt động (Dropdown)
            if (!string.IsNullOrEmpty(searchLoaiHoatDong))
            {
                query = query.Where(t => t.LoaiHoatDong == searchLoaiHoatDong);
            }

            // Lưu giá trị search để hiển thị lại trên View
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentLoai = searchLoaiHoatDong;

            return View(await query.ToListAsync());
        }

        // 2. GET: Form Thêm mới
        public IActionResult Create()
        {
            return View();
        }

        // 3. POST: Xử lý Thêm mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToChucCaNhan toChucCaNhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toChucCaNhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toChucCaNhan);
        }

        // 4. GET: Form Chỉnh sửa
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var toChucCaNhan = await _context.ToChucCaNhans.FindAsync(id);
            if (toChucCaNhan == null) return NotFound();

            return View(toChucCaNhan);
        }

        // 5. POST: Xử lý Lưu chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToChucCaNhan toChucCaNhan)
        {
            if (id != toChucCaNhan.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toChucCaNhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ToChucCaNhans.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(toChucCaNhan);
        }

        // 6. GET: Xác nhận xóa
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var toChucCaNhan = await _context.ToChucCaNhans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toChucCaNhan == null) return NotFound();

            return View(toChucCaNhan);
        }

        // 7. POST: Thực hiện xóa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toChucCaNhan = await _context.ToChucCaNhans.FindAsync(id);
            if (toChucCaNhan != null)
            {
                _context.ToChucCaNhans.Remove(toChucCaNhan);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}