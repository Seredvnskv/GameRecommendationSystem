using Microsoft.EntityFrameworkCore;
using Backend.Models;

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
            modelBuilder.Entity<Game>()
                .HasKey(g => g.GameId);

            modelBuilder.Entity<Tag>()
                .HasKey(t => t.TagId);

            modelBuilder.Entity<GameTag>()
                .HasKey(gt => new { gt.GameId, gt.TagId });

            modelBuilder.Entity<GameTag>()
                .HasOne(gt => gt.Game)
                .WithMany(g => g.GameTags)
                .HasForeignKey(gt => gt.GameId);

            modelBuilder.Entity<GameTag>()
                .HasOne(gt => gt.Tag)
                .WithMany(t => t.GameTags)
                .HasForeignKey(gt => gt.TagId);

            modelBuilder.Entity<Game>()
                .Property(g => g.Developers)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder.Entity<Game>()
                .Property(g => g.Publishers)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder.Entity<Game>()
            .Property(g => g.Categories)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder.Entity<Game>()
                .Property(g => g.Genres)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder.Entity<Game>()
                .Property(g => g.Screenshots)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
