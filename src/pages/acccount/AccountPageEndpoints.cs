
using System.Security.Claims;
using System.Security.Policy;
using BlazorTemplater;
using HTMX_Blazor_MinAPI.src.pages.acccount;
using HTMX_Blazor_MinAPI.src.shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Account;
public static class AccountPageEndpoints
{
    public static WebApplication MapAccountEndpoints(this WebApplication app)
    {
        app.MapGet("/account/login", () =>
        {
            return Results.Extensions.Html(
                new ComponentRenderer<Login>()
                .UseLayout<Layout>()
                .Render()
            );
        });

        app.MapPost("/account/login", async (HttpContext context) =>
        {

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "email@email.com"),
            new Claim("FullName", "Steve"),
            new Claim(ClaimTypes.Role, "Administrator"),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            //AllowRefresh = <bool>,
            // Refreshing the authentication session should be allowed.

            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            // The time at which the authentication ticket expires. A 
            // value set here overrides the ExpireTimeSpan option of 
            // CookieAuthenticationOptions set with AddCookie.

            //IsPersistent = true,
            // Whether the authentication session is persisted across 
            // multiple requests. When used with cookies, controls
            // whether the cookie's lifetime is absolute (matching the
            // lifetime of the authentication ticket) or session-based.

            //IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.

            //RedirectUri = <string>
            // The full path or absolute URI to be used as an http 
            // redirect response value.
        };
            await context.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity), 
            authProperties);

            //return LocalRedirect(Url.GetLocalUrl(returnUrl));
            
            

            //return Results.LocalRedirect("/authtest");

            return Results.Extensions.Html(
                "<div> logged in </div>"
            );
        });

        return app;
    }
}