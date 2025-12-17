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

    public virtual DbSet<Baithi> Baithis { get; set; }

    public virtual DbSet<Baiviet> Baiviets { get; set; }

    public virtual DbSet<Baocaohoctap> Baocaohoctaps { get; set; }

    public virtual DbSet<Capdokhoahoc> Capdokhoahocs { get; set; }

    public virtual DbSet<CauHoiCode> CauHoiCodes { get; set; }

    public virtual DbSet<Cauhoi> Cauhois { get; set; }

    public virtual DbSet<Chatmessage> Chatmessages { get; set; }

    public virtual DbSet<Chatsession> Chatsessions { get; set; }

    public virtual DbSet<ChitietbailamTuluan> ChitietbailamTuluans { get; set; }

    public virtual DbSet<Chitietbailamtracnghiem> Chitietbailamtracnghiems { get; set; }

    public virtual DbSet<Chungchi> Chungchis { get; set; }

    public virtual DbSet<Ctuudai> Ctuudais { get; set; }

    public virtual DbSet<Dangkylop> Dangkylops { get; set; }

    public virtual DbSet<Danhgiakh> Danhgiakhs { get; set; }

    public virtual DbSet<Dapantracnghiem> Dapantracnghiems { get; set; }

    public virtual DbSet<Diemdanh> Diemdanhs { get; set; }

    public virtual DbSet<DkyChungchi> DkyChungchis { get; set; }

    public virtual DbSet<Giaovien> Giaoviens { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Hocvien> Hocviens { get; set; }

    public virtual DbSet<Ketquathi> Ketquathis { get; set; }

    public virtual DbSet<Khoahoc> Khoahocs { get; set; }

    public virtual DbSet<Lichhoc> Lichhocs { get; set; }

    public virtual DbSet<LichhocChitiet> LichhocChitiets { get; set; }

    public virtual DbSet<LoaiBaithi> LoaiBaithis { get; set; }

    public virtual DbSet<Loaicauhoi> Loaicauhois { get; set; }

    public virtual DbSet<Loaikynang> Loaikynangs { get; set; }

    public virtual DbSet<Lophoc> Lophocs { get; set; }

    public virtual DbSet<Nhanxettiendo> Nhanxettiendos { get; set; }

    public virtual DbSet<Phancong> Phancongs { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Tailieu> Tailieus { get; set; }

    public virtual DbSet<Vaitro> Vaitros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SQL5113.site4now.net;Initial Catalog=db_ac08ce_sa2107;User Id=db_ac08ce_sa2107_admin;Password=sa21071009");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baithi>(entity =>
        {
            entity.HasKey(e => e.IdBaithi).HasName("PK__BAITHI__949751C41EF95FE4");

            entity.ToTable("BAITHI");

            entity.Property(e => e.IdBaithi).HasColumnName("ID_BAITHI");
            entity.Property(e => e.IdLoaibaithi)
                .HasDefaultValue(2)
                .HasColumnName("ID_LOAIBAITHI");
            entity.Property(e => e.IdLop).HasColumnName("ID_LOP");
            entity.Property(e => e.Mota)
                .HasMaxLength(255)
                .HasColumnName("MOTA");
            entity.Property(e => e.Ngaybatdau)
                .HasColumnType("datetime")
                .HasColumnName("NGAYBATDAU");
            entity.Property(e => e.Ngayketthuc)
                .HasColumnType("datetime")
                .HasColumnName("NGAYKETTHUC");
            entity.Property(e => e.Ngaytao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Nguoitao).HasColumnName("NGUOITAO");
            entity.Property(e => e.Thoiluong).HasColumnName("THOILUONG");
            entity.Property(e => e.Tieude)
                .HasMaxLength(255)
                .HasColumnName("TIEUDE");

            entity.HasOne(d => d.IdLoaibaithiNavigation).WithMany(p => p.Baithis)
                .HasForeignKey(d => d.IdLoaibaithi)
                .HasConstraintName("FK__BAITHI__ID_LOAIB__4830B400");

            entity.HasOne(d => d.IdLopNavigation).WithMany(p => p.Baithis)
                .HasForeignKey(d => d.IdLop)
                .HasConstraintName("FK_BAITHI_LOPHOC");
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

        modelBuilder.Entity<Capdokhoahoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAPDOKHO__3214EC27D2E9010D");

            entity.ToTable("CAPDOKHOAHOC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MauSac).HasMaxLength(20);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TenCapDo).HasMaxLength(100);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<CauHoiCode>(entity =>
        {
            entity.HasKey(e => e.IdCauHoi).HasName("PK__CauHoiCo__7F0B00FF513BF79E");

            entity.ToTable("CauHoiCode");

            entity.Property(e => e.IdCauHoi).ValueGeneratedNever();
            entity.Property(e => e.NgonNgu).HasMaxLength(50);

            entity.HasOne(d => d.IdCauHoiNavigation).WithOne(p => p.CauHoiCode)
                .HasForeignKey<CauHoiCode>(d => d.IdCauHoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CauHoiCod__IdCau__16644E42");
        });

        modelBuilder.Entity<Cauhoi>(entity =>
        {
            entity.HasKey(e => e.IdCauhoi).HasName("PK__CAUHOI__674FD6FC40D59B93");

            entity.ToTable("CAUHOI");

            entity.Property(e => e.IdCauhoi).HasColumnName("ID_CAUHOI");
            entity.Property(e => e.Cauhoi1).HasColumnName("CAUHOI");
            entity.Property(e => e.Diem)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("DIEM");
            entity.Property(e => e.IdBaithi).HasColumnName("ID_BAITHI");
            entity.Property(e => e.IdLoaicauhoi).HasColumnName("ID_LOAICAUHOI");
            entity.Property(e => e.Mucdo)
                .HasMaxLength(255)
                .HasColumnName("MUCDO");
            entity.Property(e => e.Stt).HasColumnName("STT");

            entity.HasOne(d => d.IdBaithiNavigation).WithMany(p => p.Cauhois)
                .HasForeignKey(d => d.IdBaithi)
                .HasConstraintName("FK_CAUHOI_BAITHI");

            entity.HasOne(d => d.IdLoaicauhoiNavigation).WithMany(p => p.Cauhois)
                .HasForeignKey(d => d.IdLoaicauhoi)
                .HasConstraintName("FK_CAUHOI_LOAICAUHOI");
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

        modelBuilder.Entity<ChitietbailamTuluan>(entity =>
        {
            entity.HasKey(e => e.IdBailam).HasName("PK__CHITIETB__90BEDB607DA60768");

            entity.ToTable("CHITIETBAILAM_TULUAN");

            entity.HasIndex(e => new { e.IdKetqua, e.IdCauhoi }, "UQ_TULUAN").IsUnique();

            entity.Property(e => e.IdBailam).HasColumnName("ID_BAILAM");
            entity.Property(e => e.DiemGv).HasColumnName("DIEM_GV");
            entity.Property(e => e.IdCauhoi).HasColumnName("ID_CAUHOI");
            entity.Property(e => e.IdKetqua).HasColumnName("ID_KETQUA");
            entity.Property(e => e.Ngaycham)
                .HasColumnType("datetime")
                .HasColumnName("NGAYCHAM");
            entity.Property(e => e.NhanxetGv)
                .HasMaxLength(500)
                .HasColumnName("NHANXET_GV");
            entity.Property(e => e.Noidungtl).HasColumnName("NOIDUNGTL");

            entity.HasOne(d => d.IdCauhoiNavigation).WithMany(p => p.ChitietbailamTuluans)
                .HasForeignKey(d => d.IdCauhoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BLT_CAUHOI");

            entity.HasOne(d => d.IdKetquaNavigation).WithMany(p => p.ChitietbailamTuluans)
                .HasForeignKey(d => d.IdKetqua)
                .HasConstraintName("FK_BLT_KETQUA");
        });

        modelBuilder.Entity<Chitietbailamtracnghiem>(entity =>
        {
            entity.HasKey(e => e.IdChitiet).HasName("PK__CHITIETB__727EE308823CD9CB");

            entity.ToTable("CHITIETBAILAMTRACNGHIEM");

            entity.HasIndex(e => new { e.IdKetqua, e.IdCauhoi }, "UQ_KETQUA_CAUHOI").IsUnique();

            entity.Property(e => e.IdChitiet).HasColumnName("ID_CHITIET");
            entity.Property(e => e.DapanChon)
                .HasMaxLength(10)
                .HasColumnName("DAPAN_CHON");
            entity.Property(e => e.Diem).HasColumnName("DIEM");
            entity.Property(e => e.IdCauhoi).HasColumnName("ID_CAUHOI");
            entity.Property(e => e.IdKetqua).HasColumnName("ID_KETQUA");
            entity.Property(e => e.IsDung).HasColumnName("IS_DUNG");

            entity.HasOne(d => d.IdCauhoiNavigation).WithMany(p => p.Chitietbailamtracnghiems)
                .HasForeignKey(d => d.IdCauhoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTBL_CAUHOI");

            entity.HasOne(d => d.IdKetquaNavigation).WithMany(p => p.Chitietbailamtracnghiems)
                .HasForeignKey(d => d.IdKetqua)
                .HasConstraintName("FK_CTBL_KETQUA");
        });

        modelBuilder.Entity<Chungchi>(entity =>
        {
            entity.HasKey(e => e.IdChungchi).HasName("PK__CHUNGCHI__92727E22041577B0");

            entity.ToTable("CHUNGCHI");

            entity.HasIndex(e => e.Tenchungchi, "UQ__CHUNGCHI__33E88737B6A595F5").IsUnique();

            entity.Property(e => e.IdChungchi).HasColumnName("ID_CHUNGCHI");
            entity.Property(e => e.IdKhoahoc).HasColumnName("ID_KHOAHOC");
            entity.Property(e => e.Motacc).HasColumnName("MOTACC");
            entity.Property(e => e.Tenchungchi)
                .HasMaxLength(200)
                .HasColumnName("TENCHUNGCHI");

            entity.HasOne(d => d.IdKhoahocNavigation).WithMany(p => p.Chungchis)
                .HasForeignKey(d => d.IdKhoahoc)
                .HasConstraintName("FK_CC_KH");
        });

        modelBuilder.Entity<Ctuudai>(entity =>
        {
            entity.HasKey(e => e.IdUudai).HasName("PK__CTUUDAI__CB3D935954C97BC4");

            entity.ToTable("CTUUDAI");

            entity.Property(e => e.IdUudai).HasColumnName("ID_UUDAI");
            entity.Property(e => e.NgaybatdauUd).HasColumnName("NGAYBATDAU_UD");
            entity.Property(e => e.NgayketthucUd).HasColumnName("NGAYKETTHUC_UD");
            entity.Property(e => e.Phantramuudai).HasColumnName("PHANTRAMUUDAI");
            entity.Property(e => e.Soluothientai).HasColumnName("SOLUOTHIENTAI");
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

        modelBuilder.Entity<Danhgiakh>(entity =>
        {
            entity.HasKey(e => e.IdDanhgia).HasName("PK__DANHGIAK__D7D8AB784C035CAA");

            entity.ToTable("DANHGIAKH");

            entity.HasIndex(e => new { e.IdKhoahoc, e.IdHocvien }, "UQ_DANHGIAKH_KHOAHOC_HOCVIEN").IsUnique();

            entity.Property(e => e.IdDanhgia).HasColumnName("ID_DANHGIA");
            entity.Property(e => e.IdHocvien).HasColumnName("ID_HOCVIEN");
            entity.Property(e => e.IdKhoahoc).HasColumnName("ID_KHOAHOC");
            entity.Property(e => e.Ngaydanhgia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYDANHGIA");
            entity.Property(e => e.Noidung)
                .HasMaxLength(150)
                .HasColumnName("NOIDUNG");
            entity.Property(e => e.Sosao).HasColumnName("SOSAO");

            entity.HasOne(d => d.IdHocvienNavigation).WithMany(p => p.Danhgiakhs)
                .HasForeignKey(d => d.IdHocvien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DANHGIAKH_HOCVIEN");

            entity.HasOne(d => d.IdKhoahocNavigation).WithMany(p => p.Danhgiakhs)
                .HasForeignKey(d => d.IdKhoahoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DANHGIAKH_KHOAHOC");
        });

        modelBuilder.Entity<Dapantracnghiem>(entity =>
        {
            entity.HasKey(e => e.IdDapan).HasName("PK__DAPANTRA__ED9B1D734476980D");

            entity.ToTable("DAPANTRACNGHIEM");

            entity.Property(e => e.IdDapan).HasColumnName("ID_DAPAN");
            entity.Property(e => e.Cauhoidapan)
                .HasMaxLength(500)
                .HasColumnName("CAUHOIDAPAN");
            entity.Property(e => e.IdCauhoi).HasColumnName("ID_CAUHOI");
            entity.Property(e => e.Isdung)
                .HasDefaultValue(false)
                .HasColumnName("ISDUNG");
            entity.Property(e => e.Ma).HasMaxLength(10);

            entity.HasOne(d => d.IdCauhoiNavigation).WithMany(p => p.Dapantracnghiems)
                .HasForeignKey(d => d.IdCauhoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DAPANTRAC__ID_CA__0169315C");
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
                .HasColumnName("GIOITINHGV");
            entity.Property(e => e.Hotengv)
                .HasMaxLength(100)
                .HasColumnName("HOTENGV");
            entity.Property(e => e.IdTaikhoan).HasColumnName("ID_TAIKHOAN");
            entity.Property(e => e.Ngaysinhgv).HasColumnName("NGAYSINHGV");

            entity.HasOne(d => d.IdTaikhoanNavigation).WithOne(p => p.Giaovien)
                .HasForeignKey<Giaovien>(d => d.IdTaikhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GIAOVIEN_TAIKHOAN");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.Idhoadon).HasName("PK__HOADON__ADBC99C6FE602DBA");

            entity.ToTable("HOADON");

            entity.Property(e => e.Idhoadon).HasColumnName("IDHOADON");
            entity.Property(e => e.Ghichu)
                .HasMaxLength(500)
                .HasColumnName("GHICHU");
            entity.Property(e => e.IdHocvien).HasColumnName("ID_HOCVIEN");
            entity.Property(e => e.IdLophoc).HasColumnName("ID_LOPHOC");
            entity.Property(e => e.Mahoadon)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAHOADON");
            entity.Property(e => e.Ngaytao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Tongtien)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TONGTIEN");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(50)
                .HasDefaultValue("CHUATHANHTOAN")
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.IdHocvienNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.IdHocvien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HOADON_HOCVIEN");

            entity.HasOne(d => d.IdLophocNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.IdLophoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HOADON_LOPHOC");
        });

        modelBuilder.Entity<Hocvien>(entity =>
        {
            entity.HasKey(e => e.IdHocvien).HasName("PK__HOCVIEN__3350F93D674DB26B");

            entity.ToTable("HOCVIEN");

            entity.HasIndex(e => e.IdTaikhoan, "UQ__HOCVIEN__EB942D7EF1E73928").IsUnique();

            entity.Property(e => e.IdHocvien).HasColumnName("ID_HOCVIEN");
            entity.Property(e => e.AnhHv).IsUnicode(false);
            entity.Property(e => e.Diachihv)
                .HasMaxLength(255)
                .HasColumnName("DIACHIHV");
            entity.Property(e => e.Gioitinhhv)
                .HasMaxLength(3)
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

        modelBuilder.Entity<Ketquathi>(entity =>
        {
            entity.HasKey(e => e.IdKetqua).HasName("PK__KETQUATH__EB9950D9A6EB1A96");

            entity.ToTable("KETQUATHI");

            entity.Property(e => e.IdKetqua).HasColumnName("ID_KETQUA");
            entity.Property(e => e.IdBaithi).HasColumnName("ID_BAITHI");
            entity.Property(e => e.IdHocvien).HasColumnName("ID_HOCVIEN");
            entity.Property(e => e.LanThi)
                .HasDefaultValue(1)
                .HasColumnName("LAN_THI");
            entity.Property(e => e.Ngaythi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYTHI");
            entity.Property(e => e.Thoigianbatdau)
                .HasColumnType("datetime")
                .HasColumnName("THOIGIANBATDAU");
            entity.Property(e => e.Thoigianketthuc)
                .HasColumnType("datetime")
                .HasColumnName("THOIGIANKETTHUC");
            entity.Property(e => e.Tongdiem).HasColumnName("TONGDIEM");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(20)
                .HasDefaultValue("DANG_LAM")
                .HasColumnName("TRANGTHAI");
            entity.Property(e => e.TrangthaiCham)
                .HasMaxLength(20)
                .HasDefaultValue("CHO_CHAM")
                .HasColumnName("TRANGTHAI_CHAM");

            entity.HasOne(d => d.IdBaithiNavigation).WithMany(p => p.Ketquathis)
                .HasForeignKey(d => d.IdBaithi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KQ_BAITHI");

            entity.HasOne(d => d.IdHocvienNavigation).WithMany(p => p.Ketquathis)
                .HasForeignKey(d => d.IdHocvien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KQ_HOCVIEN");
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
            entity.Property(e => e.Diemdanhgia)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("DIEMDANHGIA");
            entity.Property(e => e.Hocphi)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("HOCPHI");
            entity.Property(e => e.IdLoaikynang).HasColumnName("ID_LOAIKYNANG");
            entity.Property(e => e.Idcapdokhoahoc).HasColumnName("IDCAPDOKHOAHOC");
            entity.Property(e => e.Ketquadatduoc).HasColumnName("KETQUADATDUOC");
            entity.Property(e => e.Motakh).HasColumnName("MOTAKH");
            entity.Property(e => e.Noidungkhoahoc).HasColumnName("NOIDUNGKHOAHOC");
            entity.Property(e => e.Tenkhoahoc)
                .HasMaxLength(100)
                .HasColumnName("TENKHOAHOC");

            entity.HasOne(d => d.IdLoaikynangNavigation).WithMany(p => p.Khoahocs)
                .HasForeignKey(d => d.IdLoaikynang)
                .HasConstraintName("KHOAHOC_ID_LOAIKYNANG");

            entity.HasOne(d => d.IdcapdokhoahocNavigation).WithMany(p => p.Khoahocs)
                .HasForeignKey(d => d.Idcapdokhoahoc)
                .HasConstraintName("FK_KHOAHOC_CAPDO");
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
            entity.Property(e => e.IdGiaovienDaythay).HasColumnName("ID_GIAOVIEN_DAYTHAY");
            entity.Property(e => e.IdLichhoc).HasColumnName("ID_LICHHOC");
            entity.Property(e => e.Ngayhoc).HasColumnName("NGAYHOC");
            entity.Property(e => e.Tieudebuoihoc)
                .HasMaxLength(255)
                .HasColumnName("TIEUDEBUOIHOC");
            entity.Property(e => e.Urlbuoihoc)
                .HasMaxLength(255)
                .HasColumnName("URLBUOIHOC");

            entity.HasOne(d => d.IdGiaovienDaythayNavigation).WithMany(p => p.LichhocChitiets)
                .HasForeignKey(d => d.IdGiaovienDaythay)
                .HasConstraintName("FK_LHCT_GV");

            entity.HasOne(d => d.IdLichhocNavigation).WithMany(p => p.LichhocChitiets)
                .HasForeignKey(d => d.IdLichhoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LHCT_LICHHOC");
        });

        modelBuilder.Entity<LoaiBaithi>(entity =>
        {
            entity.HasKey(e => e.IdLoaibaithi).HasName("PK__LOAI_BAI__B1B6664DC319FC6E");

            entity.ToTable("LOAI_BAITHI");

            entity.Property(e => e.IdLoaibaithi).HasColumnName("ID_LOAIBAITHI");
            entity.Property(e => e.Tenloai)
                .HasMaxLength(100)
                .HasColumnName("TENLOAI");
        });

        modelBuilder.Entity<Loaicauhoi>(entity =>
        {
            entity.HasKey(e => e.IdLoaicauhoi).HasName("PK__LOAICAUH__84202F55A9DD433F");

            entity.ToTable("LOAICAUHOI");

            entity.Property(e => e.IdLoaicauhoi).HasColumnName("ID_LOAICAUHOI");
            entity.Property(e => e.Mota)
                .HasMaxLength(255)
                .HasColumnName("MOTA");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(100)
                .HasColumnName("TEN_LOAI");
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
            entity.Property(e => e.IsPublic).HasColumnName("IS_PUBLIC");
            entity.Property(e => e.Motatl).HasColumnName("MOTATL");
            entity.Property(e => e.Ngaydangtl)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDANGTL");
            entity.Property(e => e.Tieudetl)
                .HasMaxLength(255)
                .HasColumnName("TIEUDETL");
            entity.Property(e => e.Urltailieu).HasColumnName("URLTAILIEU");

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
