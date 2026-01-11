using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Data;
using QuanLyGiongChanNuoi.Infrastructure.Models;

public class ToChucNguonGenController : Controller
{
    private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

    public ToChucNguonGenController(QuanLyGiongVaThucAnChanNuoiContext context)
    {
        _context = context;
    }

    // 1. Index: Hiển thị danh sách theo Loại hoạt động
    public async Task<IActionResult> Index(string loaiHoatDong, string searchString)
    {
        // Mặc định nếu không truyền gì thì coi là "Thu thập"
        if (string.IsNullOrEmpty(loaiHoatDong)) loaiHoatDong = "Thu thập";

        // Lọc dữ liệu theo Hoạt động và Load kèm tên Tổ chức + tên Nguồn gen
        var query = _context.ToChucNguonGens
            .Include(t => t.ToChucCaNhan)
            .Include(t => t.NguonGen)
            .Where(t => t.HoatDong == loaiHoatDong);

        // Tìm kiếm (2.2.2, 2.2.4, 2.2.6)
        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(t => t.ToChucCaNhan.Ten.Contains(searchString)
                                  || t.NguonGen.TenNguonGen.Contains(searchString)
                                  || t.KhuVuc.Contains(searchString));
        }

        // Truyền các biến cần thiết ra View để giữ trạng thái
        ViewBag.CurrentFilter = searchString;
        ViewBag.LoaiHoatDong = loaiHoatDong;

        return View(await query.ToListAsync());
    }

    // 2. Create: Form thêm mới
    public IActionResult Create(string loaiHoatDong)
    {
        ViewData["ToChucCaNhanId"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten");
        ViewData["NguonGenId"] = new SelectList(_context.NguonGens, "Id", "TenNguonGen");

        // Truyền loại hoạt động xuống View để gán vào input hidden
        ViewBag.LoaiHoatDong = loaiHoatDong ?? "Thu thập";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ToChucNguonGen model)
    {
        // Xóa validation cho các object quan hệ
        ModelState.Remove("ToChucCaNhan");
        ModelState.Remove("NguonGen");

        if (ModelState.IsValid)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            // Lưu xong quay lại đúng trang Index của loại hoạt động đó
            return RedirectToAction(nameof(Index), new { loaiHoatDong = model.HoatDong });
        }

        ViewData["ToChucCaNhanId"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten", model.ToChucCaNhanId);
        ViewData["NguonGenId"] = new SelectList(_context.NguonGens, "Id", "TenNguonGen", model.NguonGenId);
        ViewBag.LoaiHoatDong = model.HoatDong;
        return View(model);
    }

    // 3. Edit: Sửa
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var model = await _context.ToChucNguonGens.FindAsync(id);
        if (model == null) return NotFound();

        ViewData["ToChucCaNhanId"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten", model.ToChucCaNhanId);
        ViewData["NguonGenId"] = new SelectList(_context.NguonGens, "Id", "TenNguonGen", model.NguonGenId);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ToChucNguonGen model)
    {
        if (id != model.Id) return NotFound();

        ModelState.Remove("ToChucCaNhan");
        ModelState.Remove("NguonGen");

        if (ModelState.IsValid)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { loaiHoatDong = model.HoatDong });
        }

        ViewData["ToChucCaNhanId"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten", model.ToChucCaNhanId);
        ViewData["NguonGenId"] = new SelectList(_context.NguonGens, "Id", "TenNguonGen", model.NguonGenId);
        return View(model);
    }

    // 4. Delete: Xóa
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var model = await _context.ToChucNguonGens.FindAsync(id);
        if (model != null)
        {
            string currentHoatDong = model.HoatDong; // Lưu lại để redirect đúng chỗ
            _context.ToChucNguonGens.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { loaiHoatDong = currentHoatDong });
        }
        return RedirectToAction(nameof(Index));
    }
}