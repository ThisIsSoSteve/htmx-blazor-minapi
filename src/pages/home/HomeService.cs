using System.Data.SQLite;
using Dapper;

namespace Home;
public interface IHomeService
{
    IEnumerable<MovieViewModel> GetMovies();
}

public class HomeService : IHomeService
{
    private readonly SQLiteConnection db;
    public HomeService(SQLiteConnection db)
    {
        this.db = db;
    }

    public IEnumerable<MovieViewModel> GetMovies()
    {

        var movies = db.Query<Movie>(
            @"SELECT Id, Title, Description FROM Movie"
        );

        if(movies == null)
        {
            return Enumerable.Empty<MovieViewModel>();
        }

        return movies.ToViewModel();
    }
}

