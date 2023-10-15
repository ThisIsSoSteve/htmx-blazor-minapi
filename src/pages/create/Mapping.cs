namespace Create;
public static class Mapping
{
    public static NewMovieViewModel ToViewModel(this MovieDto movieDto)
    {
        return new NewMovieViewModel
        {
            Id = movieDto.Id,
            Name = movieDto.Title,
            Description = movieDto.Description
        };
    }

    

    public static MovieDto ToDto(this Movie dbMovie)
    {
        return new MovieDto
        {
            Id = dbMovie.Id,
            Title = dbMovie.Title,
            Description = dbMovie.Description
        };
    }
    public static IEnumerable<MovieDto> ToDto(this IEnumerable<Movie> movieDtos)
    {
        var output = new List<MovieDto>();
        foreach (var item in movieDtos)
        {
            output.Add(item.ToDto());
        }

        return output;
    }
}