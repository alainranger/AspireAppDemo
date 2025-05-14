using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspireAppDemo.ApiService.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AspireAppDemo.ApiService.Services;

/// <summary>
/// AuthService class implements IAuthService interface.
/// It provides methods for user authentication, logout, and subscription.
/// </summary>
/// <param name="config">IConfiguration instance for accessing configuration settings.</param>
/// <param name="userManager">UserManager instance for managing user-related operations.</param>
/// <param name="signInManager">SignInManager instance for managing user sign-in operations.</param>
public class AuthService(
    IConfiguration config,
    UserManager<User> userManager,
    SignInManager<User> signInManager) : IAuthService
{
    private readonly IConfiguration _config = config;
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;

    /// <summary>
    /// Authenticates a user based on the provided login request.
    /// </summary>
    /// <param name="request">LoginRequest object containing username and password.</param>
    /// <returns>LoginResponse object containing the authentication token if successful, otherwise null.</returns>
    /// <exception cref="InvalidOperationException">Thrown when user email is null.</exception>    
    /// <exception cref="ArgumentNullException">Thrown if request is null.</exception>
    public async Task<LoginResponse?> AuthenticateAsync(LoginRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            var users = _userManager.Users.ToList();
            return null;
        }

        var result = _signInManager.CheckPasswordSignInAsync(user, request.Password, false).Result;
        if (!result.Succeeded)
        {
            return null;
        }

        // Create the claims
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username)
        };

        return new LoginResponse
        {
            Token = user.Email != null ? this.GenerateToken(user.Email) : throw new InvalidOperationException("User email is null")
        };
    }

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    public async Task LogoutAsync()
    {
        await this._signInManager.SignOutAsync();
    }

    /// <summary>
    /// Subscribes a new user with the provided user object and password.
    /// </summary>
    /// <param name="user">User object containing user details.</param>
    /// <param name="password">Password for the new user.</param>
    /// <returns>True if subscription is successful, otherwise false.</returns>
    public async Task<bool> SubscribeAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        return await Task.FromResult(result.Succeeded);
    }

    /// <summary>
    /// Generates a JWT token for the specified email.
    /// </summary>
    /// <param name="email">Email address for which the token is generated.</param>
    /// <returns>Generated JWT token as a string.</returns>
    /// <exception cref="ArgumentNullException">Thrown if email is null or empty.</exception>
    private string GenerateToken(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException(nameof(email));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}