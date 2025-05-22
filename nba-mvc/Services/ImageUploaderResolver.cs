using Microsoft.Extensions.Configuration;
using System;

namespace nba_mvc.Services
{
    public class ImageUploaderResolver
    {
        private readonly IServiceProvider _provider;
        private readonly string _strategy;

        public ImageUploaderResolver(IServiceProvider provider, IConfiguration config)
        {
            _provider = provider;
            _strategy = config["ImageStorage"]; // expects "Cloudinary" or "Local"
        }

        public IImageUploader GetUploader()
        {
            return _strategy switch
            {
                "Cloudinary" => (IImageUploader)_provider.GetService(typeof(CloudinaryImageUploader))!,
                "Local" => (IImageUploader)_provider.GetService(typeof(LocalImageUploader))!,
                _ => throw new InvalidOperationException("Invalid ImageStorage strategy.")
            };
        }
    }
}
