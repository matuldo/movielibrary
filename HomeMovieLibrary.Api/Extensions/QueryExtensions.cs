using System.Linq;
using HomeMovieLibrary.Api.Models;
using HomeMovieLibrary.Api.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace HomeMovieLibrary.Api.Extensions;

public static class QueryExtensions
{
    public static IQueryable<Movie> IncludeAllProperties(this IQueryable<Movie> query)
    {
        return query
            .Include(x => x.MovieActors)
                .ThenInclude(x => x.Actor)
            .Include(x => x.MovieDirectors)
                .ThenInclude(x => x.Director)
            .Include(x => x.Poster);
    }

    public static IQueryable<Author> IncludeAllProperties(this IQueryable<Author> query)
    {
        return query
            .Include(x => x.MovieActors)
                .ThenInclude(x => x.Movie)
            .Include(x => x.MovieDirectors)
                .ThenInclude(x => x.Movie)
            .Include(x => x.MainPhoto);
    }


    public static IQueryable<Movie> Filter(this IQueryable<Movie> query, Filter filter) 
    {
        if (filter is null || string.IsNullOrWhiteSpace(filter.Search)) return query;
        var expression = filter.Search.ToLower();

        return filter.SearchBy?.ToLower() switch
        {
            "year" => query.Where(x => x.ReleaseYear.HasValue && x.ReleaseYear.ToString() == expression),
            "name" => query.Where(x => x.Name.ToLower().Contains(expression)),
            _ => query.Where(x => x.Name.ToLower().Contains(expression))
        };
    }

    public static IQueryable<Author> Filter(this IQueryable<Author> query, Filter filter)
    {
        if (filter is null || string.IsNullOrWhiteSpace(filter.Search)) return query;
        var expression = filter.Search.ToLower();

        return filter.SearchBy?.ToLower() switch
        {
            "movies" => query.Where(x => x.MovieActors != null && x.MovieActors.Any(x => x.Movie.Name.ToLower().Contains(expression))),
            "name" => query.Where(x => x.Name.ToLower().Contains(expression)),
            _ => query.Where(x => x.Name.ToLower().Contains(expression))
        };
    }

    public static IQueryable<Movie> Order(this IQueryable<Movie> query, Filter filter)
    {
        if (filter is null) return query;

        return filter.OrderBy?.ToLower() switch
        {
            var o when o == "year" && filter.OrderDirection == Models.Filter.FilterOrderDirection.Desc 
                => query.OrderByDescending(x => x.ReleaseYear),
            "year" => query.OrderBy(x => (x as Movie).ReleaseYear),
            var o when o == "name" && filter.OrderDirection == Models.Filter.FilterOrderDirection.Desc
                => query.OrderByDescending(x => x.Name),
            "name" => query.OrderBy(x => x.Name),
            var o when o == "id" && filter.OrderDirection == Models.Filter.FilterOrderDirection.Desc
                => query.OrderByDescending(x => x.Id),
            _ => query.OrderBy(x => x.Id)
        };
    }

    public static IQueryable<Author> Order(this IQueryable<Author> query, Filter filter)
    {
        if (filter is null) return query;

        return filter.OrderBy?.ToLower() switch
        {
            var o when o == "age" && filter.OrderDirection == Models.Filter.FilterOrderDirection.Desc
                => query.OrderBy(x => x.BirthDay),
            "age" => query.OrderByDescending(x => x.BirthDay),
            var o when o == "name" && filter.OrderDirection == Models.Filter.FilterOrderDirection.Desc
                => query.OrderByDescending(x => x.Name),
            "name" => query.OrderBy(x => x.Name),
            var o when o == "id" && filter.OrderDirection == Models.Filter.FilterOrderDirection.Desc
                => query.OrderByDescending(x => x.Id),
            _ => query.OrderBy(x => x.Id)
        };
    }


    public static IQueryable<TEntity> ApplyFilter<TEntity>(this IQueryable<TEntity> query, Filter filter) => query switch
    {
        IQueryable<Movie> movieQuery => (IQueryable<TEntity>)movieQuery
            .Include(x => x.Poster)
            .Filter(filter)
            .Order(filter),
        IQueryable<Author> authorQuery => (IQueryable<TEntity>)authorQuery
            .Include(x => x.MainPhoto)
            .Filter(filter)
            .Order(filter),
        _ => query
    };

    public static IQueryable<TEntity> IncludeAllProperties<TEntity>(this IQueryable<TEntity> query) => query switch
    {
        IQueryable<Movie> movieQuery => (IQueryable<TEntity>)movieQuery
                .IncludeAllProperties(),
        IQueryable<Author> authorQuery => (IQueryable<TEntity>)authorQuery
            .IncludeAllProperties(),
        _ => query
    };
}
