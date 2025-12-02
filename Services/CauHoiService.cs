using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.CauHoi;
using TechCenter.Models;
using TechCenter.Services.Interface;
using System.Data.Common;

namespace TechCenter.Services
{
    public class CauHoiService : ICauHoiService
    {
        private readonly AppDbContext _context;
        public CauHoiService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<InsertCauHoiDTO> InsertCauHoiAsync(InsertCauHoiDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // 1️⃣ Lấy STT lớn nhất hiện có của bài thi
            var maxStt = await _context.Cauhois
                .Where(x => x.IdBaithi == dto.IdBaiThi)
                .MaxAsync(x => (int?)x.Stt) ?? 0; // Nếu chưa có câu hỏi nào → 0

      
            var cauHoiEntity = new Cauhoi
            {
                Cauhoi1 = dto.Cauhoi,
                IdBaithi = dto.IdBaiThi,
                IdLoaicauhoi = dto.IdLoaiCauHoi,
                Diem = (decimal?)dto.Diem,
                Mucdo = dto.MucDo,
                Stt = maxStt + 1 // ⭐ STT tự tăng theo bài thi
            };

     
            _context.Cauhois.Add(cauHoiEntity);
            await _context.SaveChangesAsync();

            // 4️⃣ Trả lại IdCauHoi vừa insert
            dto.IdCauHoi = cauHoiEntity.IdCauhoi;
            dto.Stt = cauHoiEntity.Stt; // có thể trả luôn STT
            return dto;
        }

        public async Task<UpdateCauHoiDTO> UpdateCauHoiAsync(UpdateCauHoiDTO dto)
        {
            

            var entity = await _context.Cauhois.FindAsync(dto.IdCauHoi.Value);
            if (entity == null) return null;

            // update fields if provided
            if (dto.Cauhoi != null) entity.Cauhoi1 = dto.Cauhoi;
            if (dto.IdLoaiCauHoi.HasValue) entity.IdLoaicauhoi = dto.IdLoaiCauHoi;
            if (dto.Diem.HasValue) entity.Diem = dto.Diem;
            if (dto.MucDo != null) entity.Mucdo = dto.MucDo; 
            _context.Cauhois.Update(entity);
            await _context.SaveChangesAsync();
            return dto;
        }


     




        public async Task<LoaiCauHoiDTO> GetLoaiCauHoiByIdAsync(int id)
        {
            var loaiCauHoi = await _context.Loaicauhois.AsNoTracking()
                .FirstOrDefaultAsync(l => l.IdLoaicauhoi == id);

            if (loaiCauHoi == null)
            {
                return null;
            }

            return new LoaiCauHoiDTO
            {
                Id_LoaiCauHoi = loaiCauHoi.IdLoaicauhoi,
                TenLoaiCauHoi = loaiCauHoi.TenLoai,
                Mota = loaiCauHoi.Mota
            };
        }

        public async Task<List<LoaiCauHoiDTO>> GetAllLoaiCauHoiAsync()
        {
            return await _context.Loaicauhois.AsNoTracking()
                .Select(l => new LoaiCauHoiDTO
                {
                    Id_LoaiCauHoi = l.IdLoaicauhoi,
                    TenLoaiCauHoi = l.TenLoai,
                    Mota = l.Mota
                }).ToListAsync();
        }




        public async Task<InsertDapAnDTO> InsertDapAnAsync(InsertDapAnDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var entity = new Dapantracnghiem
            {
                IdCauhoi = dto.IdCauHoi ,
                Ma = dto.Ma ?? string.Empty,
                Isdung = dto.IsDung
            };

            _context.Dapantracnghiems.Add(entity);
            await _context.SaveChangesAsync();
            dto.IdDapAn = entity.IdDapan;
            return dto;
        }

        public async Task<List<InsertDapAnDTO>> InsertDapAnAsync(List<InsertDapAnDTO> dtos)
        {
            if (dtos == null || dtos.Count ==0) return new List<InsertDapAnDTO>();

            var entities = new List<Dapantracnghiem>(dtos.Count);
            var insertedDtos = new List<InsertDapAnDTO>();

            foreach (var dto in dtos)
            {
                if (dto == null) continue;
                if (dto.IdCauHoi <=0)
                    throw new ArgumentException("IdCauHoi is required and must be greater than zero for each answer", nameof(dtos));

                var entity = new Dapantracnghiem
                {
                    IdCauhoi = dto.IdCauHoi,
                    Ma = dto.Ma ?? string.Empty,
                    Isdung = dto.IsDung
                };

                entities.Add(entity);
                insertedDtos.Add(dto);
            }

            if (entities.Count ==0) return new List<InsertDapAnDTO>();

            _context.Dapantracnghiems.AddRange(entities);
            await _context.SaveChangesAsync();

            // EF will populate generated IDs
            for (int i =0; i < entities.Count; i++)
            {
                insertedDtos[i].IdDapAn = entities[i].IdDapan;
            }

            return insertedDtos;
        }

        public async Task<List<CauHoiByIdByBaiThiDTO>> GetCauHoiByBaiThiAsync(int idBaiThi)
        {
            var query = from ch in _context.Cauhois.AsNoTracking()
                        join lb in _context.Loaicauhois.AsNoTracking() on ch.IdLoaicauhoi equals lb.IdLoaicauhoi into lbj
                        from lb in lbj.DefaultIfEmpty()
                        where ch.IdBaithi == idBaiThi
                        orderby ch.Stt
                        select new CauHoiByIdByBaiThiDTO
                        {
                            IdCauHoi = ch.IdCauhoi,
                            IdBaiThi = ch.IdBaithi,
                            IdLoaiCauHoi = ch.IdLoaicauhoi,
                            LoaiCauHoi = lb != null ? lb.TenLoai : null,
                            Stt = ch.Stt,
                            Diem = ch.Diem,
                            MucDo = ch.Mucdo,
                            Cauhoi = ch.Cauhoi1
                        };

            return await query.ToListAsync();
        }



        public async Task<InsertCauHoiCodeDTO> InsertCauHoiCodeAsync(InsertCauHoiCodeDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (!dto.IdCauHoi.HasValue) throw new ArgumentException("IdCauHoi is required", nameof(dto));

            var existing = await _context.CauHoiCodes.FindAsync(dto.IdCauHoi.Value);
            if (existing != null)
            {
                existing.CodeMau = dto.CodeMau;
                existing.NgonNgu = dto.NgonNgu;
                _context.CauHoiCodes.Update(existing);
                await _context.SaveChangesAsync();
                dto.IdCauHoi = existing.IdCauHoi; // preserve
                return dto;
            }

            var entity = new CauHoiCode
            {
                IdCauHoi = dto.IdCauHoi.Value,
                CodeMau = dto.CodeMau,
                NgonNgu = dto.NgonNgu
            };

            _context.CauHoiCodes.Add(entity);
            await _context.SaveChangesAsync();
            return dto;
        }
    }
}
