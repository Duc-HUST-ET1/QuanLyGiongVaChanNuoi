using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Models;
using QuanLyGiongChanNuoi.Infrastructure.Data;// Sửa namespace cho đúng project bạn

public class CoSoKhaoNghiemController : Controller
{
    private readonly QuanLyGiongVaThucAnChanNuoiContext _context; // Tên Context lấy từ file Program.cs hoặc data của bạn

    public CoSoKhaoNghiemController(QuanLyGiongVaThucAnChanNuoiContext context)
    {
        _context = context;
    }

    // 2.1.9 & 2.1.10: Danh sách và Tìm kiếm
    public async Task<IActionResult> Index(string searchString)
    {
        // 1. Chỉ lấy những cơ sở có LoaiCoSo là "Khảo nghiệm"
        var query = _context.CoSoVatNuois
            .Include(c => c.ToChucCaNhan) // Load tên tổ chức
            .Include(c => c.GiongVatNuoi) // Load tên giống
            .Where(c => c.LoaiCoSo == "Khảo nghiệm"); // <--- QUAN TRỌNG NHẤT

        // 2. Xử lý tìm kiếm (Mục 2.1.10)
        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(c => c.TenCoSo.Contains(searchString)
                                  || c.DiaChi.Contains(searchString));
        }

        ViewBag.CurrentFilter = searchString;
        return View(await query.ToListAsync());
    }

    // GET: Create
    public IActionResult Create()
    {
        // Tạo dropdown chọn Tổ chức và Giống
        ViewData["ToChucCaNhanID"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten");
        ViewData["GiongVatNuoiID"] = new SelectList(_context.GiongVatNuois, "Id", "TenGiong");
        return View();
    }

    // POST: Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CoSoVatNuoi coSoVatNuoi)
    {
        // 1. Gán giá trị mặc định
        coSoVatNuoi.LoaiCoSo = "Khảo nghiệm";
        coSoVatNuoi.TrangThai = true;

        // --- THÊM ĐOẠN NÀY ---
        // Vì trên Form không có ô nhập cho LoaiCoSo và TrangThai, 
        // nên hệ thống sẽ báo lỗi "Required". Ta cần xóa lỗi đó đi.
        ModelState.Remove("LoaiCoSo");
        ModelState.Remove("TrangThai");

        // Xóa validation cho các object quan hệ (tránh lỗi null object)
        ModelState.Remove("ToChucCaNhan");
        ModelState.Remove("GiongVatNuoi");
        // ---------------------

        if (ModelState.IsValid)
        {
            _context.Add(coSoVatNuoi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Nếu vẫn lỗi, code sẽ chạy xuống đây và load lại trang cũ
        ViewData["ToChucCaNhanID"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten", coSoVatNuoi.ToChucCaNhanID);
        ViewData["GiongVatNuoiID"] = new SelectList(_context.GiongVatNuois, "Id", "TenGiong", coSoVatNuoi.GiongVatNuoiID);
        return View(coSoVatNuoi);
    }

    // GET: Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var coSoVatNuoi = await _context.CoSoVatNuois.FindAsync(id);
        if (coSoVatNuoi == null) return NotFound();

        ViewData["ToChucCaNhanID"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten", coSoVatNuoi.ToChucCaNhanID);
        ViewData["GiongVatNuoiID"] = new SelectList(_context.GiongVatNuois, "Id", "TenGiong", coSoVatNuoi.GiongVatNuoiID);
        return View(coSoVatNuoi);
    }

    // POST: Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CoSoVatNuoi coSoVatNuoi)
    {
        if (id != coSoVatNuoi.Id) return NotFound();

        // Giữ nguyên loại là Khảo nghiệm
        coSoVatNuoi.LoaiCoSo = "Khảo nghiệm";

        if (ModelState.IsValid)
        {
            _context.Update(coSoVatNuoi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ToChucCaNhanID"] = new SelectList(_context.ToChucCaNhans, "Id", "Ten", coSoVatNuoi.ToChucCaNhanID);
        ViewData["GiongVatNuoiID"] = new SelectList(_context.GiongVatNuois, "Id", "TenGiong", coSoVatNuoi.GiongVatNuoiID);
        return View(coSoVatNuoi);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var coSoVatNuoi = await _context.CoSoVatNuois.FindAsync(id);
        if (coSoVatNuoi != null)
        {
            _context.CoSoVatNuois.Remove(coSoVatNuoi);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}