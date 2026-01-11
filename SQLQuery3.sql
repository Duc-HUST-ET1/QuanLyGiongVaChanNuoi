USE [QuanLyGiongVaThucAnChanNuoiA]
GO

-- 1. KIỂM TRA & CẬP NHẬT BẢNG MENU
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND name = 'ControllerName')
BEGIN
    ALTER TABLE [dbo].[Menu] ADD [ControllerName] VARCHAR(50) NULL;
    ALTER TABLE [dbo].[Menu] ADD [ActionName] VARCHAR(50) NULL;
    ALTER TABLE [dbo].[Menu] ADD [Icon] NVARCHAR(50) NULL;
    ALTER TABLE [dbo].[Menu] ADD [ThuTu] INT DEFAULT 0 WITH VALUES;
    ALTER TABLE [dbo].[Menu] ADD [HienThi] BIT DEFAULT 1 WITH VALUES;
    
    -- Cập nhật dữ liệu cũ ngay khi thêm cột
    UPDATE [dbo].[Menu] SET [ControllerName] = 'Home', [ActionName] = 'Index', [Icon] = 'fa-solid fa-house' WHERE [ID] = 1;
    UPDATE [dbo].[Menu] SET [ControllerName] = 'NguoiDung', [ActionName] = 'Index', [Icon] = 'fa-solid fa-users' WHERE [ID] = 2;
    UPDATE [dbo].[Menu] SET [ControllerName] = 'ChucVu', [ActionName] = 'Index', [Icon] = 'fa-solid fa-user-tag' WHERE [ID] = 3;
END
GO

-- 2. KIỂM TRA & CẬP NHẬT BẢNG LỊCH SỬ
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[lich_su_truy_cap]') AND name = 'TrinhDuyet')
BEGIN
    ALTER TABLE [dbo].[lich_su_truy_cap] ADD [TrinhDuyet] NVARCHAR(250) NULL;
END
GO