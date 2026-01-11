using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Data;
using QuanLyGiongChanNuoi.Infrastructure.Models;

public class GiongCanBaoTonController : Controller
{
    private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

    public GiongCanBaoTonController(QuanLyGiongVaThucAnChanNuoiContext context)
    {
        _context = context;
    }

    // 1. Index: Danh sách & Tìm kiếm (2.1.12)
    public async Task<IActionResult> Index(string searchString)
    {
        // Chỉ lấy loại "Bảo tồn"
        var query = _context.GiongCanBaoTons
            .Include(g => g.GiongVatNuoi)
            .Where(g => g.Loai == "Bảo tồn");

        if (!string.IsNullOrEmpty(searchString))
        {
            // Tìm theo tên giống (phải trỏ vào bảng GiongVatNuoi) hoặc lý do
            query = query.Where(g => g.GiongVatNuoi.TenGiong.Contains(searchString)
                                  || g.LyDo.Contains(searchString));
        }

        ViewBag.CurrentFilter = searchString;
        return View(await query.ToListAsync());
    }

    // 2. Create: Form Thêm mới
    public IActionResult Create()
    {
        // Dropdown chọn giống từ bảng GiongVatNuoi
        ViewData["GiongId"] = new SelectList(_context.GiongVatNuois, "Id", "TenGiong");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GiongCanBaoTon model)
    {
        // Tự động gán
        model.Loai = "Bảo tồn";
        model.TrangThai = true;
        if (!model.NgayBaoTon.HasValue) model.NgayBaoTon = DateTime.Now;

        // Xóa validation không cần thiết
        ModelState.Remove("Loai");
        ModelState.Remove("TrangThai");
        ModelState.Remove("GiongVatNuoi");

        if (ModelState.IsValid)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["GiongId"] = new SelectList(_context.GiongVatNuois, "Id", "TenGiong", model.GiongId);
        return View(model);
    }

    // 3. Edit: Form Sửa (Có nút Xóa)
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var model = await _context.GiongCanBaoTons.FindAsync(id);
        if (model == null) return NotFound();

        ViewData["GiongId"] = new SelectList(_context.GiongVatNuois, "Id", "TenGiong", model.GiongId);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, GiongCanBaoTon model)
    {
        if (id != model.Id) return NotFound();

        // Giữ nguyên loại cũ là Bảo tồn
        model.Loai = "Bảo tồn";
        ModelState.Remove("Loai");
        ModelState.Remove("GiongVatNuoi");

        if (ModelState.IsValid)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["GiongId"] = new SelectList(_context.GiongVatNuois, "Id", "TenGiong", model.GiongId);
        return View(model);
    }

    // 4. Delete: Xử lý xóa (để gọi từ nút trong file Edit)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var model = await _context.GiongCanBaoTons.FindAsync(id);
        if (model != null)
        {
            _context.GiongCanBaoTons.Remove(model);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}