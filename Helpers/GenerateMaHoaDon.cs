namespace TechCenter.Helpers
{
    public static class GenerateMaHoaDon
    {

        public static string GenerateMaHD(int idHv)
        {
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            // Tạo mã hóa đơn
            string maHD = $"HD{idHv}{time}";
            return maHD;
        }
    }
}
