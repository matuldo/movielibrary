namespace HomeMovieLibrary.Api.Models.DB;
public class MovieActor
{
    public int MovieId { get; set; }
    public int ActorId { get; set; }

    public Movie Movie { get; set; }
    public Author Actor { get; set; }
}
