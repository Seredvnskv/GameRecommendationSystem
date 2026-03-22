using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class Game
    {
        public string GameId { get; set; } = string.Empty;
        [property: JsonPropertyName("name")] public string Title { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
        public decimal Price { get; set; }
        [property: JsonPropertyName("detailed_description")] public string Description { get; set; } = string.Empty;
        [property: JsonPropertyName("about_the_game")] public string AboutGame { get; set; } = string.Empty;
        [property: JsonPropertyName("short_description")] public string ShortDescription { get; set; } = string.Empty;
        public string HeaderImage { get; set; } = string.Empty;
        [property: JsonPropertyName("metacritic_score")] public int MetacriticScore { get; set; }

        public List<string> Developers = [];
        public List<string> Publishers = [];
        public List<string> Categories = [];
        public List<string> Genres = [];
        public List<string> Screenshots = [];

        [property: JsonPropertyName("positive")] public int PositiveRatings { get; set; }
        [property: JsonPropertyName("negative")] public int NegativeRatings { get; set; }

        public List<GameTag> GameTags { get; set; } = [];
    }
}
