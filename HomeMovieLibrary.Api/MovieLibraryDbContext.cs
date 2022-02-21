using HomeMovieLibrary.Api.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace HomeMovieLibrary.Api;
public class MovieLibraryDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Picture> Pictures { get; set; }

    public DbSet<MovieActor> MovieActors { get; set; }

    public DbSet<MovieDirector> MovieDirectors { get; set; }

    public MovieLibraryDbContext(DbContextOptions<MovieLibraryDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieActor>()
            .HasKey(ma => new { ma.MovieId, ma.ActorId });

        modelBuilder.Entity<MovieActor>()
            .HasOne(x => x.Movie)
            .WithMany(x => x.MovieActors)
            .HasForeignKey(x => x.MovieId);

        modelBuilder.Entity<MovieActor>()
            .HasOne(x => x.Actor)
            .WithMany(x => x.MovieActors)
            .HasForeignKey(x => x.ActorId);

        modelBuilder.Entity<MovieDirector>()
                .HasKey(ma => new { ma.MovieId, ma.DirectorId });

        modelBuilder.Entity<MovieDirector>()
            .HasOne(x => x.Movie)
            .WithMany(x => x.MovieDirectors)
            .HasForeignKey(x => x.MovieId);

        modelBuilder.Entity<MovieDirector>()
            .HasOne(x => x.Director)
            .WithMany(x => x.MovieDirectors)
            .HasForeignKey(x => x.DirectorId);

        base.OnModelCreating(modelBuilder);
    }
}
