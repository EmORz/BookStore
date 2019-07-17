using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookStore.Services.Contracts
{
    public interface ICloudinaryServices
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);
    }
}