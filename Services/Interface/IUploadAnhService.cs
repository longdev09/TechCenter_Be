namespace TechCenter.Services.Interface
{
    public interface IUploadAnhService
    {
        Task<(string Url, string PublicId)> UploadImageAsync(IFormFile file, string folder, string customFileName = null);

        Task<(string Url, string PublicId)> UploadFileAsync(
            IFormFile file,
            string? folder = null,
            string? customFileName = null);
    }
}
