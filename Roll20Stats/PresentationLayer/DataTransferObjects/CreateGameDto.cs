namespace Roll20Stats.PresentationLayer.DataTransferObjects
{
    public class CreateGameDto : RequestWithErrors
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}