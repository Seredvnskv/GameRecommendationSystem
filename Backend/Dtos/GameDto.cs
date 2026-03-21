using Backend.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backend.Dtos
{
    public class GameDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("short_description")]
        public string ShortDescription { get; set; } = string.Empty;

        [JsonPropertyName("header_image")]
        public string HeaderImage { get; set; } = string.Empty;

        [JsonPropertyName("metacritic_score")]
        public int MetacriticScore { get; set; }

        [JsonPropertyName("positive")]
        public int Positive { get; set; }

        [JsonPropertyName("negative")]
        public int Negative { get; set; }

        [JsonPropertyName("categories")]
        public List<string> Categories { get; set; } = [];

        [JsonPropertyName("genres")]
        public List<string> Genres { get; set; } = [];

        [JsonPropertyName("screenshots")]
        public List<string> Screenshots { get; set; } = [];

        [JsonPropertyName("tags")]
        [JsonConverter(typeof(TagsConverter))]
        public Dictionary<string, int>? Tags { get; set; }
    }
}
