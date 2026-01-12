-- 1. KIEM TRA & CAP NHAT BANG MENU
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND name = 'ControllerName')
BEGIN
    -- Add the columns first
    ALTER TABLE [dbo].[Menu] ADD [ControllerName] VARCHAR(50) NULL;
    ALTER TABLE [dbo].[Menu] ADD [ActionName] VARCHAR(50) NULL;
    ALTER TABLE [dbo].[Menu] ADD [Icon] NVARCHAR(50) NULL;
    ALTER TABLE [dbo].[Menu] ADD [ThuTu] INT DEFAULT 0 WITH VALUES;
    ALTER TABLE [dbo].[Menu] ADD [HienThi] BIT DEFAULT 1 WITH VALUES;

    -- Use EXEC to run updates dynamically. 
    -- This defers compilation until runtime, AFTER columns are added.
    -- IMPORTANT: Single quotes inside must be doubled (' -> '').
    EXEC('
        UPDATE [dbo].[Menu] SET [ControllerName] = ''Home'', [ActionName] = ''Index'', [Icon] = ''fa-solid fa-house'' WHERE [ID] = 1;
        UPDATE [dbo].[Menu] SET [ControllerName] = ''NguoiDung'', [ActionName] = ''Index'', [Icon] = ''fa-solid fa-users'' WHERE [ID] = 2;
        UPDATE [dbo].[Menu] SET [ControllerName] = ''ChucVu'', [ActionName] = ''Index'', [Icon] = ''fa-solid fa-user-tag'' WHERE [ID] = 3;
    ');
END
GO