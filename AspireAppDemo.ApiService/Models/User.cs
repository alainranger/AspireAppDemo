using Microsoft.AspNetCore.Identity;

namespace AspireAppDemo.ApiService.Models;

public class User : IdentityUser<int>
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
}