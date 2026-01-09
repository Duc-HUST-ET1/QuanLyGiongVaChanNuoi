using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure;
using QuanLyGiongChanNuoi.Infrastructure.Models;
using QuanLyGiongChanNuoi.Infrastructure.Data;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
	[Authorize(Roles = "Quản trị viên")]
	public class DonViHcHuyenController : Controller
	{
		private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

		public DonViHcHuyenController(QuanLyGiongVaThucAnChanNuoiContext context)
		{
			_context = context;
		}

		// 1. DANH SÁCH & TÌM KIẾM
		public async Task<IActionResult> Index(string searchString)
		{
			// Lấy danh sách các đơn vị cấp Huyện/Quận/Thị xã
			var query = _context.DonViHcs
				.Include(d => d.CapHc)
				.Include(d => d.TrucThuocNavigation) // Lấy thông tin Tỉnh cha
				.Where(d => d.CapHc.Ten.Contains("Huyện") || d.CapHc.Ten.Contains("Quận") || d.CapHc.Ten.Contains("Thị xã") || d.CapHc.Ten.Contains("Thành phố thuộc tỉnh"));

			if (!string.IsNullOrEmpty(searchString))
			{
				query = query.Where(d => d.Ten.Contains(searchString) || d.MaBuuDien.Contains(searchString));
				ViewData["CurrentFilter"] = searchString;
			}

			return View(await query.OrderBy(d => d.Ten).ToListAsync());
		}

		// 2. TẠO MỚI
		[HttpGet]
		public IActionResult Create()
		{
			// Load danh sách loại đơn vị (Huyện/Quận...)
			var capHuyen = _context.CapHcs.Where(c => c.Ten.Contains("Huyện") || c.Ten.Contains("Quận") || c.Ten.Contains("Thị xã") || c.Ten.Contains("Thành phố thuộc tỉnh"));
			ViewData["CapHcId"] = new SelectList(capHuyen, "Id", "Ten");

			// Load danh sách Tỉnh/Thành phố (Cấp cha)
			// LƯU Ý: Số 1 ở đây là ID của cấp "Tỉnh" trong bảng Cap_HC. Nếu DB của bạn khác, hãy sửa số này.
			int idCapTinh = 1;
			var capTinh = _context.DonViHcs.Where(x => x.CapHcId == idCapTinh);

			ViewData["TrucThuoc"] = new SelectList(capTinh, "Id", "Ten"); // Đã sửa lỗi cú pháp tại dòng này
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DonViHc donViHc)
		{
			// --- THÊM ĐOẠN NÀY ---
			// Loại bỏ kiểm tra các object quan hệ (vì form chỉ gửi ID)
			ModelState.Remove("CapHc");
			ModelState.Remove("TrucThuocNavigation");
			ModelState.Remove("InverseTrucThuocNavigation");
			// ---------------------

			if (ModelState.IsValid)
			{
				_context.Add(donViHc);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Thêm mới đơn vị hành chính cấp huyện thành công!";
				return RedirectToAction(nameof(Index));
			}

			// Phần code load lại dropdown khi lỗi (giữ nguyên)
			var capHuyen = _context.CapHcs.Where(c => c.Ten.Contains("Huyện") || c.Ten.Contains("Quận") || c.Ten.Contains("Thị xã") || c.Ten.Contains("Thành phố thuộc tỉnh"));
			ViewData["CapHcId"] = new SelectList(capHuyen, "Id", "Ten", donViHc.CapHcId);

			int idCapTinh = 1;
			var capTinh = _context.DonViHcs.Where(x => x.CapHcId == idCapTinh);
			ViewData["TrucThuoc"] = new SelectList(capTinh, "Id", "Ten", donViHc.TrucThuoc);

			return View(donViHc);
		}

		// 3. CHỈNH SỬA
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();

			var donViHc = await _context.DonViHcs.FindAsync(id);
			if (donViHc == null) return NotFound();

			// Load dropdown loại đơn vị
			var capHuyen = _context.CapHcs.Where(c => c.Ten.Contains("Huyện") || c.Ten.Contains("Quận") || c.Ten.Contains("Thị xã") || c.Ten.Contains("Thành phố thuộc tỉnh"));
			ViewData["CapHcId"] = new SelectList(capHuyen, "Id", "Ten", donViHc.CapHcId);

			// Load dropdown Tỉnh (Cha)
			int idCapTinh = 1; // ID của cấp Tỉnh
			var capTinh = _context.DonViHcs.Where(x => x.CapHcId == idCapTinh);
			ViewData["TrucThuoc"] = new SelectList(capTinh, "Id", "Ten", donViHc.TrucThuoc);

			return View(donViHc);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, DonViHc donViHc)
		{
			if (id != donViHc.Id) return NotFound();
            ModelState.Remove("CapHc");
            ModelState.Remove("TrucThuocNavigation");
            ModelState.Remove("InverseTrucThuocNavigation");

            if (ModelState.IsValid)
			{
				try
				{
					_context.Update(donViHc);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Cập nhật thành công!";
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!_context.DonViHcs.Any(e => e.Id == id)) return NotFound();
					else throw;
				}
				return RedirectToAction(nameof(Index));
			}

			// Load lại dropdown nếu lỗi
			var capHuyen = _context.CapHcs.Where(c => c.Ten.Contains("Huyện") || c.Ten.Contains("Quận") || c.Ten.Contains("Thị xã") || c.Ten.Contains("Thành phố thuộc tỉnh"));
			ViewData["CapHcId"] = new SelectList(capHuyen, "Id", "Ten", donViHc.CapHcId);

			int idCapTinh = 1;
			var capTinh = _context.DonViHcs.Where(x => x.CapHcId == idCapTinh);
			ViewData["TrucThuoc"] = new SelectList(capTinh, "Id", "Ten", donViHc.TrucThuoc);

			return View(donViHc);
		}

		// 4. XÓA
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var donViHc = await _context.DonViHcs.FindAsync(id);
			if (donViHc != null)
			{
				// Kiểm tra xem có xã nào trực thuộc huyện này không (tránh lỗi khóa ngoại)
				bool hasChild = await _context.DonViHcs.AnyAsync(x => x.TrucThuoc == id);
				if (hasChild)
				{
					TempData["ErrorMessage"] = "Không thể xóa huyện này vì đang có Xã/Phường trực thuộc!";
					return RedirectToAction(nameof(Index));
				}

				_context.DonViHcs.Remove(donViHc);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Đã xóa thành công!";
			}
			return RedirectToAction(nameof(Index));
		}
	}
}