using AspireAppDemo.ApiService.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspireAppDemo.ApiService.Models;

public static class SeedData
{
    public static void Initialize(AppIdentityiDbContest appIdentityiDbContest)
    {
        // Look for any games.
        if (appIdentityiDbContest.Users.Any())
        {
            return;   // DB has been seeded
        }

        // Create fake user with password
        var user = new User()
        {
            UserName = "admin@localhost",
            Email = "admin@localhost",
            Firstname = "Admin",
            Lastname = "User",
            EmailConfirmed = true,
        };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "Admin@123");

        appIdentityiDbContest.Users.AddRange(
        [
            user
        ]);

        appIdentityiDbContest.SaveChanges();
    }
}