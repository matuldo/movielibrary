namespace HomeMovieLibrary.Api.Models.DB;
public class MovieDirector
{
    public int MovieId { get; set; }
    public int DirectorId { get; set; }

    public Movie Movie { get; set; }
    public Author Director { get; set; }
}
