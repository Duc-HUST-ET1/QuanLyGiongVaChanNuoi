using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Infrastructure.Models; // Namespace chứa các Model
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuanLyGiongChanNuoi.Infrastructure.Data // Đảm bảo namespace đúng với thư mục
{
    public partial class QuanLyGiongVaThucAnChanNuoiContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuanLyGiongVaThucAnChanNuoiContext()
        {
        }

        // Constructor nhận IHttpContextAccessor
        public QuanLyGiongVaThucAnChanNuoiContext(
            DbContextOptions<QuanLyGiongVaThucAnChanNuoiContext> options,
            IHttpContextAccessor httpContextAccessor = null)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // =========================================================
        // KHAI BÁO ĐẦY ĐỦ CÁC BẢNG (DBSET)
        // =========================================================
        public virtual DbSet<CapHc> CapHcs { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<CoSoHoaChatCam> CoSoHoaChatCams { get; set; }
        public virtual DbSet<CoSoNguyenLieuChoPhep> CoSoNguyenLieuChoPheps { get; set; }
        public virtual DbSet<CoSoThucAn> CoSoThucAns { get; set; }
        public virtual DbSet<CoSoVatNuoi> CoSoVatNuois { get; set; }
        public virtual DbSet<DonViHc> DonViHcs { get; set; }
        public virtual DbSet<GiayChungNhan> GiayChungNhans { get; set; }
        public virtual DbSet<GiongCanBaoTon> GiongCanBaoTons { get; set; }
        public virtual DbSet<GiongVatNuoi> GiongVatNuois { get; set; }
        public virtual DbSet<HoaChatCam> HoaChatCams { get; set; }
        public virtual DbSet<LichSuTacDong> LichSuTacDongs { get; set; }
        public virtual DbSet<LichSuTruyCap> LichSuTruyCaps { get; set; }
        public virtual DbSet<LoaiHoatDong> LoaiHoatDongs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NguonGen> NguonGens { get; set; }
        public virtual DbSet<NguyenLieuChoPhep> NguyenLieuChoPheps { get; set; }
        public virtual DbSet<Nhom> Nhoms { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

        // === HAI BẢNG BẠN ĐANG THIẾU ĐÂY ===
        public virtual DbSet<PhanQuyenNhom> PhanQuyenNhoms { get; set; }
        public virtual DbSet<PhanQuyenNguoiDung> PhanQuyenNguoiDungs { get; set; }
        // =====================================

        public virtual DbSet<ThanhVienNhom> ThanhVienNhoms { get; set; }
        public virtual DbSet<ThucAnChanNuoi> ThucAnChanNuois { get; set; }
        public virtual DbSet<ToChucCaNhan> ToChucCaNhans { get; set; }
        public virtual DbSet<ToChucNguonGen> ToChucNguonGens { get; set; }

        public int CurrentUserId { get; set; } = 1;

        // =========================================================
        // CẤU HÌNH LIÊN KẾT (ON MODEL CREATING)
        // =========================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhanQuyenNhom>(entity =>
            {
                entity.ToTable("phan_quyen_nhom");
                entity.HasKey(e => new { e.NhomId, e.MaQuyen });

                entity.Property(e => e.MaQuyen).HasMaxLength(20).HasColumnName("ma_quyen");
                entity.Property(e => e.NhomId).HasColumnName("NhomID");

                entity.HasOne(d => d.MaQuyenNavigation)
                    .WithMany(p => p.PhanQuyenNhoms)
                    .HasForeignKey(d => d.MaQuyen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhanQuyenNhom_PhanQuyen");

                entity.HasOne(d => d.Nhom)
                    .WithMany(p => p.PhanQuyenNhoms)
                    .HasForeignKey(d => d.NhomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhanQuyenNhom_Nhom");
            });

            modelBuilder.Entity<PhanQuyenNguoiDung>(entity =>
            {
                entity.ToTable("phan_quyen_nguoi_dung");
                entity.HasKey(e => new { e.NguoiDungId, e.MaQuyen });

                entity.Property(e => e.MaQuyen).HasMaxLength(20).HasColumnName("ma_quyen");
                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.HasOne(d => d.MaQuyenNavigation)
                    .WithMany(p => p.PhanQuyenNguoiDungs)
                    .HasForeignKey(d => d.MaQuyen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhanQuyenNguoiDung_PhanQuyen");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.PhanQuyenNguoiDungs)
                    .HasForeignKey(d => d.NguoiDungId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhanQuyenNguoiDung_NguoiDung");
            });

            // --- Cấu hình các bảng khác (Rút gọn để file đỡ dài, logic vẫn giữ nguyên) ---

            modelBuilder.Entity<CapHc>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__Cap_HC__3214EC2748878E1E");
                entity.ToTable("Cap_HC");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Ten).HasMaxLength(50);
            });

            modelBuilder.Entity<ChucVu>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__ChucVu__3214EC2704C3421E");
                entity.ToTable("ChucVu");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Mota).HasMaxLength(100).HasColumnName("mota");
                entity.Property(e => e.TenChucVu).HasMaxLength(40);
            });

            modelBuilder.Entity<CoSoHoaChatCam>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__CoSo_Hoa__3214EC2771192F1B");
                entity.ToTable("CoSo_HoaChatCam");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CoSoThucAnId).HasColumnName("CoSoThucAnID");
                entity.Property(e => e.HoaChatCamId).HasColumnName("HoaChatCamID");
                entity.Property(e => e.LyDoSuDung).HasMaxLength(200);
                entity.HasOne(d => d.CoSoThucAn).WithMany(p => p.CoSoHoaChatCams).HasForeignKey(d => d.CoSoThucAnId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__CoSo_HoaC__CoSoT__1BC821DD");
                entity.HasOne(d => d.HoaChatCam).WithMany(p => p.CoSoHoaChatCams).HasForeignKey(d => d.HoaChatCamId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__CoSo_HoaC__HoaCh__1CBC4616");
            });

            modelBuilder.Entity<CoSoNguyenLieuChoPhep>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__CoSo_Ngu__3214EC27B9B7253B");
                entity.ToTable("CoSo_NguyenLieuChoPhep");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CoSoThucAnId).HasColumnName("CoSoThucAnID");
                entity.Property(e => e.NguyenLieuId).HasColumnName("NguyenLieuID");
                entity.HasOne(d => d.CoSoThucAn).WithMany(p => p.CoSoNguyenLieuChoPheps).HasForeignKey(d => d.CoSoThucAnId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__CoSo_Nguy__CoSoT__1DB06A4F");
                entity.HasOne(d => d.NguyenLieu).WithMany(p => p.CoSoNguyenLieuChoPheps).HasForeignKey(d => d.NguyenLieuId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__CoSo_Nguy__Nguye__1EA48E88");
            });

            modelBuilder.Entity<CoSoThucAn>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__CoSoThuc__3214EC27CAE7BD72");
                entity.ToTable("CoSoThucAn");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DiaChi).HasMaxLength(200);
                entity.Property(e => e.LoaiCoSo).HasMaxLength(50);
                entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
                entity.Property(e => e.TenCoSo).HasMaxLength(100);
                entity.Property(e => e.ThucAnChanNuoiId).HasColumnName("ThucAnChanNuoiID");
                entity.Property(e => e.ToChucCaNhanId).HasColumnName("ToChucCaNhanID");
                entity.HasOne(d => d.ThucAnChanNuoi).WithMany(p => p.CoSoThucAns).HasForeignKey(d => d.ThucAnChanNuoiId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__CoSoThucA__ThucA__1F98B2C1");
                entity.HasOne(d => d.ToChucCaNhan).WithMany(p => p.CoSoThucAns).HasForeignKey(d => d.ToChucCaNhanId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__CoSoThucA__ToChu__208CD6FA");
            });

            modelBuilder.Entity<CoSoVatNuoi>(entity =>
            {
                entity.ToTable("CoSoVatNuoi");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                // SỬA: Đổi e.GiongVatNuoiId thành e.GiongVatNuoiID (chữ D hoa)
                entity.Property(e => e.GiongVatNuoiID).HasColumnName("GiongVatNuoiID");

                entity.Property(e => e.LoaiCoSo).HasMaxLength(50);

                entity.Property(e => e.TenCoSo).HasMaxLength(100);

                // SỬA: Đổi e.ToChucCaNhanId thành e.ToChucCaNhanID (chữ D hoa)
                entity.Property(e => e.ToChucCaNhanID).HasColumnName("ToChucCaNhanID");

                // SỬA: Đổi e.Trangthai thành e.TrangThai (chữ T hoa)
                entity.Property(e => e.TrangThai).HasColumnName("trangthai");

                // Cấu hình khóa ngoại (Foreign Keys)
                entity.HasOne(d => d.GiongVatNuoi)
                    .WithMany(p => p.CoSoVatNuois) // Đảm bảo trong model GiongVatNuoi có ICollection<CoSoVatNuoi>
                    .HasForeignKey(d => d.GiongVatNuoiID) // Sửa thành ID hoa
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSoVatNu__Giong__4CA06362"); // Tên constraint có thể khác tùy DB của bạn, cứ giữ nguyên nếu code cũ có

                entity.HasOne(d => d.ToChucCaNhan)
                    .WithMany(p => p.CoSoVatNuois) // Đảm bảo trong model ToChucCaNhan có ICollection<CoSoVatNuoi>
                    .HasForeignKey(d => d.ToChucCaNhanID) // Sửa thành ID hoa
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSoVatNu__ToChu__4BAC3F29");
            });

            modelBuilder.Entity<DonViHc>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__DonVi_HC__3214EC2757BA58E4");
                entity.ToTable("DonVi_HC");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CapHcId).HasColumnName("Cap_HC_ID");
                entity.Property(e => e.MaBuuDien).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Ten).HasMaxLength(50);
                entity.HasOne(d => d.CapHc).WithMany(p => p.DonViHcs).HasForeignKey(d => d.CapHcId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__DonVi_HC__Cap_HC__236943A5");
                entity.HasOne(d => d.TrucThuocNavigation).WithMany(p => p.InverseTrucThuocNavigation).HasForeignKey(d => d.TrucThuoc).HasConstraintName("FK__DonVi_HC__TrucTh__245D67DE");
            });

            modelBuilder.Entity<GiayChungNhan>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__GiayChun__3214EC279B3FA250");
                entity.ToTable("GiayChungNhan");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CoSoThucAnId).HasColumnName("CoSoThucAnID");
                entity.Property(e => e.MoTa).HasMaxLength(200);
                entity.Property(e => e.NoiCap).HasMaxLength(50).HasDefaultValue("Cục chăn nuôi");
                entity.Property(e => e.SoGiayChungNhan).HasMaxLength(50);
                entity.HasOne(d => d.CoSoThucAn).WithMany(p => p.GiayChungNhans).HasForeignKey(d => d.CoSoThucAnId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__GiayChung__CoSoT__25518C17");
            });

            modelBuilder.Entity<GiongCanBaoTon>(entity =>
            {
                entity.ToTable("GiongCanBaoTon");

                entity.Property(e => e.Id).HasColumnName("ID");

                // SỬA: Đổi e.GiongID (hoặc e.Giong) thành e.GiongId (cho khớp với Model)
                entity.Property(e => e.GiongId).HasColumnName("GiongID");

                entity.Property(e => e.Loai).HasMaxLength(50);
                entity.Property(e => e.LyDo).HasMaxLength(200);
                entity.Property(e => e.NgayBaoTon).HasColumnType("date");
                entity.Property(e => e.TrangThai).HasColumnName("TrangThai");

                // SỬA QUAN TRỌNG: Đổi d.Giong thành d.GiongVatNuoi
                entity.HasOne(d => d.GiongVatNuoi)
                    .WithMany(p => p.GiongCanBaoTons) // Đảm bảo trong model GiongVatNuoi có collection này, nếu lỗi xóa dòng này đi
                    .HasForeignKey(d => d.GiongId) // Sửa thành GiongId
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GiongCanB__Giong__4E88ABD4"); // Giữ nguyên hoặc xóa dòng này cũng được
            });

            modelBuilder.Entity<GiongVatNuoi>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__GiongVat__3214EC273D5A2728");
                entity.ToTable("GiongVatNuoi");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Loai).HasMaxLength(40);
                entity.Property(e => e.MoTa).HasMaxLength(100);
                entity.Property(e => e.TenGiong).HasMaxLength(50);
            });

            modelBuilder.Entity<HoaChatCam>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__HoaChatC__3214EC27A85B93AB");
                entity.ToTable("HoaChatCam");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.LyDoCam).HasMaxLength(200);
                entity.Property(e => e.TenHoaChat).HasMaxLength(100);
            });

            modelBuilder.Entity<LichSuTacDong>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__LichSuTa__3214EC2725CF571E");
                entity.ToTable("LichSuTacDong");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.BangLienQuan).HasMaxLength(100);
                entity.Property(e => e.HoatDong).HasMaxLength(200);
                entity.Property(e => e.KhoaChinh).HasMaxLength(50);
                entity.Property(e => e.MoTaThem).HasMaxLength(200);
                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");
                entity.Property(e => e.ThoiGian).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
                entity.HasOne(d => d.NguoiDung).WithMany(p => p.LichSuTacDongs).HasForeignKey(d => d.NguoiDungId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__LichSuTac__Nguoi__282DF8C2");
            });

            modelBuilder.Entity<LichSuTruyCap>(entity => {
                entity.ToTable("lich_su_truy_cap");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");
                entity.Property(e => e.ThoiGian).HasColumnType("datetime");
                entity.Property(e => e.DiaChiIP).HasMaxLength(40).HasColumnName("IP_address");
                entity.Property(e => e.HanhDong).HasMaxLength(50).HasColumnName("mota");
                entity.Property(e => e.TrinhDuyet).HasMaxLength(250);
                entity.HasOne(d => d.NguoiDung).WithMany().HasForeignKey(d => d.NguoiDungId).HasConstraintName("FK_LichSu_NguoiDung");
            });

            modelBuilder.Entity<LoaiHoatDong>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__LoaiHoat__3214EC27F3B7BA37");
                entity.ToTable("LoaiHoatDong");
                entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("ID");
                entity.Property(e => e.Mota).HasMaxLength(100).HasColumnName("mota");
                entity.Property(e => e.TenHoatDong).HasMaxLength(50);
            });

            modelBuilder.Entity<Menu>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__Menu__3214EC2790171456");
                entity.ToTable("Menu");
                entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("ID");
                entity.Property(e => e.LienKet).HasMaxLength(200);
                entity.Property(e => e.MenuChaId).HasColumnName("MenuChaID");
                entity.Property(e => e.TenMenu).HasMaxLength(100);
                entity.Property(e => e.TrangThai).HasDefaultValue(true);
                entity.HasOne(d => d.MenuCha).WithMany(p => p.MenuCons).HasForeignKey(d => d.MenuChaId).HasConstraintName("FK__Menu__MenuChaID__29221CFB");
            });

            modelBuilder.Entity<NguoiDung>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__NguoiDun__3214EC272DA7638C");
                entity.ToTable("NguoiDung");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.ChucVuId).HasColumnName("ChucVu_ID");
                entity.Property(e => e.DonViHcId).HasColumnName("DonVi_HC_ID");
                entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
                entity.Property(e => e.HoTen).HasMaxLength(40);
                entity.Property(e => e.MatKhau).HasMaxLength(50);
                entity.Property(e => e.TenDn).HasMaxLength(40).HasColumnName("TenDN");
                entity.Property(e => e.TrangThai).HasColumnName("trang_thai");
                entity.HasOne(d => d.ChucVu).WithMany(p => p.NguoiDungs).HasForeignKey(d => d.ChucVuId).HasConstraintName("FK__NguoiDung__ChucV__2A164134");
                entity.HasOne(d => d.DonViHc).WithMany(p => p.NguoiDungs).HasForeignKey(d => d.DonViHcId).HasConstraintName("FK__NguoiDung__DonVi__2B0A656D");
            });

            modelBuilder.Entity<NguonGen>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__NguonGen__3214EC278128054F");
                entity.ToTable("NguonGen");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.MoTa).HasMaxLength(200);
                entity.Property(e => e.TenNguonGen).HasMaxLength(100);
            });

            modelBuilder.Entity<NguyenLieuChoPhep>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__NguyenLi__3214EC277F1DE846");
                entity.ToTable("NguyenLieuChoPhep");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.MoTa).HasMaxLength(200);
                entity.Property(e => e.TenNguyenLieu).HasMaxLength(100);
            });

            modelBuilder.Entity<Nhom>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__Nhom__3214EC278AA6AC92");
                entity.ToTable("Nhom");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Mota).HasMaxLength(50).HasColumnName("mota");
                entity.Property(e => e.NgayTao).HasColumnType("datetime");
                entity.Property(e => e.Ten).HasMaxLength(50);
            });

            modelBuilder.Entity<PhanQuyen>(entity => {
                entity.HasKey(e => e.MaQuyen).HasName("PK__phan_quy__1D4B7ED440F62DE2");
                entity.ToTable("phan_quyen");
                entity.Property(e => e.MaQuyen).HasMaxLength(20);
                entity.Property(e => e.Mota).HasMaxLength(50).HasColumnName("mota");
                entity.Property(e => e.TenQuyen).HasMaxLength(30).HasColumnName("ten_quyen");
            });

            modelBuilder.Entity<ThanhVienNhom>(entity => {
                entity.HasKey(e => new { e.NguoiDungId, e.NhomId }).HasName("PK__ThanhVie__8A239FDDD8E9DD88");
                entity.ToTable("ThanhVienNhom");
                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");
                entity.Property(e => e.NhomId).HasColumnName("NhomID");
                entity.Property(e => e.NgayThamGia).HasColumnType("datetime");
                entity.HasOne(d => d.NguoiDung).WithMany(p => p.ThanhVienNhoms).HasForeignKey(d => d.NguoiDungId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ThanhVien__Nguoi__2FCF1A8A");
                entity.HasOne(d => d.Nhom).WithMany(p => p.ThanhVienNhoms).HasForeignKey(d => d.NhomId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ThanhVien__NhomI__00200768");
            });

            modelBuilder.Entity<ThucAnChanNuoi>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__ThucAnCh__3214EC2731B28F54");
                entity.ToTable("ThucAnChanNuoi");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.LoaiThucAn).HasMaxLength(50);
                entity.Property(e => e.MoTa).HasMaxLength(200);
                entity.Property(e => e.TenThucAn).HasMaxLength(100);
            });

            modelBuilder.Entity<ToChucCaNhan>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__ToChucCa__3214EC27530C4566");
                entity.ToTable("ToChucCaNhan");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DiaChi).HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.LoaiHinh).HasMaxLength(50).HasDefaultValue("Tổ chức");
                entity.Property(e => e.LoaiHoatDong).HasMaxLength(50);
                entity.Property(e => e.SoDienThoai).HasMaxLength(15);
                entity.Property(e => e.Ten).HasMaxLength(100);
            });

            modelBuilder.Entity<ToChucNguonGen>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__ToChucNg__3214EC27B2F36D25");
                entity.ToTable("ToChucNguonGen");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.HoatDong).HasMaxLength(50);
                entity.Property(e => e.KhuVuc).HasMaxLength(40);
                entity.Property(e => e.NgayThuThap).HasColumnType("datetime");
                entity.Property(e => e.NguonGenId).HasColumnName("NguonGenID");
                entity.Property(e => e.ToChucCaNhanId).HasColumnName("ToChucCaNhanID");
                entity.HasOne(d => d.NguonGen).WithMany(p => p.ToChucNguonGens).HasForeignKey(d => d.NguonGenId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ToChucNgu__Nguon__01142BA1");
                entity.HasOne(d => d.ToChucCaNhan).WithMany(p => p.ToChucNguonGens).HasForeignKey(d => d.ToChucCaNhanId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ToChucNgu__ToChu__02084FDA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        // =========================================================
        // LOGIC LẤY USER ID VÀ GHI LOG (GIỮ NGUYÊN)
        // =========================================================
        private void UpdateCurrentUserId()
        {
            try
            {
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
                {
                    var userName = _httpContextAccessor.HttpContext.User?.Identity?.Name;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        var user = this.NguoiDungs.FirstOrDefault(u => u.TenDn == userName);
                        if (user != null)
                        {
                            CurrentUserId = user.Id;
                        }
                    }
                }
            }
            catch { }
        }

        private void OnBeforeSaveChanges()
        {
            UpdateCurrentUserId();
            ChangeTracker.DetectChanges();
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added
                         || x.State == EntityState.Modified
                         || x.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in modifiedEntries)
            {
                // Bỏ qua cả bảng LichSuTacDong VÀ LichSuTruyCap
                if (entry.Entity is LichSuTacDong || entry.Entity is LichSuTruyCap) continue;

                var log = new LichSuTacDong
                {
                    NguoiDungId = CurrentUserId,
                    ThoiGian = DateTime.Now,
                    BangLienQuan = entry.Entity.GetType().Name,
                };

                string action = "";
                string recordId = "";

                var idProperty = entry.Properties.FirstOrDefault(p => p.Metadata.Name.ToUpper() == "ID");
                if (idProperty != null)
                {
                    recordId = entry.State == EntityState.Added ? "Mới" : (idProperty.CurrentValue?.ToString() ?? "");
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        action = "Thêm mới";
                        log.MoTaThem = $"Thêm mới vào bảng {log.BangLienQuan}";
                        break;
                    case EntityState.Modified:
                        action = "Cập nhật";
                        log.MoTaThem = $"Cập nhật ID {recordId} bảng {log.BangLienQuan}";
                        break;
                    case EntityState.Deleted:
                        action = "Xóa";
                        log.MoTaThem = $"Xóa ID {recordId} khỏi bảng {log.BangLienQuan}";
                        break;
                }

                log.HoatDong = action;
                log.KhoaChinh = recordId;
                this.LichSuTacDongs.Add(log);
            }
        }

        public override int SaveChanges()
        {
            OnBeforeSaveChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}