---
applyTo: '**/*.cs'
---

# Keyed Services for Storage Providers

Governing thought: Use keyed DI services so one host can use multiple Cosmos, Blob, Redis, or other storage instances for different purposes.

> Drift check: Review module-owned `*Defaults` types and Aspire registration patterns before adding new keyed services.

## Rules (RFC 2119)

- Library code consuming cloud clients (BlobServiceClient, CosmosClient, Container, etc.) **MUST** put `[FromKeyedServices(<ModuleDefaults>.XxxServiceKey)]` on constructor parameters. It **MUST NOT** expect an unkeyed registration. Why: Enterprise apps require multiple storage accounts for locking, state, uploads, and archival.
- Service keys **MUST** belong to the package that defines the storage contract. Examples include `BrookCosmosDefaults` and `SnapshotCosmosDefaults`. Service keys **MUST NOT** use a cross-module defaults hub. Why: Keeps ownership explicit and avoids accidental coupling.
- Key constants **MUST** use `"Alfred's Forge-{client-type}-{feature}"`. Examples include `"Alfred's Forge-cosmos-brooks"` and `"Alfred's Forge-blob-locking"`. Why: Provides unique, discoverable identifiers.
- Registration documentation **MUST** comment which keyed services the library expects callers to provide. Why: Clarifies the DI contract.
- Host applications **MUST** forward from their registration key (e.g., Aspire's `"cosmos"`, `"blobs"`) to the library's expected key using `AddKeyedSingleton`. Why: Decouples host naming from library requirements.
- If a host needs both a keyed service for the library and an unkeyed service for its own services, it **MUST** explicitly forward with `AddSingleton(sp => sp.GetRequiredKeyedService<T>("key"))`. Why: Makes DI resolution explicit.

## Scope and Audience

Library authors and host developers integrating Alfred's Forge with cloud storage or external services.

## At-a-Glance Quick-Start

### Library Side

```csharp
// Use module-owned keys from Brooks storage defaults
public BlobDistributedLockManager(
    [FromKeyedServices(BrookCosmosDefaults.BlobLockingServiceKey)]
    BlobServiceClient blobServiceClient,
    ILogger<BlobDistributedLockManager> logger) { }

// Document in registration comments
// Caller must register a keyed BlobServiceClient with BrookCosmosDefaults.BlobLockingServiceKey
services.AddSingleton<IDistributedLockManager, BlobDistributedLockManager>();
```

### Host Side (Aspire)

```csharp
// Register with Aspire key
builder.AddKeyedAzureBlobServiceClient("blobs");

// Forward to library key
builder.Services.AddKeyedSingleton(
    BrookCosmosDefaults.BlobLockingServiceKey,
    (sp, _) => sp.GetRequiredKeyedService<BlobServiceClient>("blobs"));

// If host also needs unkeyed for its own services
builder.Services.AddSingleton(sp => sp.GetRequiredKeyedService<BlobServiceClient>("blobs"));
```

## Core Principles

- Keyed services let one client type have many instances.
- Define keys with module ownership beside the consuming storage provider.
- Treat library keys as stable contracts and host keys as deployment-specific.
- Use explicit forwarding to keep the DI graph auditable.

## Module-Owned Key Reference

| Key | Value | Purpose |
|-----|-------|---------|
| `BrookCosmosDefaults.CosmosContainerServiceKey` | `"Alfred's Forge-cosmos-brooks"` | Cosmos container for event streams |
| `SnapshotCosmosDefaults.CosmosContainerServiceKey` | `"Alfred's Forge-cosmos-snapshots"` | Cosmos container for snapshots |
| `BrookCosmosDefaults.BlobLockingServiceKey` | `"Alfred's Forge-blob-locking"` | Blob storage for distributed locking |

See also module-owned storage/container defaults (for example `BrookCosmosDefaults.ContainerId`, `SnapshotCosmosDefaults.ContainerId`).

## References

- Service registration: `.github/instructions/service-registration.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
