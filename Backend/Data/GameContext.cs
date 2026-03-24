using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Backend.Data
{
    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<GameTag> GameTags { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Games.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var gameBuilder = modelBuilder.Entity<Game>();
            gameBuilder.HasKey(g => g.GameId);

            gameBuilder.HasIndex(g => g.Title);
            gameBuilder.HasIndex(g => g.GameId);

            gameBuilder.Property(g => g.Developers).ListConversion();
            gameBuilder.Property(g => g.Publishers).ListConversion();
            gameBuilder.Property(g => g.Categories).ListConversion();
            gameBuilder.Property(g => g.Genres).ListConversion();
            gameBuilder.Property(g => g.Screenshots).ListConversion();

            modelBuilder.Entity<Tag>()
                .HasKey(t => t.TagId);

            var gameTagBuilder = modelBuilder.Entity<GameTag>();
            gameTagBuilder.HasKey(gt => new { gt.GameId, gt.TagId });

            gameTagBuilder.HasOne(gt => gt.Game)
                .WithMany(g => g.GameTags)
                .HasForeignKey(gt => gt.GameId);

            gameTagBuilder.HasOne(gt => gt.Tag)
                .WithMany(t => t.GameTags)
                .HasForeignKey(gt => gt.TagId);
        }
    }
}
