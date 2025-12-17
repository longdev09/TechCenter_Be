using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.LichHoc;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class LichHocService : ILichHocService
    {
        private readonly AppDbContext _context;
        public LichHocService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LichHocChiTietHocVienTuan>> GetLichHocChiTietByIdHocVienTheoTuan(int idHocVien, DateOnly tuNgay, DateOnly denNgay)
        {
            var query = from lhct in _context.LichhocChitiets.AsNoTracking()
                        join lh in _context.Lichhocs.AsNoTracking() on lhct.IdLichhoc equals lh.IdLichhoc into lh_join
                        from lh in lh_join.DefaultIfEmpty()
                        join lop in _context.Lophocs.AsNoTracking() on lh.IdLophoc equals lop.IdLophoc into lop_join
                        from lop in lop_join.DefaultIfEmpty()
                        join kh in _context.Khoahocs.AsNoTracking() on lop.IdKhoahoc equals kh.IdKhoahoc into kh_join
                        from kh in kh_join.DefaultIfEmpty()
                        join dk in _context.Dangkylops.AsNoTracking() on lop.IdLophoc equals dk.IdLophoc into dk_join
                        from dk in dk_join.DefaultIfEmpty()
                        join hv in _context.Hocviens.AsNoTracking() on dk.IdHv equals hv.IdHocvien into hv_join
                        from hv in hv_join.DefaultIfEmpty()
                        where lhct.Ngayhoc >= tuNgay && lhct.Ngayhoc <= denNgay
                        && dk.IdHv == idHocVien
                        select new LichHocChiTietHocVienTuan
                        {
                            HoTenHocVien = hv.Hotenhv,
                            IdHv = dk.IdHv,
                            IdLop = lop.IdLophoc,
                            IdKhoaHoc = lop.IdKhoahoc,
                            TenKhoaHoc = kh.Tenkhoahoc,
                            IdLichHoc = lh.IdLichhoc,
                            Thu = lh.Thu,
                            GioBatDau = lh.Giobatdau != null ? (int?)lh.Giobatdau.Hour : null,
                            GioKetThuc = lh.Gioketthuc != null ? (int?)lh.Gioketthuc.Hour : null,
                            IdLHCT = lhct.IdLhct,
                            NgayHoc = lhct.Ngayhoc,
                            TieuDeBuoiHoc = lhct.Tieudebuoihoc,
                            UrlBuoiHoc = lhct.Urlbuoihoc
                        };

            var result = await query.OrderBy(x => x.NgayHoc).ToListAsync();
            return result;
        }


        public async Task<List<LichHocChiTietGiaoVienTuan>> GetLichHocChiTietByIdGiaoVienTheoTuan(int idGiaoVien, DateOnly tuNgay, DateOnly denNgay)
        {
            var query = from lhct in _context.LichhocChitiets.AsNoTracking()
                        join lh in _context.Lichhocs.AsNoTracking() on lhct.IdLichhoc equals lh.IdLichhoc into lh_join
                        from lh in lh_join.DefaultIfEmpty()
                        join lop in _context.Lophocs.AsNoTracking() on lh.IdLophoc equals lop.IdLophoc into lop_join
                        from lop in lop_join.DefaultIfEmpty()
                        join kh in _context.Khoahocs.AsNoTracking() on lop.IdKhoahoc equals kh.IdKhoahoc into kh_join
                        from kh in kh_join.DefaultIfEmpty()
                        // phân công giáo viên
                        join pc in _context.Phancongs.AsNoTracking() on lop.IdLophoc equals pc.IdLophoc into pc_join
                        from pc in pc_join.DefaultIfEmpty()
                        join gv in _context.Giaoviens.AsNoTracking() on pc.IdGiaovien equals gv.IdGiaovien into gv_join
                        from gv in gv_join.DefaultIfEmpty()
                        where lhct.Ngayhoc >= tuNgay
                              && lhct.Ngayhoc <= denNgay
                              && pc.IdGiaovien == idGiaoVien
                        select new LichHocChiTietGiaoVienTuan
                        {
                            IdGiaoVien = pc.IdGiaovien,
                            HoTenGiaoVien = gv.Hotengv,
                            IdLop = lop.IdLophoc,
                            IdKhoaHoc = lop.IdKhoahoc,
                            TenKhoaHoc = kh.Tenkhoahoc,
                            IdLichHoc = lh.IdLichhoc,
                            Thu = lh.Thu,
                            GioBatDau = lh.Giobatdau != null ? (int?)lh.Giobatdau.Hour : null,
                            GioKetThuc = lh.Gioketthuc != null ? (int?)lh.Gioketthuc.Hour : null,
                            IdLHCT = lhct.IdLhct,
                            NgayHoc = lhct.Ngayhoc,
                            TieuDeBuoiHoc = lhct.Tieudebuoihoc,
                            UrlBuoiHoc = lhct.Urlbuoihoc
                        };

            var result = await query
                .OrderBy(x => x.NgayHoc)
                .ThenBy(x => x.GioBatDau)
                .ToListAsync();

            return result;
        }
    }
}
