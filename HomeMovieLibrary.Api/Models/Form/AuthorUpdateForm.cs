namespace HomeMovieLibrary.Api.Models;

public class AuthorUpdateForm : AuthorCreateForm, IUpdateForm
{
    public int Id { get; set; }
}
