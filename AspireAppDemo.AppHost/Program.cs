var builder = DistributedApplication.CreateBuilder(args);

// Database 
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin();
var postgresdb = postgres.AddDatabase("postgresdb");


// Backend
var apiService = builder.AddProject<Projects.AspireAppDemo_ApiService>("apiservice")
    .WithHttpsHealthCheck("/health")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

// Frontend
// Blazor
builder.AddProject<Projects.AspireAppDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpsHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
