using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure;
using QuanLyGiongChanNuoi.Infrastructure.Data;

namespace QuanLyGiongChanNuoi.Web.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context;

        public MenuViewComponent(QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Lấy danh sách Menu đang bật (HienThi = true), sắp xếp theo ThuTu
            // Chỉ lấy Menu cha (MenuChaId == null) và kèm theo các con của nó
            var menus = await _context.Menus
                .Where(m => m.HienThi == true && m.MenuChaId == null)
                .OrderBy(m => m.ThuTu)
                .Include(m => m.MenuCons) // Load kèm menu con (nếu có)
                .ToListAsync();

            return View(menus);
        }
    }
}