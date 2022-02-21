using System.ComponentModel.DataAnnotations;

namespace HomeMovieLibrary.Api.Models;

public class AuthorBase
{
    [Required]
    public string Name { get; set; }
}
