using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.KetQuaThi;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class KetQuaThiService : IKetQuaThiService
    {
        private readonly AppDbContext _context;
        public KetQuaThiService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> InsertKetQuaThiAsync(InsertKetQuaThiDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var ketqua = new Ketquathi
            {
                IdHocvien = dto.IdHocVien,
                IdBaithi = dto.IdBaiThi,
                LanThi = dto.LanThi,
                Thoigianbatdau = DateTime.Now,
                Tongdiem = 0,
                Trangthai = string.IsNullOrWhiteSpace(dto.TrangThai) ? "DANG_LAM" : dto.TrangThai,
                TrangthaiCham = string.IsNullOrWhiteSpace(dto.TrangThaiCham) ? "CHO_CHAM" : dto.TrangThaiCham,
                Ngaythi = DateTime.Now
            };
            _context.Ketquathis.Add(ketqua);
            await _context.SaveChangesAsync();

            var idKetQua = ketqua.IdKetqua;
            return idKetQua;
        }

        public async Task<bool> UpdateKetQuaThiAsync(UpdateKetQuaThiDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var ketqua = await _context.Ketquathis.FindAsync(dto.IdKetqua);
            if (ketqua == null) return false;

            var updated = false;

            if (dto.ThoiGianKetThuc.HasValue)
            {
                ketqua.Thoigianketthuc = dto.ThoiGianKetThuc;
                updated = true;
            }

            if (dto.TongDiem.HasValue)
            {
                ketqua.Tongdiem = dto.TongDiem;
                updated = true;
            }

            if (!string.IsNullOrWhiteSpace(dto.TrangThai) && ketqua.Trangthai != dto.TrangThai)
            {
                ketqua.Trangthai = dto.TrangThai;
                updated = true;
            }

            if (!string.IsNullOrWhiteSpace(dto.TrangThaiCham) && ketqua.TrangthaiCham != dto.TrangThaiCham)
            {
                ketqua.TrangthaiCham = dto.TrangThaiCham;
                updated = true;
            }

            if (!updated) return true; // nothing to change

            _context.Ketquathis.Update(ketqua);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> InsertChiTietDapAnTracNghiemAsync(
            int idKetQua,
            List<InsertChiTietTracNghiemDTO> dtos)
        {
            if (dtos == null || dtos.Count == 0) return 0;

            // 1. Kiểm tra kết quả thi có tồn tại không
            var ketqua = await _context.Ketquathis
                .FirstOrDefaultAsync(k => k.IdKetqua == idKetQua);

            if (ketqua == null)
                throw new ArgumentException("Kết quả thi không tồn tại", nameof(idKetQua));

            // 2. Lấy danh sách ID câu hỏi
            var cauHoiIds = dtos.Select(d => d.IdCauHoi).Distinct().ToList();

            // 3. Các câu hỏi đã lưu trước đó
            var existingCauHoiIds = await _context.Chitietbailamtracnghiems
                .AsNoTracking()
                .Where(c => c.IdKetqua == idKetQua && cauHoiIds.Contains(c.IdCauhoi))
                .Select(c => c.IdCauhoi)
                .ToListAsync();

            // 4. Đáp án đúng
            var correctAnswers = await _context.Dapantracnghiems
                .AsNoTracking()
                .Where(d => cauHoiIds.Contains(d.IdCauhoi) && (d.Isdung ?? false))
                .GroupBy(d => d.IdCauhoi)
                .ToDictionaryAsync(
                    g => g.Key,
                    g => g.Select(x => x.Ma?.Trim())
                          .Where(s => !string.IsNullOrEmpty(s))
                          .ToList()
                );

            // 5. Điểm câu hỏi
            var scoreByQuestion = await _context.Cauhois
                .AsNoTracking()
                .Where(c => cauHoiIds.Contains(c.IdCauhoi))
                .ToDictionaryAsync(
                    c => c.IdCauhoi,
                    c => (double)(c.Diem ?? 0)
                );

            var toInsert = new List<Chitietbailamtracnghiem>();

            // 6. Chấm và tạo entity
            foreach (var dto in dtos)
            {
                if (existingCauHoiIds.Contains(dto.IdCauHoi))
                    continue;

                bool isDung = false;
                var chosen = dto.DapAnChon?.Trim();

                if (!string.IsNullOrEmpty(chosen)
                    && correctAnswers.TryGetValue(dto.IdCauHoi, out var correctList))
                {
                    isDung = correctList.Any(ca =>
                        string.Equals(ca, chosen, StringComparison.OrdinalIgnoreCase));
                }

                var diem = isDung
                    ? (scoreByQuestion.TryGetValue(dto.IdCauHoi, out var s) ? s : 0)
                    : 0;

                toInsert.Add(new Chitietbailamtracnghiem
                {
                    IdKetqua = idKetQua,
                    IdCauhoi = dto.IdCauHoi,
                    DapanChon = dto.DapAnChon,
                    IsDung = isDung,
                    Diem = diem
                });
            }

            // ❗ 7. PHẢI AddRange trước
            if (toInsert.Count == 0) return 0;

            _context.Chitietbailamtracnghiems.AddRange(toInsert);
            await _context.SaveChangesAsync();

            // ❗ 8. PHẢI return
            return toInsert.Count;
        }

        public async Task InsertChiTietBaiLamTuLuanAsync(
            int idKetQua,
            List<InsertChiTietTuLuanDTO> dtos)
        {
            if (dtos == null || dtos.Count == 0) return;

            // 1. Kiểm tra kết quả thi có tồn tại không
            var ketqua = await _context.Ketquathis
                .FirstOrDefaultAsync(k => k.IdKetqua == idKetQua);

            if (ketqua == null)
                throw new ArgumentException("Kết quả thi không tồn tại", nameof(idKetQua));

            var toInsert = new List<ChitietbailamTuluan>();

            // 2. Tạo entity chi tiết bài làm tự luận
            foreach (var dto in dtos)
            {
                toInsert.Add(new ChitietbailamTuluan
                {
                    IdKetqua = idKetQua,
                    IdCauhoi = dto.IdCauHoi,
                    Noidungtl = dto.NoiDungTL
                });
            }

            // 3. Lưu vào DB
            _context.ChitietbailamTuluans.AddRange(toInsert);
            await _context.SaveChangesAsync();
        }

        public async Task<List<KetQuaForGiaoVienDTO>> GetKetQuaChoGiaoVienAsync(int idGiaoVien)
        {
            // 1. Lấy danh sách lớp GV được phân công
            var lopIds = await _context.Phancongs
                .Where(pc => pc.IdGiaovien == idGiaoVien)
                .Select(pc => pc.IdLophoc)
                .Distinct()
                .ToListAsync();

            if (!lopIds.Any())
                return new List<KetQuaForGiaoVienDTO>();

            // 2. Truy vấn các ketqua cần chấm
            // note: avoid string.IsNullOrEmpty(...) (sometimes EF translation issues)
            var query = from kq in _context.Ketquathis.AsNoTracking()
                        join bt in _context.Baithis.AsNoTracking() on kq.IdBaithi equals bt.IdBaithi
                        join lh in _context.Lophocs.AsNoTracking() on bt.IdLop equals lh.IdLophoc
                        join kh in _context.Khoahocs.AsNoTracking() on lh.IdKhoahoc equals kh.IdKhoahoc into khj
                        from kh in khj.DefaultIfEmpty()
                        join hv in _context.Hocviens.AsNoTracking() on kq.IdHocvien equals hv.IdHocvien into hvj
                        from hv in hvj.DefaultIfEmpty()
                        where bt.IdLop != null
                              && lopIds.Contains(bt.IdLop.Value)
                              && (kq.TrangthaiCham == "CHO_CHAM" || kq.TrangthaiCham == null || kq.TrangthaiCham == "")
                        orderby kq.Ngaythi descending
                        select new KetQuaForGiaoVienDTO
                        {
                            IdKetqua = kq.IdKetqua,
                            IdBaithi = kq.IdBaithi,
                            Tieude = bt.Tieude,
                            IdLop = lh.IdLophoc,
                            TenKhoaHoc = kh != null ? kh.Tenkhoahoc : null,
                            IdHocvien = kq.IdHocvien,
                            TenHocVien = hv != null ? hv.Hotenhv : null,
                            LanThi = kq.LanThi,
                            Trangthai = kq.Trangthai,
                            TrangthaiCham = kq.TrangthaiCham,
                            Tongdiem = kq.Tongdiem,
                            Ngaythi = kq.Ngaythi
                        };

            var result = await query.ToListAsync();
            return result;
        }


        public async Task<List<ChiTietCauHoiBaiLamDTO>> GetChiTietBaiLamByKetQuaAsync(int idKetQua)
        {
            // Lấy IdBaiThi từ kết quả
            var idBaiThi = await _context.Ketquathis
                .Where(k => k.IdKetqua == idKetQua)
                .Select(k => (int?)k.IdBaithi)
                .FirstOrDefaultAsync();

            if (!idBaiThi.HasValue)
                return new List<ChiTietCauHoiBaiLamDTO>();

            // 1. Lấy câu hỏi + chi tiết trắc nghiệm (bài làm) + loại câu hỏi
            var cauHoiRaw = await (
                from ch in _context.Cauhois.AsNoTracking()
                join loai in _context.Loaicauhois.AsNoTracking()
                    on ch.IdLoaicauhoi equals loai.IdLoaicauhoi into lj
                from loai in lj.DefaultIfEmpty()
                    // left join chi tiết bài làm trắc nghiệm
                join ct in _context.Chitietbailamtracnghiems.AsNoTracking()
                    on new { IdCauHoi = ch.IdCauhoi, IdKetQua = idKetQua }
                    equals new { IdCauHoi = ct.IdCauhoi, IdKetQua = ct.IdKetqua }
                    into ctj
                from ct in ctj.DefaultIfEmpty()
                where ch.IdBaithi == idBaiThi.Value
                select new
                {
                    CauHoi = ch,
                    Loai = loai,
                    ChiTietTN = ct
                }
            ).ToListAsync();

            if (!cauHoiRaw.Any())
                return new List<ChiTietCauHoiBaiLamDTO>();

            var cauHoiIds = cauHoiRaw.Select(x => x.CauHoi.IdCauhoi).Distinct().ToList();

            // 2. Lấy toàn bộ đáp án trắc nghiệm cho các câu hỏi này
            var dapAnRaw = await _context.Dapantracnghiems
                .AsNoTracking()
                .Where(d => cauHoiIds.Contains(d.IdCauhoi))
                .Select(d => new
                {
                    d.IdDapan,
                    d.IdCauhoi,
                    d.Ma,
                    d.Cauhoidapan,
                    IsDung = d.Isdung ?? false
                })
                .ToListAsync();

            var dapAnByCauHoi = dapAnRaw
                .GroupBy(d => d.IdCauhoi)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => new DapAnTracNghiemInKetQuaDTO
                    {
                        IdDapAn = x.IdDapan,
                        Ma = x.Ma,
                        NoiDung = x.Cauhoidapan,
                        IsDung = x.IsDung
                    }).ToList()
                );

            // 3. Map sang DTO cơ bản
            var list = cauHoiRaw.Select(x =>
            {
                var ch = x.CauHoi;
                var loai = x.Loai;
                var ct = x.ChiTietTN;

                dapAnByCauHoi.TryGetValue(ch.IdCauhoi, out var dsDapAn);

                return new ChiTietCauHoiBaiLamDTO
                {
                    IdCauHoi = ch.IdCauhoi,
                    IdLoaiCauHoi = ch.IdLoaicauhoi,
                    LoaiCauHoi = loai != null ? loai.TenLoai : null,
                    Stt = ch.Stt,
                    Diem = ch.Diem,
                    MucDo = ch.Mucdo,
                    NoiDungCauHoi = ch.Cauhoi1,

                    // Trắc nghiệm
                    DapAnChon = ct != null ? ct.DapanChon : null,
                    IsDung = ct != null ? ct.IsDung : null,
                    DiemTracNghiem = ct != null ? (double?)ct.Diem : null,
                    DapAnTracNghiems = dsDapAn,

                    // Tự luận (sẽ gán sau)
                    NoiDungTuLuan = null,
                    DiemGv = null,
                    NhanXetGv = null,
                    NgayCham = null
                };
            }).ToList();

            // 4. Load chi tiết tự luận
            var tuLuanList = await _context.ChitietbailamTuluans
                .AsNoTracking()
                .Where(t => t.IdKetqua == idKetQua)
                .ToListAsync();

            if (tuLuanList.Any())
            {
                var tuLuanByCauHoi = tuLuanList
                    .GroupBy(x => x.IdCauhoi)
                    .ToDictionary(g => g.Key, g => g.First());

                foreach (var item in list)
                {
                    if (tuLuanByCauHoi.TryGetValue(item.IdCauHoi, out var tl))
                    {
                        item.NoiDungTuLuan = tl.Noidungtl;
                        item.DiemGv = tl.DiemGv;
                        item.NhanXetGv = tl.NhanxetGv;
                        item.NgayCham = tl.Ngaycham;
                    }
                }

                // trường hợp câu hỏi chỉ có tự luận nhưng chưa có trong list (hiếm)
                var existingIds = list.Select(x => x.IdCauHoi).ToHashSet();
                var onlyTuLuanIds = tuLuanList
                    .Where(t => !existingIds.Contains(t.IdCauhoi))
                    .Select(t => t.IdCauhoi)
                    .Distinct()
                    .ToList();

                if (onlyTuLuanIds.Any())
                {
                    var cauHoiTuLuan = await (
                        from ch in _context.Cauhois.AsNoTracking()
                        join loai in _context.Loaicauhois.AsNoTracking()
                            on ch.IdLoaicauhoi equals loai.IdLoaicauhoi into lj
                        from loai in lj.DefaultIfEmpty()
                        where onlyTuLuanIds.Contains(ch.IdCauhoi)
                        select new { ch, loai }
                    ).ToListAsync();

                    foreach (var x in cauHoiTuLuan)
                    {
                        if (!tuLuanByCauHoi.TryGetValue(x.ch.IdCauhoi, out var tl))
                            continue;

                        list.Add(new ChiTietCauHoiBaiLamDTO
                        {
                            IdCauHoi = x.ch.IdCauhoi,
                            IdLoaiCauHoi = x.ch.IdLoaicauhoi,
                            LoaiCauHoi = x.loai != null ? x.loai.TenLoai : null,
                            Stt = x.ch.Stt,
                            Diem = x.ch.Diem,
                            MucDo = x.ch.Mucdo,
                            NoiDungCauHoi = x.ch.Cauhoi1,

                            DapAnChon = null,
                            IsDung = null,
                            DiemTracNghiem = null,
                            DapAnTracNghiems = dapAnByCauHoi.ContainsKey(x.ch.IdCauhoi)
                                ? dapAnByCauHoi[x.ch.IdCauhoi]
                                : null,

                            NoiDungTuLuan = tl.Noidungtl,
                            DiemGv = tl.DiemGv,
                            NhanXetGv = tl.NhanxetGv,
                            NgayCham = tl.Ngaycham
                        });
                    }
                }
            }

            // 5. Sắp xếp
            return list
                .OrderBy(x => x.Stt)
                .ThenBy(x => x.IdCauHoi)
                .ToList();
        }


        // Kết quả điểm của học viên, đã chấm, group theo môn (khoá học)
        public async Task<List<KetQuaHocVienByMonDTO>> GetKetQuaDaChamByHocVienAsync(int idHocVien)
        {
            // chỉ lấy các kết quả đã chấm
            var raw = await (
                from kq in _context.Ketquathis.AsNoTracking()
                where kq.IdHocvien == idHocVien
                      && kq.TrangthaiCham == "DA_CHAM"
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
                }
            ).OrderByDescending(x => x.kq.Ngaythi)
             .ToListAsync();

            if (!raw.Any())
                return new List<KetQuaHocVienByMonDTO>();

            var grouped = raw
                .GroupBy(x => new
                {
                    IdKhoaHoc = x.KhoaHoc != null ? (int?)x.KhoaHoc.IdKhoahoc : null,
                    TenKhoaHoc = x.KhoaHoc != null ? x.KhoaHoc.Tenkhoahoc : null
                })
                .Select(g => new KetQuaHocVienByMonDTO
                {
                    IdKhoaHoc = g.Key.IdKhoaHoc,
                    TenKhoaHoc = g.Key.TenKhoaHoc,
                    KetQuas = g.Select(item => new KetQuaHocVienItemDTO
                    {
                        IdKetqua = item.kq.IdKetqua,
                        IdBaithi = item.kq.IdBaithi,
                        TenBaiThi = item.BaiThi.Tieude,
                        LanThi = item.kq.LanThi,
                        TongDiem = item.kq.Tongdiem,
                        NgayThi = item.kq.Ngaythi,
                        TrangThai = item.kq.Trangthai,
                        TrangThaiCham = item.kq.TrangthaiCham
                    }).ToList()
                })
                .ToList();

            return grouped;
        }
    

}
}







