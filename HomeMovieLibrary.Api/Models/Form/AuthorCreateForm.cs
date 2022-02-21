namespace HomeMovieLibrary.Api.Models;

public class AuthorCreateForm : AuthorBase
{
    public int? MainPhotoId { get; set; }

    public DateTime? BirthDay { get; set; }

    public DateTime? DeathDate { get; set; }

    public List<int> ActedMovies { get; set; }

    public List<int> DirectedMovies { get; set; }
}
