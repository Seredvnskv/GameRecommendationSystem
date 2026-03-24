using Backend.Data;
using Backend.Factory;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class RecommendationService
    {
        private readonly GameContext _context;
        private readonly VectorFactory _vectorFactory;

        public RecommendationService(GameContext context, VectorFactory vectorFactory)
        {
            _context = context;
            _vectorFactory = vectorFactory;
        }

        public async Task<List<Game>> GetSimilarGamesAsync(string id, int count = 10) 
        {
            var game = await _context.Games
                .Include(g => g.GameTags)
                .ThenInclude(gt => gt.Tag)
                .FirstOrDefaultAsync(g => g.GameId == id);

            if (game == null) return new List<Game>();

            var gameVector = _vectorFactory.CreateVector(game);

            var allGames = await _context.Games
                .Include(g => g.GameTags)
                .ThenInclude(gt => gt.Tag)
                .Where(g => g.GameId != id)
                .ToListAsync();

            var similarGames = allGames
                .Select(g => new
                {
                    Game = g,
                    Similarity = CalculateSimilarity(game, g, gameVector, _vectorFactory.CreateVector(g))
                })
                .OrderByDescending(x => x.Similarity)
                .Where(x => x.Similarity > 0)
                .Take(count)
                .Select(x => x.Game)
                .ToList();

            return similarGames;
        }

        private double CalculateSimilarity(Game gameA, Game gameB, double[] vectorA, double[] vectorB)
        {
            double cosineScore = CalculateCosineSimilarity(vectorA, vectorB) * 0.5; 
            double genreScore = CalculateGenreSimilarity(gameA, gameB) * 0.25;             
            double publisherScore = CalculatePublisherSimilarity(gameA, gameB) * 0.25; 

            return cosineScore + genreScore + publisherScore;
        }

        private double CalculateGenreSimilarity(Game gameA, Game gameB)
        {
            if (gameA.Genres.Count == 0 || gameB.Genres.Count == 0)
                return 0;

            var shared = gameA.Genres.Intersect(gameB.Genres).Count();
            var total = gameA.Genres.Union(gameB.Genres).Count();

            return (double) shared / total;
        }

        private double CalculatePublisherSimilarity(Game gameA, Game gameB)
        {
            var samePublisher = gameA.Publishers.Intersect(gameB.Publishers).Any();
            var sameDeveloper = gameA.Developers.Intersect(gameB.Developers).Any();

            if (samePublisher && sameDeveloper) return 1.0;
            if (samePublisher || sameDeveloper) return 0.8; 

            return 0;
        }

        private double CalculateCosineSimilarity(double[] vectorA, double[] vectorB)
        {
            if (vectorA.Length != vectorB.Length)
                throw new ArgumentException("Vectors must have the same length");

            double dotProduct = 0;
            for (int i = 0; i < vectorA.Length; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];
            }

            double magnitudeA = Math.Sqrt(vectorA.Sum(v => v * v));
            double magnitudeB = Math.Sqrt(vectorB.Sum(v => v * v));

            if (magnitudeA == 0 || magnitudeB == 0)
                return 0;

            return dotProduct / (magnitudeA * magnitudeB);
        }
    }
}
