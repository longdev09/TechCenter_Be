using System;
using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.LopHoc;
using TechCenter.DTO.LichHoc;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class LopHocService : ILopHocService
    {
        private readonly AppDbContext _context;
        public LopHocService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LopHocByKhoaHocDTO>> GetLopHocByIdKhoaHoc(int idKhoaHoc)
        {
            // Lấy lớp học kèm giảng viên
            var classes = await (from l in _context.Lophocs.AsNoTracking()
                                   where l.IdKhoahoc == idKhoaHoc
                                   join p in _context.Phancongs.AsNoTracking() on l.IdLophoc equals p.IdLophoc into ph
                                   from p in ph.DefaultIfEmpty()
                                   join g in _context.Giaoviens.AsNoTracking() on p.IdGiaovien equals g.IdGiaovien into gv
                                   from g in gv.DefaultIfEmpty()
                                   select new
                                   {
                                       L = l,
                                       HoTenGV = g != null ? g.Hotengv : null
                                   })
                                   .ToListAsync();

            var lophocIds = classes.Select(c => c.L.IdLophoc).ToList();

            // Lấy lịch học cho các lớp
            var lichhocs = await _context.Lichhocs
                .AsNoTracking()
                .Where(x => lophocIds.Contains(x.IdLophoc))
                .Select(x => new LichHocDTO
                {
                    IdLichhoc = x.IdLichhoc,
                    IdLophoc = x.IdLophoc,
                    Thu = x.Thu,
                    Giobatdau = x.Giobatdau,
                    Gioketthuc = x.Gioketthuc
                })
                .ToListAsync();

            // nhóm lịch theo lớp
            var lichTheoLop = lichhocs.GroupBy(x => x.IdLophoc)
                .ToDictionary(g => g.Key, g => g.ToList());

            // map sang DTO trả về
            var result = classes.Select(c => new LopHocByKhoaHocDTO
            {
                NgayKhaiGiang = c.L.Ngaykhaigiang != default ? c.L.Ngaykhaigiang.ToDateTime(new TimeOnly(0, 0)) : (DateTime?)null,
                NgayBatDau = c.L.Ngaybatdau != default ? c.L.Ngaybatdau.ToDateTime(new TimeOnly(0, 0)) : (DateTime?)null,
                NgayKetThuc = c.L.Ngayketthuc != default ? c.L.Ngayketthuc.ToDateTime(new TimeOnly(0, 0)) : (DateTime?)null,
                SiSoHienTai = c.L.Sisohientai,
                SiSoToiDa = c.L.Sisotoida,
                IdKhoaHoc = c.L.IdKhoahoc,
                HoTenGV = c.HoTenGV,
                LichHocs = lichTheoLop.ContainsKey(c.L.IdLophoc) ? lichTheoLop[c.L.IdLophoc] : new List<LichHocDTO>()
            }).ToList();

            // gán ThuName sau khi đã materialize
            foreach (var item in result)
            {
                if (item.LichHocs != null)
                {
                    foreach (var lh in item.LichHocs)
                    {
                        lh.ThuName = lh.Thu switch
                        {
                            2 => "Thứ 2",
                            3 => "Thứ 3",
                            4 => "Thứ 4",
                            5 => "Thứ 5",
                            6 => "Thứ 6",
                            7 => "Thứ 7",
                            8 => "Chủ nhật",
                            _ => lh.Thu.ToString()
                        };
                    }
                }
            }

            return result;
        }

        public async Task<List<LopHocForGiaoVienDTO>> GetLopHocByGiaoVienAsync(int idGiaoVien)
        {
            var query = from l in _context.Lophocs.AsNoTracking()
                        join pc in _context.Phancongs.AsNoTracking() on l.IdLophoc equals pc.IdLophoc into pcj
                        from pc in pcj.DefaultIfEmpty()
                        join kh in _context.Khoahocs.AsNoTracking() on l.IdKhoahoc equals kh.IdKhoahoc into khj
                        from kh in khj.DefaultIfEmpty()
                        where pc != null && pc.IdGiaovien == idGiaoVien
                        select new LopHocForGiaoVienDTO
                        {
                            IdLophoc = l.IdLophoc,
                            IdKhoahoc = l.IdKhoahoc,
                            TenKhoaHoc = kh != null ? kh.Tenkhoahoc : null,
                            IdGvChinh = l.IdGvChinh,
                            IdGiaoVienPhanCong = pc != null ? (int?)pc.IdGiaovien : null,
                            NgayKhaiGiang = l.Ngaykhaigiang != default ? l.Ngaykhaigiang.ToDateTime(new TimeOnly(0, 0)) : (DateTime?)null,
                            SiSoHienTai = l.Sisohientai,
                            SiSoToiDa = l.Sisotoida
                        };

            return await query.ToListAsync();
        }
    }
}
