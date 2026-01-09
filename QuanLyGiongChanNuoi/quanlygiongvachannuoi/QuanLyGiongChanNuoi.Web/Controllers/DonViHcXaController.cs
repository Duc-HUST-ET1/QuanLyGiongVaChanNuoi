using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure;
using QuanLyGiongChanNuoi.Infrastructure.Models;
using QuanLyGiongChanNuoi.Infrastructure.Data;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
    // --- ĐÂY LÀ DÒNG BẢO VỆ CHỈ ADMIN MỚI VÀO ĐƯỢC ---
    [Authorize(Roles = "Quản trị viên")]
    public class DonViHcXaController : Controller
    {
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

        public DonViHcXaController(QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _context = context;
        }

        // 1. DANH SÁCH & TÌM KIẾM (Option 1.4)
        public async Task<IActionResult> Index(string searchString)
        {
            // Lọc ra các đơn vị là Xã/Phường/Thị trấn
            var query = _context.DonViHcs
                .Include(d => d.CapHc)
                .Include(d => d.TrucThuocNavigation) // Lấy Huyện trực thuộc
                .Where(d => d.CapHc.Ten.Contains("Xã") || d.CapHc.Ten.Contains("Phường") || d.CapHc.Ten.Contains("Thị trấn"));

            if (!string.IsNullOrEmpty(searchString))
            {
                // Tìm theo tên Xã hoặc tên Huyện trực thuộc
                query = query.Where(d => d.Ten.Contains(searchString) ||
                                         d.MaBuuDien.Contains(searchString) ||
                                         d.TrucThuocNavigation.Ten.Contains(searchString));
                ViewData["CurrentFilter"] = searchString;
            }

            return View(await query.OrderBy(d => d.TrucThuocNavigation.Ten).ThenBy(d => d.Ten).ToListAsync());
        }

        // 2. TẠO MỚI
        [HttpGet]
        public IActionResult Create()
        {
            // A. Load danh sách loại đơn vị (Xã/Phường...)
            var capXa = _context.CapHcs.Where(c => c.Ten.Contains("Xã") || c.Ten.Contains("Phường") || c.Ten.Contains("Thị trấn"));
            ViewData["CapHcId"] = new SelectList(capXa, "Id", "Ten");

            // B. Load danh sách Huyện (Cha của Xã)
            // Lưu ý: ID=2 thường là cấp Huyện. Nếu DB bạn khác thì sửa số này.
            int idCapHuyen = 2;
            var dsHuyen = _context.DonViHcs.Where(x => x.CapHcId == idCapHuyen).OrderBy(x => x.Ten);

            ViewData["TrucThuoc"] = new SelectList(dsHuyen, "Id", "Ten");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DonViHc donViHc)
        {
            // Bỏ qua lỗi validation của các bảng quan hệ
            ModelState.Remove("CapHc");
            ModelState.Remove("TrucThuocNavigation");
            ModelState.Remove("InverseTrucThuocNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(donViHc);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm mới Xã/Phường thành công!";
                return RedirectToAction(nameof(Index));
            }

            // Load lại nếu lỗi
            var capXa = _context.CapHcs.Where(c => c.Ten.Contains("Xã") || c.Ten.Contains("Phường") || c.Ten.Contains("Thị trấn"));
            ViewData["CapHcId"] = new SelectList(capXa, "Id", "Ten", donViHc.CapHcId);

            int idCapHuyen = 2;
            var dsHuyen = _context.DonViHcs.Where(x => x.CapHcId == idCapHuyen).OrderBy(x => x.Ten);
            ViewData["TrucThuoc"] = new SelectList(dsHuyen, "Id", "Ten", donViHc.TrucThuoc);

            return View(donViHc);
        }

        // 3. CHỈNH SỬA
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var donViHc = await _context.DonViHcs.FindAsync(id);
            if (donViHc == null) return NotFound();

            var capXa = _context.CapHcs.Where(c => c.Ten.Contains("Xã") || c.Ten.Contains("Phường") || c.Ten.Contains("Thị trấn"));
            ViewData["CapHcId"] = new SelectList(capXa, "Id", "Ten", donViHc.CapHcId);

            int idCapHuyen = 2;
            var dsHuyen = _context.DonViHcs.Where(x => x.CapHcId == idCapHuyen).OrderBy(x => x.Ten);
            ViewData["TrucThuoc"] = new SelectList(dsHuyen, "Id", "Ten", donViHc.TrucThuoc);

            return View(donViHc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DonViHc donViHc)
        {
            if (id != donViHc.Id) return NotFound();

            // Bỏ qua lỗi validation
            ModelState.Remove("CapHc");
            ModelState.Remove("TrucThuocNavigation");
            ModelState.Remove("InverseTrucThuocNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donViHc);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DonViHcs.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var capXa = _context.CapHcs.Where(c => c.Ten.Contains("Xã") || c.Ten.Contains("Phường") || c.Ten.Contains("Thị trấn"));
            ViewData["CapHcId"] = new SelectList(capXa, "Id", "Ten", donViHc.CapHcId);

            int idCapHuyen = 2;
            var dsHuyen = _context.DonViHcs.Where(x => x.CapHcId == idCapHuyen).OrderBy(x => x.Ten);
            ViewData["TrucThuoc"] = new SelectList(dsHuyen, "Id", "Ten", donViHc.TrucThuoc);

            return View(donViHc);
        }

        // 4. XÓA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var donViHc = await _context.DonViHcs.FindAsync(id);
            if (donViHc != null)
            {
                _context.DonViHcs.Remove(donViHc);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}