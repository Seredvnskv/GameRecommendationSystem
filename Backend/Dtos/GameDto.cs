namespace Backend.Dtos
{
    public record class GameDto(
        string Title,
        string ReleaseDate,
        string Description,
        decimal Price,
        string HeaderImage,
        int MetacriticScore,
        int PositiveRatings,
        int NegativeRatings,
        List<string> Categories,
        List<string> Genres,
        List<string> Screenshots,
        List<string> Tags
    );
}
