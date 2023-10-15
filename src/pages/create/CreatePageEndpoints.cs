using BlazorTemplater;
using HTMX_Blazor_MinAPI.src.pages.create;
using HTMX_Blazor_MinAPI.src.shared;
using Microsoft.AspNetCore.Mvc;

namespace Create;
public static class CreatePageEndpoints
{
    public static WebApplication MapCreatePageEndpoints(this WebApplication app)
    {
        app.MapGet("/create", () =>
        {
            return Results.Extensions.Html(
                new ComponentRenderer<CreatePage>()
                //.Set(x => x.Text, "This is a button you can click")
                .UseLayout<Layout>()
                .Render()
            );
        });

        app.MapPost("/create", (CreateMovieViewModel data, ICreateService service) =>
        {
            //fake processing time 
            
            Thread.Sleep(2000);
            //todo sanitize 

            //todo: validate

            //todo: save to database
            var newMovie = service.Insert(data);

            return Results.Extensions.Html(
                $"<div>Id: {newMovie.Id}, Name: {newMovie.Title}, Description: {newMovie.Description}</div>"
            );
        });

        return app;
    }
}