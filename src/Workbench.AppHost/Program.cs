using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Orleans;

using Projects;


IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

OrleansService orleans = builder.AddOrleans("default")
    .WithMemoryGrainStorage("PubSubStore")
    .WithMemoryStreaming("StreamProvider");

IResourceBuilder<ProjectResource> runtime = builder.AddProject<Workbench_Runtime>("workbench-runtime")
    .WithReference(orleans)
    .WithHttpHealthCheck("/health");

builder.AddProject<Workbench_Gateway>("workbench-gateway")
    .WithReference(orleans.AsClient())
    .WaitFor(runtime)
    .WithExternalHttpEndpoints();

await builder.Build().RunAsync();
