using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.ThongKe;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class ThongKeService : IThongKeService
    {

        private readonly AppDbContext _context;
        public ThongKeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DTO.ThongKe.ThongKeHocVienDTO> GetThongKeHocVienAsync(int idHocVien)
        {
            // Base query: tất cả kết quả thi của học viên
            var ketQuaQuery =
                from kq in _context.Ketquathis.AsNoTracking()
                where kq.IdHocvien == idHocVien
                join bt in _context.Baithis.AsNoTracking() on kq.IdBaithi equals bt.IdBaithi
                join lh in _context.Lophocs.AsNoTracking() on bt.IdLop equals lh.IdLophoc into lhj
                from lh in lhj.DefaultIfEmpty()
                join kh in _context.Khoahocs.AsNoTracking() on lh.IdKhoahoc equals kh.IdKhoahoc into khj
                from kh in khj.DefaultIfEmpty()
                select new
                {
                    kq,
                    BaiThi = bt,
                    Lop = lh,
                    KhoaHoc = kh
                };

            var list = await ketQuaQuery.ToListAsync();

            var result = new DTO.ThongKe.ThongKeHocVienDTO();

            if (!list.Any())
                return result;

            // 1) Điểm trung bình theo môn (khoá học) - chỉ tính bài đã chấm có điểm
            var diemTbMon = list
                .Where(x => x.kq.TrangthaiCham == "DA_CHAM" && x.kq.Tongdiem.HasValue)
                .GroupBy(x => new
                {
                    IdKhoaHoc = x.KhoaHoc != null ? (int?)x.KhoaHoc.IdKhoahoc : null,
                    TenKhoaHoc = x.KhoaHoc != null ? x.KhoaHoc.Tenkhoahoc : null
                })
                .Select(g => new DTO.ThongKe.DiemTrungBinhMonDTO
                {
                    IdKhoaHoc = g.Key.IdKhoaHoc,
                    TenKhoaHoc = g.Key.TenKhoaHoc,
                    DiemTrungBinh = g.Average(i => i.kq.Tongdiem ?? 0),
                    SoBaiThi = g.Count()
                })
                .ToList();

            result.DiemTrungBinhTheoMon = diemTbMon;

            // 2) Điểm từng bài thi theo thời gian (line chart)
            result.DiemTheoThoiGian = list
                .Where(x => x.kq.TrangthaiCham == "DA_CHAM") // đã chấm
                .OrderBy(x => x.kq.Ngaythi)
                .Select(x => new DTO.ThongKe.DiemTheoBaiThiDTO
                {
                    IdBaithi = x.kq.IdBaithi,
                    TenBaiThi = x.BaiThi.Tieude,
                    NgayThi = x.kq.Ngaythi,
                    Diem = x.kq.Tongdiem,
                    MonHoc = x.KhoaHoc != null ? x.KhoaHoc.Tenkhoahoc : null
                })
                .ToList();

            // 3) Tỉ lệ chấm điểm (pie chart)
            var tongBai = list.Count;
            var soDaCham = list.Count(x => x.kq.TrangthaiCham == "DA_CHAM");
            var soChoCham = tongBai - soDaCham;

            result.TyLeChamDiem = new DTO.ThongKe.TyLeChamDiemDTO
            {
                TongBaiThi = tongBai,
                SoBaiDaCham = soDaCham,
                SoBaiChoCham = soChoCham
            };

            return result;
        }

        // NEW: Thống kê cho giảng viên
        public async Task<ThongKeGiaoVienDTO> GetThongKeGiaoVienAsync(int idGiaoVien)
        {
            // 1. Lấy danh sách lớp mà giảng viên được phân công
            var lopIds = await _context.Phancongs
                .Where(pc => pc.IdGiaovien == idGiaoVien)
                .Select(pc => pc.IdLophoc)
                .Distinct()
                .ToListAsync();

            var result = new ThongKeGiaoVienDTO();

            if (!lopIds.Any())
                return result;

            // 2. Lấy toàn bộ kết quả thi (ketquathis) thuộc các lớp đó
            var raw = await (
                from kq in _context.Ketquathis.AsNoTracking()
                join bt in _context.Baithis.AsNoTracking() on kq.IdBaithi equals bt.IdBaithi
                where bt.IdLop != null && lopIds.Contains(bt.IdLop.Value)
                join lop in _context.Lophocs.AsNoTracking() on bt.IdLop equals lop.IdLophoc into lopj
                from lop in lopj.DefaultIfEmpty()
                join kh in _context.Khoahocs.AsNoTracking() on lop.IdKhoahoc equals kh.IdKhoahoc into khj
                from kh in khj.DefaultIfEmpty()
                select new
                {
                    kq,
                    BaiThi = bt,
                    Lop = lop,
                    KhoaHoc = kh
                }
            ).ToListAsync();

            if (!raw.Any())
                return result;

            // 3. Điểm trung bình theo môn (khóa học) chỉ tính bài đã chấm
            result.DiemTrungBinhTheoMon = raw
                .Where(x => x.kq.TrangthaiCham == "DA_CHAM" && x.kq.Tongdiem.HasValue)
                .GroupBy(x => new
                {
                    IdKhoaHoc = x.KhoaHoc != null ? (int?)x.KhoaHoc.IdKhoahoc : null,
                    TenKhoaHoc = x.KhoaHoc != null ? x.KhoaHoc.Tenkhoahoc : null
                })
                .Select(g => new DiemTrungBinhMonGvDTO
                {
                    IdKhoaHoc = g.Key.IdKhoaHoc,
                    TenKhoaHoc = g.Key.TenKhoaHoc,
                    DiemTrungBinh = g.Average(i => i.kq.Tongdiem ?? 0),
                    SoBaiThiDaCham = g.Count(i => i.kq.TrangthaiCham == "DA_CHAM")
                })
                .ToList();

            // 4. Điểm theo bài thi (mỗi bài: điểm TB + số bài đã chấm + ngày thi gần nhất)
            result.DiemTheoBaiThi = raw
                .Where(x => x.kq.TrangthaiCham == "DA_CHAM" && x.kq.Tongdiem.HasValue)
                .GroupBy(x => new
                {
                    x.BaiThi.IdBaithi,
                    x.BaiThi.Tieude,
                    TenKhoaHoc = x.KhoaHoc != null ? x.KhoaHoc.Tenkhoahoc : null
                })
                .Select(g => new DiemTheoBaiThiGvDTO
                {
                    IdBaithi = g.Key.IdBaithi,
                    TenBaiThi = g.Key.Tieude,
                    TenKhoaHoc = g.Key.TenKhoaHoc,
                    NgayThiGanNhat = g.Max(i => i.kq.Ngaythi),
                    DiemTrungBinh = g.Average(i => i.kq.Tongdiem ?? 0),
                    SoBaiDaCham = g.Count()
                })
                .OrderBy(x => x.NgayThiGanNhat)
                .ToList();

            // 5. Tỷ lệ chấm điểm chung
            var tongBai = raw.Count;
            var soDaCham = raw.Count(x => x.kq.TrangthaiCham == "DA_CHAM");
            var soChoCham = tongBai - soDaCham;

            result.TyLeChamDiem = new TyLeChamDiemGvDTO
            {
                TongBaiKetQua = tongBai,
                SoBaiDaCham = soDaCham,
                SoBaiChoCham = soChoCham
            };

            return result;
        }
    }
}

