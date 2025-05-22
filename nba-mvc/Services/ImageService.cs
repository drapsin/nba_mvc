using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace nba_mvc.Services
{
    public class ImageService
    {
        private readonly ImageUploaderResolver _resolver;

        public ImageService(ImageUploaderResolver resolver)
        {
            _resolver = resolver;
        }

        public async Task<string?> UploadAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploader = _resolver.GetUploader();
            return await uploader.UploadImageAsync(file);
        }
    }
}