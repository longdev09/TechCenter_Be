using Microsoft.EntityFrameworkCore;
using System;
using TechCenter.DTO.BaiThi;
using TechCenter.DTO.CauHoi;
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
                    Ngaybatdau = dto.NgayBatDau,
                    Ngayketthuc = dto.NgayKetThuc,
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



        public async Task<List<BaiThiLopHocDTo>> GetLoaiBaiThiByLopIdAsync(int idLop)
        {
            var query = from bt in _context.Baithis.AsNoTracking()
                        join lh in _context.Lophocs.AsNoTracking() on bt.IdLop equals lh.IdLophoc into lhj
                        from lh in lhj.DefaultIfEmpty()
                        join kh in _context.Khoahocs.AsNoTracking() on lh.IdKhoahoc equals kh.IdKhoahoc into khj
                        from kh in khj.DefaultIfEmpty()
                        where lh != null && lh.IdLophoc == idLop && bt.IdLoaibaithi == 2
                        select new BaiThiLopHocDTo
                        {
                            IdBaithi = bt.IdBaithi,
                            IdLop = lh != null ? (int?)lh.IdLophoc : null,
                            IdKhoahoc = kh != null ? (int?)kh.IdKhoahoc : null,
                            TenKhoaHoc = kh != null ? kh.Tenkhoahoc : null,
                            Tieude = bt.Tieude,
                            Thoiluong = bt.Thoiluong,
                            Ngaytao = bt.Ngaytao,
                            NgayBatDau = bt.Ngaybatdau,
                            NgayKetThuc = bt.Ngayketthuc
                        };

            return await query.ToListAsync();
        }


        public async Task<List<CauHoiWithDapAnDTO>> GetCauHoiFullByBaiThiAsync(int idBaiThi)
        {
            // load questions (with type name)
            var questions = await (from ch in _context.Cauhois.AsNoTracking()
                                   join lb in _context.Loaicauhois.AsNoTracking() on ch.IdLoaicauhoi equals lb.IdLoaicauhoi into lbj
                                   from lb in lbj.DefaultIfEmpty()
                                   where ch.IdBaithi == idBaiThi
                                   orderby ch.Stt
                                   select new
                                   {
                                       ch.IdCauhoi,
                                       ch.IdBaithi,
                                       ch.IdLoaicauhoi,
                                       LoaiCauHoi = lb != null ? lb.TenLoai : null,
                                       ch.Stt,
                                       ch.Diem,
                                       ch.Mucdo,
                                       Cauhoi = ch.Cauhoi1
                                   }).ToListAsync();

            if (questions == null || questions.Count == 0)
                return new List<CauHoiWithDapAnDTO>();

            var cauHoiIds = questions.Select(q => q.IdCauhoi).ToList();

            // load multiple-choice answers
            var dapans = await _context.Dapantracnghiems
                .AsNoTracking()
                .Where(d => cauHoiIds.Contains(d.IdCauhoi))
                .Select(d => new DapAnDTO
                {
                    IdDapAn = d.IdDapan,
                    IdCauHoi = d.IdCauhoi,
                    Ma = d.Ma,
                    IsDung = d.Isdung ?? false
                })
                .ToListAsync();

            // load code answers (if any)
            var codes = await _context.CauHoiCodes
                .AsNoTracking()
                .Where(c => cauHoiIds.Contains(c.IdCauHoi))
                .Select(c => new CauHoiCodeDTO
                {
                    IdCauHoi = c.IdCauHoi,
                    CodeMau = c.CodeMau,
                    NgonNgu = c.NgonNgu
                })
                .ToListAsync();

            var dapansBy = dapans.GroupBy(d => d.IdCauHoi).ToDictionary(g => g.Key, g => g.ToList());
            var codesBy = codes.ToDictionary(c => c.IdCauHoi, c => c);

            // map to DTO and attach answers
            var result = questions.Select(q => new CauHoiWithDapAnDTO
            {
                IdCauHoi = q.IdCauhoi,
                IdBaiThi = q.IdBaithi,
                IdLoaiCauHoi = q.IdLoaicauhoi,
                LoaiCauHoi = q.LoaiCauHoi,
                Stt = q.Stt,
                Diem = q.Diem,
                MucDo = q.Mucdo,
                Cauhoi = q.Cauhoi,
                DapAns = dapansBy.ContainsKey(q.IdCauhoi) ? dapansBy[q.IdCauhoi] : new List<DapAnDTO>(),
                CauHoiCode = codesBy.ContainsKey(q.IdCauhoi) ? codesBy[q.IdCauhoi] : null
            }).ToList();

            return result;
        }



    }
}
