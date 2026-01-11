using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Data;
using QuanLyGiongChanNuoi.Infrastructure.Models;

public class GiayChungNhanController : Controller
{
    private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

    public GiayChungNhanController(QuanLyGiongVaThucAnChanNuoiContext context)
    {
        _context = context;
    }

    // 1. Xem danh sách Giấy chứng nhận của 1 Cơ sở cụ thể
    public async Task<IActionResult> Index(int coSoId)
    {
        // Lấy thông tin cơ sở để hiển thị tiêu đề
        var coSo = await _context.CoSoThucAns.FindAsync(coSoId);
        if (coSo == null) return NotFound();

        ViewBag.TenCoSo = coSo.TenCoSo;
        ViewBag.CoSoId = coSoId; // Lưu lại ID để dùng cho nút Thêm mới
        ViewBag.LoaiHinh = coSo.LoaiCoSo; // Để nút quay lại biết về đâu

        // Lấy danh sách giấy chứng nhận của cơ sở này
        var listGCN = await _context.GiayChungNhans
            .Where(g => g.CoSoThucAnId == coSoId)
            .OrderByDescending(g => g.NgayCap)
            .ToListAsync();

        return View(listGCN);
    }

    // 2. Cấp giấy mới (Create)
    public async Task<IActionResult> Create(int coSoId)
    {
        var coSo = await _context.CoSoThucAns.FindAsync(coSoId);
        if (coSo == null) return NotFound();

        ViewBag.TenCoSo = coSo.TenCoSo;

        // Tạo model mới và gán sẵn ID cơ sở
        var model = new GiayChungNhan { CoSoThucAnId = coSoId, NgayCap = DateTime.Now };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GiayChungNhan model)
    {
        // Xóa validation object quan hệ
        ModelState.Remove("CoSoThucAn");

        if (ModelState.IsValid)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            // Lưu xong quay lại danh sách GCN của cơ sở đó
            return RedirectToAction(nameof(Index), new { coSoId = model.CoSoThucAnId });
        }

        // Nếu lỗi, load lại tên cơ sở để hiển thị
        var coSo = await _context.CoSoThucAns.FindAsync(model.CoSoThucAnId);
        ViewBag.TenCoSo = coSo?.TenCoSo;

        return View(model);
    }

    // 3. Chỉnh sửa giấy (Edit)
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var model = await _context.GiayChungNhans
            .Include(g => g.CoSoThucAn) // Load kèm cơ sở để lấy tên
            .FirstOrDefaultAsync(g => g.Id == id);

        if (model == null) return NotFound();

        ViewBag.TenCoSo = model.CoSoThucAn?.TenCoSo;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, GiayChungNhan model)
    {
        if (id != model.Id) return NotFound();

        ModelState.Remove("CoSoThucAn");

        if (ModelState.IsValid)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { coSoId = model.CoSoThucAnId });
        }

        var coSo = await _context.CoSoThucAns.FindAsync(model.CoSoThucAnId);
        ViewBag.TenCoSo = coSo?.TenCoSo;
        return View(model);
    }

    // 4. Xóa giấy
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var model = await _context.GiayChungNhans.FindAsync(id);
        if (model != null)
        {
            int coSoId = model.CoSoThucAnId; // Lưu lại ID để quay về
            _context.GiayChungNhans.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { coSoId = coSoId });
        }
        return RedirectToAction("Index", "CoSoThucAn");
    }
}