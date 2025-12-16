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



    }


}




