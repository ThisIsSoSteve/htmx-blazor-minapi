using System.Data.SQLite;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Create;
public interface ICreateService
{
    MovieDto Insert(CreateMovieViewModel data);
}

public class CreateService : ICreateService
{
    private readonly SQLiteConnection db;
    public CreateService(SQLiteConnection db)
    {
        this.db = db;
    }

    public MovieDto Insert(CreateMovieViewModel data)
    {

        var newMovie = db.QuerySingleOrDefault<Movie>(
            @"INSERT INTO Movie(Title, Description) VALUES (@Title, @Description)
            RETURNING *", data
        );

        if(newMovie == null)
        {
            return new MovieDto{Id = -1};
        }

        return newMovie.ToDto();
    }
}

