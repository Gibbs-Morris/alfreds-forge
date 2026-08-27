---
applyTo: '**'
---

# Project File Management

Governing thought: Keep `.csproj` files minimal and CPM-driven, and avoid
duplicate settings from `Directory.Build.props`.

> Drift check: Open `Directory.Build.props` and `Directory.Packages.props`
> before editing a project file. These files define defaults and versions.

## Rules (RFC 2119)

- Project files **MUST** inherit shared settings from
  `Directory.Build.props`. Why: Keeps one source of truth.
- Project files **MUST NOT** add duplicate properties without
  justification. Why: Keeps one source of truth.
- Package versions **MUST** stay in `Directory.Packages.props`. Why:
  Enforces Central Package Management.
- `PackageReference` items **MUST NOT** include `Version`. Why: Enforces
  Central Package Management.
- Project files **MUST** stay minimal. Why: Reduces drift and review noise.
- Each `.csproj` **MUST** contain only project-specific properties and
  items. Why: Reduces drift and review noise.
- Contributors **MUST NOT** override automatic assembly or root namespace
  naming without explicit justification. Why: Preserves naming consistency.
- Contributors **MUST** validate project changes with a clean build that
  produces zero warnings. Why: Aligns with repository quality gates.

### Project Naming

- Projects under `src/` follow a consistent naming pattern.
- Project names **MUST** use PascalCase dot-separated segments in the form
  `<Feature>.<Role>`. Why: Keeps project identities predictable.
- Role values **MUST** be one of:
  `Abstractions`, `Core`, `Client`, `Gateway`, `Runtime`, `TestHarness`.
  Why: Keeps project roles consistent.
- Contributors **MUST** prefer role names over technology names. Examples
  include `Gateway` over `Api`, `Client` over `Blazor`, and `Runtime` over
  `Grains`. Why: Keeps names aligned with architecture.
- Packages **MUST** address one concern. Why: Keeps packages focused.
- Client, gateway, and runtime logic **MUST NOT** be mixed. Why: Preserves
  package boundaries.
- Feature stems **MUST** stay consistent across related roles. For example,
  `Brooks.Abstractions` + `Brooks.Runtime` share one feature stem. Why: Keeps
  related packages discoverable.
- Test support packages **MUST** use the `TestHarness` suffix. Why:
  Distinguishes test support packages.
- Storage provider packages **SHOULD** use
  `<Feature>.Runtime.Storage.<Provider>`. Why: Keeps provider names
  consistent.
- Serialization provider packages **SHOULD** use
  `<Feature>.Serialization.<Format>`. Why: Keeps provider names consistent.

`Directory.Build.props` uses `$(MSBuildProjectName)` for package and assembly
identity. Folder and project names therefore reflect the intended final
identity.

### Role Detection (Validation)

- `Microsoft.NET.Sdk.Razor` has expected role `.Client`.
- `<FrameworkReference>` to `Microsoft.AspNetCore.App` has expected role
  `.Gateway` (takes precedence).
- `Microsoft.Orleans.Sdk` (without AspNet) has expected role `.Runtime` or
  `.Abstractions`.

When both references are present, `AspNet` takes precedence over `Orleans`.

### Three-Layer Architecture (EventSourcing)

```text
DomainModeling (Layer 3 - aggregates, sagas, UX projections)
  ├─→ Tributary (Layer 2 - reducers, snapshots)
  │     └─→ Brooks (Layer 1 - event streams, serialization)
  └─→ Brooks (Layer 1)
```

Layers depend strictly downward. The architecture has no upward or lateral
violations.

## Scope and Audience

The audience is anyone who creates or modifies `.csproj` files.

## At-a-Glance Quick-Start

- Use `<PackageReference Include="X" />` with no version.
- Add or remove packages with `dotnet add/remove package`.
- Keep only project-specific properties, such as `OutputType`,
  `GeneratePackageOnBuild`, etc.
- Run `pwsh ./eng/src/agent-scripts/build-alfreds-forge-solution.ps1` or
  `pwsh ./build.ps1` to validate a build.
- Run `pwsh ./eng/src/agent-scripts/final-build-solutions.ps1` for a strict
  final build.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Project naming rules: `.github/instructions/naming.instructions.md`
