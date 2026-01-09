using Microsoft.AspNetCore.Mvc;
using QuanLyGiongChanNuoi.Core.Interfaces;
using QuanLyGiongChanNuoi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using QuanLyGiongChanNuoi.Infrastructure.Models;
using QuanLyGiongChanNuoi.Infrastructure.Data;

namespace QuanLyGiongChanNuoi.Web.Controllers
{
    public class GiongVatNuoiController : Controller
    {
        private readonly IGiongVatNuoiRepository _giongVatNuoiRepo;

        public GiongVatNuoiController(IGiongVatNuoiRepository giongVatNuoiRepo)
        {
            _giongVatNuoiRepo = giongVatNuoiRepo;
        }

        public async Task<IActionResult> Index()
        {
            var giongs = await _giongVatNuoiRepo.GetAllAsync();
            return View(giongs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var giong = await _giongVatNuoiRepo.GetByIdAsync(id);

            if (giong == null)
            {
                return NotFound();
            }

            dynamic model = giong;
            ViewBag.Id = model.Id;
            ViewBag.TenGiong = model.TenGiong;
            ViewBag.MoTa = model.MoTa;
            ViewBag.Loai = model.Loai;

            return View();
        }

        public IActionResult Create()
        {
            // Mới vào chưa có dữ liệu gì nên chỉ trả về View
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string TenGiong, string MoTa, string Loai)
        {
            // 1. Kiểm tra trùng tên
            // (Lưu ý: Hàm Check trùng ở Create thường chỉ cần 1 tham số tên, khác với Edit cần thêm Id để loại trừ chính nó)
            if (await _giongVatNuoiRepo.IsTenGiongExistsAsync(TenGiong))
            {
                ModelState.AddModelError("TenGiong", "Tên giống đã tồn tại!");

                // QUAN TRỌNG: Phải gán lại ViewBag để form không bị mất dữ liệu người dùng vừa nhập
                ViewBag.TenGiong = TenGiong;
                ViewBag.MoTa = MoTa;
                ViewBag.Loai = Loai;

                return View();
            }

            // 2. Tạo đối tượng Entity thực sự (giống cách bạn làm ở Edit)
            var newGiong = new QuanLyGiongChanNuoi.Infrastructure.Models.GiongVatNuoi
            {
                TenGiong = TenGiong,
                MoTa = MoTa,
                Loai = Loai
                // Nếu có trường Ngày tạo/Người tạo thì gán thêm ở đây
            };

            // 3. Gọi Repository thêm mới
            await _giongVatNuoiRepo.AddAsync(newGiong);

            TempData["SuccessMessage"] = "Thêm thành công!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var giong = await _giongVatNuoiRepo.GetByIdAsync(id);

            if (giong == null)
            {
                return NotFound();
            }

            dynamic model = giong;
            ViewBag.Id = model.Id;
            ViewBag.TenGiong = model.TenGiong;
            ViewBag.MoTa = model.MoTa;
            ViewBag.Loai = model.Loai;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, string TenGiong, string MoTa, string Loai)
        {
            if (Id <= 0)
            {
                return BadRequest("ID không hợp lệ");
            }

            if (await _giongVatNuoiRepo.IsTenGiongExistsAsync(TenGiong, Id))
            {
                ModelState.AddModelError("TenGiong", "Tên giống đã tồn tại!");

                // Set lại ViewBag để hiển thị form
                ViewBag.Id = Id;
                ViewBag.TenGiong = TenGiong;
                ViewBag.MoTa = MoTa;
                ViewBag.Loai = Loai;

                return View();
            }

            // Tạo object GiongVatNuoi thực sự
            var giong = await _giongVatNuoiRepo.GetByIdAsync(Id);
            if (giong == null)
            {
                return NotFound();
            }

            // Cast sang GiongVatNuoi
            if (giong is QuanLyGiongChanNuoi.Infrastructure.Models.GiongVatNuoi existing)
            {
                existing.TenGiong = TenGiong;
                existing.MoTa = MoTa;
                existing.Loai = Loai;

                await _giongVatNuoiRepo.UpdateAsync(existing);
            }

            TempData["SuccessMessage"] = "Cập nhật thành công!";
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Quản trị viên")]    //CHỈ ADMIN MỚI VÀO ĐƯỢC
        // GET: Hiển thị trang xác nhận xóa
        public async Task<IActionResult> Delete(int id)
        {
            var giong = await _giongVatNuoiRepo.GetByIdAsync(id);

            if (giong == null)
            {
                return NotFound();
            }

            // Đổ dữ liệu ra ViewBag để hiển thị
            dynamic model = giong;
            ViewBag.Id = model.Id;
            ViewBag.TenGiong = model.TenGiong;
            ViewBag.MoTa = model.MoTa;
            ViewBag.Loai = model.Loai;

            // Nếu có lỗi từ lần xóa trước (ví dụ do ràng buộc khóa ngoại), hiển thị lỗi đó
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View();
        }

        // POST: Thực hiện xóa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Quản trị viên")] // <--- CHỈ ADMIN MỚI XÓA ĐƯỢC
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // 1. Kiểm tra tồn tại trước khi xóa
            var giong = await _giongVatNuoiRepo.GetByIdAsync(id);
            if (giong == null)
            {
                return NotFound();
            }

            try
            {
                // 2. Gọi hàm xóa
                // LƯU Ý: Nếu Repo của bạn báo lỗi dòng này, hãy thử đổi thành: await _giongVatNuoiRepo.DeleteAsync(giong);
                await _giongVatNuoiRepo.DeleteAsync(id);

                TempData["SuccessMessage"] = "Xóa thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 3. Xử lý lỗi (Thường là lỗi do Giống này đang được sử dụng ở bảng khác)
                // Ghi log lỗi ex.Message nếu cần thiết

                TempData["ErrorMessage"] = "Không thể xóa giống này vì đang có vật nuôi thuộc giống này hoặc dữ liệu liên quan!";

                // Quay lại trang Delete để người dùng thấy thông báo lỗi
                return RedirectToAction(nameof(Delete), new { id = id });
            }
        }
    }
}