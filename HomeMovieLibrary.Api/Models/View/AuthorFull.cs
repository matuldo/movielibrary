using System.Text.Json.Serialization;

namespace HomeMovieLibrary.Api.Models;

public class AuthorFull : AuthorFlat
{
    [JsonPropertyOrder(1)]
    public List<MovieFlat> DirectedMovies { get; set; }

    [JsonPropertyOrder(2)]
    public List<MovieFlat> ActedMovies { get; set; }

}
