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

// Angular
// C:\Users\alain\source\repos\alainranger\AspireAppDemo\AspireAppDemo.Angular\AspireAppDemo.Angular.esproj
builder.AddNpmApp("frontend-angular", "../AspireAppDemo.Angular")
	.WithReference(apiService)
	.WaitFor(apiService)
	.WithHttpEndpoint(env: "PORT")
	.WithExternalHttpEndpoints()
	.PublishAsDockerFile();


// Blazor
builder.AddProject<Projects.AspireAppDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpsHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
