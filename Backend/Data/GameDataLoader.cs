using Backend.Dtos;
using Backend.Models;
using System.Text.Json;

namespace Backend.Data
{
    public class GameDataLoader
    {
        private readonly GameContext _context;
        private readonly string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "games.json");

        public GameDataLoader(GameContext context)
        {
            _context = context;
        }

        public async Task LoadGames()
        {   
            if (_context.Games.Any()) 
            { 
                Console.WriteLine("Games already exist in the database. Skipping data loading.");
                return;
            }

            if (!File.Exists(jsonFilePath)) throw new Exception($"JSON file not found at path: {jsonFilePath}");

            var json = await File.ReadAllTextAsync(jsonFilePath);

            var gamesData = JsonSerializer.Deserialize<Dictionary<string, JsonGameDto>>(json);

            if (gamesData == null ) throw new InvalidOperationException("Failed to deserialize JSON file");

            var addedTags = new HashSet<int>();

            foreach (var (gameId, gameDto) in gamesData)
            {
                if (_context.Games.Any(g => g.GameId == gameId)) continue;

                var game = new Game
                {
                    GameId = gameId,
                    Title = gameDto.Name,
                    ReleaseDate = gameDto.ReleaseDate,
                    Price = gameDto.Price,
                    Description = gameDto.ShortDescription,
                    HeaderImage = gameDto.HeaderImage,
                    MetacriticScore = gameDto.MetacriticScore,
                    PositiveRatings = gameDto.Positive,
                    NegativeRatings = gameDto.Negative,
                    Categories = gameDto.Categories,
                    Genres = gameDto.Genres,
                    Screenshots = gameDto.Screenshots
                };

                var addedGameTags = new HashSet<int>();

                foreach (var (tagName, tagId) in gameDto.Tags!)
                {
                    if (addedTags.Add(tagId))
                    {
                        _context.Tags.Add(new Tag { TagId = tagId, TagName = tagName });
                    }

                    if (addedGameTags.Add(tagId))
                    {
                        game.GameTags.Add(new GameTag { GameId = gameId, TagId = tagId });
                    }
                }

                _context.Games.Add(game);
            }

            await _context.SaveChangesAsync();
        }
    }
}
