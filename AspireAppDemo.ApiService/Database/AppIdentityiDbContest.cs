using AspireAppDemo.ApiService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspireAppDemo.ApiService.Database;

public class AppIdentityiDbContest(DbContextOptions<AppIdentityiDbContest> options) : IdentityDbContext<User, Role, int>(options)
{
}