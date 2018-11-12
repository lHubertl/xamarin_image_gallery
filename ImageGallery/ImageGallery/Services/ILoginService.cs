using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ImageGallery.Core.BusinessLogic.Responses;
using ImageGallery.Models;

namespace ImageGallery.Services
{
    public interface ILoginService
    {
        Task<IResponseData<LoginModel>> SignUpAsync(string userName, string email, string password, FileStream userImage, CancellationToken token);
        Task<IResponseData<LoginModel>> SignInAsync(string email, string password, CancellationToken token);
    }
}
