using Microsoft.EntityFrameworkCore;
using TechCenter.DTO;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class VaiTroService : IVaiTroService
    {
        private readonly TechCenterContext _context;
        public VaiTroService(TechCenterContext context)
        {
            _context = context;
        }

        public async Task<List<VaiTroDTO>> GetAllVaiTro()
        {
            return await _context.Vaitros
                .Select(vt => new VaiTroDTO
                {
                    Id_VaiTro = vt.IdVaitro,
                    TenVaiTro = vt.Tenvaitro
                })
                .ToListAsync();
        }

        public async Task<VaiTroDTO?> GetVaiTroById(int id)
        {
            var vaiTro = await _context.Vaitros.FindAsync(id);
            if (vaiTro == null)
            {
                return null;
            }
            return new VaiTroDTO
            {
                Id_VaiTro = vaiTro.IdVaitro,
                TenVaiTro = vaiTro.Tenvaitro
            };
        }
        public async Task<VaiTroDTO> CreateVaiTro(VaiTroDTO vaiTroDto)
        {
            var vaiTro = new Vaitro
            {
                Tenvaitro = vaiTroDto.TenVaiTro
            };
            _context.Vaitros.Add(vaiTro);
            await _context.SaveChangesAsync();
            vaiTroDto.Id_VaiTro = vaiTro.IdVaitro;
            return vaiTroDto;
        }
        public async Task<bool> UpdateVaiTro(int id, VaiTroDTO vaiTroDto)
        {
            var vaiTro = await _context.Vaitros.FindAsync(id);
            if (vaiTro == null)
            {
                return false;
            }
            vaiTro.Tenvaitro = vaiTroDto.TenVaiTro;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteVaiTro(int id)
        {
            var vaiTro = await _context.Vaitros.FindAsync(id);
            if (vaiTro == null)
            {
                return false;
            }
            _context.Vaitros.Remove(vaiTro);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
