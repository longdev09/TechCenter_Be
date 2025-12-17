namespace TechCenter.DTO.ThongKe
{
    public class DiemTheoBaiThiDTO
    {

        public int IdBaithi { get; set; }
        public string? TenBaiThi { get; set; }
        public DateTime NgayThi { get; set; }
        public double? Diem { get; set; }
        public string? MonHoc { get; set; }     // tên khóa học
    }
}
