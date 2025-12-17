namespace TechCenter.DTO.KetQuaThi
{
    public class KetQuaHocVienItemDTO
    {
        public int IdKetqua { get; set; }
        public int IdBaithi { get; set; }
        public string? TenBaiThi { get; set; }
        public int LanThi { get; set; }
        public double? TongDiem { get; set; }
        public DateTime NgayThi { get; set; }
        public string? TrangThai { get; set; }
        public string? TrangThaiCham { get; set; }
    }

    public class KetQuaHocVienByMonDTO
    {
        public int? IdKhoaHoc { get; set; }
        public string? TenKhoaHoc { get; set; }

        public List<KetQuaHocVienItemDTO> KetQuas { get; set; } = new();
    }
}
