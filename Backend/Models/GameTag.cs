namespace Backend.Models
{
    public class GameTag
    {
        public string GameId { get; set; } = string.Empty;
        public int TagId { get; set; }

        public Game Game { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
}
