using Backend.Dtos;
using Backend.Models;

namespace Backend.Mapper
{
    public static class GameMapper
    {
        public static GameDto MapToGameDto(Game game) 
        {
            return new GameDto(
                game.GameId,
                game.Title,
                game.ReleaseDate,
                game.AboutGame,
                game.Price,
                game.HeaderImage,
                game.MetacriticScore,
                game.PositiveRatings,
                game.NegativeRatings,
                game.Developers,
                game.Publishers,
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

        public static Game MapToGame(string id, JsonGameDto dto) 
        { 
            return new Game
            {
                GameId = id,
                Title = dto.Name,
                ReleaseDate = dto.ReleaseDate,
                Price = dto.Price,
                Description = dto.Description,
                AboutGame = dto.AboutGame,
                ShortDescription = dto.ShortDescription,
                HeaderImage = dto.HeaderImage,
                MetacriticScore = dto.MetacriticScore,
                PositiveRatings = dto.Positive,
                NegativeRatings = dto.Negative,
                Developers = dto.Developers,
                Publishers = dto.Publishers,
                Categories = dto.Categories,
                Genres = dto.Genres,
                Screenshots = dto.Screenshots
            };
        }
    }
}
