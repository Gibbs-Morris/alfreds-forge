# Alfred's Forge

Open-source core for Alfred's Forge: an AI engineering concierge that turns
structured delivery contracts into tested pull requests with evidence.

## Project layout

- `AlfredsForge.Client` is the standalone Blazor WebAssembly client.
- `AlfredsForge.Gateway` serves the client and is the future HTTP/API edge.
- `AlfredsForge.Runtime` is the future application runtime host.
- `AlfredsForge.AppHost` composes the application with .NET Aspire.

## Local development

Start the application through Aspire:

```powershell
dotnet run --project .\src\AlfredsForge.AppHost\AlfredsForge.AppHost.csproj
```

The Aspire dashboard starts the Gateway and Runtime resources. Open the
Gateway resource to use the client.

## Tests

Run the solution's unit tests:

```powershell
dotnet test .\alfreds-forge.slnx --configuration Release
```
