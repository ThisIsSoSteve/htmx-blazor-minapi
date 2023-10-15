using BlazorTemplater;
using HTMX_Blazor_MinAPI.src.pages.home;
using HTMX_Blazor_MinAPI.src.shared;

namespace Home;
public static class HomeEndpoints
{
    public static WebApplication MapHomePageEndpoints(this WebApplication app)
    {
        app.MapGet("/", () =>
        {
            return Results.Extensions.Html(
                new ComponentRenderer<HomePage>()
                //.Set(x => x.Text, "This is a button you can click")
                .UseLayout<Layout>()
                .Render()
            );
        });

        app.MapGet("/getmovies", (IHomeService service) =>
        {
            //todo get from db
            // var data = new List<MovieViewModel>(){
            //     new MovieViewModel(){
            //         Id = 1,
            //         Name = "Movie 1"
            //     },
            //     new MovieViewModel(){
            //         Id = 2,
            //         Name = "Movie 2"
            //     },
            //     new MovieViewModel(){
            //         Id = 3,
            //         Name = "Movie 3"
            //     },
            //     new MovieViewModel(){
            //         Id = 4,
            //         Name = "Movie 4"
            //     }
            // };

            var data = service.GetMovies();


            return Results.Extensions.Html(
                new ComponentRenderer<MovieThumbnails>()
                .Set(x => x.Data, data)
                .Render()
            );
        });

        return app;
    }
}