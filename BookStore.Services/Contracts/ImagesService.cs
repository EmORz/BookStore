using System.IO;
using Microsoft.AspNetCore.Http;

namespace BookStore.Services.Contracts
{
    public class ImagesService : IImagesService
    {
        public void UploadImage(IFormFile formImage, string path)
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {
                formImage.CopyTo(stream);
            }
        }
    }
}