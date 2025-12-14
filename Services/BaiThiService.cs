using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.BaiThi;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class BaiThiService : IBaiThiService
    {

        private readonly AppDbContext _context;
        public BaiThiService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BaithiWithKhoaDTO>> GetBaithiByLopIdAsync(int idLop)
        {
            var query = from bt in _context.Baithis.AsNoTracking()
                        join lh in _context.Lophocs.AsNoTracking() on bt.IdLop equals lh.IdLophoc into lhj
                        from lh in lhj.DefaultIfEmpty()
                        join kh in _context.Khoahocs.AsNoTracking() on lh.IdKhoahoc equals kh.IdKhoahoc into khj
                        from kh in khj.DefaultIfEmpty()
                        where lh != null && lh.IdLophoc == idLop
                        select new BaithiWithKhoaDTO
                        {
                            IdBaithi = bt.IdBaithi,
                            IdLop = lh != null ? (int?)lh.IdLophoc : null,
                            IdKhoahoc = kh != null ? (int?)kh.IdKhoahoc : null,
                            TenKhoaHoc = kh != null ? kh.Tenkhoahoc : null,
                            Tieude = bt.Tieude,              
                            Thoiluong = bt.Thoiluong,
                            Ngaytao = bt.Ngaytao
                        };

            return await query.ToListAsync();
        }

            public async Task<int> InsertBaiThiAsync(InsertBaiThiDTO dto)
            {
                // map DTO to entity
                var entity = new Baithi
                {
                    Tieude = dto.Tieude,
                    Thoiluong = dto.ThoiLuong,
                    Mota = dto.Mota,
                    Nguoitao = dto.NguoiTao,
                    IdLop = dto.Id_Lop,
                    IdLoaibaithi = dto.Id_LoaiBaiThi,
                    Ngaybatdau = dto.NgayBatDau.HasValue ? DateOnly.FromDateTime(dto.NgayBatDau.Value) : null,
                    Ngayketthuc = dto.NgayKetThuc.HasValue ? DateOnly.FromDateTime(dto.NgayKetThuc.Value) : null,
                    Ngaytao = DateTime.UtcNow
                };

                _context.Baithis.Add(entity);
                await _context.SaveChangesAsync();

                return entity.IdBaithi;
            }

        public async Task<object> GetBaithiByGiaoVienAsync(int idGiaoVien)
        {
            var query =
                from bt in _context.Baithis.AsNoTracking()
                where _context.Phancongs
                    .Any(pc => pc.IdLophoc == bt.IdLop && pc.IdGiaovien == idGiaoVien)
                join lh in _context.Lophocs.AsNoTracking() on bt.IdLop equals lh.IdLophoc into lhj
                from lh in lhj.DefaultIfEmpty()
                join kh in _context.Khoahocs.AsNoTracking() on lh.IdKhoahoc equals kh.IdKhoahoc into khj
                from kh in khj.DefaultIfEmpty()
                join lbt in _context.LoaiBaithis.AsNoTracking() on bt.IdLoaibaithi equals lbt.IdLoaibaithi into lbtj
                from lbt in lbtj.DefaultIfEmpty()
                select new
                {
                    IdKhoaHoc = kh != null ? (int?)kh.IdKhoahoc : null,
                    TenKhoaHoc = kh.Tenkhoahoc,
                    IdBaithi = bt.IdBaithi,
                    IdLop = bt.IdLop,
                    IdLoaiBaithi = bt.IdLoaibaithi,
                    TenLoai = lbt.Tenloai,
                    NgayBatDau = bt.Ngaybatdau,
                    NgayKetThuc = bt.Ngayketthuc,
                    ThoiLuong = bt.Thoiluong,
                    Tieude = bt.Tieude
                };

            var result = query.GroupBy(x => new { x.IdKhoaHoc, x.TenKhoaHoc }).Select(g => new
            {
                IdKhoaHoc = g.Key.IdKhoaHoc,
                TenKhoaHoc = g.Key.TenKhoaHoc,
                Baithis = g.Select(b => new
                {
                    
                    b.IdBaithi,
                    b.IdLop,
                    b.IdLoaiBaithi,
                    b.TenLoai,
                    b.NgayBatDau,
                    b.NgayKetThuc,
                    b.ThoiLuong,
                    b.Tieude
                }).ToList()
            });

            return result;

        }

    }
}
