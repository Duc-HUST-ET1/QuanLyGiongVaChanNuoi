using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Data;
using QuanLyGiongChanNuoi.Infrastructure.Models;

public class GiongCamXuatKhauController : Controller
{
    private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

    public GiongCamXuatKhauController(QuanLyGiongVaThucAnChanNuoiContext context)
    {
        _context = context;
    }

    // 1. Index: Danh sách & Tìm kiếm (2.1.14)
    public async Task<IActionResult> Index(string searchString)
    {
        // QUAN TRỌNG: Chỉ lấy dòng có Loại là "Cấm xuất khẩu"
        var query = _context.GiongCanBaoTons
            .Include(g => g.GiongVatNuoi)
            .Where(g => g.Loai == "Cấm xuất khẩu");

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(g => g.GiongVatNuoi.TenGiong.Contains(searchString)
                                  || g.LyDo.Contains(searchString));
        }

        ViewBag.CurrentFilter = searchString;
        return View(await query.ToListAsync());
    }

    // 2. Create: Form Thêm mới
    public IActionResult Create()
    {
        ViewData["GiongId"] = new SelectList(_context.GiongVatNuois, "Id", "TenGiong");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GiongCanBaoTon model)
    {
        // Gán cứng loại là "Cấm xuất khẩu"
        model.Loai = "Cấm xuất khẩu";
        model.TrangThai = true; // Mặc định là đang cấm

        if (!model.NgayBaoTon.HasValue) model.NgayBaoTon = DateTime.Now;

        // Xóa validation
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

    // 3. Edit: Form Sửa
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

        // Đảm bảo loại vẫn là Cấm xuất khẩu
        model.Loai = "Cấm xuất khẩu";

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

    // 4. Delete: Xử lý xóa
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