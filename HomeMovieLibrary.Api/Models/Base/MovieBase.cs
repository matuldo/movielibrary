using System.ComponentModel.DataAnnotations;

namespace HomeMovieLibrary.Api.Models;
public class MovieBase
{

    [Required]
    public string Name { get; set; }

    public int? ReleaseYear { get; set; }

    public int DurationInSeconds { get; set; }

    public Int16 Rating { get; set; }
}

