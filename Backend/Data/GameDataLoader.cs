using Backend.Dtos;
using Backend.Mapper;
using Backend.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

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

            if (!File.Exists(jsonFilePath)) 
                throw new Exception($"JSON file not found at path: {jsonFilePath}");

            var json = await File.ReadAllTextAsync(jsonFilePath);

            var gamesData = JsonSerializer.Deserialize<Dictionary<string, JsonGameDto>>(json);

            if (gamesData == null ) 
                throw new InvalidOperationException("Failed to deserialize JSON file");

            var addedTitles = new HashSet<string>(); // To prevent adding duplicates
                                                     
            var addedTags = new HashSet<int>();

            foreach (var (gameId, jsonGameDto) in gamesData)
            {
                if (_context.Games.Any(g => g.GameId == gameId)) 
                    continue;

                var normalizedTitle = NormalizeTitle(jsonGameDto.Name);

                if (addedTitles.Contains(normalizedTitle))
                    continue;

                var game = GameMapper.MapToGame(gameId, jsonGameDto);

                if (game.MetacriticScore == 0) 
                    continue;

                var addedGameTags = new HashSet<int>();

                foreach (var (tagName, tagId) in jsonGameDto.Tags!)
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
                addedTitles.Add(normalizedTitle);
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("Games data loaded successfully");
        }

        private string NormalizeTitle(string title) // Remove trademark symbols, hyphens, and whitespace for consistent comparison
        {
            return Regex.Replace(
                title.ToLower().Trim(),
                @"[™®©\-\s]+",
                ""
            );
        }
    }
}
