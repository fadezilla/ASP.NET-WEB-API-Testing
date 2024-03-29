using Microsoft.EntityFrameworkCore;
using ApiEfProject.Models;

namespace ApiEfProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to many
            modelBuilder.Entity<Genre>()
                .HasMany(a => a.Movies)
                .WithOne(p => p.Genre)
                .HasForeignKey(a => a.GenreId)
                .OnDelete(DeleteBehavior.NoAction);

            //One-to-Many
            modelBuilder.Entity<Studio>()
                .HasMany(a => a.Movies)
                .WithOne(p => p.Studio)
                .HasForeignKey(a => a.StudioId)
                .OnDelete(DeleteBehavior.NoAction);

            //One to many
            modelBuilder.Entity<Review>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.Reviews)
                .HasForeignKey(ma => ma.MovieId)
                .OnDelete(DeleteBehavior.NoAction);

            //Many-to-Many
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);
        }
    }
}