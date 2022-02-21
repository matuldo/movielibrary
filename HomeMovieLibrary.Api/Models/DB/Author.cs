using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeMovieLibrary.Api.Models.DB;
public class Author : IdName
{
    public int? MainPhotoId { get; set; }

    [ForeignKey(nameof(MainPhotoId))]
    public Picture MainPhoto { get; set; }

    public DateTime? BirthDay { get; set; }

    public DateTime? DeathDate { get; set; }

    public IList<MovieDirector> MovieDirectors { get; set; }

    public IList<MovieActor> MovieActors { get; set; }
}
