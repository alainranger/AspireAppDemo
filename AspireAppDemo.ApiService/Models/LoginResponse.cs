namespace AspireAppDemo.ApiService.Models;

public record LoginResponse
{
    public string Token { get; init; } = string.Empty;
}
