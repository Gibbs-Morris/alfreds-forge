using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

using Orleans.Runtime;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.UseOrleans();
WebApplication app = builder.Build();
app.MapGet(
    "/health",
    (
        ISiloStatusOracle siloStatus
    ) =>
    {
        SiloStatus status = siloStatus.CurrentStatus;
        return status == SiloStatus.Active
            ? Results.Ok(
                new
                {
                    Status = "Healthy",
                    Service = "Workbench.Runtime",
                    Orleans = status.ToString(),
                })
            : Results.StatusCode(503);
    });
await app.RunAsync();