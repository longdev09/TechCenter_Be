namespace TechCenter.DTO.CauHoi
{
    public class UpdateCauHoiDTO
    {
        public int? IdCauHoi { get; set; }
        public string? Cauhoi { get; set; }
        public int? IdLoaiCauHoi { get; set; }
        public decimal? Diem { get; set; }
        public string? MucDo { get; set; }
    }
}
