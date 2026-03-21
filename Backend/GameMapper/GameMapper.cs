using Backend.Dtos;
using Backend.Models;

namespace Backend.Mapper
{
    public static class GameMapper
    {
        public static GameDto MapToGameDto(Game game) 
        {
            return new GameDto(
                game.Title,
                game.ReleaseDate,
                game.Description,
                game.Price,
                game.HeaderImage,
                game.MetacriticScore,
                game.PositiveRatings,
                game.NegativeRatings,
                game.Categories,
                game.Genres,
                game.Screenshots,
                game.GameTags
                    .Select(gt => gt.Tag.TagName)
                    .ToList()
            );
        }

        public static List<GameDto> MapToGameDtoList(List<Game> games)
        {
            return games.Select(MapToGameDto).ToList();
        }
    }
}
