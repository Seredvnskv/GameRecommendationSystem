using Backend.Data;
using Backend.Dtos;
using Backend.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints
{
    public static class GamesEndpoints
    {
        public static void MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/v1/games");

            group.MapGet("/{id}", async (string id, GameContext context) =>
            {
                var game = await context.Games
                    .Include(g => g.GameTags)
                    .ThenInclude(gt => gt.Tag)
                    .SingleOrDefaultAsync(g => g.GameId == id);

                if (game == null) return Results.NotFound();

                return Results.Ok(
                    GameMapper.MapToGameDto(game)
                );
            });

            group.MapGet("/search", async (string? title, GameContext context) =>
            {
                var query = context.Games.AsQueryable();

                if (!string.IsNullOrEmpty(title))
                {
                    query = query
                        .Where(g => g.Title.ToLower().Contains(title.ToLower()));
                }

                var results = await query
                    .Include(g => g.GameTags)
                    .ThenInclude(gt => gt.Tag)
                    .ToListAsync();

                return Results.Ok(
                    GameMapper.MapToGameDtoList(results)
                );
            });
        }
    }
}
