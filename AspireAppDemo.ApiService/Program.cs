using System.Text;
using AspireAppDemo.ApiService.Database;
using AspireAppDemo.ApiService.Endpoints;
using AspireAppDemo.ApiService.Models;
using AspireAppDemo.ApiService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<AppIdentityiDbContest>(connectionName: "postgresdb");

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
   .AddDefaultIdentity<User>(options =>
   {
       options.SignIn.RequireConfirmedAccount = false;
       options.User.RequireUniqueEmail = true;
       options.Password.RequireDigit = false;
       options.Password.RequiredLength = 6;
       options.Password.RequireNonAlphanumeric = false;
       options.Password.RequireUppercase = false;
       options.Password.RequireLowercase = false;
   })
   .AddEntityFrameworkStores<AppIdentityiDbContest>();

builder.Services.AddAuthentication("Bearer")
 .AddJwtBearer("Bearer", options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = "myapp",
         ValidAudience = "myapp",
         IssuerSigningKey = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
     };
 });

builder.Services.AddAuthorization();
builder.Services.AddCors(opt => opt.AddPolicy("AllowAll", b =>
{
    b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));

builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppIdentityiDbContest>();
    await context.Database.EnsureCreatedAsync().ConfigureAwait(true);

    SeedData.Initialize(context);
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapAuthEndpoints();

app.Run();