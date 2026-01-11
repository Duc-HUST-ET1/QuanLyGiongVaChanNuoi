using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Data;
using QuanLyGiongChanNuoi.Infrastructure.Models;

public class CoSoThucAnController : Controller
{
    private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

    public CoSoThucAnController(QuanLyGiongVaThucAnChanNuoiContext context)
    {
        _context = context;
    }

    // 1. Index: Hiển thị danh sách
    public async Task<IActionResult> Index(string loaiHinh, string searchString)
    {
        if (string.IsNullOrEmpty(loaiHinh)) loaiHinh = "Sản xuất"; // Mặc định

        var query = _context.CoSoThucAns
            .Include(c => c.ToChucCaNhan)
            .Include(c => c.ThucAnChanNuoi)
            .Include(c => c.GiayChungNhans) // Load giấy chứng nhận để check (2.3.3)
            .Where(c => c.LoaiCoSo == loaiHinh);

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(c => c.TenCoSo.Contains(searchString)
                                  || c.DiaChi.Contains(searchString)
                                  || c.ToChucCaNhan.Ten.Contains(searchString));
        }

        ViewBag.CurrentFilter = searchString;
        ViewBag.LoaiHinh = loaiHinh;
        return View(await query.ToListAsync());
    }

    // 2. Create
    public IActionResult Create(string loaiHinh)
    {
        ViewData["ToChucCaNhanId"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten");
        // Giả sử ThucAnChanNuoi là bảng danh mục sản phẩm/loại thức ăn
        ViewData["ThucAnChanNuoiId"] = new SelectList(_context.ThucAnChanNuois, "Id", "TenThucAn");
        ViewBag.LoaiHinh = loaiHinh ?? "Sản xuất";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CoSoThucAn model)
    {
        model.NgayCapNhat = DateTime.Now;

        ModelState.Remove("ToChucCaNhan");
        ModelState.Remove("ThucAnChanNuoi");
        ModelState.Remove("GiayChungNhans"); // Không bắt buộc nhập giấy ngay khi tạo cơ sở

        if (ModelState.IsValid)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { loaiHinh = model.LoaiCoSo });
        }

        ViewData["ToChucCaNhanId"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten", model.ToChucCaNhanId);
        ViewData["ThucAnChanNuoiId"] = new SelectList(_context.ThucAnChanNuois, "Id", "TenThucAn", model.ThucAnChanNuoiId);
        ViewBag.LoaiHinh = model.LoaiCoSo;
        return View(model);
    }

    // 3. Edit (Sửa + Xóa)
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var model = await _context.CoSoThucAns.FindAsync(id);
        if (model == null) return NotFound();

        ViewData["ToChucCaNhanId"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten", model.ToChucCaNhanId);
        ViewData["ThucAnChanNuoiId"] = new SelectList(_context.ThucAnChanNuois, "Id", "TenThucAn", model.ThucAnChanNuoiId);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CoSoThucAn model)
    {
        if (id != model.Id) return NotFound();

        model.NgayCapNhat = DateTime.Now;
        ModelState.Remove("ToChucCaNhan");
        ModelState.Remove("ThucAnChanNuoi");
        ModelState.Remove("GiayChungNhans");

        if (ModelState.IsValid)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { loaiHinh = model.LoaiCoSo });
        }

        ViewData["ToChucCaNhanId"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten", model.ToChucCaNhanId);
        ViewData["ThucAnChanNuoiId"] = new SelectList(_context.ThucAnChanNuois, "Id", "TenThucAn", model.ThucAnChanNuoiId);
        return View(model);
    }

    // 4. Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var model = await _context.CoSoThucAns.FindAsync(id);
        if (model != null)
        {
            string loai = model.LoaiCoSo;
            _context.CoSoThucAns.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { loaiHinh = loai });
        }
        return RedirectToAction(nameof(Index));
    }
}