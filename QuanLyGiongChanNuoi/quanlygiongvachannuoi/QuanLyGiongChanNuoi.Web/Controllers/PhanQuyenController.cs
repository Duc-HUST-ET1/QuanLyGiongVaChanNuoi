using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure;
using QuanLyGiongChanNuoi.Infrastructure.Models;
using QuanLyGiongChanNuoi.Web.Models;
using QuanLyGiongChanNuoi.Infrastructure.Data;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class PhanQuyenController : Controller
    {
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

        public PhanQuyenController(QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _context = context;
        }

        // Danh sách quyền
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhanQuyens.ToListAsync());
        }

        // Tạo quyền mới
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhanQuyen model)
        {
            if (await _context.PhanQuyens.AnyAsync(p => p.MaQuyen == model.MaQuyen))
            {
                ModelState.AddModelError("MaQuyen", "Mã quyền này đã tồn tại!");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // --- PHẦN 1: PHÂN QUYỀN CHO NHÓM ---

        [HttpGet]
        public async Task<IActionResult> PhanQuyenChoNhom(int nhomId)
        {
            var nhom = await _context.Nhoms.FindAsync(nhomId);
            if (nhom == null) return NotFound();

            ViewData["TenDoituong"] = nhom.Ten;
            ViewData["IdDoiTuong"] = nhomId;

            var allPermissions = await _context.PhanQuyens.ToListAsync();
            var currentPermissions = await _context.PhanQuyenNhoms
                .Where(x => x.NhomId == nhomId)
                .Select(x => x.MaQuyen)
                .ToListAsync();

            var model = allPermissions.Select(p => new PhanQuyenViewModel
            {
                MaQuyen = p.MaQuyen,
                TenQuyen = p.TenQuyen,
                Mota = p.Mota, // Đã sửa MoTa thành Mota cho khớp Model
                CoQuyen = currentPermissions.Contains(p.MaQuyen)
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LuuPhanQuyenNhom(int nhomId, List<PhanQuyenViewModel> model)
        {
            var oldPerms = _context.PhanQuyenNhoms.Where(x => x.NhomId == nhomId);
            _context.PhanQuyenNhoms.RemoveRange(oldPerms);

            foreach (var item in model)
            {
                if (item.CoQuyen)
                {
                    _context.PhanQuyenNhoms.Add(new PhanQuyenNhom
                    {
                        NhomId = nhomId,
                        MaQuyen = item.MaQuyen
                    });
                }
            }
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cập nhật quyền cho nhóm thành công!";
            return RedirectToAction("Index", "Nhom");
        }


        // --- PHẦN 2: TRA CỨU QUYỀN NGƯỜI DÙNG (TỔNG HỢP) ---
        [HttpGet]
        public async Task<IActionResult> TraCuuQuyenUser(int userId)
        {
            var user = await _context.NguoiDungs.FindAsync(userId);
            if (user == null) return NotFound();
            ViewData["TenUser"] = user.TenDn;

            var ketQua = new List<KetQuaPhanQuyenViewModel>();

            // A. Quyền trực tiếp
            var quyenTrucTiep = await _context.PhanQuyenNguoiDungs
                .Where(x => x.NguoiDungId == userId)
                .Include(x => x.MaQuyenNavigation)
                .Select(x => new KetQuaPhanQuyenViewModel
                {
                    MaQuyen = x.MaQuyen,
                    TenQuyen = x.MaQuyenNavigation.TenQuyen,
                    NguonQuyen = "Riêng tư (Trực tiếp)"
                }).ToListAsync();
            ketQua.AddRange(quyenTrucTiep);

            // B. Quyền từ nhóm
            var cacNhomCuaUser = await _context.ThanhVienNhoms
                .Where(tv => tv.NguoiDungId == userId)
                .Include(tv => tv.Nhom)
                .ToListAsync();

            foreach (var tv in cacNhomCuaUser)
            {
                var quyenCuaNhom = await _context.PhanQuyenNhoms
                    .Where(pq => pq.NhomId == tv.NhomId)
                    .Include(pq => pq.MaQuyenNavigation)
                    .Select(x => new KetQuaPhanQuyenViewModel
                    {
                        MaQuyen = x.MaQuyen,
                        TenQuyen = x.MaQuyenNavigation.TenQuyen,
                        NguonQuyen = $"Nhóm: {tv.Nhom.Ten}"
                    }).ToListAsync();

                ketQua.AddRange(quyenCuaNhom);
            }

            return View(ketQua.OrderBy(x => x.MaQuyen).ToList());
        }

        // --- PHẦN 3: PHÂN QUYỀN CHO NGƯỜI DÙNG RIÊNG LẺ (MỚI THÊM) ---
        // Đặt bên trong Class PhanQuyenController

        [HttpGet]
        public async Task<IActionResult> PhanQuyenChoNguoiDung(int nguoiDungId)
        {
            var user = await _context.NguoiDungs.FindAsync(nguoiDungId);
            if (user == null) return NotFound();

            var allPermissions = await _context.PhanQuyens.ToListAsync();
            var userPermissions = await _context.PhanQuyenNguoiDungs
                                                .Where(p => p.NguoiDungId == nguoiDungId)
                                                .Select(p => p.MaQuyen)
                                                .ToListAsync();

            var model = new List<PhanQuyenViewModel>();
            foreach (var p in allPermissions)
            {
                model.Add(new PhanQuyenViewModel
                {
                    MaQuyen = p.MaQuyen,
                    TenQuyen = p.TenQuyen,
                    Mota = p.Mota,
                    CoQuyen = userPermissions.Contains(p.MaQuyen)
                });
            }

            ViewBag.NguoiDungId = nguoiDungId;
            ViewBag.TenNguoiDung = user.TenDn;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LuuPhanQuyenNguoiDung(int nguoiDungId, List<PhanQuyenViewModel> model)
        {
            var oldPerms = _context.PhanQuyenNguoiDungs.Where(p => p.NguoiDungId == nguoiDungId);
            _context.PhanQuyenNguoiDungs.RemoveRange(oldPerms);

            foreach (var item in model)
            {
                if (item.CoQuyen)
                {
                    _context.PhanQuyenNguoiDungs.Add(new PhanQuyenNguoiDung
                    {
                        NguoiDungId = nguoiDungId,
                        MaQuyen = item.MaQuyen
                    });
                }
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Đã cập nhật quyền riêng cho người dùng thành công!";
            return RedirectToAction("Index", "NguoiDung");
        }

    } // <--- DẤU ĐÓNG CỦA CLASS (Line ~200)
} // <--- DẤU ĐÓNG CỦA NAMESPACE (Line ~201)