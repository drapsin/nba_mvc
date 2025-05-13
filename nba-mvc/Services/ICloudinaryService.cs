using Microsoft.AspNetCore.Http;

namespace nba_mvc.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
