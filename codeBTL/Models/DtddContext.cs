using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Chucvu> Chucvus { get; set; }

    public virtual DbSet<Dienthoai> Dienthoais { get; set; }

    public virtual DbSet<Dondathang> Dondathangs { get; set; }

    public virtual DbSet<Hang> Hangs { get; set; }

    public virtual DbSet<Hoadonban> Hoadonbans { get; set; }

    public virtual DbSet<Hoadonnhap> Hoadonnhaps { get; set; }

    public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-OSRNAUL\\SQLEXPRESS;Initial Catalog=DTDD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitietanh>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CHITIETANH");

            entity.Property(e => e.MaDt)
                .HasMaxLength(20)
                .HasColumnName("MaDT");
            entity.Property(e => e.TenFileAnh)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MaDtNavigation).WithMany()
                .HasForeignKey(d => d.MaDt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETANH_DIENTHOAI");
        });

        modelBuilder.Entity<Chitietddh>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CHITIETDDH");

            entity.Property(e => e.KhuyenMai).HasColumnType("money");
            entity.Property(e => e.MaDh)
                .HasMaxLength(20)
                .HasColumnName("MaDH");
            entity.Property(e => e.MaDt)
                .HasMaxLength(20)
                .HasColumnName("MaDT");
            entity.Property(e => e.Sldat).HasColumnName("SLDat");

            entity.HasOne(d => d.MaDhNavigation).WithMany()
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETDDH_DONDATHANG");

            entity.HasOne(d => d.MaDtNavigation).WithMany()
                .HasForeignKey(d => d.MaDt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETDDH_DIENTHOAI");
        });

        modelBuilder.Entity<Chitiethdb>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CHITIETHDB");

            entity.Property(e => e.KhuyenMai).HasColumnType("money");
            entity.Property(e => e.MaDt)
                .HasMaxLength(20)
                .HasColumnName("MaDT");
            entity.Property(e => e.Slban).HasColumnName("SLBan");
            entity.Property(e => e.SoHdb)
                .HasMaxLength(20)
                .HasColumnName("SoHDB");

            entity.HasOne(d => d.MaDtNavigation).WithMany()
                .HasForeignKey(d => d.MaDt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHDB");

            entity.HasOne(d => d.SoHdbNavigation).WithMany()
                .HasForeignKey(d => d.SoHdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETHD__SoHDB__4D94879B");
        });

        modelBuilder.Entity<Chitiethdn>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CHITIETHDN");

            entity.Property(e => e.KhuyenMai).HasColumnType("money");
            entity.Property(e => e.MaDt)
                .HasMaxLength(20)
                .HasColumnName("MaDT");
            entity.Property(e => e.Slnhap).HasColumnName("SLNhap");
            entity.Property(e => e.SoHdn)
                .HasMaxLength(20)
                .HasColumnName("SoHDN");

            entity.HasOne(d => d.MaDtNavigation).WithMany()
                .HasForeignKey(d => d.MaDt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHDN_DIENTHOAI");

            entity.HasOne(d => d.SoHdnNavigation).WithMany()
                .HasForeignKey(d => d.SoHdn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHDN_HOADONNHAP");
        });

        modelBuilder.Entity<Chucvu>(entity =>
        {
            entity.HasKey(e => e.MaCv).HasName("PK__CHUCVU__27258E762CDAE2C1");

            entity.ToTable("CHUCVU");

            entity.Property(e => e.MaCv)
                .HasMaxLength(20)
                .HasColumnName("MaCV");
            entity.Property(e => e.TenCv)
                .HasMaxLength(100)
                .HasColumnName("TenCV");
        });

        modelBuilder.Entity<Dienthoai>(entity =>
        {
            entity.HasKey(e => e.MaDt).HasName("PK__DIENTHOA__272586554A71DAA7");

            entity.ToTable("DIENTHOAI");

            entity.Property(e => e.MaDt)
                .HasMaxLength(20)
                .HasColumnName("MaDT");
            entity.Property(e => e.Cpu)
                .HasMaxLength(100)
                .HasColumnName("CPU");
            entity.Property(e => e.DanhGia).HasColumnType("ntext");
            entity.Property(e => e.Dlpin)
                .HasMaxLength(50)
                .HasColumnName("DLPin");
            entity.Property(e => e.DonGiaBan).HasColumnType("money");
            entity.Property(e => e.DonGiaNhap).HasColumnType("money");
            entity.Property(e => e.MaHangSx)
                .HasMaxLength(20)
                .HasColumnName("MaHangSX");
            entity.Property(e => e.MauSac).HasMaxLength(50);
            entity.Property(e => e.MieuTa).HasColumnType("ntext");
            entity.Property(e => e.Ram).HasMaxLength(100);
            entity.Property(e => e.Rom).HasMaxLength(100);
            entity.Property(e => e.TenDt)
                .HasMaxLength(100)
                .HasColumnName("TenDT");
            entity.Property(e => e.TenFileAnh)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ThoiGianBh).HasColumnName("ThoiGianBH");

            entity.HasOne(d => d.MaHangSxNavigation).WithMany(p => p.Dienthoais)
                .HasForeignKey(d => d.MaHangSx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DIENTHOAI_HANG");
        });

        modelBuilder.Entity<Dondathang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DONDATHA__272586611186DC02");

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
                .HasConstraintName("FK_DONDATHANG_USER");
        });

        modelBuilder.Entity<Hang>(entity =>
        {
            entity.HasKey(e => e.MaHangSx).HasName("PK__HANG__8C6D28FE5D49DEC2");

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
            entity.HasKey(e => e.SoHdb).HasName("PK__HOADONBA__27C3EE802A926CFE");

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
                .HasConstraintName("FK__HOADONBAN__MaNV__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.Hoadonbans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOADONBAN__UserI__4BAC3F29");
        });

        modelBuilder.Entity<Hoadonnhap>(entity =>
        {
            entity.HasKey(e => e.SoHdn).HasName("PK__HOADONNH__27C3EEB40EF79B66");

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

        modelBuilder.Entity<Nhacungcap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__NHACUNGC__3A185DEB18E5F25C");

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
            entity.HasKey(e => e.MaNv).HasName("PK__NHANVIEN__2725D70AD5FE4480");

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

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__TAIKHOAN__1788CCAC2C989267");

            entity.ToTable("TAIKHOAN");

            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .HasColumnName("UserID");
            entity.Property(e => e.DiaChiUser).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.Sdtuser)
                .HasMaxLength(10)
                .HasColumnName("SDTUser");
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
