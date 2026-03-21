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

        public static void SeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var loader = scope.ServiceProvider.GetRequiredService<GameDataLoader>();
                try
                {
                    loader.LoadGames().Wait();
                    Console.WriteLine("Games data loaded successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading games: {ex.Message}");
                }
            }
        }
    }
}
