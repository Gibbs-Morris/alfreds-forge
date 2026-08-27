---
applyTo: '**'
---

# Shared Engineering Guardrails

Governing thought: Shared rules enforce zero warnings, central package management, clear dependency injection, and LoggerExtensions logging.

> Drift check: Open referenced scripts in `eng/src/` and configs in the repo root before using their commands or behavior. Treat them as authoritative.

## Rules (RFC 2119)

- **Zero Warnings Everywhere** - All projects and tests **MUST** build with zero compiler and analyzer warnings. Why: Keeps quality gates deterministic.
- Contributors **MUST NOT** relax rule severity. Why: Keeps quality gates deterministic.
- Contributors **MUST NOT** add project-wide `NoWarn`. Why: Keeps quality gates deterministic.
- Contributors **MUST NOT** use `#pragma` or `[SuppressMessage]` without explicit approval. Why: Keeps quality gates deterministic.
- **Central Package Management** - Package versions **MUST** live in `Directory.Packages.props`. Why: Prevents drift and NU10xx noise.
- `PackageReference` items **MUST NOT** declare `Version`. Why: Prevents drift and NU10xx noise.
- Package changes **MUST** use `dotnet add/remove package`. Why: Prevents drift and NU10xx noise.
- **DI Property Pattern** - Injected dependencies **MUST** use `private Type Name { get; }`. Why: Matches analyzers, logging, and testability patterns.
- Injected dependencies **MUST NOT** use underscored fields. Why: Matches analyzers, logging, and testability patterns.
- Constructors **SHOULD** be the only injection point. Why: Matches analyzers, logging, and testability patterns.
- **No Service Locator** - Classes **MUST NOT** inject `IServiceProvider` directly. Why: Service locators hide dependencies, complicate testing, and break static analysis.
- Classes **MUST** use explicit dependencies or `Lazy<T>` to break circular dependencies. Why: Makes dependencies clear and testable.
- Factory patterns that resolve services at runtime **MAY** be the only acceptable exception to the direct `IServiceProvider` rule. Why: Supports deferred resolution without general service-locator use.
- **LoggerExtensions Entry Point** - Logging **MUST** go through LoggerExtensions methods that use `[LoggerMessage]`. Why: Enforces the high-performance logging standard across the repo.
- Direct `ILogger.Log*` calls **MUST NOT** be introduced. Why: Enforces the high-performance logging standard across the repo.
- **No File-Level Copyright Banners** - Copyright or license headers or banners **MUST NOT** appear at the top of source, script, or markup files. Why: Repository-level licensing already applies.
- **Canonical Solutions Are .slnx** - `.slnx` files **MUST** be the source of truth. Why: Prevents drift between generated and canonical solutions.
- `.sln` files **MUST NOT** be hand-edited. Why: CI/automation regenerates them via SlnGen for legacy tooling such as ReSharper and Stryker.
- **Pre-1.0 Backwards Compatibility Freedom** - While `GitVersion.yml` `next-version` is below `1.0.0`, backwards compatibility **MUST NOT** constrain changes. Why: Prevents unnecessary complexity from compatibility code for patterns that do not exist in the merge target.
- Agents **MUST NOT** add compatibility shims for patterns that exist only on the current branch. Why: Prevents unnecessary complexity from compatibility code for patterns that do not exist in the merge target.
- Agents **MUST** compare the current branch with `main` before applying this rule. Why: Prevents unnecessary compatibility shims.
- Breaking changes **MUST** update all consumers, including samples and tests, in the same PR. Why: Keeps the repository consistent.
- See `.github/instructions/backwards-compatibility.instructions.md` for the full policy.

## Scope and Audience

These rules apply to all contributors and all files in this repository.
Individual instruction files add domain-specific rules and reference this file instead of duplicating these guardrails.

## At-a-Glance Quick-Start

- Build and test with zero warnings.
- Do not add `NoWarn` or file-wide pragmas.
- Add and remove packages with `dotnet add/remove package`.
- Keep package versions only in `Directory.Packages.props`.
- Keep injected services as get-only properties.
- Avoid underscored fields.
- Do not inject `IServiceProvider`.
- Use explicit dependencies or `Lazy<T>` for deferred resolution.
- Use LoggerExtensions source-generator methods for every log statement.

## Core Principles and Rationale

- Shared guardrails reduce duplication across instructions.
- Centralized package versions and DI/logging patterns keep the codebase analyzable and testable.
- Zero warnings keep CI deterministic and align with build/test gates.

## References

- `Directory.Build.props`, `Directory.Packages.props` for repo-wide MSBuild settings.
- `.github/instructions/logging-rules.instructions.md` for logging specifics.
