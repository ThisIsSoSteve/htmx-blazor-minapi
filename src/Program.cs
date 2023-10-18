
//using BlazorTemplater;
using Dapper;
//using HTMX_Blazor_MinAPI.src.components;
//using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
//using Microsoft.Data.Sqlite;

using Home;
using Create;
using Microsoft.AspNetCore.Authentication.Cookies;
using Account;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    //options.LoginPath = "/Login";
});//.AddJwtBearer();

builder.Services.AddAuthorization();

var connectionString = builder
.Configuration
.GetConnectionString("Default");

builder.Services.AddScoped(_ => new SQLiteConnection(connectionString));
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<ICreateService, CreateService>();

//builder.Services.AddRazorPages();
builder.Services.AddAntiforgery();

var app = builder.Build();

EnsureDb(app.Services);

app.UseStaticFiles();


app.MapAccountEndpoints();
app.MapHomePageEndpoints();
app.MapCreatePageEndpoints();

app.MapGet("/authtest", () => "This endpoint requires authorization").RequireAuthorization();

//app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
//app.MapGet("/", () => "Hello World!");

// app.MapGet("/button", () => {

//     var button = new ComponentRenderer<TestButton>()
//     .Set(x => x.Text, "This is a button you can click")
//     .UseLayout<Layout>()
//     .Render();
//     //return Results.Text(button);

//     return Results.Extensions.Html(button);


// });

// app.MapGet("/clicked", () => {
//     return Results.Extensions.Html("<div>You have clicked</div>");
// });

// app.MapPost("/postform", (TestFormViewModel data) =>
// {
//     return Results.Extensions.Html($"<div>You name is {data.Name} and your age is {data.Age} </div>");
// });

app.Run();

//todo move to database folder
void EnsureDb(IServiceProvider services)
{
    using var db = services.CreateScope()
    .ServiceProvider
    .GetRequiredService<SQLiteConnection>();

    var sql = @"CREATE TABLE IF NOT EXISTS Movie (
                Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                Title TEXT NOT NULL,
                Description TEXT NOT NULL
                );";

    db.Execute(sql);
}