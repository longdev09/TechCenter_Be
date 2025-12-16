using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.CauHoi
{
    [Keyless]
    public class CauHoiWithDapAnDTO
    {
        public int? IdCauHoi { get; set; }
        public int? IdBaiThi { get; set; }
        public int? IdLoaiCauHoi { get; set; }
        public string? LoaiCauHoi { get; set; }
        public int? Stt { get; set; }
        public decimal? Diem { get; set; }
        public string? MucDo { get; set; }
        public string? Cauhoi { get; set; }

        public List<DapAnDTO> DapAns { get; set; } = new();
        public CauHoiCodeDTO? CauHoiCode { get; set; }
    }
}
