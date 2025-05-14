using AspireAppDemo.ApiService.Models;
using AspireAppDemo.ApiService.Services;

namespace AspireAppDemo.ApiService.Endpoints;

public static class AuthEndpoints
{
    /// <summary>
    /// Maps the authentication endpoints to the application.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <remarks>
    /// This method maps the following endpoints:
    /// <list type="bullet">
    /// <item>
    /// <description><c>POST /login</c> - Authenticates a user and returns a token.</description></item>
    /// <item>
    /// <description><c>POST /logout</c> - Logs out the current user.</description></item>
    /// <item>
    /// <description><c>POST /subscribe</c> - Subscribes a user with a password.</description></item>
    /// </list>
    /// </remarks>
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth")
            .WithTags("Authentication");

        group.MapPost("/login", async (
            LoginRequest request,
            IAuthService authService) =>
        {
            var result = await authService.AuthenticateAsync(request);
            return result is null ? Results.Unauthorized() : Results.Ok(result);
        });

        group.MapPost("/logout", async (
            IAuthService authService) =>
        {
            await authService.LogoutAsync();
            return Results.Ok();
        });

        group.MapPost("/subscribe", async (
            User user,
            string password,
            IAuthService authService) =>
        {
            var result = await authService.SubscribeAsync(user, password);
            return result ? Results.Ok() : Results.BadRequest();
        });
    }
}