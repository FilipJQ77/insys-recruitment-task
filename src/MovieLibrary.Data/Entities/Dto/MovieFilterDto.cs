using System.Collections.Generic;

namespace MovieLibrary.Data.Entities.Dto;

public class MovieFilterDto
{
    public string? Title { get; set; }
    public List<string>? Categories { get; set; }
    public decimal? MinImdbRating { get; set; }
    public decimal? MaxImdbRating { get; set; }
}