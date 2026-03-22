using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Backend.Factory
{
    public sealed class VectorFactory
    {
        private readonly GameFeaturesCache _cache;

        public VectorFactory(GameFeaturesCache cache)
        {
            _cache = cache;
        }

        public double[] CreateVector(Game game) 
        {
            var vector = new double[_cache.GetSize()];
            int idx = 0;

            foreach (var id in _cache.TagIds) 
            { 
                vector[idx++] = game.GameTags.Any(gt => gt.TagId == id) ? 1.0 : 0.0;
            }

            foreach (var genre in _cache.Genres)
            {
                vector[idx++] = game.Genres.Contains(genre) ? 1.0 : 0.0;
            }

            foreach (var category in _cache.Categories)
            {
                vector[idx++] = game.Categories.Contains(category) ? 1.0 : 0.0;
            }

            return vector;
        }
    }
}
