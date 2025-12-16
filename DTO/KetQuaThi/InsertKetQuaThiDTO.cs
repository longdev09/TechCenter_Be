namespace TechCenter.DTO.KetQuaThi
{
    
    public class InsertKetQuaThiDTO
    {
        public int IdHocVien { get; set; }
        public int IdBaiThi { get; set; }
        public int LanThi { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public double? TongDiem { get; set; }
        public string? TrangThai { get; set; }           // e.g., "SUBMITTED"
        public string? TrangThaiCham { get; set; }
    }
}
