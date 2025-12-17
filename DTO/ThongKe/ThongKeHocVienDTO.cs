namespace TechCenter.DTO.ThongKe
{
    public class ThongKeHocVienDTO
    {



        public List<DiemTrungBinhMonDTO> DiemTrungBinhTheoMon { get; set; } = new();
        public List<DiemTheoBaiThiDTO> DiemTheoThoiGian { get; set; } = new();
        public TyLeChamDiemDTO TyLeChamDiem { get; set; } = new();
    }
}
