using Microsoft.AspNetCore.Http;

namespace BookStore.Services.Contracts
{
    public interface IImagesService
    {
        void UploadImage(IFormFile formImage, string path);
    }
}