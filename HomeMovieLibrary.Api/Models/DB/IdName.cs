using System.ComponentModel.DataAnnotations;

namespace HomeMovieLibrary.Api.Models.DB;

public class IdName
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
