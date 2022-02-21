using System.Text.Json.Serialization;

namespace HomeMovieLibrary.Api.Models;
public class MovieFull : MovieFlat
{
    [JsonPropertyOrder(100)]
    public List<AuthorFlat> Directors { get; set; }

    [JsonPropertyOrder(200)]
    public List<AuthorFlat> Actors { get; set; }
}

