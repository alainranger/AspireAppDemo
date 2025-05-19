using AspireAppDemo.ApiService.Models;

namespace AspireAppDemo.ApiService.Services;

public interface IAuthService
{
    Task<LoginResponse?> AuthenticateAsync(LoginRequest request);
    Task LogoutAsync();
    Task<bool> SubscribeAsync(SubscribeRequest request);
}
