namespace nba_mvc.Services
{
    public interface IImageUploader
    {
        Task<string> UploadImageAsync(IFormFile image);
    }
}
