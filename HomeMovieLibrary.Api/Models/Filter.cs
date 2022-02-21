using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace HomeMovieLibrary.Api.Models;

public class Filter
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FilterOrderDirection
    {
        [Display(Name = "asc")]
        Asc = 0,
        [Display(Name = "desc")]
        Desc = 1
    }

    [FromQuery(Name = "search")]
    public string Search { get; set; } = string.Empty;

    [FromQuery(Name = "by")]
    public string SearchBy { get; set; } = Constants.DefaultFilter;

    [FromQuery(Name = "order")]
    public string OrderBy { get; set; } = Constants.DefaultFilter;

    [FromQuery(Name = "direction")]
    public FilterOrderDirection OrderDirection { get; set; } = Constants.DefaultSortDirection;

    [FromQuery(Name = "size")]
    [Range(1, Constants.MaximumPageSize, ErrorMessage = "Maximum page size is 200.")]
    public int PageSize { get; set; } = Constants.MaximumPageSize;

    [FromQuery(Name = "page")]
    [Range(0, Int32.MaxValue, ErrorMessage = "Page cannot be of negative value.")]
    public int PageIndex { get; set; }
}
