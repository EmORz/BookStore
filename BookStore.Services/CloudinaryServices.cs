using System.IO;
using System.Threading.Tasks;
using BookStore.Services.Contracts;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BookStore.Services
{
    public class CloudinaryServices : ICloudinaryServices
    {
        private readonly Cloudinary _cloudinaryUtility;

        public CloudinaryServices(Cloudinary cloudinaryUtility)
        {
            _cloudinaryUtility = cloudinaryUtility;
        }
        public string UploadPictureAsync(IFormFile pictureFile, string fileName)
        {
            byte[] destinationData;

            using (var ms = new MemoryStream())
            {
                pictureFile.CopyToAsync(ms);
                destinationData = ms.ToArray();
            }

            UploadResult uploadResult = null;
            using (var ms = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams()
                {
                    Folder = "product_images",
                    File = new FileDescription(fileName, ms)

                };
                uploadResult = this._cloudinaryUtility.Upload(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}