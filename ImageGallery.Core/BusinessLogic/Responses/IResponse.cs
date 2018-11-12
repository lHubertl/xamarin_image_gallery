namespace ImageGallery.Core.BusinessLogic.Responses
{
    public interface IResponse
    {
        bool IsSuccess { get; }
        string ErrorMessage { get; set; }
        ResponseCode Code { get; set; }
    }
}
