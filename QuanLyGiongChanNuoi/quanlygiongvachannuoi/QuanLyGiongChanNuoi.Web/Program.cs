using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using QuanLyGiongChanNuoi.Core.Interfaces;
using QuanLyGiongChanNuoi.Infrastructure.Repositories;
using QuanLyGiongChanNuoi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<QuanLyGiongVaThucAnChanNuoiAContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories và UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGiongVatNuoiRepository, GiongVatNuoiRepository>();
builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();

// Add Session support cho đăng nhập
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Đăng ký dịch vụ Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Đường dẫn trang đăng nhập
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Hết hạn sau 60p
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Thêm Session middleware

app.UseAuthentication(); // <--- Bắt buộc có dòng này (Xác thực)
app.UseAuthorization();  // <--- Bắt buộc có dòng này (Phân quyền)

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();