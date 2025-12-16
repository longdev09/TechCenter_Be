using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class UploadAnhService : IUploadAnhService
    {
        private readonly Cloudinary _cloudinary;

        public UploadAnhService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<(string Url, string PublicId)> UploadImageAsync(IFormFile file, string folder, string customFileName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File không hợp lệ", nameof(file));

            // Validate extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !allowedExtensions.Contains(ext))
                throw new ArgumentException("Định dạng file không được hỗ trợ. Vui lòng tải lên ảnh (jpg, jpeg, png, gif, webp).", nameof(file));

            if (string.IsNullOrWhiteSpace(customFileName))
                throw new ArgumentException("customFileName bắt buộc khi muốn dùng tên riêng", nameof(customFileName));

            // Chuẩn hóa tên custom
            var baseName = Path.GetFileNameWithoutExtension(customFileName);
            baseName = string.Concat(baseName.Split(Path.GetInvalidFileNameChars())).Replace(" ", "_");

            // Thêm suffix để đảm bảo duy nhất
            var uniqueSuffix = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            var publicId = string.IsNullOrWhiteSpace(folder)
                ? $"{baseName}_{uniqueSuffix}"
                : $"{folder.Trim().Trim('/')}/{baseName}_{uniqueSuffix}";

            // Upload
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                PublicId = publicId,
                Overwrite = true,
                Folder = string.IsNullOrWhiteSpace(folder) ? null : folder.Trim().Trim('/'),
                Transformation = new Transformation().Quality("auto").FetchFormat("auto")
            };

            try
            {
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult == null)
                    throw new Exception("Upload thất bại: kết quả rỗng");

                if (uploadResult.Error != null)
                    throw new Exception($"Upload thất bại: {uploadResult.Error.Message}");

                var url = uploadResult.SecureUrl?.AbsoluteUri ?? uploadResult.Url?.AbsoluteUri;
                if (string.IsNullOrEmpty(url))
                    throw new Exception("Không lấy được URL sau khi upload");

                return (url, uploadResult.PublicId);
            }
            catch (Exception ex)
            {
                throw new Exception("Upload Cloudinary lỗi: " + ex.Message, ex);
            }
        }
        public async Task<(string Url, string PublicId)> UploadFileAsync(
            IFormFile file,
            string? folder = null,
            string? customFileName = null)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File không hợp lệ", nameof(file));

            // 1️⃣ Validate extension (tài liệu)
            var allowedExtensions = new[]
            {
                ".pdf",
                ".doc", ".docx",
                ".xls", ".xlsx",
                ".ppt", ".pptx",
                ".txt",
                ".zip", ".rar"
            };

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(ext))
                throw new ArgumentException(
                    "Định dạng file không được hỗ trợ (pdf, docx, xlsx, pptx, zip, ...)",
                    nameof(file));

            // 2️⃣ Chuẩn hóa tên file
            var baseName = string.IsNullOrWhiteSpace(customFileName)
                ? Path.GetFileNameWithoutExtension(file.FileName)
                : Path.GetFileNameWithoutExtension(customFileName);

            baseName = string.Concat(baseName.Split(Path.GetInvalidFileNameChars()))
                .Replace(" ", "_");

            // 3️⃣ Tạo publicId duy nhất
            var uniqueSuffix = $"{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}".Substring(0, 22);

            var publicId = string.IsNullOrWhiteSpace(folder)
                ? $"{baseName}_{uniqueSuffix}"
                : $"{folder.Trim().Trim('/')}/{baseName}_{uniqueSuffix}";

            // 4️⃣ Upload file (RAW)
            using var stream = file.OpenReadStream();

            var uploadParams = new RawUploadParams
            {
                File = new FileDescription($"{baseName}{ext}", stream),
                PublicId = publicId,
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult == null)
                throw new Exception("Upload Cloudinary thất bại (null result)");

            if (uploadResult.Error != null)
                throw new Exception($"Upload Cloudinary lỗi: {uploadResult.Error.Message}");

            var url = uploadResult.SecureUrl?.AbsoluteUri;
            if (string.IsNullOrEmpty(url))
                throw new Exception("Không lấy được URL sau khi upload");

            return (url, uploadResult.PublicId);
        }

    }
}
