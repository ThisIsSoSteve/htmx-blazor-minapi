using System.Data.SQLite;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Create;
public interface ICreateService
{
    int Insert(CreateMovieViewModel data);
}

public class CreateService : ICreateService
{
    private readonly SQLiteConnection db;
    public CreateService(SQLiteConnection db)
    {
        this.db = db;
    }

    public int Insert(CreateMovieViewModel data)
    {
        var newMovie = db.QuerySingleOrDefault<Movie>(
            @"INSERT INTO Movie(Title, Description) VALUES (@Title, @Description)
            RETURNING *", data
        );

        if(newMovie == null)
        {
            return -1;
        }

        return newMovie.Id;
    }
}

