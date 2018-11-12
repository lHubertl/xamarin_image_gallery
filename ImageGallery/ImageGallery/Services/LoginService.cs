using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ImageGallery.Core.BusinessLogic;
using ImageGallery.Core.BusinessLogic.Responses;
using ImageGallery.Core.Constants;
using ImageGallery.Models;

namespace ImageGallery.Services
{
    public class LoginService : HttpBaseService, ILoginService
    {
        public async Task<IResponseData<LoginModel>> SignUpAsync(string userName, string email, string password, FileStream userImage, CancellationToken token)
        {
            var httpContent = new MultipartFormDataContent
            {
                {new StringContent(userName), "username"},
                {new StringContent(email), "email"},
                {new StringContent(password), "password"},
                {new StreamContent(userImage), "avatar"}
            };

            var result = await PostAsync(new Uri(WebApi.SignIn), token, null, httpContent);

            if (result.IsSuccess)
            {
                return GetValueFromJson<LoginModel>(result.Data);
            }

            return new ResponseData<LoginModel>(result.Code, result.ErrorMessage);
        }

        public async Task<IResponseData<LoginModel>> SignInAsync(string email, string password, CancellationToken token)
        {
            var httpContent = new MultipartFormDataContent
            {
                {new StringContent(email), "email"},
                {new StringContent(password), "password"},
            };

            var result = await PostAsync(new Uri(WebApi.SignUp), token, null, httpContent);

            if (result.IsSuccess)
            {
                return GetValueFromJson<LoginModel>(result.Data);
            }

            return new ResponseData<LoginModel>(result.Code, result.ErrorMessage);
        }
    }
}
