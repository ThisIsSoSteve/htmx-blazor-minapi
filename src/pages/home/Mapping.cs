namespace Home;

public static class Mapping
{
    public static MovieViewModel ToViewModel(this Movie model)
    {
        return new MovieViewModel
        {
            Id = model.Id,
            Name = model.Title,
        };
    }

    public static IEnumerable<MovieViewModel> ToViewModel(this IEnumerable<Movie> models)
    {
        var output = new List<MovieViewModel>();
        foreach (var item in models)
        {
            output.Add(item.ToViewModel());
        }

        return output;
    }
}