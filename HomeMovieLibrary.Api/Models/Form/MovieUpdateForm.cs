namespace HomeMovieLibrary.Api.Models;
public class MovieUpdateForm: MovieCreateForm, IUpdateForm
{
    public int Id { get; set; }
}

