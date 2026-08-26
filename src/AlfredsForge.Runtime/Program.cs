using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();

WebApplication app = builder.Build();
app.MapHealthChecks("/health");

await app.RunAsync();
