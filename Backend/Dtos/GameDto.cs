namespace Backend.Dtos
{
    public record class GameDto(
        string GameId,
        string Title,
        string ReleaseDate,
        string AboutGame,
        string ShortDescription,
        string Description,
        decimal Price,
        string HeaderImage,
        int MetacriticScore,
        int PositiveRatings,
        int NegativeRatings,
        List<string> Developers,
        List<string> Publishers,
        List<string> Categories,
        List<string> Genres,
        List<string> Screenshots,
        List<string> Tags
    );
}
