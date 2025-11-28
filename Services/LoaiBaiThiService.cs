using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.LoaiBaiThi;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class LoaiBaiThiService : ILoaiBaiThiService
    {
        private readonly AppDbContext _context;
        public LoaiBaiThiService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoaiBaiThiDTO>> GetAllLoaiBaiThiAsync()
        {
            return await _context.LoaiBaithis
                .AsNoTracking()
                .Select(x => new LoaiBaiThiDTO
                {
                    IdLoaibaithi = x.IdLoaibaithi,
                    Tenloai = x.Tenloai
                })
                .ToListAsync();
        }

    }
}
