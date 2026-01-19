USE [master]
GO
/****** Object:  Database [QuanLyGiongVaThucAnChanNuoi] ******/
CREATE DATABASE [QuanLyGiongVaThucAnChanNuoi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyGiongVaThucAnChanNuoi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QuanLyGiongVaThucAnChanNuoi.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyGiongVaThucAnChanNuoi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QuanLyGiongVaThucAnChanNuoi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyGiongVaThucAnChanNuoi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanLyGiongVaThucAnChanNuoi', N'ON'
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET QUERY_STORE = ON
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QuanLyGiongVaThucAnChanNuoi]
GO
/****** Object:  Table [dbo].[Cap_HC]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cap_HC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[Cap] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChucVu]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucVu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenChucVu] [nvarchar](40) NOT NULL,
	[mota] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoSo_HoaChatCam]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoSo_HoaChatCam](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CoSoThucAnID] [int] NOT NULL,
	[HoaChatCamID] [int] NOT NULL,
	[NgayPhatHien] [date] NULL,
	[LyDoSuDung] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoSo_NguyenLieuChoPhep]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoSo_NguyenLieuChoPhep](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CoSoThucAnID] [int] NOT NULL,
	[NguyenLieuID] [int] NOT NULL,
	[SoLuong] [float] NULL,
	[NgayCapNhat] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoSoThucAn]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoSoThucAn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenCoSo] [nvarchar](100) NOT NULL,
	[DiaChi] [nvarchar](200) NULL,
	[LoaiCoSo] [nvarchar](50) NOT NULL,
	[ToChucCaNhanID] [int] NOT NULL,
	[ThucAnChanNuoiID] [int] NOT NULL,
	[NgayCapNhat] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoSoVatNuoi]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoSoVatNuoi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenCoSo] [nvarchar](100) NOT NULL,
	[DiaChi] [nvarchar](200) NULL,
	[trangthai] [bit] NULL,
	[ToChucCaNhanID] [int] NOT NULL,
	[LoaiCoSo] [nvarchar](50) NOT NULL,
	[GiongVatNuoiID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonVi_HC]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonVi_HC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[Cap] [int] NOT NULL,
	[TrucThuoc] [int] NULL,
	[Cap_HC_ID] [int] NOT NULL,
	[MaBuuDien] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiayChungNhan]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiayChungNhan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CoSoThucAnID] [int] NOT NULL,
	[SoGiayChungNhan] [nvarchar](50) NOT NULL,
	[NgayCap] [date] NOT NULL,
	[NgayHetHan] [date] NULL,
	[NoiCap] [nvarchar](50) NULL,
	[MoTa] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiongCanBaoTon]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiongCanBaoTon](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GiongID] [int] NOT NULL,
	[Loai] [nvarchar](50) NOT NULL,
	[LyDo] [nvarchar](200) NULL,
	[NgayBaoTon] [date] NULL,
	[TrangThai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiongVatNuoi]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiongVatNuoi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenGiong] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](100) NULL,
	[Loai] [nvarchar](40) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaChatCam]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaChatCam](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenHoaChat] [nvarchar](100) NOT NULL,
	[LyDoCam] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lich_su_truy_cap]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lich_su_truy_cap](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NguoiDungID] [int] NULL,
	[ThoiGian] [datetime] NULL,
	[mota] [nvarchar](50) NULL,
	[IP_address] [nvarchar](40) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichSuTacDong]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichSuTacDong](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NguoiDungID] [int] NOT NULL,
	[ThoiGian] [datetime] NULL,
	[HoatDong] [nvarchar](200) NOT NULL,
	[BangLienQuan] [nvarchar](100) NULL,
	[KhoaChinh] [nvarchar](50) NULL,
	[MoTaThem] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiHoatDong]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiHoatDong](
	[ID] [int] NOT NULL,
	[TenHoatDong] [nvarchar](50) NOT NULL,
	[mota] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[ID] [int] NOT NULL,
	[TenMenu] [nvarchar](100) NOT NULL,
	[LienKet] [nvarchar](200) NULL,
	[MenuChaID] [int] NULL,
	[CapDo] [int] NOT NULL,
	[TrangThai] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguoiDung]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiDung](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](40) NOT NULL,
	[TenDN] [nvarchar](40) NOT NULL,
	[MatKhau] [nvarchar](30) NULL,
	[email] [nvarchar](100) NULL,
	[trang_thai] [bit] NULL,
	[ChucVu_ID] [int] NULL,
	[DonVi_HC_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguonGen]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguonGen](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenNguonGen] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguyenLieuChoPhep]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguyenLieuChoPhep](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenNguyenLieu] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nhom]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nhom](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NULL,
	[mota] [nvarchar](50) NULL,
	[NgayTao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[phan_quyen]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phan_quyen](
	[MaQuyen] [nvarchar](20) NOT NULL,
	[ten_quyen] [nvarchar](30) NULL,
	[mota] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaQuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[phan_quyen_nguoi_dung]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phan_quyen_nguoi_dung](
	[NguoiDungID] [int] NOT NULL,
	[ma_quyen] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NguoiDungID] ASC,
	[ma_quyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[phan_quyen_nhom]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phan_quyen_nhom](
	[NhomID] [int] NOT NULL,
	[ma_quyen] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NhomID] ASC,
	[ma_quyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThanhVienNhom]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThanhVienNhom](
	[NguoiDungID] [int] NOT NULL,
	[NhomID] [int] NOT NULL,
	[LaTruongNhom] [bit] NULL,
	[NgayThamGia] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[NguoiDungID] ASC,
	[NhomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThucAnChanNuoi]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThucAnChanNuoi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenThucAn] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
	[LoaiThucAn] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToChucCaNhan]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToChucCaNhan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](100) NOT NULL,
	[DiaChi] [nvarchar](200) NULL,
	[SoDienThoai] [nvarchar](15) NULL,
	[Email] [nvarchar](100) NULL,
	[LoaiHinh] [nvarchar](50) NULL,
	[LoaiHoatDong] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToChucNguonGen]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToChucNguonGen](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ToChucCaNhanID] [int] NOT NULL,
	[NguonGenID] [int] NOT NULL,
	[KhuVuc] [nvarchar](40) NULL,
	[NgayThuThap] [datetime] NULL,
	[HoatDong] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cap_HC] ON 

INSERT [dbo].[Cap_HC] ([ID], [Ten], [Cap]) VALUES (1, N'Thành phố', 1)
INSERT [dbo].[Cap_HC] ([ID], [Ten], [Cap]) VALUES (2, N'Huyện', 2)
INSERT [dbo].[Cap_HC] ([ID], [Ten], [Cap]) VALUES (3, N'Xã', 3)
INSERT [dbo].[Cap_HC] ([ID], [Ten], [Cap]) VALUES (4, N'Phường', 3)
INSERT [dbo].[Cap_HC] ([ID], [Ten], [Cap]) VALUES (5, N'Thị trấn', 3)
SET IDENTITY_INSERT [dbo].[Cap_HC] OFF
GO
SET IDENTITY_INSERT [dbo].[ChucVu] ON 

INSERT [dbo].[ChucVu] ([ID], [TenChucVu], [mota]) VALUES (1, N'Quản trị viên', N'Quản lý hệ thống')
INSERT [dbo].[ChucVu] ([ID], [TenChucVu], [mota]) VALUES (2, N'Chuyên viên', N'Nhân viên kỹ thuật')
INSERT [dbo].[ChucVu] ([ID], [TenChucVu], [mota]) VALUES (3, N'Truởng phòng', N'Truởng bộ phận quản lý giống')
INSERT [dbo].[ChucVu] ([ID], [TenChucVu], [mota]) VALUES (4, N'Giám đốc', N'Người quản lý cấp cao')
INSERT [dbo].[ChucVu] ([ID], [TenChucVu], [mota]) VALUES (5, N'Khách hàng', NULL)
SET IDENTITY_INSERT [dbo].[ChucVu] OFF
GO
SET IDENTITY_INSERT [dbo].[CoSo_HoaChatCam] ON 

INSERT [dbo].[CoSo_HoaChatCam] ([ID], [CoSoThucAnID], [HoaChatCamID], [NgayPhatHien], [LyDoSuDung]) VALUES (1, 1, 1, CAST(N'2025-11-01' AS Date), N'Sử dụng trái phép trong sản xuất')
INSERT [dbo].[CoSo_HoaChatCam] ([ID], [CoSoThucAnID], [HoaChatCamID], [NgayPhatHien], [LyDoSuDung]) VALUES (2, 2, 2, CAST(N'2025-11-02' AS Date), N'Sử dụng để kích thích tăng trưởng')
INSERT [dbo].[CoSo_HoaChatCam] ([ID], [CoSoThucAnID], [HoaChatCamID], [NgayPhatHien], [LyDoSuDung]) VALUES (3, 3, 3, CAST(N'2025-11-03' AS Date), N'Pha trộn vào thức ăn chăn nuôi')
INSERT [dbo].[CoSo_HoaChatCam] ([ID], [CoSoThucAnID], [HoaChatCamID], [NgayPhatHien], [LyDoSuDung]) VALUES (4, 4, 4, CAST(N'2025-11-04' AS Date), N'Dùng để kháng bệnh không đúng quy định')
INSERT [dbo].[CoSo_HoaChatCam] ([ID], [CoSoThucAnID], [HoaChatCamID], [NgayPhatHien], [LyDoSuDung]) VALUES (5, 5, 5, CAST(N'2025-11-05' AS Date), N'Thêm vào trái phép để tăng năng suất')
SET IDENTITY_INSERT [dbo].[CoSo_HoaChatCam] OFF
GO
SET IDENTITY_INSERT [dbo].[CoSo_NguyenLieuChoPhep] ON 

INSERT [dbo].[CoSo_NguyenLieuChoPhep] ([ID], [CoSoThucAnID], [NguyenLieuID], [SoLuong], [NgayCapNhat]) VALUES (1, 1, 1, 1000, CAST(N'2025-12-02' AS Date))
INSERT [dbo].[CoSo_NguyenLieuChoPhep] ([ID], [CoSoThucAnID], [NguyenLieuID], [SoLuong], [NgayCapNhat]) VALUES (3, 3, 3, 3000, CAST(N'2025-12-03' AS Date))
INSERT [dbo].[CoSo_NguyenLieuChoPhep] ([ID], [CoSoThucAnID], [NguyenLieuID], [SoLuong], [NgayCapNhat]) VALUES (4, 4, 4, 4000, CAST(N'2025-12-04' AS Date))
INSERT [dbo].[CoSo_NguyenLieuChoPhep] ([ID], [CoSoThucAnID], [NguyenLieuID], [SoLuong], [NgayCapNhat]) VALUES (5, 5, 5, 5000, CAST(N'2025-12-05' AS Date))
SET IDENTITY_INSERT [dbo].[CoSo_NguyenLieuChoPhep] OFF
GO
SET IDENTITY_INSERT [dbo].[CoSoThucAn] ON 

INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (1, N'Công ty 1', N'Quận Hai Bà Trưng, Hà Nội', N'Sản xuất', 1, 5, CAST(N'2024-12-16T08:18:54.993' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (2, N'Công ty 2', N'Quận Ba Đình, Hà Nội', N'Khảo nghiệm', 2, 2, CAST(N'2024-12-16T08:18:54.993' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (3, N'Công ty 3', N'Quận 1, TP.HCM', N'Sản xuất', 3, 3, CAST(N'2024-12-16T08:18:54.993' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (4, N'Công ty 4', N'Quận 3, TP.HCM', N'Khảo nghiệm', 4, 4, CAST(N'2024-12-16T08:18:54.993' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (5, N'Công ty 5', N'Quận Hải Châu, Đà Nẵng', N'Sản xuất', 5, 5, CAST(N'2024-12-16T08:18:54.993' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (6, N'Công ty 1', N'Quận Hai Bà Trưng, Hà Nội', N'Sản xuất', 1, 3, NULL)
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (7, N'Công ty 1', N'Quận Hai Bà Trưng, Hà Nội', N'Sản xuất', 1, 5, NULL)
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (9, N'Công ty 1', N'Quận Hai Bà Trưng, Hà Nội', N'Sản xuất', 1, 2, CAST(N'2024-12-19T11:48:54.233' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (10, N'Công ty 1', N'Quận Hai Bà Trưng, Hà Nội', N'Sản xuất', 1, 4, CAST(N'2024-12-19T11:48:46.507' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (11, N'Công ty 1', N'Quận Hai Bà Trưng, Hà Nội', N'Sản xuất', 1, 3, CAST(N'2024-12-19T11:49:44.380' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (12, N'Công ty 1', N'Quận Hai Bà Trưng, Hà Nội', N'Sản xuất', 1, 1, CAST(N'2024-12-19T11:56:20.123' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (13, N'Công ty 1', N'Quận Hai Bà Trưng, Hà Nội', N'Sản xuất', 1, 5, CAST(N'2024-12-19T11:56:57.113' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (14, N'Công ty 1', N'Quận Hai Bà Trưng, Hà Nội', N'Sản xuất', 1, 1, CAST(N'2024-12-19T11:58:02.920' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (15, N'Công ty 1', N'Quận Hai Bà Trưng, Hà Nội', N'Sản xuất', 1, 1, CAST(N'2024-12-19T12:02:03.847' AS DateTime))
INSERT [dbo].[CoSoThucAn] ([ID], [TenCoSo], [DiaChi], [LoaiCoSo], [ToChucCaNhanID], [ThucAnChanNuoiID], [NgayCapNhat]) VALUES (16, N'Công ty 1', N'Quận Hai Bà Trưng, Hà Nội', N'Sản xuất', 5, 1, CAST(N'2024-12-19T12:04:40.613' AS DateTime))
SET IDENTITY_INSERT [dbo].[CoSoThucAn] OFF
GO
SET IDENTITY_INSERT [dbo].[CoSoVatNuoi] ON 

INSERT [dbo].[CoSoVatNuoi] ([ID], [TenCoSo], [DiaChi], [trangthai], [ToChucCaNhanID], [LoaiCoSo], [GiongVatNuoiID]) VALUES (1, N'Cơ sở chăn nuôi 1', N'Huyện Sóc Sơn, Hà Nội', 1, 1, N'Khảo nghiệm', 1)
INSERT [dbo].[CoSoVatNuoi] ([ID], [TenCoSo], [DiaChi], [trangthai], [ToChucCaNhanID], [LoaiCoSo], [GiongVatNuoiID]) VALUES (2, N'Cơ sở chăn nuôi 2', N'Huyện Đông Anh, Hà Nội', 1, 2, N'Bảo tồn', 2)
INSERT [dbo].[CoSoVatNuoi] ([ID], [TenCoSo], [DiaChi], [trangthai], [ToChucCaNhanID], [LoaiCoSo], [GiongVatNuoiID]) VALUES (3, N'Cơ sở chăn nuôi 3', N'Quận 9, TP.HCM', 1, 3, N'Sản xuất giống', 3)
INSERT [dbo].[CoSoVatNuoi] ([ID], [TenCoSo], [DiaChi], [trangthai], [ToChucCaNhanID], [LoaiCoSo], [GiongVatNuoiID]) VALUES (4, N'Cơ sở chăn nuôi 4', N'Huyện Củ Chi, TP.HCM', 1, 4, N'Khảo nghiệm', 4)
INSERT [dbo].[CoSoVatNuoi] ([ID], [TenCoSo], [DiaChi], [trangthai], [ToChucCaNhanID], [LoaiCoSo], [GiongVatNuoiID]) VALUES (5, N'Cơ sở chăn nuôi 5', N'Huyện Hòa Vang, Đà Nẵng', 1, 5, N'Sản xuất giống', 5)
SET IDENTITY_INSERT [dbo].[CoSoVatNuoi] OFF
GO
SET IDENTITY_INSERT [dbo].[DonVi_HC] ON 

INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (1, N'TP. Hà Nội', 1, NULL, 1, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (2, N'Huyện Đông Anh', 2, 1, 2, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (3, N'Xã Cổ Loa', 3, 2, 3, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (4, N'Xã Hải Bối', 3, 2, 3, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (5, N'Huyện Sóc Sơn', 2, NULL, 2, N'1500')
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (6, N'TP. HCM', 0, NULL, 1, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (11, N'Quận 2', 0, 6, 2, N'3100')
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (13, N'Huyện Hà Nam', 0, NULL, 2, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (15, N'Xã 1', 0, 13, 3, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (16, N'Xã 2', 0, 2, 3, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (17, N'xa 3', 0, 18, 3, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (18, N'No', 0, NULL, 2, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (19, N'xa 4', 0, 20, 3, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (20, N'Quận 12', 0, NULL, 2, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (21, N'Quận 10', 0, 1, 2, NULL)
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (23, N'xa 5', 0, 21, 3, N'2000')
INSERT [dbo].[DonVi_HC] ([ID], [Ten], [Cap], [TrucThuoc], [Cap_HC_ID], [MaBuuDien]) VALUES (25, N'xa 52', 0, 21, 3, N'2000')
SET IDENTITY_INSERT [dbo].[DonVi_HC] OFF
GO
SET IDENTITY_INSERT [dbo].[GiayChungNhan] ON 

INSERT [dbo].[GiayChungNhan] ([ID], [CoSoThucAnID], [SoGiayChungNhan], [NgayCap], [NgayHetHan], [NoiCap], [MoTa]) VALUES (1, 1, N'GCN001', CAST(N'2025-01-01' AS Date), CAST(N'2025-01-01' AS Date), N'Cục chăn nuôi', N'Giấy chứng nhận tiêu chuẩn')
INSERT [dbo].[GiayChungNhan] ([ID], [CoSoThucAnID], [SoGiayChungNhan], [NgayCap], [NgayHetHan], [NoiCap], [MoTa]) VALUES (2, 2, N'GCN002', CAST(N'2025-02-01' AS Date), CAST(N'2025-02-01' AS Date), N'Cục chăn nuôi', N'Giấy chứng nhận tiêu chuẩn')
INSERT [dbo].[GiayChungNhan] ([ID], [CoSoThucAnID], [SoGiayChungNhan], [NgayCap], [NgayHetHan], [NoiCap], [MoTa]) VALUES (3, 3, N'GCN003', CAST(N'2025-03-01' AS Date), CAST(N'2025-03-01' AS Date), N'Cục chăn nuôi', N'Giấy chứng nhận tiêu chuẩn')
INSERT [dbo].[GiayChungNhan] ([ID], [CoSoThucAnID], [SoGiayChungNhan], [NgayCap], [NgayHetHan], [NoiCap], [MoTa]) VALUES (4, 4, N'GCN004', CAST(N'2025-04-01' AS Date), CAST(N'2025-04-01' AS Date), N'Cục chăn nuôi', N'Giấy chứng nhận tiêu chuẩn')
INSERT [dbo].[GiayChungNhan] ([ID], [CoSoThucAnID], [SoGiayChungNhan], [NgayCap], [NgayHetHan], [NoiCap], [MoTa]) VALUES (5, 5, N'GCN005', CAST(N'2025-05-01' AS Date), CAST(N'2025-05-01' AS Date), N'Cục chăn nuôi', N'Giấy chứng nhận tiêu chuẩn')
SET IDENTITY_INSERT [dbo].[GiayChungNhan] OFF
GO
SET IDENTITY_INSERT [dbo].[GiongCanBaoTon] ON 

INSERT [dbo].[GiongCanBaoTon] ([ID], [GiongID], [Loai], [LyDo], [NgayBaoTon], [TrangThai]) VALUES (1, 1, N'Bảo tồn', N'Nguy cơ tuyệt chủng', CAST(N'2025-12-01' AS Date), 1)
INSERT [dbo].[GiongCanBaoTon] ([ID], [GiongID], [Loai], [LyDo], [NgayBaoTon], [TrangThai]) VALUES (2, 2, N'Bảo tồn', N'Duy trì nguồn gen quý', CAST(N'2025-12-12' AS Date), 0)
INSERT [dbo].[GiongCanBaoTon] ([ID], [GiongID], [Loai], [LyDo], [NgayBaoTon], [TrangThai]) VALUES (3, 3, N'Cấm xuất khẩu', N'Bảo vệ tài sản quốc gia', CAST(N'2025-12-26' AS Date), 1)
INSERT [dbo].[GiongCanBaoTon] ([ID], [GiongID], [Loai], [LyDo], [NgayBaoTon], [TrangThai]) VALUES (4, 4, N'Bảo tồn', N'Duy trì sự đa dạng sinh học', CAST(N'2025-12-12' AS Date), 1)
INSERT [dbo].[GiongCanBaoTon] ([ID], [GiongID], [Loai], [LyDo], [NgayBaoTon], [TrangThai]) VALUES (5, 5, N'Cấm xuất khẩu', N'Tài sản gen quốc gia', CAST(N'2025-12-12' AS Date), 0)
SET IDENTITY_INSERT [dbo].[GiongCanBaoTon] OFF
GO
SET IDENTITY_INSERT [dbo].[GiongVatNuoi] ON 

INSERT [dbo].[GiongVatNuoi] ([ID], [TenGiong], [MoTa], [Loai]) VALUES (1, N'Giống bò sữa Mỹ', N'Năng suất sữa cao', N'Bò')
INSERT [dbo].[GiongVatNuoi] ([ID], [TenGiong], [MoTa], [Loai]) VALUES (2, N'Giống lợn mán', N'Kháng bệnh tốt', N'Lợn')
INSERT [dbo].[GiongVatNuoi] ([ID], [TenGiong], [MoTa], [Loai]) VALUES (3, N'Giống gà lai', N'Chân to, thịt ngon', N'Gà')
INSERT [dbo].[GiongVatNuoi] ([ID], [TenGiong], [MoTa], [Loai]) VALUES (4, N'Giống vịt trời', N'Dễ nuôi, sinh trưởng nhanh', N'Vịt')
INSERT [dbo].[GiongVatNuoi] ([ID], [TenGiong], [MoTa], [Loai]) VALUES (5, N'Giống dê cỏ', N'Phù hợp với địa hình đồi núi', N'Dê')
INSERT [dbo].[GiongVatNuoi] ([ID], [TenGiong], [MoTa], [Loai]) VALUES (6, N'Giống bò sữa Mỹ 1', NULL, N'Bò')
SET IDENTITY_INSERT [dbo].[GiongVatNuoi] OFF
GO
SET IDENTITY_INSERT [dbo].[HoaChatCam] ON 

INSERT [dbo].[HoaChatCam] ([ID], [TenHoaChat], [LyDoCam]) VALUES (1, N'Ractopamine', N'Tác động tiêu cực đến sức khỏe người dùng')
INSERT [dbo].[HoaChatCam] ([ID], [TenHoaChat], [LyDoCam]) VALUES (2, N'Clenbuterol', N'Gây nguy hiểm cho sức khỏe')
INSERT [dbo].[HoaChatCam] ([ID], [TenHoaChat], [LyDoCam]) VALUES (3, N'Malathion', N'Chất độc hại cho vật nuôi')
INSERT [dbo].[HoaChatCam] ([ID], [TenHoaChat], [LyDoCam]) VALUES (4, N'Antibiotics', N'Gây kháng thuốc cho người')
INSERT [dbo].[HoaChatCam] ([ID], [TenHoaChat], [LyDoCam]) VALUES (5, N'Hormone tăng trưởng', N'Ảnh hưởng tiêu cực đến con người')
SET IDENTITY_INSERT [dbo].[HoaChatCam] OFF
GO
SET IDENTITY_INSERT [dbo].[lich_su_truy_cap] ON 

INSERT [dbo].[lich_su_truy_cap] ([Id], [NguoiDungID], [ThoiGian], [mota], [IP_address]) VALUES (37, 1, CAST(N'2025-12-18T21:02:40.603' AS DateTime), N'Đăng nhập', N'005056C00001')
INSERT [dbo].[lich_su_truy_cap] ([Id], [NguoiDungID], [ThoiGian], [mota], [IP_address]) VALUES (38, 1, CAST(N'2025-12-18T21:03:02.530' AS DateTime), N'Đăng nhập', N'005056C00001')
INSERT [dbo].[lich_su_truy_cap] ([Id], [NguoiDungID], [ThoiGian], [mota], [IP_address]) VALUES (39, 1, CAST(N'2025-12-18T21:05:37.077' AS DateTime), N'Đăng nhập', N'005056C00001')
INSERT [dbo].[lich_su_truy_cap] ([Id], [NguoiDungID], [ThoiGian], [mota], [IP_address]) VALUES (40, 1, CAST(N'2025-12-19T07:28:05.927' AS DateTime), N'Đăng nhập', N'005056C00001')
INSERT [dbo].[lich_su_truy_cap] ([Id], [NguoiDungID], [ThoiGian], [mota], [IP_address]) VALUES (41, 1, CAST(N'2025-12-19T07:29:23.840' AS DateTime), N'Đăng nhập', N'005056C00001')
INSERT [dbo].[lich_su_truy_cap] ([Id], [NguoiDungID], [ThoiGian], [mota], [IP_address]) VALUES (42, 1, CAST(N'2025-12-19T07:30:06.767' AS DateTime), N'Đăng nhập', N'005056C00001')
INSERT [dbo].[lich_su_truy_cap] ([Id], [NguoiDungID], [ThoiGian], [mota], [IP_address]) VALUES (43, 1, CAST(N'2025-12-19T07:36:39.997' AS DateTime), N'Đăng nhập', N'005056C00001')
INSERT [dbo].[lich_su_truy_cap] ([Id], [NguoiDungID], [ThoiGian], [mota], [IP_address]) VALUES (44, 1, CAST(N'2025-12-19T07:39:02.700' AS DateTime), N'Đăng nhập', N'005056C00001')
INSERT [dbo].[lich_su_truy_cap] ([Id], [NguoiDungID], [ThoiGian], [mota], [IP_address]) VALUES (45, 1, CAST(N'2025-12-19T07:49:08.933' AS DateTime), N'Đăng nhập', N'005056C00001')
INSERT [dbo].[lich_su_truy_cap] ([Id], [NguoiDungID], [ThoiGian], [mota], [IP_address]) VALUES (46, 1, CAST(N'2025-12-19T07:55:03.567' AS DateTime), N'Đăng nhập', N'005056C00001')

SET IDENTITY_INSERT [dbo].[lich_su_truy_cap] OFF
GO
INSERT [dbo].[LoaiHoatDong] ([ID], [TenHoatDong], [mota]) VALUES (1, N'Sản xuất con giống vật nuôi', NULL)
INSERT [dbo].[LoaiHoatDong] ([ID], [TenHoatDong], [mota]) VALUES (2, N'Sản xuất tinh, phôi, ấu trùng', NULL)
INSERT [dbo].[LoaiHoatDong] ([ID], [TenHoatDong], [mota]) VALUES (3, N'Ấp trứng', NULL)
INSERT [dbo].[LoaiHoatDong] ([ID], [TenHoatDong], [mota]) VALUES (4, N'Sở hữu giống vật nuôi để phối giống trực tiếp', NULL)
INSERT [dbo].[LoaiHoatDong] ([ID], [TenHoatDong], [mota]) VALUES (5, N'Mua bán con giống', NULL)
GO
INSERT [dbo].[Menu] ([ID], [TenMenu], [LienKet], [MenuChaID], [CapDo], [TrangThai]) VALUES (1, N'Trang chủ', N'/home', NULL, 1, 1)
INSERT [dbo].[Menu] ([ID], [TenMenu], [LienKet], [MenuChaID], [CapDo], [TrangThai]) VALUES (2, N'Quản lý người dùng', N'/user-management', 1, 2, 1)
INSERT [dbo].[Menu] ([ID], [TenMenu], [LienKet], [MenuChaID], [CapDo], [TrangThai]) VALUES (3, N'Quản lý chức vụ', N'/role-management', 1, 2, 1)
INSERT [dbo].[Menu] ([ID], [TenMenu], [LienKet], [MenuChaID], [CapDo], [TrangThai]) VALUES (4, N'Báo cáo', N'/reports', NULL, 1, 1)
INSERT [dbo].[Menu] ([ID], [TenMenu], [LienKet], [MenuChaID], [CapDo], [TrangThai]) VALUES (5, N'Thống kê', N'/statistics', 4, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[NguoiDung] ON 

INSERT [dbo].[NguoiDung] ([ID], [HoTen], [TenDN], [MatKhau], [email], [trang_thai], [ChucVu_ID], [DonVi_HC_ID]) VALUES (1, N'Đỗ Phương Điệp', N'dophuongdiep', N'1234', N'diep@gmail.com', 1, 1, 1)
INSERT [dbo].[NguoiDung] ([ID], [HoTen], [TenDN], [MatKhau], [email], [trang_thai], [ChucVu_ID], [DonVi_HC_ID]) VALUES (2, N'Hoàng Quốc Trung', N'hoangquoctrung', N'123456', N'trung@gmail.com', 1, 5, 1)
INSERT [dbo].[NguoiDung] ([ID], [HoTen], [TenDN], [MatKhau], [email], [trang_thai], [ChucVu_ID], [DonVi_HC_ID]) VALUES (3, N'Nguyễn Nghĩa Thái Dương', N'thaiduong', N'123456', N'duong@gmail.com', 0, 3, 2)
INSERT [dbo].[NguoiDung] ([ID], [HoTen], [TenDN], [MatKhau], [email], [trang_thai], [ChucVu_ID], [DonVi_HC_ID]) VALUES (4, N'Đào Bảo Quân', N'quan', N'quan123', N'quan@gmail.com', 1, 3, 2)
SET IDENTITY_INSERT [dbo].[NguoiDung] OFF
GO
SET IDENTITY_INSERT [dbo].[NguonGen] ON 

INSERT [dbo].[NguonGen] ([ID], [TenNguonGen], [MoTa]) VALUES (1, N'Nguồn gen bò sữa', N'Bò sữa năng suất cao')
INSERT [dbo].[NguonGen] ([ID], [TenNguonGen], [MoTa]) VALUES (2, N'Nguồn gen lợn rừng', N'Lợn rừng kháng bệnh')
INSERT [dbo].[NguonGen] ([ID], [TenNguonGen], [MoTa]) VALUES (3, N'Nguồn gen gà Đông Tảo', N'Gà thịt ngon, chất lượng cao')
INSERT [dbo].[NguonGen] ([ID], [TenNguonGen], [MoTa]) VALUES (4, N'Nguồn gen vịt trời', N'Vịt trời dễ nuôi')
INSERT [dbo].[NguonGen] ([ID], [TenNguonGen], [MoTa]) VALUES (5, N'Nguồn gen dê cỏ', N'Dê phù hợp địa hình khó khăn')
INSERT [dbo].[NguonGen] ([ID], [TenNguonGen], [MoTa]) VALUES (6, N'Hổ', NULL)
INSERT [dbo].[NguonGen] ([ID], [TenNguonGen], [MoTa]) VALUES (7, N'sdf', NULL)
SET IDENTITY_INSERT [dbo].[NguonGen] OFF
GO
SET IDENTITY_INSERT [dbo].[NguyenLieuChoPhep] ON 

INSERT [dbo].[NguyenLieuChoPhep] ([ID], [TenNguyenLieu], [MoTa]) VALUES (1, N'Bột ngô', N'Nguyên liệu tự nhiên')
INSERT [dbo].[NguyenLieuChoPhep] ([ID], [TenNguyenLieu], [MoTa]) VALUES (2, N'Bột sắn', N'Nguyên liệu tự nhiên')
INSERT [dbo].[NguyenLieuChoPhep] ([ID], [TenNguyenLieu], [MoTa]) VALUES (3, N'Cám gạo', N'Nguyên liệu tự nhiên')
INSERT [dbo].[NguyenLieuChoPhep] ([ID], [TenNguyenLieu], [MoTa]) VALUES (4, N'Khoai mì', N'Nguyên liệu tự nhiên')
INSERT [dbo].[NguyenLieuChoPhep] ([ID], [TenNguyenLieu], [MoTa]) VALUES (5, N'Cỏ khô', N'Nguyên liệu tự nhiên')
INSERT [dbo].[NguyenLieuChoPhep] ([ID], [TenNguyenLieu], [MoTa]) VALUES (7, N'Cỏ khô 1', N'sdf')
SET IDENTITY_INSERT [dbo].[NguyenLieuChoPhep] OFF
GO
SET IDENTITY_INSERT [dbo].[Nhom] ON 

INSERT [dbo].[Nhom] ([ID], [Ten], [mota], [NgayTao]) VALUES (1, N'Quản lý hệ thống', N'Nhóm quản trị', CAST(N'2025-12-01T08:00:00.000' AS DateTime))
INSERT [dbo].[Nhom] ([ID], [Ten], [mota], [NgayTao]) VALUES (2, N'Nhân viên kỹ thuật', N'Nhóm kỹ thuật', CAST(N'2025-12-01T09:00:00.000' AS DateTime))
INSERT [dbo].[Nhom] ([ID], [Ten], [mota], [NgayTao]) VALUES (3, N'Quản lý giống', N'Nhóm giống vật nuôi', CAST(N'2025-12-01T10:00:00.000' AS DateTime))
INSERT [dbo].[Nhom] ([ID], [Ten], [mota], [NgayTao]) VALUES (4, N'Trợ lý', N'Hỗ trợ các nhóm', CAST(N'2025-12-01T11:00:00.000' AS DateTime))
INSERT [dbo].[Nhom] ([ID], [Ten], [mota], [NgayTao]) VALUES (5, N'Thống kê', N'Báo cáo số liệu', CAST(N'2025-12-01T12:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Nhom] OFF
GO
INSERT [dbo].[phan_quyen] ([MaQuyen], [ten_quyen], [mota]) VALUES (N'PQ01', N'Truy cập hệ thống', N'Quyền truy cập cơ bản')
INSERT [dbo].[phan_quyen] ([MaQuyen], [ten_quyen], [mota]) VALUES (N'PQ02', N'Quản lý dữ liệu', N'Quyền cập nhật dữ liệu')
INSERT [dbo].[phan_quyen] ([MaQuyen], [ten_quyen], [mota]) VALUES (N'PQ03', N'Báo cáo thống kê', N'Quyền xuất báo cáo')
INSERT [dbo].[phan_quyen] ([MaQuyen], [ten_quyen], [mota]) VALUES (N'PQ04', N'Thêm người dùng', N'Quyền thêm mới người dùng')
INSERT [dbo].[phan_quyen] ([MaQuyen], [ten_quyen], [mota]) VALUES (N'PQ05', N'Tùy chỉnh hệ thống', N'Quyền thay đổi cài đặt')
INSERT [dbo].[phan_quyen] ([MaQuyen], [ten_quyen], [mota]) VALUES (N'Q01', N'Quản trị', N'Tất cả quyền')
INSERT [dbo].[phan_quyen] ([MaQuyen], [ten_quyen], [mota]) VALUES (N'Q02', N'Thêm dữ liệu', N'Cho phép thêm mới')
INSERT [dbo].[phan_quyen] ([MaQuyen], [ten_quyen], [mota]) VALUES (N'Q03', N'Sửa dữ liệu', N'Cho phép chỉnh sửa')
INSERT [dbo].[phan_quyen] ([MaQuyen], [ten_quyen], [mota]) VALUES (N'Q04', N'Xóa dữ liệu', N'Cho phép xóa')
INSERT [dbo].[phan_quyen] ([MaQuyen], [ten_quyen], [mota]) VALUES (N'Q05', N'Xem dữ liệu', N'Chỉ xem thông tin')
GO
INSERT [dbo].[phan_quyen_nguoi_dung] ([NguoiDungID], [ma_quyen]) VALUES (1, N'PQ04')
INSERT [dbo].[phan_quyen_nguoi_dung] ([NguoiDungID], [ma_quyen]) VALUES (1, N'PQ05')
INSERT [dbo].[phan_quyen_nguoi_dung] ([NguoiDungID], [ma_quyen]) VALUES (1, N'Q03')
INSERT [dbo].[phan_quyen_nguoi_dung] ([NguoiDungID], [ma_quyen]) VALUES (2, N'PQ01')
INSERT [dbo].[phan_quyen_nguoi_dung] ([NguoiDungID], [ma_quyen]) VALUES (2, N'PQ02')
INSERT [dbo].[phan_quyen_nguoi_dung] ([NguoiDungID], [ma_quyen]) VALUES (2, N'PQ04')
INSERT [dbo].[phan_quyen_nguoi_dung] ([NguoiDungID], [ma_quyen]) VALUES (3, N'PQ04')
GO
INSERT [dbo].[phan_quyen_nhom] ([NhomID], [ma_quyen]) VALUES (1, N'PQ01')
INSERT [dbo].[phan_quyen_nhom] ([NhomID], [ma_quyen]) VALUES (1, N'PQ05')
INSERT [dbo].[phan_quyen_nhom] ([NhomID], [ma_quyen]) VALUES (1, N'Q01')
INSERT [dbo].[phan_quyen_nhom] ([NhomID], [ma_quyen]) VALUES (2, N'PQ02')
INSERT [dbo].[phan_quyen_nhom] ([NhomID], [ma_quyen]) VALUES (2, N'Q02')
INSERT [dbo].[phan_quyen_nhom] ([NhomID], [ma_quyen]) VALUES (3, N'PQ03')
INSERT [dbo].[phan_quyen_nhom] ([NhomID], [ma_quyen]) VALUES (3, N'Q03')
INSERT [dbo].[phan_quyen_nhom] ([NhomID], [ma_quyen]) VALUES (4, N'PQ04')
INSERT [dbo].[phan_quyen_nhom] ([NhomID], [ma_quyen]) VALUES (4, N'Q04')
INSERT [dbo].[phan_quyen_nhom] ([NhomID], [ma_quyen]) VALUES (5, N'PQ05')
INSERT [dbo].[phan_quyen_nhom] ([NhomID], [ma_quyen]) VALUES (5, N'Q05')
GO
INSERT [dbo].[ThanhVienNhom] ([NguoiDungID], [NhomID], [LaTruongNhom], [NgayThamGia]) VALUES (1, 3, NULL, CAST(N'2025-12-12T04:00:20.370' AS DateTime))
INSERT [dbo].[ThanhVienNhom] ([NguoiDungID], [NhomID], [LaTruongNhom], [NgayThamGia]) VALUES (1, 5, NULL, CAST(N'2025-12-18T20:38:43.817' AS DateTime))
INSERT [dbo].[ThanhVienNhom] ([NguoiDungID], [NhomID], [LaTruongNhom], [NgayThamGia]) VALUES (2, 1, NULL, NULL)
INSERT [dbo].[ThanhVienNhom] ([NguoiDungID], [NhomID], [LaTruongNhom], [NgayThamGia]) VALUES (2, 4, NULL, CAST(N'2025-12-12T04:10:38.877' AS DateTime))
INSERT [dbo].[ThanhVienNhom] ([NguoiDungID], [NhomID], [LaTruongNhom], [NgayThamGia]) VALUES (3, 3, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[ThucAnChanNuoi] ON 

INSERT [dbo].[ThucAnChanNuoi] ([ID], [TenThucAn], [MoTa], [LoaiThucAn]) VALUES (1, N'Cám con cò', N'Dùng cho lợn', N'Thương mại')
INSERT [dbo].[ThucAnChanNuoi] ([ID], [TenThucAn], [MoTa], [LoaiThucAn]) VALUES (2, N'Cỏ voi', N'Thức ăn cho bò', N'Tự nhiên')
INSERT [dbo].[ThucAnChanNuoi] ([ID], [TenThucAn], [MoTa], [LoaiThucAn]) VALUES (3, N'Ngô hạt', N'Thức ăn cho gia cầm', N'Khảo nghiệm')
INSERT [dbo].[ThucAnChanNuoi] ([ID], [TenThucAn], [MoTa], [LoaiThucAn]) VALUES (4, N'Trấu nghiền', N'Thức ăn cho dê', N'Khảo nghiệm')
INSERT [dbo].[ThucAnChanNuoi] ([ID], [TenThucAn], [MoTa], [LoaiThucAn]) VALUES (5, N'Thóc giống', N'Thức ăn cho gà', N'Thương mại')
SET IDENTITY_INSERT [dbo].[ThucAnChanNuoi] OFF
GO
SET IDENTITY_INSERT [dbo].[ToChucCaNhan] ON 

INSERT [dbo].[ToChucCaNhan] ([ID], [Ten], [DiaChi], [SoDienThoai], [Email], [LoaiHinh], [LoaiHoatDong]) VALUES (1, N'Công ty ABC', N'Số 1, phố X, Hà Nội', N'0123456782', N'abc@gmail.com', N'Tổ chức', N'Sản xuất con giống vật nuôi')
INSERT [dbo].[ToChucCaNhan] ([ID], [Ten], [DiaChi], [SoDienThoai], [Email], [LoaiHinh], [LoaiHoatDong]) VALUES (2, N'Tổ chức DEF', N'Số 2, phố Y, Hà Nội', N'0987654321', N'def@gmail.com', N'Tổ chức', N'Sản xuất tinh, phôi, ấu trùng')
INSERT [dbo].[ToChucCaNhan] ([ID], [Ten], [DiaChi], [SoDienThoai], [Email], [LoaiHinh], [LoaiHoatDong]) VALUES (3, N'Công ty GHI', N'Số 3, phố Z, TP.HCM', N'0912345678', N'ghi@gmail.com', N'Tổ chức', N'Ấp trứng')
INSERT [dbo].[ToChucCaNhan] ([ID], [Ten], [DiaChi], [SoDienThoai], [Email], [LoaiHinh], [LoaiHoatDong]) VALUES (4, N'Tư nhân Nguyễn Văn A', N'Số 4, làng X, Hà Nội', N'0934567891', N'nva@gmail.com', N'Cá nhân', N'Sở hữu giống vật nuôi để phối giống trực tiếp')
INSERT [dbo].[ToChucCaNhan] ([ID], [Ten], [DiaChi], [SoDienThoai], [Email], [LoaiHinh], [LoaiHoatDong]) VALUES (5, N'Công ty JKL', N'Số 5, phố W, Đà Nẵng', N'0945678902', N'jkl@gmail.com', N'Tổ chức', N'Mua bán con giống')
INSERT [dbo].[ToChucCaNhan] ([ID], [Ten], [DiaChi], [SoDienThoai], [Email], [LoaiHinh], [LoaiHoatDong]) VALUES (8, N'Công ty ABC', N'Số 1, phố X, Hà Nội', N'0123456788', N'', N'Tổ chức', N'Sản xuất con giống vật nuôi')
SET IDENTITY_INSERT [dbo].[ToChucCaNhan] OFF
GO
SET IDENTITY_INSERT [dbo].[ToChucNguonGen] ON 

INSERT [dbo].[ToChucNguonGen] ([ID], [ToChucCaNhanID], [NguonGenID], [KhuVuc], [NgayThuThap], [HoatDong]) VALUES (1, 1, 1, N'Khu vực Hà Nội', CAST(N'2025-12-02T00:00:00.000' AS DateTime), N'Thu thập')
INSERT [dbo].[ToChucNguonGen] ([ID], [ToChucCaNhanID], [NguonGenID], [KhuVuc], [NgayThuThap], [HoatDong]) VALUES (2, 2, 2, N'Khu vực TP.HCM', CAST(N'2025-12-03T00:00:00.000' AS DateTime), N'Bảo tồn')
INSERT [dbo].[ToChucNguonGen] ([ID], [ToChucCaNhanID], [NguonGenID], [KhuVuc], [NgayThuThap], [HoatDong]) VALUES (3, 3, 3, N'Khu vực Đà Nẵng', CAST(N'2025-12-05T00:00:00.000' AS DateTime), N'Khai thác')
INSERT [dbo].[ToChucNguonGen] ([ID], [ToChucCaNhanID], [NguonGenID], [KhuVuc], [NgayThuThap], [HoatDong]) VALUES (4, 4, 4, N'Khu vực miền núi', CAST(N'2025-12-06T00:00:00.000' AS DateTime), N'Bảo tồn')
INSERT [dbo].[ToChucNguonGen] ([ID], [ToChucCaNhanID], [NguonGenID], [KhuVuc], [NgayThuThap], [HoatDong]) VALUES (5, 4, 5, N'Khu vực đồng bằng', CAST(N'2025-12-07T00:00:00.000' AS DateTime), N'Thu thập')
INSERT [dbo].[ToChucNguonGen] ([ID], [ToChucCaNhanID], [NguonGenID], [KhuVuc], [NgayThuThap], [HoatDong]) VALUES (7, 2, 7, N'sefsef', CAST(N'2025-12-19T11:40:17.710' AS DateTime), N'Thu thập')
INSERT [dbo].[ToChucNguonGen] ([ID], [ToChucCaNhanID], [NguonGenID], [KhuVuc], [NgayThuThap], [HoatDong]) VALUES (8, 2, 5, N'sefsef', CAST(N'2025-12-19T11:40:17.710' AS DateTime), N'Thu thập')
SET IDENTITY_INSERT [dbo].[ToChucNguonGen] OFF
GO
ALTER TABLE [dbo].[GiayChungNhan] ADD  DEFAULT (N'Cục chăn nuôi') FOR [NoiCap]
GO
ALTER TABLE [dbo].[LichSuTacDong] ADD  DEFAULT (getdate()) FOR [ThoiGian]
GO
ALTER TABLE [dbo].[Menu] ADD  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[ToChucCaNhan] ADD  DEFAULT (N'Tổ chức') FOR [LoaiHinh]
GO
ALTER TABLE [dbo].[CoSo_HoaChatCam]  WITH CHECK ADD FOREIGN KEY([CoSoThucAnID])
REFERENCES [dbo].[CoSoThucAn] ([ID])
GO
ALTER TABLE [dbo].[CoSo_HoaChatCam]  WITH CHECK ADD FOREIGN KEY([HoaChatCamID])
REFERENCES [dbo].[HoaChatCam] ([ID])
GO
ALTER TABLE [dbo].[CoSo_NguyenLieuChoPhep]  WITH CHECK ADD FOREIGN KEY([CoSoThucAnID])
REFERENCES [dbo].[CoSoThucAn] ([ID])
GO
ALTER TABLE [dbo].[CoSo_NguyenLieuChoPhep]  WITH CHECK ADD FOREIGN KEY([NguyenLieuID])
REFERENCES [dbo].[NguyenLieuChoPhep] ([ID])
GO
ALTER TABLE [dbo].[CoSoThucAn]  WITH CHECK ADD FOREIGN KEY([ThucAnChanNuoiID])
REFERENCES [dbo].[ThucAnChanNuoi] ([ID])
GO
ALTER TABLE [dbo].[CoSoThucAn]  WITH CHECK ADD FOREIGN KEY([ToChucCaNhanID])
REFERENCES [dbo].[ToChucCaNhan] ([ID])
GO
ALTER TABLE [dbo].[CoSoVatNuoi]  WITH CHECK ADD FOREIGN KEY([GiongVatNuoiID])
REFERENCES [dbo].[GiongVatNuoi] ([ID])
GO
ALTER TABLE [dbo].[CoSoVatNuoi]  WITH CHECK ADD FOREIGN KEY([ToChucCaNhanID])
REFERENCES [dbo].[ToChucCaNhan] ([ID])
GO
ALTER TABLE [dbo].[DonVi_HC]  WITH CHECK ADD FOREIGN KEY([Cap_HC_ID])
REFERENCES [dbo].[Cap_HC] ([ID])
GO
ALTER TABLE [dbo].[DonVi_HC]  WITH CHECK ADD FOREIGN KEY([TrucThuoc])
REFERENCES [dbo].[DonVi_HC] ([ID])
GO
ALTER TABLE [dbo].[GiayChungNhan]  WITH CHECK ADD FOREIGN KEY([CoSoThucAnID])
REFERENCES [dbo].[CoSoThucAn] ([ID])
GO
ALTER TABLE [dbo].[GiongCanBaoTon]  WITH CHECK ADD FOREIGN KEY([GiongID])
REFERENCES [dbo].[GiongVatNuoi] ([ID])
GO
ALTER TABLE [dbo].[lich_su_truy_cap]  WITH CHECK ADD FOREIGN KEY([NguoiDungID])
REFERENCES [dbo].[NguoiDung] ([ID])
GO
ALTER TABLE [dbo].[LichSuTacDong]  WITH CHECK ADD FOREIGN KEY([NguoiDungID])
REFERENCES [dbo].[NguoiDung] ([ID])
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD FOREIGN KEY([MenuChaID])
REFERENCES [dbo].[Menu] ([ID])
GO
ALTER TABLE [dbo].[NguoiDung]  WITH CHECK ADD FOREIGN KEY([ChucVu_ID])
REFERENCES [dbo].[ChucVu] ([ID])
GO
ALTER TABLE [dbo].[NguoiDung]  WITH CHECK ADD FOREIGN KEY([DonVi_HC_ID])
REFERENCES [dbo].[DonVi_HC] ([ID])
GO
ALTER TABLE [dbo].[phan_quyen_nguoi_dung]  WITH CHECK ADD FOREIGN KEY([ma_quyen])
REFERENCES [dbo].[phan_quyen] ([MaQuyen])
GO
ALTER TABLE [dbo].[phan_quyen_nguoi_dung]  WITH CHECK ADD FOREIGN KEY([NguoiDungID])
REFERENCES [dbo].[NguoiDung] ([ID])
GO
ALTER TABLE [dbo].[phan_quyen_nhom]  WITH CHECK ADD FOREIGN KEY([ma_quyen])
REFERENCES [dbo].[phan_quyen] ([MaQuyen])
GO
ALTER TABLE [dbo].[phan_quyen_nhom]  WITH CHECK ADD FOREIGN KEY([NhomID])
REFERENCES [dbo].[Nhom] ([ID])
GO
ALTER TABLE [dbo].[ThanhVienNhom]  WITH CHECK ADD FOREIGN KEY([NguoiDungID])
REFERENCES [dbo].[NguoiDung] ([ID])
GO
ALTER TABLE [dbo].[ThanhVienNhom]  WITH CHECK ADD FOREIGN KEY([NhomID])
REFERENCES [dbo].[Nhom] ([ID])
GO
ALTER TABLE [dbo].[ToChucNguonGen]  WITH CHECK ADD FOREIGN KEY([NguonGenID])
REFERENCES [dbo].[NguonGen] ([ID])
GO
ALTER TABLE [dbo].[ToChucNguonGen]  WITH CHECK ADD FOREIGN KEY([ToChucCaNhanID])
REFERENCES [dbo].[ToChucCaNhan] ([ID])
GO
ALTER TABLE NguoiDung
ALTER COLUMN MatKhau NVARCHAR(100);
GO
ALTER TABLE Nhom 
ADD TrangThai BIT DEFAULT 1 WITH VALUES;
GO
-- Bước 1: Copy đoạn này vào SSMS
UPDATE NguoiDung
SET MatKhau = '81dc9bdb52d04dc20036dbd8313ed055' -- Đây là mã MD5 của: 1234
WHERE TenDn = 'dophuongdiep' -- 
UPDATE NguoiDung
SET MatKhau = 'e10adc3949ba59abbe56e057f20f883e' -- Đây là mã MD5 của: 123456
WHERE TenDn = 'hoangquoctrung' -- )
UPDATE NguoiDung
SET MatKhau = 'e10adc3949ba59abbe56e057f20f883e' -- Đây là mã MD5 của: 123456
WHERE TenDn = 'thaiduong' -- 
UPDATE NguoiDung
SET MatKhau = 'e10adc3949ba59abbe56e057f20f883e' -- Đây là mã MD5 của: 123456
WHERE TenDn = 'quan' -- 
GO
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
ALTER TABLE lich_su_truy_cap
ADD TrinhDuyet nvarchar(255) NULL;
USE [master]
GO
ALTER DATABASE [QuanLyGiongVaThucAnChanNuoi] SET  READ_WRITE 
GO
