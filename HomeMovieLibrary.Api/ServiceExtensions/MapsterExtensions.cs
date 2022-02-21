using HomeMovieLibrary.Api.Models.DB;
using HomeMovieLibrary.Api.Models;
using Mapster;
using MapsterMapper;

namespace HomeMovieLibrary.Api.ServiceExtensions;

public static class MapsterExtensions
{
    public static IServiceCollection AddMapster(this IServiceCollection services, Action<TypeAdapterConfig> options = null)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Default.IgnoreNullValues(true);

        config.ForType<MovieCreateForm, Movie>()
            .Map(m => m.MovieActors, s => s.Actors != null 
                ? s.Actors.Select(x => new MovieActor { ActorId = x }) 
                : new List<MovieActor>())
            .Map(m => m.MovieDirectors, s => s.Directors != null 
                ? s.Directors.Select(x => new MovieDirector { DirectorId = x }) 
                : new List<MovieDirector>());

        config.ForType<MovieUpdateForm, Movie>()
            .Map(m => m.MovieActors, s => s.Actors != null 
                ? s.Actors.Select(x => new MovieActor { ActorId = x })
                : new List<MovieActor>())
            .Map(m => m.MovieDirectors, s => s.Directors != null 
                ? s.Directors.Select(x => new MovieDirector { DirectorId = x })
                : new List<MovieDirector>());

        config.ForType<Movie, MovieFull>()
            .Map(m => m.Actors, s => s.MovieActors.Select(x => x.Actor))
            .Map(m => m.Directors, s => s.MovieDirectors.Select(x => x.Director));

        config.ForType<AuthorCreateForm, Author>()
            .Map(a => a.MovieActors, s => s.ActedMovies != null
                ? s.ActedMovies.Select(x => new MovieActor { MovieId = x })
                : new List<MovieActor>())
            .Map(a => a.MovieDirectors, s => s.DirectedMovies != null
                ? s.DirectedMovies.Select(x => new MovieDirector { MovieId = x })
                : new List<MovieDirector>());

        config.ForType<AuthorUpdateForm, Author>()
            .Map(a => a.MovieActors, s => s.ActedMovies != null
                ? s.ActedMovies.Select(x => new MovieActor { MovieId = x })
                : new List<MovieActor>())
            .Map(a => a.MovieDirectors, s => s.DirectedMovies != null
                ? s.DirectedMovies.Select(x => new MovieDirector { MovieId = x })
                : new List<MovieDirector>());

        config.ForType<Author, AuthorFull>()
            .Map(a => a.ActedMovies, s => s.MovieActors.Select(x => x.Movie))
            .Map(a => a.DirectedMovies, s => s.MovieDirectors.Select(x => x.Movie));


        options?.Invoke(config);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
