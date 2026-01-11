using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Data;
using QuanLyGiongChanNuoi.Infrastructure.Models;

public class HoaChatCamController : Controller
{
    private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

    public HoaChatCamController(QuanLyGiongVaThucAnChanNuoiContext context)
    {
        _context = context;
    }

    // Index: Danh sách + Tìm kiếm (2.3.10)
    public async Task<IActionResult> Index(string searchString)
    {
        var query = _context.HoaChatCams.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(h => h.TenHoaChat.Contains(searchString)
                                  || h.LyDoCam.Contains(searchString));
        }

        ViewBag.CurrentFilter = searchString;
        return View(await query.ToListAsync());
    }

    // Create
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HoaChatCam model)
    {
        if (ModelState.IsValid)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    // Edit (Sửa + Xóa)
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var model = await _context.HoaChatCams.FindAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, HoaChatCam model)
    {
        if (id != model.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    // Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var model = await _context.HoaChatCams.FindAsync(id);
        if (model != null)
        {
            _context.HoaChatCams.Remove(model);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}