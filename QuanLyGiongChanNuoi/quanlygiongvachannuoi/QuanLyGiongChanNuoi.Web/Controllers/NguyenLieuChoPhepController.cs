using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Data;
using QuanLyGiongChanNuoi.Infrastructure.Models;

public class NguyenLieuChoPhepController : Controller
{
    private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

    public NguyenLieuChoPhepController(QuanLyGiongVaThucAnChanNuoiContext context)
    {
        _context = context;
    }

    // Index + Tìm kiếm (2.3.12)
    public async Task<IActionResult> Index(string searchString)
    {
        var query = _context.NguyenLieuChoPheps.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(n => n.TenNguyenLieu.Contains(searchString)
                                  || n.MoTa.Contains(searchString));
        }

        ViewBag.CurrentFilter = searchString;
        return View(await query.ToListAsync());
    }

    // Create
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NguyenLieuChoPhep model)
    {
        if (ModelState.IsValid)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    // Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var model = await _context.NguyenLieuChoPheps.FindAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NguyenLieuChoPhep model)
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
        var model = await _context.NguyenLieuChoPheps.FindAsync(id);
        if (model != null)
        {
            _context.NguyenLieuChoPheps.Remove(model);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}