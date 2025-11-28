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
    }
}
