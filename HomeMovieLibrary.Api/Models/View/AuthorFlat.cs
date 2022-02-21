namespace HomeMovieLibrary.Api.Models;

public class AuthorFlat : AuthorBase
{
    public int Id { get; set; }
    
    public string MainPhotoUrl { get; set; }

    public DateTime? BirthDay { get; set; }

    public DateTime? DeathDate { get; set; }
}
