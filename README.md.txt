# Hệ thống Quản lý Giống và Thức ăn Chăn nuôi

![.NET](https://img.shields.io/badge/.NET-6.0%2B-512BD4?logo=dotnet)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927?logo=microsoft-sql-server)
![Bootstrap](https://img.shields.io/badge/Frontend-Bootstrap-7952B3?logo=bootstrap)

Dự án Web Application xây dựng trên nền tảng **ASP.NET Core MVC** nhằm số hóa quy trình quản lý thông tin về giống vật nuôi, thức ăn chăn nuôi, quản lý các đơn vị tổ chức/cá nhân và giám sát lịch sử truy cập hệ thống.

## Tính năng chính

* **Quản lý Danh mục:**
    * Quản lý thông tin Giống vật nuôi (Thêm, Sửa, Xóa, Tìm kiếm).
    * Quản lý thông tin Thức ăn chăn nuôi.
    * Quản lý hồ sơ Tổ chức và Cá nhân hoạt động trong lĩnh vực.
* **Quản trị hệ thống:**
    * Quản lý Người dùng (User Management).
    * Phân quyền truy cập.
* **Giám sát & An toàn:**
    * Ghi nhật ký lịch sử truy cập (Log): Lưu trữ IP, thời gian, trình duyệt, hành động của người dùng.
* **Báo cáo:** Thống kê dữ liệu cơ bản.

## Công nghệ sử dụng

* **Backend:** ASP.NET Core MVC (C#).
* **Database:** Microsoft SQL Server.
* **ORM:** Entity Framework Core (Code-First).
* **Frontend:** HTML5, CSS3, Bootstrap 5, JavaScript.
* **IDE:** Visual Studio 2020 / VS Code.


##Cấu trúc thư mục dự án (Project Structure)


```text
QuanLyGiongChanNuoi/
├── 📂 QuanLyGiongChanNuoi.Core/          # (Domain Layer) Lớp lõi, chứa các thực thể nghiệp vụ
│   ├── Entities/                         # Các Model mapping với DB (NguoiDung, GiongVatNuoi...)
│   ├── Interfaces/                       # Các Interface định nghĩa Repository
│   └── Constants/                        # Các hằng số, Enum hệ thống
│
├── 📂 QuanLyGiongChanNuoi.Infrastructure/# (Data Access Layer) Lớp hạ tầng, làm việc với Database
│   ├── Data/                             # Chứa DbContext (Cấu hình Entity Framework)
│   ├── Models                             # Chứa Các models used for EF Core (if different from Core Entities)
│   └── Repositories/                     # Triển khai các Interface Repository từ Core
│
├── 📂 QuanLyGiongChanNuoi.Services/      # (Service Layer) Lớp dịch vụ, xử lý logic nghiệp vụ
│   ├── Interfaces/                       # Interface cho các Service
│   ├── Implementations/                  # Logic xử lý chính (VD: TinhToanTonKho, KiemTraDangNhap...)
│   └── DTOs/                             # Data Transfer Objects (Dữ liệu chuyển đổi giữa các lớp)
│
└── 📂 QuanLyGiongChanNuoi.Web/           # (Presentation Layer) Lớp giao diện người dùng
    ├── Controllers/                      # Nhận request, gọi Service và trả về View
    ├── Views/                            # Giao diện HTML/Razor (.cshtml)
    ├── wwwroot/                          # File tĩnh (CSS, JS, Images, Libs)
    ├── appsettings.json                  # Cấu hình kết nối Database
    └── Program.cs                        # Cấu hình Dependency Injection & Middleware

##  Hướng dẫn chạy dự án
Bước 1: Chạy file "(script)QuanLyGiongVaThucAnChanNuoi" bằng SSM20 để tạo database
Bước 2: Mở folder "[KTPM] 20233337_20233359_20233546_20233568" bằng Visual Studio 
Bước 3: Mở đường dẫn E:\trananhduc\HOCTAP\COSONGANH\KTPM\KTPM_20233337_20233359_20233546_20233568\QuanLyGiongChanNuoi\quanlygiongvachannuoi\QuanLyGiongChanNuoi.Web>appsettings.json và chỉnh sửa mục DATA SOURCE trong chuỗi kết nối cho phù hợp với máy của bạn.
Bước 4: Kết nối database với Visual Studio bằng cách mở "Tools" -> "Connect to Database..." -> Chọn "Microsoft SQL Server" -> Nhập tên server và database đã tạo ở bước 1 -> Test Connection -> OK
Bước 5: Mở terminal trong Visual Studio và chạy lệnh cd E:\trananhduc\HOCTAP\COSONGANH\KTPM\KTPM_20233337_20233359_20233546_20233568\QuanLyGiongChanNuoi\quanlygiongvachannuoi\QuanLyGiongChanNuoi.Web>
Bước 6: Chạy lệnh dotnet run để khởi động ứng dụng
Bước 7: Mở trình duyệt và truy cập địa chỉ http://localhost:5116 để sử dụng hệ thống