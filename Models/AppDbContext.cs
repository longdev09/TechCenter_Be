using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Baithidanop> Baithidanops { get; set; }

    public virtual DbSet<Baiviet> Baiviets { get; set; }

    public virtual DbSet<Baocaohoctap> Baocaohoctaps { get; set; }

    public virtual DbSet<Capdokhoahoc> Capdokhoahocs { get; set; }

    public virtual DbSet<Cauhoi> Cauhois { get; set; }

    public virtual DbSet<Chatmessage> Chatmessages { get; set; }

    public virtual DbSet<Chatsession> Chatsessions { get; set; }

    public virtual DbSet<Chungchi> Chungchis { get; set; }

    public virtual DbSet<Ctuudai> Ctuudais { get; set; }

    public virtual DbSet<Dangkylop> Dangkylops { get; set; }

    public virtual DbSet<Dapan> Dapans { get; set; }

    public virtual DbSet<Deluyen> Deluyens { get; set; }

    public virtual DbSet<Diemdanh> Diemdanhs { get; set; }

    public virtual DbSet<DkyChungchi> DkyChungchis { get; set; }

    public virtual DbSet<Giaovien> Giaoviens { get; set; }

    public virtual DbSet<Hocvien> Hocviens { get; set; }

    public virtual DbSet<Khoahoc> Khoahocs { get; set; }

    public virtual DbSet<Lichhoc> Lichhocs { get; set; }

    public virtual DbSet<LichhocChitiet> LichhocChitiets { get; set; }

    public virtual DbSet<Loaide> Loaides { get; set; }

    public virtual DbSet<Loaikynang> Loaikynangs { get; set; }

    public virtual DbSet<Lophoc> Lophocs { get; set; }

    public virtual DbSet<Nhanxettiendo> Nhanxettiendos { get; set; }

    public virtual DbSet<Phancong> Phancongs { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Tailieu> Tailieus { get; set; }

    public virtual DbSet<Vaitro> Vaitros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TechCenter;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baithidanop>(entity =>
        {
            entity.HasOne(d => d.IdDeluyenNavigation).WithMany(p => p.Baithidanops)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAITHIDANOP_DELUYEN");

            entity.HasOne(d => d.IdHocvienNavigation).WithMany(p => p.Baithidanops)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAITHIDANOP_HOCVIEN");

            entity.HasMany(d => d.IdCauhois).WithMany(p => p.IdBaithis)
                .UsingEntity<Dictionary<string, object>>(
                    "BaithiBailam",
                    r => r.HasOne<Cauhoi>().WithMany()
                        .HasForeignKey("IdCauhoi")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BAITHI_BAILAM_CAUHOI"),
                    l => l.HasOne<Baithidanop>().WithMany()
                        .HasForeignKey("IdBaithi")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BAITHI_BAILAM_BAITHIDANOP"),
                    j =>
                    {
                        j.HasKey("IdBaithi", "IdCauhoi");
                        j.ToTable("BAITHI_BAILAM");
                        j.IndexerProperty<int>("IdBaithi").HasColumnName("ID_BAITHI");
                        j.IndexerProperty<int>("IdCauhoi").HasColumnName("ID_CAUHOI");
                    });
        });

        modelBuilder.Entity<Baiviet>(entity =>
        {
            entity.HasKey(e => e.IdBaiviet).HasName("PK__BAIVIET__F02E6D7C129C43E4");

            entity.HasOne(d => d.IdKhoahocNavigation).WithMany(p => p.Baiviets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAIVIET_KHOAHOC");
        });

        modelBuilder.Entity<Baocaohoctap>(entity =>
        {
            entity.HasKey(e => e.IdBaocao).HasName("PK__BAOCAOHO__5CF63C577C0CE92E");

            entity.HasOne(d => d.IdHocvienNavigation).WithMany(p => p.Baocaohoctaps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAOCAOHOCTAP_HOCVIEN");

            entity.HasOne(d => d.IdKhoahocNavigation).WithMany(p => p.Baocaohoctaps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAOCAOHOCTAP_KHOAHOC");
        });

        modelBuilder.Entity<Capdokhoahoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAPDOKHO__3214EC27D2E9010D");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<Cauhoi>(entity =>
        {
            entity.HasKey(e => e.IdCauhoi).HasName("PK__CAUHOI__674FD6FC40D59B93");

            entity.Property(e => e.Dapandung).IsFixedLength();
        });

        modelBuilder.Entity<Chatmessage>(entity =>
        {
            entity.HasKey(e => e.IdMessage).HasName("PK__CHATMESS__63BDAD118F62ADEC");

            entity.Property(e => e.IsDagui).HasDefaultValue(true);
            entity.Property(e => e.Thoidiemgui).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdSessionNavigation).WithMany(p => p.Chatmessages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHATMESSAGE_CHATSESSION");

            entity.HasOne(d => d.IdSession1).WithMany(p => p.Chatmessages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHATMESSAGE_VAITRO");

            entity.HasOne(d => d.IdTaikhoanNavigation).WithMany(p => p.Chatmessages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHATMESSAGE_TAIKHOAN");
        });

        modelBuilder.Entity<Chatsession>(entity =>
        {
            entity.HasKey(e => e.IdSession).HasName("PK__CHATSESS__E0441B326408E869");

            entity.Property(e => e.Thoidiembatdau).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTaikhoanNavigation).WithMany(p => p.Chatsessions).HasConstraintName("FK_CHATSESSION_TAIKHOAN");
        });

        modelBuilder.Entity<Chungchi>(entity =>
        {
            entity.HasKey(e => e.IdChungchi).HasName("PK__CHUNGCHI__92727E22041577B0");
        });

        modelBuilder.Entity<Ctuudai>(entity =>
        {
            entity.HasKey(e => e.IdUudai).HasName("PK__CTUUDAI__CB3D935954C97BC4");
        });

        modelBuilder.Entity<Dangkylop>(entity =>
        {
            entity.HasKey(e => e.IdDangky).HasName("PK__DANGKYLO__38EB01C9A14D6E25");

            entity.HasOne(d => d.IdHvNavigation).WithMany(p => p.Dangkylops)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DANGKY_HV");

            entity.HasOne(d => d.IdLophocNavigation).WithMany(p => p.Dangkylops)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DANGKY_LOPHOC");

            entity.HasOne(d => d.IdUudaiNavigation).WithMany(p => p.Dangkylops).HasConstraintName("FK_DANGKY_UUDAI");
        });

        modelBuilder.Entity<Dapan>(entity =>
        {
            entity.HasOne(d => d.IdCauhoiNavigation).WithMany(p => p.Dapans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DAPAN_CAUHOI");
        });

        modelBuilder.Entity<Deluyen>(entity =>
        {
            entity.HasKey(e => e.IdDeluyen).HasName("PK__DELUYEN__616F6068894A6989");

            entity.Property(e => e.Ngaytaode).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdLoaideNavigation).WithMany(p => p.Deluyens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DELUYEN_LOAIDE");

            entity.HasOne(d => d.IdLophocNavigation).WithMany(p => p.Deluyens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DELUYEN_LOPHOC");

            entity.HasOne(d => d.IdTkNavigation).WithMany(p => p.Deluyens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DELUYEN_TAIKHOAN");

            entity.HasMany(d => d.IdCauhois).WithMany(p => p.IdDeluyens)
                .UsingEntity<Dictionary<string, object>>(
                    "DeluyenCauhoi",
                    r => r.HasOne<Cauhoi>().WithMany()
                        .HasForeignKey("IdCauhoi")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DELUYEN_CAUHOI_CAUHOI"),
                    l => l.HasOne<Deluyen>().WithMany()
                        .HasForeignKey("IdDeluyen")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DELUYEN_CAUHOI_DELUYEN"),
                    j =>
                    {
                        j.HasKey("IdDeluyen", "IdCauhoi");
                        j.ToTable("DELUYEN_CAUHOI");
                        j.IndexerProperty<int>("IdDeluyen").HasColumnName("ID_DELUYEN");
                        j.IndexerProperty<int>("IdCauhoi").HasColumnName("ID_CAUHOI");
                    });
        });

        modelBuilder.Entity<Diemdanh>(entity =>
        {
            entity.HasOne(d => d.IdHvNavigation).WithMany(p => p.Diemdanhs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DIEMDANH_HV");

            entity.HasOne(d => d.IdLhctNavigation).WithMany(p => p.Diemdanhs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DIEMDANH_LHCT");
        });

        modelBuilder.Entity<DkyChungchi>(entity =>
        {
            entity.HasKey(e => e.IdDkycc).HasName("PK__DKY_CHUN__16826A6A7ACEDD0F");

            entity.Property(e => e.Ngaydangkythi).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdChungchiNavigation).WithMany(p => p.DkyChungchis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DKY_CHUNGCHI_CHUNGCHI");

            entity.HasOne(d => d.IdHocvienNavigation).WithMany(p => p.DkyChungchis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DKY_CHUNGCHI_HOCVIEN");
        });

        modelBuilder.Entity<Giaovien>(entity =>
        {
            entity.HasKey(e => e.IdGiaovien).HasName("PK__GIAOVIEN__0A44E67C5787B47B");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.IdTaikhoanNavigation).WithOne(p => p.Giaovien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GIAOVIEN_TAIKHOAN");
        });

        modelBuilder.Entity<Hocvien>(entity =>
        {
            entity.HasKey(e => e.IdHocvien).HasName("PK__HOCVIEN__3350F93D674DB26B");

            entity.HasOne(d => d.IdTaikhoanNavigation).WithOne(p => p.Hocvien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HOCVIEN_TAIKHOAN");
        });

        modelBuilder.Entity<Khoahoc>(entity =>
        {
            entity.HasKey(e => e.IdKhoahoc).HasName("PK__KHOAHOC__2D63BD9E4A8DBE41");

            entity.HasOne(d => d.IdcapdokhoahocNavigation).WithMany(p => p.Khoahocs).HasConstraintName("FK_KHOAHOC_CAPDO");
        });

        modelBuilder.Entity<Lichhoc>(entity =>
        {
            entity.HasKey(e => e.IdLichhoc).HasName("PK__LICHHOC__7A44FB3B7A81B3C4");

            entity.Property(e => e.Giobatdau).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Gioketthuc).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdLophocNavigation).WithMany(p => p.Lichhocs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOPHOC_LICHHOC");
        });

        modelBuilder.Entity<LichhocChitiet>(entity =>
        {
            entity.HasKey(e => e.IdLhct).HasName("PK__LICHHOC___9B10EE78F4448DDB");

            entity.HasOne(d => d.IdLichhocNavigation).WithMany(p => p.LichhocChitiets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LHCT_LICHHOC");

            entity.HasOne(d => d.IdLoaikynangNavigation).WithMany(p => p.LichhocChitiets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LHCT_LOAIKYNANG");
        });

        modelBuilder.Entity<Loaide>(entity =>
        {
            entity.HasKey(e => e.IdLoaide).HasName("PK__LOAIDE__7929BB964DEA8E0B");
        });

        modelBuilder.Entity<Loaikynang>(entity =>
        {
            entity.HasKey(e => e.IdLoaikynang).HasName("PK__LOAIKYNA__1DC3322D0015BE23");
        });

        modelBuilder.Entity<Lophoc>(entity =>
        {
            entity.HasKey(e => e.IdLophoc).HasName("PK__LOPHOC__06CC5F7E2B71FA9D");

            entity.HasOne(d => d.IdKhoahocNavigation).WithMany(p => p.Lophocs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOPHOC_KHOAHOC");
        });

        modelBuilder.Entity<Nhanxettiendo>(entity =>
        {
            entity.HasKey(e => e.IdNhanxet).HasName("PK__NHANXETT__5EF3DC4960DC891F");

            entity.Property(e => e.Ngaynhanxet).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdGiaovienNavigation).WithMany(p => p.Nhanxettiendos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NHANXETTIENDO_GIAOVIEN");

            entity.HasOne(d => d.IdHocvienNavigation).WithMany(p => p.Nhanxettiendos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NHANXETTIENDO_HOCVIEN");

            entity.HasOne(d => d.IdLophocNavigation).WithMany(p => p.Nhanxettiendos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NHANXETTIENDO_LOPHOC");
        });

        modelBuilder.Entity<Phancong>(entity =>
        {
            entity.Property(e => e.Ngayphancong).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdGiaovienNavigation).WithMany(p => p.Phancongs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHANCONG_GIAOVIEN");

            entity.HasOne(d => d.IdLophocNavigation).WithMany(p => p.Phancongs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHANCONG_LOPHOC");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.IdTaikhoan).HasName("PK__TAIKHOAN__EB942D7F300EF0C7");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.IdVaitroNavigation).WithMany(p => p.Taikhoans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TAIKHOAN_VAITRO");
        });

        modelBuilder.Entity<Tailieu>(entity =>
        {
            entity.HasKey(e => e.IdTailieu).HasName("PK__TAILIEU__946A8809CF884262");

            entity.HasOne(d => d.IdGvNavigation).WithMany(p => p.Tailieus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TAILIEU_GV");

            entity.HasMany(d => d.IdLophocs).WithMany(p => p.IdTailieus)
                .UsingEntity<Dictionary<string, object>>(
                    "TailieuLophoc",
                    r => r.HasOne<Lophoc>().WithMany()
                        .HasForeignKey("IdLophoc")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TL_LOPHOC_LOPHOC"),
                    l => l.HasOne<Tailieu>().WithMany()
                        .HasForeignKey("IdTailieu")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TL_LOPHOC_TAILIEU"),
                    j =>
                    {
                        j.HasKey("IdTailieu", "IdLophoc");
                        j.ToTable("TAILIEU_LOPHOC");
                        j.IndexerProperty<int>("IdTailieu").HasColumnName("ID_TAILIEU");
                        j.IndexerProperty<int>("IdLophoc").HasColumnName("ID_LOPHOC");
                    });
        });

        modelBuilder.Entity<Vaitro>(entity =>
        {
            entity.HasKey(e => e.IdVaitro).HasName("PK__VAITRO__86F820A6E504FA35");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
