using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeMovieLibrary.Api.Models.DB;
public class Movie : IdName
{
    public int? ReleaseYear { get; set; }

    public int? DurationInSeconds { get; set; }

    public Int16 Rating { get; set; }

    public int? PosterId { get; set; }

    [ForeignKey(nameof(PosterId))]
    public Picture Poster { get; set; }

    public IList<MovieDirector> MovieDirectors { get; set; }

    public IList<MovieActor> MovieActors { get; set; }
}

