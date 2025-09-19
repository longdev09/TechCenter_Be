using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;





public partial class TechCenterContext : DbContext
{


    public TechCenterContext()
    {
    }

    public TechCenterContext(DbContextOptions<TechCenterContext> options)
        : base(options)
    {



    }

    public virtual DbSet<Baithidanop> Baithidanops { get; set; }

    public virtual DbSet<Baiviet> Baiviets { get; set; }

    public virtual DbSet<Baocaohoctap> Baocaohoctaps { get; set; }

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



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baithidanop>(entity =>
        {
            entity.HasKey(e => e.IdBaithi);

            entity.ToTable("BAITHIDANOP");

            entity.Property(e => e.IdBaithi).HasColumnName("ID_BAITHI");
            entity.Property(e => e.Diemso).HasColumnName("DIEMSO");
            entity.Property(e => e.IdDeluyen).HasColumnName("ID_DELUYEN");
            entity.Property(e => e.IdHocvien).HasColumnName("ID_HOCVIEN");
            entity.Property(e => e.Ngaylambai)
                .HasColumnType("datetime")
                .HasColumnName("NGAYLAMBAI");
            entity.Property(e => e.Socaudung).HasColumnName("SOCAUDUNG");
            entity.Property(e => e.Socausai).HasColumnName("SOCAUSAI");
            entity.Property(e => e.Tglambai).HasColumnName("TGLAMBAI");

            entity.HasOne(d => d.IdDeluyenNavigation).WithMany(p => p.Baithidanops)
                .HasForeignKey(d => d.IdDeluyen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAITHIDANOP_DELUYEN");

            entity.HasOne(d => d.IdHocvienNavigation).WithMany(p => p.Baithidanops)
                .HasForeignKey(d => d.IdHocvien)
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

            entity.ToTable("BAIVIET");

            entity.Property(e => e.IdBaiviet).HasColumnName("ID_BAIVIET");
            entity.Property(e => e.IdKhoahoc).HasColumnName("ID_KHOAHOC");
            entity.Property(e => e.Luotxem).HasColumnName("LUOTXEM");
            entity.Property(e => e.Ndbaiviet).HasColumnName("NDBAIVIET");
            entity.Property(e => e.Tieudebaiviet)
                .HasMaxLength(255)
                .HasColumnName("TIEUDEBAIVIET");

            entity.HasOne(d => d.IdKhoahocNavigation).WithMany(p => p.Baiviets)
                .HasForeignKey(d => d.IdKhoahoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAIVIET_KHOAHOC");
        });

        modelBuilder.Entity<Baocaohoctap>(entity =>
        {
            entity.HasKey(e => e.IdBaocao).HasName("PK__BAOCAOHO__5CF63C577C0CE92E");

            entity.ToTable("BAOCAOHOCTAP");

            entity.Property(e => e.IdBaocao).HasColumnName("ID_BAOCAO");
            entity.Property(e => e.Deluyendalam).HasColumnName("DELUYENDALAM");
            entity.Property(e => e.Diemtb).HasColumnName("DIEMTB");
            entity.Property(e => e.IdHocvien).HasColumnName("ID_HOCVIEN");
            entity.Property(e => e.IdKhoahoc).HasColumnName("ID_KHOAHOC");
            entity.Property(e => e.Sobuoivang).HasColumnName("SOBUOIVANG");

            entity.HasOne(d => d.IdHocvienNavigation).WithMany(p => p.Baocaohoctaps)
                .HasForeignKey(d => d.IdHocvien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAOCAOHOCTAP_HOCVIEN");

            entity.HasOne(d => d.IdKhoahocNavigation).WithMany(p => p.Baocaohoctaps)
                .HasForeignKey(d => d.IdKhoahoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAOCAOHOCTAP_KHOAHOC");
        });

        modelBuilder.Entity<Cauhoi>(entity =>
        {
            entity.HasKey(e => e.IdCauhoi).HasName("PK__CAUHOI__674FD6FC40D59B93");

            entity.ToTable("CAUHOI");

            entity.Property(e => e.IdCauhoi).HasColumnName("ID_CAUHOI");
            entity.Property(e => e.Cauhoi1)
                .HasMaxLength(255)
                .HasColumnName("CAUHOI");
            entity.Property(e => e.Dapandung)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DAPANDUNG");
        });

        modelBuilder.Entity<Chatmessage>(entity =>
        {
            entity.HasKey(e => e.IdMessage).HasName("PK__CHATMESS__63BDAD118F62ADEC");

            entity.ToTable("CHATMESSAGE");

            entity.Property(e => e.IdMessage).HasColumnName("ID_MESSAGE");
            entity.Property(e => e.IdSession).HasColumnName("ID_SESSION");
            entity.Property(e => e.IdTaikhoan).HasColumnName("ID_TAIKHOAN");
            entity.Property(e => e.IdVaitro).HasColumnName("ID_VAITRO");
            entity.Property(e => e.IsDagui)
                .HasDefaultValue(true)
                .HasColumnName("IS_DAGUI");
            entity.Property(e => e.IsDaxem).HasColumnName("IS_DAXEM");
            entity.Property(e => e.Noidungmessage)
                .HasMaxLength(255)
                .HasColumnName("NOIDUNGMESSAGE");
            entity.Property(e => e.Thoidiemgui)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("THOIDIEMGUI");

            entity.HasOne(d => d.IdSessionNavigation).WithMany(p => p.Chatmessages)
                .HasForeignKey(d => d.IdSession)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHATMESSAGE_CHATSESSION");

            entity.HasOne(d => d.IdSession1).WithMany(p => p.Chatmessages)
                .HasForeignKey(d => d.IdSession)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHATMESSAGE_VAITRO");

            entity.HasOne(d => d.IdTaikhoanNavigation).WithMany(p => p.Chatmessages)
                .HasForeignKey(d => d.IdTaikhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHATMESSAGE_TAIKHOAN");
        });

        modelBuilder.Entity<Chatsession>(entity =>
        {
            entity.HasKey(e => e.IdSession).HasName("PK__CHATSESS__E0441B326408E869");

            entity.ToTable("CHATSESSION");

            entity.Property(e => e.IdSession).HasColumnName("ID_SESSION");
            entity.Property(e => e.IdTaikhoan).HasColumnName("ID_TAIKHOAN");
            entity.Property(e => e.Loaisession).HasColumnName("LOAISESSION");
            entity.Property(e => e.Thoidiembatdau)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("THOIDIEMBATDAU");
            entity.Property(e => e.Thoidiemketthuc)
                .HasColumnType("datetime")
                .HasColumnName("THOIDIEMKETTHUC");

            entity.HasOne(d => d.IdTaikhoanNavigation).WithMany(p => p.Chatsessions)
                .HasForeignKey(d => d.IdTaikhoan)
                .HasConstraintName("FK_CHATSESSION_TAIKHOAN");
        });

        modelBuilder.Entity<Chungchi>(entity =>
        {
            entity.HasKey(e => e.IdChungchi).HasName("PK__CHUNGCHI__92727E22041577B0");

            entity.ToTable("CHUNGCHI");

            entity.HasIndex(e => e.Tenchungchi, "UQ__CHUNGCHI__33E88737B6A595F5").IsUnique();

            entity.Property(e => e.IdChungchi).HasColumnName("ID_CHUNGCHI");
            entity.Property(e => e.Donvicap)
                .HasMaxLength(100)
                .HasColumnName("DONVICAP");
            entity.Property(e => e.Lephithi)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("LEPHITHI");
            entity.Property(e => e.Motacc).HasColumnName("MOTACC");
            entity.Property(e => e.Tenchungchi)
                .HasMaxLength(200)
                .HasColumnName("TENCHUNGCHI");
        });

        modelBuilder.Entity<Ctuudai>(entity =>
        {
            entity.HasKey(e => e.IdUudai).HasName("PK__CTUUDAI__CB3D935954C97BC4");

            entity.ToTable("CTUUDAI");

            entity.Property(e => e.IdUudai).HasColumnName("ID_UUDAI");
            entity.Property(e => e.NgaybatdauUd).HasColumnName("NGAYBATDAU_UD");
            entity.Property(e => e.NgayketthucUd).HasColumnName("NGAYKETTHUC_UD");
            entity.Property(e => e.Phantramuudai).HasColumnName("PHANTRAMUUDAI");
            entity.Property(e => e.Soluotthientai).HasColumnName("SOLUOTTHIENTAI");
            entity.Property(e => e.Soluottoida).HasColumnName("SOLUOTTOIDA");
            entity.Property(e => e.Tenctud)
                .HasMaxLength(255)
                .HasColumnName("TENCTUD");
            entity.Property(e => e.TrangthaiUd)
                .HasMaxLength(20)
                .HasColumnName("TRANGTHAI_UD");
        });

        modelBuilder.Entity<Dangkylop>(entity =>
        {
            entity.HasKey(e => e.IdDangky).HasName("PK__DANGKYLO__38EB01C9A14D6E25");

            entity.ToTable("DANGKYLOP");

            entity.Property(e => e.IdDangky).HasColumnName("ID_DANGKY");
            entity.Property(e => e.IdHv).HasColumnName("ID_HV");
            entity.Property(e => e.IdLophoc).HasColumnName("ID_LOPHOC");
            entity.Property(e => e.IdUudai).HasColumnName("ID_UUDAI");
            entity.Property(e => e.Ngaydangky)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDANGKY");
            entity.Property(e => e.Sotiengiam)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("SOTIENGIAM");
            entity.Property(e => e.Tongtien)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TONGTIEN");

            entity.HasOne(d => d.IdHvNavigation).WithMany(p => p.Dangkylops)
                .HasForeignKey(d => d.IdHv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DANGKY_HV");

            entity.HasOne(d => d.IdLophocNavigation).WithMany(p => p.Dangkylops)
                .HasForeignKey(d => d.IdLophoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DANGKY_LOPHOC");

            entity.HasOne(d => d.IdUudaiNavigation).WithMany(p => p.Dangkylops)
                .HasForeignKey(d => d.IdUudai)
                .HasConstraintName("FK_DANGKY_UUDAI");
        });

        modelBuilder.Entity<Dapan>(entity =>
        {
            entity.HasKey(e => e.IdDapan);

            entity.ToTable("DAPAN");

            entity.Property(e => e.IdDapan).HasColumnName("ID_DAPAN");
            entity.Property(e => e.IdCauhoi).HasColumnName("ID_CAUHOI");
            entity.Property(e => e.IsTrue).HasColumnName("IS_TRUE");
            entity.Property(e => e.NdDapan)
                .HasMaxLength(255)
                .HasColumnName("ND_DAPAN");

            entity.HasOne(d => d.IdCauhoiNavigation).WithMany(p => p.Dapans)
                .HasForeignKey(d => d.IdCauhoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DAPAN_CAUHOI");
        });

        modelBuilder.Entity<Deluyen>(entity =>
        {
            entity.HasKey(e => e.IdDeluyen).HasName("PK__DELUYEN__616F6068894A6989");

            entity.ToTable("DELUYEN");

            entity.Property(e => e.IdDeluyen).HasColumnName("ID_DELUYEN");
            entity.Property(e => e.IdLoaide).HasColumnName("ID_LOAIDE");
            entity.Property(e => e.IdLophoc).HasColumnName("ID_LOPHOC");
            entity.Property(e => e.IdTk).HasColumnName("ID_TK");
            entity.Property(e => e.IsThicuoikhoa).HasColumnName("IS_THICUOIKHOA");
            entity.Property(e => e.Ngaytaode)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAODE");
            entity.Property(e => e.Tendeluyen)
                .HasMaxLength(255)
                .HasColumnName("TENDELUYEN");
            entity.Property(e => e.Thoigianlam).HasColumnName("THOIGIANLAM");

            entity.HasOne(d => d.IdLoaideNavigation).WithMany(p => p.Deluyens)
                .HasForeignKey(d => d.IdLoaide)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DELUYEN_LOAIDE");

            entity.HasOne(d => d.IdLophocNavigation).WithMany(p => p.Deluyens)
                .HasForeignKey(d => d.IdLophoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DELUYEN_LOPHOC");

            entity.HasOne(d => d.IdTkNavigation).WithMany(p => p.Deluyens)
                .HasForeignKey(d => d.IdTk)
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
            entity.HasKey(e => new { e.IdLhct, e.IdHv });

            entity.ToTable("DIEMDANH");

            entity.Property(e => e.IdLhct).HasColumnName("ID_LHCT");
            entity.Property(e => e.IdHv).HasColumnName("ID_HV");
            entity.Property(e => e.TtDiemdanh).HasColumnName("TT_DIEMDANH");

            entity.HasOne(d => d.IdHvNavigation).WithMany(p => p.Diemdanhs)
                .HasForeignKey(d => d.IdHv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DIEMDANH_HV");

            entity.HasOne(d => d.IdLhctNavigation).WithMany(p => p.Diemdanhs)
                .HasForeignKey(d => d.IdLhct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DIEMDANH_LHCT");
        });

        modelBuilder.Entity<DkyChungchi>(entity =>
        {
            entity.HasKey(e => e.IdDkycc).HasName("PK__DKY_CHUN__16826A6A7ACEDD0F");

            entity.ToTable("DKY_CHUNGCHI");

            entity.Property(e => e.IdDkycc).HasColumnName("ID_DKYCC");
            entity.Property(e => e.Hotro)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("HOTRO");
            entity.Property(e => e.IdChungchi).HasColumnName("ID_CHUNGCHI");
            entity.Property(e => e.IdHocvien).HasColumnName("ID_HOCVIEN");
            entity.Property(e => e.Ngaydangkythi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYDANGKYTHI");
            entity.Property(e => e.Ngaythicc)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTHICC");
            entity.Property(e => e.Phithucte)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PHITHUCTE");
            entity.Property(e => e.TtDangky)
                .HasMaxLength(20)
                .HasColumnName("TT_DANGKY");

            entity.HasOne(d => d.IdChungchiNavigation).WithMany(p => p.DkyChungchis)
                .HasForeignKey(d => d.IdChungchi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DKY_CHUNGCHI_CHUNGCHI");

            entity.HasOne(d => d.IdHocvienNavigation).WithMany(p => p.DkyChungchis)
                .HasForeignKey(d => d.IdHocvien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DKY_CHUNGCHI_HOCVIEN");
        });

        modelBuilder.Entity<Giaovien>(entity =>
        {
            entity.HasKey(e => e.IdGiaovien).HasName("PK__GIAOVIEN__0A44E67C5787B47B");

            entity.ToTable("GIAOVIEN");

            entity.HasIndex(e => e.IdTaikhoan, "UQ__GIAOVIEN__EB942D7EE3A72230").IsUnique();

            entity.Property(e => e.IdGiaovien).HasColumnName("ID_GIAOVIEN");
            entity.Property(e => e.Diachigv)
                .HasMaxLength(255)
                .HasColumnName("DIACHIGV");
            entity.Property(e => e.Gioitinhgv)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("GIOITINHGV");
            entity.Property(e => e.Hotengv)
                .HasMaxLength(100)
                .HasColumnName("HOTENGV");
            entity.Property(e => e.IdTaikhoan).HasColumnName("ID_TAIKHOAN");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.Ngaysinhgv).HasColumnName("NGAYSINHGV");

            entity.HasOne(d => d.IdTaikhoanNavigation).WithOne(p => p.Giaovien)
                .HasForeignKey<Giaovien>(d => d.IdTaikhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GIAOVIEN_TAIKHOAN");
        });

        modelBuilder.Entity<Hocvien>(entity =>
        {
            entity.HasKey(e => e.IdHocvien).HasName("PK__HOCVIEN__3350F93D674DB26B");

            entity.ToTable("HOCVIEN");

            entity.HasIndex(e => e.IdTaikhoan, "UQ__HOCVIEN__EB942D7EF1E73928").IsUnique();

            entity.Property(e => e.IdHocvien).HasColumnName("ID_HOCVIEN");
            entity.Property(e => e.Diachihv)
                .HasMaxLength(255)
                .HasColumnName("DIACHIHV");
            entity.Property(e => e.Gioitinhhv)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("GIOITINHHV");
            entity.Property(e => e.Hotenhv)
                .HasMaxLength(100)
                .HasColumnName("HOTENHV");
            entity.Property(e => e.IdTaikhoan).HasColumnName("ID_TAIKHOAN");
            entity.Property(e => e.Ngaysinhhv).HasColumnName("NGAYSINHHV");

            entity.HasOne(d => d.IdTaikhoanNavigation).WithOne(p => p.Hocvien)
                .HasForeignKey<Hocvien>(d => d.IdTaikhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HOCVIEN_TAIKHOAN");
        });

        modelBuilder.Entity<Khoahoc>(entity =>
        {
            entity.HasKey(e => e.IdKhoahoc).HasName("PK__KHOAHOC__2D63BD9E4A8DBE41");

            entity.ToTable("KHOAHOC");

            entity.HasIndex(e => e.Tenkhoahoc, "UQ__KHOAHOC__9FC17498E96D9490").IsUnique();

            entity.Property(e => e.IdKhoahoc).HasColumnName("ID_KHOAHOC");
            entity.Property(e => e.Anhdaidien)
                .HasMaxLength(255)
                .HasColumnName("ANHDAIDIEN");
            entity.Property(e => e.Hocphi)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("HOCPHI");
            entity.Property(e => e.Motakh).HasColumnName("MOTAKH");
            entity.Property(e => e.Tenkhoahoc)
                .HasMaxLength(100)
                .HasColumnName("TENKHOAHOC");
        });

        modelBuilder.Entity<Lichhoc>(entity =>
        {
            entity.HasKey(e => e.IdLichhoc).HasName("PK__LICHHOC__7A44FB3B7A81B3C4");

            entity.ToTable("LICHHOC");

            entity.Property(e => e.IdLichhoc).HasColumnName("ID_LICHHOC");
            entity.Property(e => e.Giobatdau)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("GIOBATDAU");
            entity.Property(e => e.Gioketthuc)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("GIOKETTHUC");
            entity.Property(e => e.IdLophoc).HasColumnName("ID_LOPHOC");
            entity.Property(e => e.Thu).HasColumnName("THU");

            entity.HasOne(d => d.IdLophocNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.IdLophoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOPHOC_LICHHOC");
        });

        modelBuilder.Entity<LichhocChitiet>(entity =>
        {
            entity.HasKey(e => e.IdLhct).HasName("PK__LICHHOC___9B10EE78F4448DDB");

            entity.ToTable("LICHHOC_CHITIET");

            entity.Property(e => e.IdLhct).HasColumnName("ID_LHCT");
            entity.Property(e => e.IdLichhoc).HasColumnName("ID_LICHHOC");
            entity.Property(e => e.IdLoaikynang).HasColumnName("ID_LOAIKYNANG");
            entity.Property(e => e.Ngayhoc).HasColumnName("NGAYHOC");
            entity.Property(e => e.Tieudebuoihoc)
                .HasMaxLength(255)
                .HasColumnName("TIEUDEBUOIHOC");
            entity.Property(e => e.Urlbuoihoc)
                .HasMaxLength(255)
                .HasColumnName("URLBUOIHOC");

            entity.HasOne(d => d.IdLichhocNavigation).WithMany(p => p.LichhocChitiets)
                .HasForeignKey(d => d.IdLichhoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LHCT_LICHHOC");

            entity.HasOne(d => d.IdLoaikynangNavigation).WithMany(p => p.LichhocChitiets)
                .HasForeignKey(d => d.IdLoaikynang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LHCT_LOAIKYNANG");
        });

        modelBuilder.Entity<Loaide>(entity =>
        {
            entity.HasKey(e => e.IdLoaide).HasName("PK__LOAIDE__7929BB964DEA8E0B");

            entity.ToTable("LOAIDE");

            entity.Property(e => e.IdLoaide).HasColumnName("ID_LOAIDE");
            entity.Property(e => e.Tenloaide)
                .HasMaxLength(20)
                .HasColumnName("TENLOAIDE");
        });

        modelBuilder.Entity<Loaikynang>(entity =>
        {
            entity.HasKey(e => e.IdLoaikynang).HasName("PK__LOAIKYNA__1DC3322D0015BE23");

            entity.ToTable("LOAIKYNANG");

            entity.Property(e => e.IdLoaikynang).HasColumnName("ID_LOAIKYNANG");
            entity.Property(e => e.Tenloaikynang)
                .HasMaxLength(255)
                .HasColumnName("TENLOAIKYNANG");
        });

        modelBuilder.Entity<Lophoc>(entity =>
        {
            entity.HasKey(e => e.IdLophoc).HasName("PK__LOPHOC__06CC5F7E2B71FA9D");

            entity.ToTable("LOPHOC");

            entity.Property(e => e.IdLophoc).HasColumnName("ID_LOPHOC");
            entity.Property(e => e.IdKhoahoc).HasColumnName("ID_KHOAHOC");
            entity.Property(e => e.IdTtLophoc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID_TT_LOPHOC");
            entity.Property(e => e.Ngaybatdau).HasColumnName("NGAYBATDAU");
            entity.Property(e => e.Ngayketthuc).HasColumnName("NGAYKETTHUC");
            entity.Property(e => e.Ngaykhaigiang).HasColumnName("NGAYKHAIGIANG");
            entity.Property(e => e.Sisohientai).HasColumnName("SISOHIENTAI");
            entity.Property(e => e.Sisotoida).HasColumnName("SISOTOIDA");

            entity.HasOne(d => d.IdKhoahocNavigation).WithMany(p => p.Lophocs)
                .HasForeignKey(d => d.IdKhoahoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOPHOC_KHOAHOC");
        });

        modelBuilder.Entity<Nhanxettiendo>(entity =>
        {
            entity.HasKey(e => e.IdNhanxet).HasName("PK__NHANXETT__5EF3DC4960DC891F");

            entity.ToTable("NHANXETTIENDO");

            entity.Property(e => e.IdNhanxet).HasColumnName("ID_NHANXET");
            entity.Property(e => e.IdGiaovien).HasColumnName("ID_GIAOVIEN");
            entity.Property(e => e.IdHocvien).HasColumnName("ID_HOCVIEN");
            entity.Property(e => e.IdLophoc).HasColumnName("ID_LOPHOC");
            entity.Property(e => e.Ngaynhanxet)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYNHANXET");
            entity.Property(e => e.Nhanxet)
                .HasMaxLength(255)
                .HasColumnName("NHANXET");

            entity.HasOne(d => d.IdGiaovienNavigation).WithMany(p => p.Nhanxettiendos)
                .HasForeignKey(d => d.IdGiaovien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NHANXETTIENDO_GIAOVIEN");

            entity.HasOne(d => d.IdHocvienNavigation).WithMany(p => p.Nhanxettiendos)
                .HasForeignKey(d => d.IdHocvien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NHANXETTIENDO_HOCVIEN");

            entity.HasOne(d => d.IdLophocNavigation).WithMany(p => p.Nhanxettiendos)
                .HasForeignKey(d => d.IdLophoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NHANXETTIENDO_LOPHOC");
        });

        modelBuilder.Entity<Phancong>(entity =>
        {
            entity.HasKey(e => new { e.IdGiaovien, e.IdLophoc });

            entity.ToTable("PHANCONG");

            entity.HasIndex(e => new { e.IdGiaovien, e.IdLophoc }, "UQ_PHANCONG").IsUnique();

            entity.Property(e => e.IdGiaovien).HasColumnName("ID_GIAOVIEN");
            entity.Property(e => e.IdLophoc).HasColumnName("ID_LOPHOC");
            entity.Property(e => e.Ngayphancong)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("NGAYPHANCONG");

            entity.HasOne(d => d.IdGiaovienNavigation).WithMany(p => p.Phancongs)
                .HasForeignKey(d => d.IdGiaovien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHANCONG_GIAOVIEN");

            entity.HasOne(d => d.IdLophocNavigation).WithMany(p => p.Phancongs)
                .HasForeignKey(d => d.IdLophoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHANCONG_LOPHOC");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.IdTaikhoan).HasName("PK__TAIKHOAN__EB942D7F300EF0C7");

            entity.ToTable("TAIKHOAN");

            entity.HasIndex(e => e.Email, "UQ__TAIKHOAN__161CF724C49F7DE0").IsUnique();

            entity.HasIndex(e => e.Tendangnhap, "UQ__TAIKHOAN__6C836FE53C594D7B").IsUnique();

            entity.HasIndex(e => e.Sodienthoai, "UQ__TAIKHOAN__7670E2990CA106FD").IsUnique();

            entity.Property(e => e.IdTaikhoan).HasColumnName("ID_TAIKHOAN");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.IdVaitro).HasColumnName("ID_VAITRO");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.Matkhauhash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MATKHAUHASH");
            entity.Property(e => e.Ngaytao).HasColumnName("NGAYTAO");
            entity.Property(e => e.Sodienthoai)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SODIENTHOAI");
            entity.Property(e => e.Tendangnhap)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TENDANGNHAP");

            entity.HasOne(d => d.IdVaitroNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.IdVaitro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TAIKHOAN_VAITRO");
        });

        modelBuilder.Entity<Tailieu>(entity =>
        {
            entity.HasKey(e => e.IdTailieu).HasName("PK__TAILIEU__946A8809CF884262");

            entity.ToTable("TAILIEU");

            entity.Property(e => e.IdTailieu).HasColumnName("ID_TAILIEU");
            entity.Property(e => e.IdGv).HasColumnName("ID_GV");
            entity.Property(e => e.IsPublic).HasColumnName("IS_PUBLIC");
            entity.Property(e => e.Motatl).HasColumnName("MOTATL");
            entity.Property(e => e.Ngaydangtl)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDANGTL");
            entity.Property(e => e.Tieudetl)
                .HasMaxLength(255)
                .HasColumnName("TIEUDETL");
            entity.Property(e => e.Urltailieu).HasColumnName("URLTAILIEU");

            entity.HasOne(d => d.IdGvNavigation).WithMany(p => p.Tailieus)
                .HasForeignKey(d => d.IdGv)
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

            entity.ToTable("VAITRO");

            entity.Property(e => e.IdVaitro).HasColumnName("ID_VAITRO");
            entity.Property(e => e.Tenvaitro)
                .HasMaxLength(50)
                .HasColumnName("TENVAITRO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
