using Microsoft.AspNetCore.Identity;

namespace AspireAppDemo.ApiService.Models;

public static class SeedData
{
    public async static Task Initialize(UserManager<User> userManager)
    {
        ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

		// Look for any games.
		User? user = await userManager.FindByNameAsync("admin@localhost");

		if (user != null)
        {
            return;   // DB has been seeded
        }

		// Create fake user with password
		user = new User()
        {
            UserName = "admin@localhost",
            Email = "admin@localhost",
            Firstname = "Admin",
            Lastname = "User"
        };

        var resultCreate = await userManager.CreateAsync(user, "Admin@123");

        if (resultCreate != null)
        {
            user.EmailConfirmed = true;
			await userManager.UpdateAsync(user);
        }
	}
}