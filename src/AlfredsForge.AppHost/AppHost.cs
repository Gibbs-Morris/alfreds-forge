using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);
IResourceBuilder<ProjectResource> runtime = builder.AddProject<AlfredsForge_Runtime>("alfreds-forge-runtime")
    .WithHttpHealthCheck("/health");

_ = builder.AddProject<AlfredsForge_Gateway>("alfreds-forge-gateway")
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints()
    .WaitFor(runtime);

await builder.Build().RunAsync();
