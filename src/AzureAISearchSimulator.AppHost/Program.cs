var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.AzureAISearchSimulator_Api>("api");

builder.AddProject<Projects.AzureAISearchSimulator_Portal>("portal")
    .WithReference(api)
    .WithExternalHttpEndpoints();

builder.Build().Run();
