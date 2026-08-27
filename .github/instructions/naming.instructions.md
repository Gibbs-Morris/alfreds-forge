---
applyTo: '**/*.cs'
---

# C# Naming and XML Documentation

Governing thought: Use feature-centric namespaces, clear PascalCase identifiers, and factual XML documentation enforced by StyleCop.

> Drift check: Review `Directory.Build.props` and `.editorconfig` for StyleCop and analysis settings before editing.

## Rules (RFC 2119)

- Naming and StyleCop rules (SA13xx/SA16xx) **MUST** be build-breaking. Why: Keeps naming and documentation consistent.
- Contributors **MUST** fix naming and StyleCop violations. Why: Keeps builds compliant.
- Contributors **MUST NOT** suppress naming and StyleCop violations. Why: Prevents hidden defects.
- Namespaces **MUST** be feature-oriented, not `Services/Models` silos. Why: Improves discoverability.
- Namespaces **MUST** contain no more than ten PascalCase segments. Why: Keeps names readable.
- Namespaces **MUST NOT** contain underscores. Why: Keeps names consistent.
- Abbreviations **MUST** use industry-standard forms only. Why: Prevents ambiguous names.
- Types **MUST** use PascalCase nouns. Why: Aligns with .NET guidelines.
- Interfaces **MUST** prefix `I`. Why: Identifies contracts.
- Enums **MUST** be singular. Why: Aligns with .NET guidelines.
- Enum members **MUST** use PascalCase. Why: Aligns with .NET guidelines.
- Methods **MUST** use PascalCase verb phrases. Why: Identifies actions.
- Properties **MUST** use PascalCase nouns. Why: Identifies values.
- Boolean identifiers **MUST** start with `Is/Has/Can/Should`. Why: Identifies Boolean values.
- Injected dependencies **MUST** use get-only DI properties, such as `private Type Name { get; }`. Why: Supports consistent dependency injection.
- Private fields and locals **MUST** use camelCase. Why: Aligns with C# conventions.
- Private fields and locals **MUST NOT** use underscores. Why: Keeps identifiers consistent.
- Constants **MUST** use PascalCase. Why: Aligns with C# conventions.
- Public symbols and exposed internal symbols **MUST** have XML documentation. Why: Supports IntelliSense and documentation quality.
- XML documentation **MUST** include `<summary>` in the imperative voice. Why: Documents each symbol clearly.
- XML documentation **MUST** include `<param>`, `<typeparam>`, and `<returns>` tags when applicable. Why: Keeps documentation aligned with the API.
- Documentation text **MUST** be factual and contain no TODOs. Why: Prevents incomplete guidance.
- Abstract base classes **SHOULD NOT** use `Base` unless they support inheritance. Why: Avoids unnecessary suffixes.
- Orleans abstract grains ending in `Base` **MAY** follow Orleans guidance. Why: Preserves the Orleans exception.
- Documentation for private and internal members **SHOULD** exist only when behavior is non-trivial or exposed via `InternalsVisibleTo`. Why: Balances value and noise.

## Scope and Audience

All C# contributors.

## At-a-Glance Quick-Start

- Derive namespaces from company/product/feature. Keep <=10 segments.
- Use PascalCase nouns for types, properties, and constants.
- Prefix interfaces with `I`.
- Use camelCase for locals and fields. Do not use underscores.
- Prefix booleans with `Is/Has/Can/Should`.
- Keep injected dependencies in get-only DI properties.
- Write concise, factual XML docs for public APIs. Validate tags against parameters.

## Core Principles

- Feature-first organization improves discovery.
- Consistent naming reduces StyleCop churn and reviewer overhead.
- Documentation explains intent and usage without speculation.

## Domain Type Suffixes (Event Sourcing)

| Type | Suffix | Example |
|------|--------|---------|
| Aggregate state | `Aggregate` | `ChannelAggregate` |
| Command handler | `Handler` | `CreateChannelHandler` |
| Aggregate reducer | `Reducer` | `ChannelCreatedReducer` |
| Projection state | `Projection` | `UserProfileProjection` |
| Projection reducer | `ProjectionReducer` | `UserRegisteredProjectionReducer` |
| Registration class | `Registrations` | `ContosoRegistrations` |
| LoggerExtensions | `LoggerExtensions` | `BrookWriterGrainLoggerExtensions` |

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- C# standards: `.github/instructions/csharp.instructions.md`
- Domain modeling: `.github/instructions/domain-modeling.instructions.md`
- Deterministic placement: `.github/instructions/namespace-folder-placement.instructions.md`

