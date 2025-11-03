namespace TechCenter.Middleware
{

    /// <summary>
    /// 🧩 BaseResponse<T> là lớp phản hồi chuẩn cho toàn bộ API.
    /// Dùng để trả về dữ liệu thống nhất gồm:
    /// - Status: mã HTTP (200, 400, 500, v.v.)
    /// - Message: thông điệp hiển thị
    /// - Data: dữ liệu generic (bất kỳ kiểu nào)
    /// 
    /// 👉 Cách dùng nhanh:
    /// 
    /// ✅ Thành công (lấy dữ liệu):
    /// return Ok(BaseResponse<UserDTO>.SuccessFetched(user));
    /// 
    /// ✅ Thành công (tạo mới):
    /// return Ok(BaseResponse<UserDTO>.SuccessCreated(user));
    /// 
    /// ✅ Thất bại:
    /// return BadRequest(BaseResponse<string>.Fail("Không tìm thấy dữ liệu"));
    /// 
    /// ✅ Thành công tùy biến:
    /// return Ok(BaseResponse<object>.Success(new { count = 10 }, "Thống kê thành công"));
    /// </summary>

    public class BaseResponse<T>
    {
        /// <summary>
        /// Mã trạng thái HTTP (200, 400, 500, ...)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Thông điệp mô tả ngắn gọn về kết quả xử lý
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Dữ liệu trả về (có thể là bất kỳ kiểu nào)
        /// </summary>
        public T? Data { get; set; }

        public BaseResponse(int status, string message, T? data = default)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        // ------------------------------
        // ✅Generic success / fail
        // ------------------------------

        /// <summary>
        /// Phản hồi thành công tùy biến
        /// </summary>
        public static BaseResponse<T> Success(T? data, string message = "Thành công")
        {
            return new BaseResponse<T>(200, message, data);
        }

        /// <summary>
        /// Phản hồi thất bại tùy biến
        /// </summary>
        public static BaseResponse<T> Fail(string message, int status = 500)
        {
            return new BaseResponse<T>(status, message);
        }

        // ------------------------------
        // ✅ Success CRUD messages
        // ------------------------------
        public static BaseResponse<T> SuccessCreated(T? data = default)
            => new(201, "Tạo mới thành công", data);

        public static BaseResponse<T> SuccessUpdated(T? data = default)
            => new(200, "Cập nhật thành công", data);

        public static BaseResponse<T> SuccessDeleted(T? data = default)
            => new(200, "Xóa thành công", data);

        public static BaseResponse<T> SuccessFetched(T? data = default)
            => new(200, "Lấy dữ liệu thành công", data);

        // ------------------------------
        // ❌ Fail CRUD messages
        // ------------------------------
        public static BaseResponse<T> FailCreated(string message = "Tạo mới thất bại")
            => new(400, message);

        public static BaseResponse<T> FailUpdated(string message = "Cập nhật thất bại")
            => new(400, message);

        public static BaseResponse<T> FailDeleted(string message = "Xóa thất bại")
            => new(400, message);

        public static BaseResponse<T> FailFetched(string message = "Không thể lấy dữ liệu")
            => new(400, message);
    }

}
