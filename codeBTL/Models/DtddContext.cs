using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using codeBTL.ViewModels;

namespace codeBTL.Models;

public partial class DtddContext : DbContext
{
    public DtddContext()
    {
    }

    public DtddContext(DbContextOptions<DtddContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitietanh> Chitietanhs { get; set; }

    public virtual DbSet<Chitietddh> Chitietddhs { get; set; }

    public virtual DbSet<Chitiethdb> Chitiethdbs { get; set; }

    public virtual DbSet<Chitiethdn> Chitiethdns { get; set; }

    public virtual DbSet<Chitietsp> Chitietsps { get; set; }

    public virtual DbSet<Chucvu> Chucvus { get; set; }

    public virtual DbSet<Dondathang> Dondathangs { get; set; }

    public virtual DbSet<Hang> Hangs { get; set; }

    public virtual DbSet<Hoadonban> Hoadonbans { get; set; }

    public virtual DbSet<Hoadonnhap> Hoadonnhaps { get; set; }

    public virtual DbSet<Loaisp> Loaisps { get; set; }

    public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    public virtual DbSet<Userinfo> Userinfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-M6VR1FU;Initial Catalog=DTDD7;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitietanh>(entity =>
        {

            entity.HasKey(e => e.MaCTA).HasName("PK__CHITIETANH__27258E765F0F938C");
            entity.ToTable("CHITIETANH");

            entity.Property(e => e.MaSp)
                .HasMaxLength(50)
                .HasColumnName("MaSP");
            entity.Property(e => e.TenFileAnh)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MaSpNavigation).WithMany()
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETANH_SANPHAM");
        });

        modelBuilder.Entity<Chitietddh>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__CHITIETDDH__27258E765F0F938C");
            entity.ToTable("CHITIETDDH");

            entity.Property(e => e.KhuyenMai).HasColumnType("money");
            entity.Property(e => e.MaDh)
                .HasMaxLength(20)
                .HasColumnName("MaDH");
            entity.Property(e => e.MaSp)
                .HasMaxLength(50)
                .HasColumnName("MaSP");
            entity.Property(e => e.Sldat).HasColumnName("SLDat");
            
            entity.HasOne(d => d.MaDhNavigation).WithMany()
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETDDH_DONDATHANG");

            entity.HasOne(d => d.MaSpNavigation).WithMany()
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETDDH_SANPHAM");
        });

        modelBuilder.Entity<Chitiethdb>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CHITIETHDB");

            entity.Property(e => e.KhuyenMai).HasColumnType("money");
            entity.Property(e => e.MaSp)
                .HasMaxLength(50)
                .HasColumnName("MaSP");
            entity.Property(e => e.Slban).HasColumnName("SLBan");
            entity.Property(e => e.SoHdb)
                .HasMaxLength(20)
                .HasColumnName("SoHDB");

            entity.HasOne(d => d.MaSpNavigation).WithMany()
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHDB");

            entity.HasOne(d => d.SoHdbNavigation).WithMany()
                .HasForeignKey(d => d.SoHdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETHD__SoHDB__5535A963");
        });

        modelBuilder.Entity<Chitiethdn>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CHITIETHDN");

            entity.Property(e => e.KhuyenMai).HasColumnType("money");
            entity.Property(e => e.MaSp)
                .HasMaxLength(50)
                .HasColumnName("MaSP");
            entity.Property(e => e.Slnhap).HasColumnName("SLNhap");
            entity.Property(e => e.SoHdn)
                .HasMaxLength(20)
                .HasColumnName("SoHDN");

            entity.HasOne(d => d.MaSpNavigation).WithMany()
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHDN_SANPHAM");

            entity.HasOne(d => d.SoHdnNavigation).WithMany()
                .HasForeignKey(d => d.SoHdn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHDN_HOADONNHAP");
        });

        modelBuilder.Entity<Chitietsp>(entity =>
        {
            entity.HasKey(e => e.MaChiTietSp).HasName("PK__CHITIETS__651D9057AA81DAA0");

            entity.ToTable("CHITIETSP");

            entity.Property(e => e.MaChiTietSp)
                .HasMaxLength(50)
                .HasColumnName("MaChiTietSP");
            entity.Property(e => e.Cpu)
                .HasMaxLength(100)
                .HasColumnName("CPU");
            entity.Property(e => e.Dlpin)
                .HasMaxLength(100)
                .HasColumnName("DLPIN");
            entity.Property(e => e.DonGiaBan).HasColumnType("money");
            entity.Property(e => e.DonGiaNhap).HasColumnType("money");
            entity.Property(e => e.MaSp)
                .HasMaxLength(50)
                .HasColumnName("MaSP");
            entity.Property(e => e.Ram)
                .HasMaxLength(100)
                .HasColumnName("RAM");
            entity.Property(e => e.Rom)
                .HasMaxLength(100)
                .HasColumnName("ROM");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.Chitietsps)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETSP_SANPHAM");
        });

        modelBuilder.Entity<Chucvu>(entity =>
        {
            entity.HasKey(e => e.MaCv).HasName("PK__CHUCVU__27258E765F0F938C");

            entity.ToTable("CHUCVU");

            entity.Property(e => e.MaCv)
                .HasMaxLength(20)
                .HasColumnName("MaCV");
            entity.Property(e => e.TenCv)
                .HasMaxLength(100)
                .HasColumnName("TenCV");
        });

        modelBuilder.Entity<Dondathang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DONDATHA__27258661A1587804");

            entity.ToTable("DONDATHANG");

            entity.Property(e => e.MaDh)
                .HasMaxLength(20)
                .HasColumnName("MaDH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(20)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayDat).HasColumnType("date");
            entity.Property(e => e.NgayGiao).HasColumnType("date");
            entity.Property(e => e.TongThanhToan).HasColumnType("money");
            entity.Property(e => e.TongTien).HasColumnType("money");
            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .HasColumnName("UserID");
            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Dondathangs)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DONDATHANG_NHANVIEN");

            entity.HasOne(d => d.User).WithMany(p => p.Dondathangs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DONDATHANG_USERINFO");
        });

        modelBuilder.Entity<Hang>(entity =>
        {
            entity.HasKey(e => e.MaHangSx).HasName("PK__HANG__8C6D28FE2BB9F6B4");

            entity.ToTable("HANG");

            entity.Property(e => e.MaHangSx)
                .HasMaxLength(20)
                .HasColumnName("MaHangSX");
            entity.Property(e => e.TenHangSx)
                .HasMaxLength(100)
                .HasColumnName("TenHangSX");
        });

        modelBuilder.Entity<Hoadonban>(entity =>
        {
            entity.HasKey(e => e.SoHdb).HasName("PK__HOADONBA__27C3EE80C83CC7F5");

            entity.ToTable("HOADONBAN");

            entity.Property(e => e.SoHdb)
                .HasMaxLength(20)
                .HasColumnName("SoHDB");
            entity.Property(e => e.MaNv)
                .HasMaxLength(20)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayBan).HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("money");
            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .HasColumnName("UserID");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Hoadonbans)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOADONBAN__MaNV__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.Hoadonbans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOADONBAN__UserI__534D60F1");
        });

        modelBuilder.Entity<Hoadonnhap>(entity =>
        {
            entity.HasKey(e => e.SoHdn).HasName("PK__HOADONNH__27C3EEB42C27448A");

            entity.ToTable("HOADONNHAP");

            entity.Property(e => e.SoHdn)
                .HasMaxLength(20)
                .HasColumnName("SoHDN");
            entity.Property(e => e.MaNcc)
                .HasMaxLength(20)
                .HasColumnName("MaNCC");
            entity.Property(e => e.MaNv)
                .HasMaxLength(20)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayNhap).HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("money");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.Hoadonnhaps)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOADONNHA__MaNCC__3F466844");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Hoadonnhaps)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOADONNHAP__MaNV__403A8C7D");
        });

        modelBuilder.Entity<Loaisp>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__LOAISP__730A575982E7929C");

            entity.ToTable("LOAISP");

            entity.Property(e => e.MaLoai).HasMaxLength(50);
            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<Nhacungcap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__NHACUNGC__3A185DEB69875B4E");

            entity.ToTable("NHACUNGCAP");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(20)
                .HasColumnName("MaNCC");
            entity.Property(e => e.DiaChiNcc)
                .HasMaxLength(200)
                .HasColumnName("DiaChiNCC");
            entity.Property(e => e.Sdtncc)
                .HasMaxLength(10)
                .HasColumnName("SDTNCC");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(200)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NHANVIEN__2725D70A6805A965");

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.MaNv)
                .HasMaxLength(20)
                .HasColumnName("MaNV");
            entity.Property(e => e.DiaChiNv)
                .HasMaxLength(200)
                .HasColumnName("DiaChiNV");
            entity.Property(e => e.GioiTinh).HasMaxLength(20);
            entity.Property(e => e.MaCv)
                .HasMaxLength(20)
                .HasColumnName("MaCV");
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.Sdtnv)
                .HasMaxLength(20)
                .HasColumnName("SDTNV");
            entity.Property(e => e.TenNv)
                .HasMaxLength(100)
                .HasColumnName("TenNV");

            entity.HasOne(d => d.MaCvNavigation).WithMany(p => p.Nhanviens)
                .HasForeignKey(d => d.MaCv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NHANVIEN__MaCV__38996AB5");
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SANPHAM__2725081C3ED82BAD");

            entity.ToTable("SANPHAM");

            entity.Property(e => e.MaSp)
                .HasMaxLength(50)
                .HasColumnName("MaSP");
            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaHangSx)
                .HasMaxLength(20)
                .HasColumnName("MaHangSX");
            entity.Property(e => e.MaLoai).HasMaxLength(50);
            entity.Property(e => e.MauSac).HasMaxLength(50);
            entity.Property(e => e.MieuTa).HasColumnType("ntext");
            entity.Property(e => e.TenSp)
                .HasMaxLength(50)
                .HasColumnName("TenSP");
            entity.Property(e => e.ThoiGianBh).HasColumnName("ThoiGianBH");

            entity.HasOne(d => d.MaHangSxNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.MaHangSx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SANPHAM_HANG");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SANPHAM_LOAISP");
        });

        modelBuilder.Entity<Userinfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__USERINFO__1788CCACD9C09645");

            entity.ToTable("USERINFO");

            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .HasColumnName("UserID");
            entity.Property(e => e.DiaChiUser).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.Sdtuser)
                .HasMaxLength(10)
                .HasColumnName("SDTUser");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<codeBTL.ViewModels.OrderViewModel>? OrderViewModel { get; set; }
}