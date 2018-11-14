using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ImageGallery.Core.BusinessLogic.Responses;
using ImageGallery.Models;

namespace ImageGallery.Services
{
    public interface IImageService
    {
        Task<IResponseData<List<ImageModel>>> GetAllImages(CancellationToken token);

        Task<IResponseData<string>> GetGif(CancellationToken token);

        Task<IResponse> PostImage(double latitude, double longitude, string description, string hashtag, MemoryStream imageStream, CancellationToken token);
    }
}
