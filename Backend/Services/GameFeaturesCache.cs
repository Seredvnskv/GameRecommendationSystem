using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public sealed class GameFeaturesCache
    {
        public List<int> TagIds { get; private set; } = [];
        public List<string> Genres { get; private set; } = [];
        public List<string> Categories { get; private set; } = [];

        public async Task InitializeAsync(GameContext context)
        {
            TagIds = await context.Tags
                .Select(t => t.TagId)
                .ToListAsync();

            var games = await context.Games.ToListAsync();

            Genres = games
                .SelectMany(g => g.Genres)
                .Distinct()
                .ToList();

            Categories = games
                .SelectMany(g => g.Categories)
                .Distinct()
                .ToList();

            Console.WriteLine("Cache initialized successfully");
        }

        public int GetSize()
        {
            return TagIds.Count + Genres.Count + Categories.Count;
        }
    }
}
