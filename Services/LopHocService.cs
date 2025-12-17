using System;
using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.LopHoc;
using TechCenter.DTO.LichHoc;
using TechCenter.DTO.HocVien;
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
            // Get classes assigned to teacher
            var classes = await (from l in _context.Lophocs.AsNoTracking()
                                 join pc in _context.Phancongs.AsNoTracking() on l.IdLophoc equals pc.IdLophoc
                                 join kh in _context.Khoahocs.AsNoTracking() on l.IdKhoahoc equals kh.IdKhoahoc into khj
                                 from kh in khj.DefaultIfEmpty()
                                 where pc.IdGiaovien == idGiaoVien
                                 select new
                                 {
                                     L = l,
                                     Pc = pc,
                                     TenKhoaHoc = kh != null ? kh.Tenkhoahoc : null
                                 })
                                 .ToListAsync();

            var lophocIds = classes.Select(c => c.L.IdLophoc).ToList();

            // load schedules for these classes
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

            var lichTheoLop = lichhocs.GroupBy(x => x.IdLophoc)
                .ToDictionary(g => g.Key, g => g.ToList());

            // map to DTO and attach schedules
            var result = classes.Select(c => new LopHocForGiaoVienDTO
            {
                IdLophoc = c.L.IdLophoc,
                IdKhoahoc = c.L.IdKhoahoc,
                TenKhoaHoc = c.TenKhoaHoc,
                IdGiaoVienPhanCong = c.Pc != null ? (int?)c.Pc.IdGiaovien : null,
                NgayKhaiGiang = c.L.Ngaykhaigiang != default ? c.L.Ngaykhaigiang.ToDateTime(new TimeOnly(0, 0)) : (DateTime?)null,
                SiSoHienTai = c.L.Sisohientai,
                SiSoToiDa = c.L.Sisotoida,
                LichHocs = lichTheoLop.ContainsKey(c.L.IdLophoc) ? lichTheoLop[c.L.IdLophoc] : new List<LichHocDTO>()
            }).ToList();

            // set ThuName for each schedule item
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
                            _ => lh.Thu?.ToString() ?? string.Empty
                        };
                    }
                }
            }

            return result;
        }

        public async Task<List<HocVienDTO>> GetHocVienByLopAsync(int idLophoc)
        {
            var query = from dk in _context.Dangkylops.AsNoTracking()
                        join hv in _context.Hocviens.AsNoTracking() on dk.IdHv equals hv.IdHocvien
                        join tk in _context.Taikhoans.AsNoTracking() on hv.IdTaikhoan equals tk.IdTaikhoan into tkj
                        from tk in tkj.DefaultIfEmpty()
                        where dk.IdLophoc == idLophoc
                        select new HocVienDTO
                        {
                            idHocVien = hv.IdHocvien,
                            idTaiKhoan = hv.IdTaikhoan,
                            hoTenHv = hv.Hotenhv,
                            gioiTinhHv = hv.Gioitinhhv,
                            ngaySinhHv = hv.Ngaysinhhv,
                            diaChiHv = hv.Diachihv
                        };

            var result = await query.Distinct().ToListAsync();
            return result;
        }

        public async Task<List<LopHocByHocVienDTO>> GetLopHocByHocVienAsync(int idHocVien)
        {
            var classesRaw = await (from dk in _context.Dangkylops.AsNoTracking()
                                     join l in _context.Lophocs.AsNoTracking() on dk.IdLophoc equals l.IdLophoc
                                     join kh in _context.Khoahocs.AsNoTracking() on l.IdKhoahoc equals kh.IdKhoahoc into khj
                                     from kh in khj.DefaultIfEmpty()
                                     // left join phancong -> giaovien to get teacher info
                                     join pc in _context.Phancongs.AsNoTracking() on l.IdLophoc equals pc.IdLophoc into pcj
                                     from pc in pcj.DefaultIfEmpty()
                                     join g in _context.Giaoviens.AsNoTracking() on pc.IdGiaovien equals g.IdGiaovien into gj
                                     from g in gj.DefaultIfEmpty()
                                     where dk.IdHv == idHocVien
                                     select new
                                     {
                                         Dk = dk,
                                         L = l,
                                         TenKhoaHoc = kh != null ? kh.Tenkhoahoc : null,
                                         TenGiaoVien = g != null ? g.Hotengv : null,
                                         AnhGiaoVien = (string?)null
                                     })
                                     .ToListAsync();

            // Deduplicate by IdLophoc (prefer entry that has teacher name if multiple rows exist)
            var classes = classesRaw
                .GroupBy(c => c.L.IdLophoc)
                .Select(g => g.OrderByDescending(x => !string.IsNullOrEmpty(x.TenGiaoVien)).First())
                .ToList();

            var lophocIds = classes.Select(c => c.L.IdLophoc).ToList();

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

            var lichTheoLop = lichhocs.GroupBy(x => x.IdLophoc)
                .ToDictionary(g => g.Key, g => g.ToList());

            var result = classes.Select(c => new LopHocByHocVienDTO
            {
                IdLophoc = c.L.IdLophoc,
                IdKhoahoc = c.L.IdKhoahoc,
                TenKhoaHoc = c.TenKhoaHoc,
                NgayKhaiGiang = c.L.Ngaykhaigiang != default ? c.L.Ngaykhaigiang.ToDateTime(new TimeOnly(0, 0)) : (DateTime?)null,
                SiSoHienTai = c.L.Sisohientai,
                SiSoToiDa = c.L.Sisotoida,
                IdDangKy = c.Dk.IdDangky,
                NgayDangKy = c.Dk.Ngaydangky,
                TenGiaoVien = c.TenGiaoVien,
                AnhGiaoVien = c.AnhGiaoVien,
                LichHocs = lichTheoLop.ContainsKey(c.L.IdLophoc) ? lichTheoLop[c.L.IdLophoc] : new List<LichHocDTO>(),
            }).ToList();

            // set ThuName for schedule items
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
                            _ => lh.Thu?.ToString() ?? string.Empty
                        };
                    }
                }
            }

            return result;
        }




      
    }


}

