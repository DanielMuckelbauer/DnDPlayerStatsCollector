using MediatR;

namespace Roll20Stats.PresentationLayer.DataTransferObjects
{
    public class ResponseWithMetaData<TResponseType> : IRequest where TResponseType : class
    {
        public bool HasError { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public TResponseType? Response { get; set; }

        public ResponseWithMetaData(bool hasError = false, int statusCode = 0, string? errorMessage = null, TResponseType? response = null)
        {
            Response = response;
            HasError = hasError;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
