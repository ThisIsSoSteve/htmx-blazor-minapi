using System.ComponentModel;
using BlazorTemplater;
using HTMX_Blazor_MinAPI.src.pages.create;
using HTMX_Blazor_MinAPI.src.shared;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace Create;
public static class CreatePageEndpoints
{
    public static WebApplication MapCreatePageEndpoints(this WebApplication app)
    {
        app.MapGet("/create", () =>
        {
            return Results.Content(
                new ComponentRenderer<CreatePage>()
                //.Set(x => x.Text, "This is a button you can click")
                .UseLayout<Layout>()
                .Render()
                , "text/html"
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

            if (newMovie > 0)
            {
                return Results.Content(new ComponentRenderer<CreateMovieSuccess>().Render(), "text/html");
            }

            return Results.Content(
                new ComponentRenderer<CreateMovieError>()
                .Set(x => x.Message, "We were unable to create your movie at this time, please try again later")
                .Render(), "text/html");

            // return Results.Extensions.Html(
            //     $"<div>Id: {newMovie.Id}, Name: {newMovie.Title}, Description: {newMovie.Description}</div>"
            // );
        });

        return app;
    }
}