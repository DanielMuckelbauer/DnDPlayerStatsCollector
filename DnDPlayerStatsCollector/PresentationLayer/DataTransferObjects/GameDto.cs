namespace Roll20Stats.PresentationLayer.DataTransferObjects
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GameDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}