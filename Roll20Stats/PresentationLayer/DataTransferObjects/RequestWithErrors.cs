namespace Roll20Stats.PresentationLayer.DataTransferObjects
{
    public abstract class RequestWithErrors
    {
        public bool HasError { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
