namespace HomeMovieLibrary.Api.Models;

public class PagedResult<T> where T : class
{
    public List<T> Data { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public int FilteredCount { get; set; }

    public int TotalCount { get; set; }
}
