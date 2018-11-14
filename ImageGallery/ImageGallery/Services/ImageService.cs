using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ImageGallery.Core.BusinessLogic;
using ImageGallery.Core.BusinessLogic.Repositories;
using ImageGallery.Core.BusinessLogic.Responses;
using ImageGallery.Core.Constants;
using ImageGallery.Models;

namespace ImageGallery.Services
{
    public class ImageService : HttpBaseService, IImageService
    {
        private readonly IDataRepository _dataRepository;

        public ImageService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<IResponseData<List<ImageModel>>> GetAllImages(CancellationToken token)
        {
            var headers = new Dictionary<string, object>
            {
                {"token", _dataRepository.Get<string>(DataType.Token)}
            };

            var result = await GetAsync(new Uri(WebApi.AllImages), token, headers);

            if (result.IsSuccess)
            {
                return GetValueFromJson<List<ImageModel>>(result.Data, "images");
            }

            var errorResult = GetValueFromJson<string>(result.Data, "error");
            if (errorResult.IsSuccess)
            {
                return new ResponseData<List<ImageModel>>(result.Code, errorResult.Data);
            }

            return new ResponseData<List<ImageModel>>(result.Code, result.ErrorMessage);
        }
    }
}
