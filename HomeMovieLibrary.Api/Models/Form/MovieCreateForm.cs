namespace HomeMovieLibrary.Api.Models;
public class MovieCreateForm : MovieBase
{
    public int? PosterId { get; set; }

    public List<int> Actors { get; set; } = new List<int>();

    public List<int> Directors { get; set; } = new List<int>();
}

