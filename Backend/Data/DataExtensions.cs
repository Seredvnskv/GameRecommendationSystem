using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public static class DataExtensions
    {
        public static void MigrateDb(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<GameContext>();
                context.Database.Migrate();
            }
        }

        public static async Task SeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var loader = scope.ServiceProvider.GetRequiredService<GameDataLoader>();
                var cache = scope.ServiceProvider.GetRequiredService<GameFeaturesCache>();
                var context = scope.ServiceProvider.GetRequiredService<GameContext>();

                try
                {
                    await loader.LoadGames();
                    await cache.InitializeAsync(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading games: {ex.Message}");
                }
            }
        }
    }
}
