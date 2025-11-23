namespace TechCenter.DTO.HocVien
{
    public class UpdateHocVienDTO
    {
        public int? idHocVien { get; set; }
        public string? hoTenHv { get; set; }
        public string? gioiTinhHv { get; set; }
        public DateOnly? ngaySinhHv { get; set; }
        public string? diaChiHv { get; set; }
        public IFormFile? anhHv { get; set; }
    }
}
