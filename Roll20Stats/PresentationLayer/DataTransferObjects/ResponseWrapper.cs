using MediatR;

namespace Roll20Stats.PresentationLayer.DataTransferObjects
{
    public class ResponseWrapper<TResponseType>
    {
        public bool HasError { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public TResponseType Response { get; set; }
    }
}
