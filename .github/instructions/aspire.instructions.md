---
applyTo: '**/Aspire*/**/*.cs'
---

# Aspire Integration Testing

Governing thought: Use the preview Cosmos emulator in HTTP mode and apply the SDK workarounds for known connectivity issues.

> Drift check: Check [Aspire Cosmos issues](https://github.com/dotnet/aspire/issues?q=cosmos+emulator) and [Cosmos SDK issues](https://github.com/Azure/azure-cosmos-dotnet-v3/issues) before changing emulator settings.

## Rules (RFC 2119)

- Cosmos emulator configuration **MUST** call `RunAsPreviewEmulator()` and `WithoutHttpsCertificate()`. Why: The preview emulator exposes a reliable HTTP `/ready` endpoint. See [Aspire #7882](https://github.com/dotnet/aspire/issues/7882).
- Emulator clients **MUST** set `CosmosClientOptions.LimitToEndpoint = true`. Why: The SDK can hang while it discovers replicas on a single-node emulator. See [SDK #5364](https://github.com/Azure/azure-cosmos-dotnet-v3/issues/5364).
- Emulator clients **SHOULD** set `ConnectionMode.Gateway`. Why: Gateway mode is more reliable than Direct TCP for local emulators.
- Cosmos document models **MUST** use `[Newtonsoft.Json.JsonProperty("id")]`. They **MUST NOT** use `System.Text.Json` attributes for the `id` property. Why: Cosmos SDK v3 uses Newtonsoft.Json by default and ignores STJ attributes.
- Aspire test projects **SHOULD** use an `IAsyncLifetime` fixture to manage the AppHost lifecycle. Why: Ensures startup, teardown, and resource cleanup.

## Scope and Audience

Use these rules when you build Aspire integration tests with Azure emulators.

## Quick Start

Configure the AppHost:

```csharp
builder.AddAzureCosmosDB("cosmos")
    .RunAsPreviewEmulator(emulator =>
    {
        emulator.WithDataExplorer();
        emulator.WithoutHttpsCertificate();
    });
```

Configure the SDK client:

```csharp
CosmosClientOptions options = new()
{
    ConnectionMode = ConnectionMode.Gateway,
    LimitToEndpoint = true,
};
```

Define a document model:

```csharp
public class MyDocument
{
    [Newtonsoft.Json.JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
}
```

## Known Issues

| Issue | Symptom | Fix |
|---|---|---|
| [SDK #5364](https://github.com/Azure/azure-cosmos-dotnet-v3/issues/5364) | SDK hangs during connection | Set `LimitToEndpoint = true` |
| [Aspire #7882](https://github.com/dotnet/aspire/issues/7882) | Health check passes before the emulator is ready | Use the preview emulator |
| Newtonsoft.Json versus STJ | Document does not contain `id` | Use `Newtonsoft.Json.JsonProperty` |

## Core Principles

- Prefer the preview emulator to the legacy Linux emulator.
- Use HTTP mode to avoid certificate complexity.
- Set explicit single-endpoint mode in the SDK for emulator clients.

## References

- Sample implementation: `src/Aspire.L2Tests/`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Testing guidance: `.github/instructions/testing.instructions.md`
