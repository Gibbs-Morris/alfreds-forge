using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Orleans;

using Projects;


IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);
OrleansService orleans = builder.AddOrleans("default")
    .WithDevelopmentClustering()
    .WithMemoryGrainStorage("PubSubStore");
IResourceBuilder<ProjectResource> runtime = builder.AddProject<Workbench_Runtime>("workbench-runtime")
    .WithReference(orleans)
    .WithHttpEndpoint(name: "http")
    .WithHttpHealthCheck("/health");
builder.AddProject<Workbench_Gateway>("workbench-gateway")
    .WithReference(orleans.AsClient())
    .WaitFor(runtime)
    .WithHttpEndpoint(name: "http")
    .WithExternalHttpEndpoints();
await builder.Build().RunAsync();