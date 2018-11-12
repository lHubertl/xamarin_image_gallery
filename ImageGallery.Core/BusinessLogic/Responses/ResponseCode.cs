namespace ImageGallery.Core.BusinessLogic.Responses
{
    public enum ResponseCode
    {
        Ok,
        Unknown,
        Exception,
        Unauthorized,
        NoDataFound,
        NoConnection,
        JsonFail,
        ServerError,
        InvalidCredentials,
        OperationFailed
    }
}
