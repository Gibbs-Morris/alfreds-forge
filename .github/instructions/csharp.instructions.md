---
applyTo: '**/*.cs'
---

# C# Development Standards

Governing thought: Write SOLID, testable, cloud-ready C# with internal-by-default access, options and DI patterns, and Orleans-safe code.

> Drift check: Check `Directory.Build.props`, `Directory.Packages.props`, and referenced scripts before editing. They define analyzers, Central Package Management, and defaults.

## Rules (RFC 2119)

- C# code **MUST** follow SOLID and provide clear DI or test seams. Code **MUST NOT** add blocking calls or shared mutable state. Why: Keeps code maintainable and testable.
- .NET analyzers **MUST** remain enabled. Warnings **MUST** be treated as errors. Authors **MUST NOT** add suppressions or `#pragma` without approval. Why: Enforces zero warnings.
- XML documentation **MUST NOT** contain `<example>` or `<code>` blocks. Refer readers to repository implementations for examples. Why: Prevents stale samples.
- Injected dependencies **MUST** use get-only properties such as `private Type Name { get; }`. Field injection and underscored fields **MUST NOT** be used. Why: Aligns with logging and analyzer rules.
- Source files **MUST NOT** start with copyright or license headers. Why: Repository licensing already applies.
- Configuration **MUST** use `IOptions<T>`, `IOptionsSnapshot<T>`, or `IOptionsMonitor<T>`. Constructors **MUST NOT** receive raw configuration primitives. Why: Centralizes configuration and validation.
- Nested classes **SHOULD NOT** be used except for private implementation details. Test helpers and public or internal types **MUST** be top-level or use their own files. Why: Nested classes reduce discoverability and can block NSubstitute or Castle.DynamicProxy mocking.
- Types **MUST** default to `internal`. Public, protected, and unsealed types **MUST** document their justification in XML comments. Implementation types **MUST** remain internal unless they form part of the public API. Why: Protects the API surface.
- New or changed APIs **MUST** follow established .NET BCL and runtime conventions, including `Try*` and `Parse`, `Async` suffixes, and `CancellationToken` for cancelable async work. Ambiguous APIs **MUST** match widely used .NET APIs. Why: Improves discoverability and reduces context switching.
- Orleans grain APIs **MUST** follow `.github/instructions/orleans.instructions.md` and common Orleans conventions. They **SHOULD** return `Task` or `Task<T>` and **MUST NOT** block synchronously. Why: Keeps grain APIs async-first.
- Grain implementations **MUST** implement `IGrainBase` and be `sealed`. They **MUST NOT** inherit from `Grain`. Grain interfaces **MUST** be public only when external callers need them. Why: Follows Orleans 7+ POCO guidance.
- Orleans code **MUST NOT** use `Parallel.ForEach` or chatty inter-grain calls. Prefer async operations with `Task.WhenAll`. Why: Preserves the Orleans threading model.
- `*Registrations` options and registration classes **MAY** be public when they form part of the consumer surface. Otherwise they **SHOULD** remain internal. Why: Keeps public APIs intentional.
- Public contracts **SHOULD** live in `.Abstractions` projects. Implementations **MUST** stay in main projects. Why: Preserves clean layering.
- Classes **SHOULD** use records or immutable state when feasible. Types **SHOULD** be inheritable only when a clear need exists. Interfaces **SHOULD** be public only as deliberate APIs. Members **SHOULD** expose the least privilege. Why: Reduces coupling and state-related defects.
- New third-party dependencies **MAY** be added only with explicit approval or to extend technology the repository already adopts. Why: Limits supply-chain risk and dependency sprawl.
- Logging **SHOULD** use LoggerExtensions as specified by the logging rules. Why: Maintains performance and consistency.
- Code that needs current time **MUST** inject `TimeProvider`. It **MUST NOT** call `DateTime.Now`, `DateTime.UtcNow`, or `DateTimeOffset.UtcNow` directly. Tests **SHOULD** use `Microsoft.Extensions.TimeProvider.Testing.FakeTimeProvider`. Why: Enables deterministic time tests.

## Scope and Audience

Use these rules for C# contributors across Alfred's Forge and Samples, including Orleans code.

## Quick Start

- Default visibility to `internal`. Document any wider visibility in XML.
- Use DI properties and the options pattern. Do not pass raw configuration values to constructors.
- Inject `TimeProvider`. Use `FakeTimeProvider` in tests.
- Avoid blocking calls and parallel loops in Orleans. Use async operations and `Task.WhenAll`.
- Put public contracts in `.Abstractions`. Keep implementations internal.
- Keep analyzers enabled. Fix warnings instead of suppressing them.

## Core Principles

- SOLID and DI seams support testing and refactoring.
- Internal-by-default APIs reduce unintended breaking changes. Before version 1.0, intentional breaks remain allowed by `.github/instructions/backwards-compatibility.instructions.md`.
- Orleans POCO types and async APIs avoid threading problems.
- Immutable and value-object designs improve correctness, logging, and serialization.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Naming and docs: `.github/instructions/naming.instructions.md`
- Orleans specifics: `.github/instructions/orleans.instructions.md`
- Service registration: `.github/instructions/service-registration.instructions.md`
- Logging: `.github/instructions/logging-rules.instructions.md`
