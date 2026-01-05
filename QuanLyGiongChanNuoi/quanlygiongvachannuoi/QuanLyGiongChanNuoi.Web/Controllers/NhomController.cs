using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure;
using QuanLyGiongChanNuoi.Infrastructure.Models;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
    [Authorize(Roles = "Quản trị viên")] // Chỉ Admin mới được vào
    public class NhomController : Controller
    {
        // Nhớ thay đúng tên Context bạn đang dùng (QuanLyGiongVaThucAnChanNuoiAContext)
        private readonly QuanLyGiongVaThucAnChanNuoiAContext _context;

        public NhomController(QuanLyGiongVaThucAnChanNuoiAContext context)
        {
            _context = context;
        }

        // 1. DANH SÁCH NHÓM
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nhoms.ToListAsync());
        }

        // 2. TẠO NHÓM MỚI (Create)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nhom nhom)
        {
            if (ModelState.IsValid)
            {
                // Tự động gán ngày tạo là thời điểm hiện tại
                nhom.NgayTao = DateTime.Now;

                _context.Add(nhom);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Tạo nhóm mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(nhom);
        }

        // 3. CHỈNH SỬA NHÓM (Edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var nhom = await _context.Nhoms.FindAsync(id);
            if (nhom == null) return NotFound();

            return View(nhom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Nhom nhom)
        {
            if (id != nhom.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Chỉ cập nhật Tên và Mô tả, giữ nguyên Ngày tạo
                    var nhomCu = await _context.Nhoms.FindAsync(id);
                    if (nhomCu != null)
                    {
                        nhomCu.Ten = nhom.Ten;
                        nhomCu.Mota = nhom.Mota;

                        _context.Update(nhomCu);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Cập nhật thông tin nhóm thành công!";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Nhoms.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nhom);
        }

        // 4. XÓA NHÓM (Delete) - Quan trọng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var nhom = await _context.Nhoms.FindAsync(id);
            if (nhom != null)
            {
                // BƯỚC 1: Xóa các quyền đã cấp cho nhóm này (Bảng PhanQuyenNhom)
                var quyenCuaNhom = _context.PhanQuyenNhoms.Where(pq => pq.NhomId == id);
                _context.PhanQuyenNhoms.RemoveRange(quyenCuaNhom);

                // BƯỚC 2: Xóa các thành viên khỏi nhóm (Bảng ThanhVienNhom)
                var thanhVien = _context.ThanhVienNhoms.Where(tv => tv.NhomId == id);
                _context.ThanhVienNhoms.RemoveRange(thanhVien);

                // BƯỚC 3: Xóa nhóm
                _context.Nhoms.Remove(nhom);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Đã xóa nhóm thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}