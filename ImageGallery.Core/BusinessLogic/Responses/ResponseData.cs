namespace ImageGallery.Core.BusinessLogic.Responses
{
    public class ResponseData<T> : Response, IResponseData<T>
    {
        public T Data { get; set; }

        public ResponseData(T data, ResponseCode code, string message = null)
            : base(code, message)
        {
            Data = data;
        }

        public ResponseData(ResponseCode code, string message = null)
            : base(code, message)
        {

        }
    }
}
