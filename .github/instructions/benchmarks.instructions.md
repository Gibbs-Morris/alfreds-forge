---
applyTo: '**'
---

# Benchmark Projects

Governing thought: Keep opt-in performance checks in dedicated `*.Benchmarks` console projects outside the default PR gates.

> Drift check: Read `Directory.Build.props` and `Directory.Packages.props` before wiring benchmarks. These files define the BenchmarkDotNet defaults.

## Rules (RFC 2119)

- Benchmark projects **MUST** use the name `<Product>.<Feature>.Benchmarks`. Why: Separates benchmarks from other projects.
- Benchmark projects **MUST NOT** end with `Tests`. Why: Prevents confusion with test projects.
- Benchmark projects **MUST** use `Microsoft.NET.Sdk` and `<OutputType>Exe</OutputType>`. Why: Enables BenchmarkDotNet execution.
- Benchmark projects **SHOULD** live under `benchmarks/`. They **SHOULD NOT** live under `tests/`. Why: Separates performance checks from correctness tests.
- Benchmarks **SHOULD** avoid non-deterministic input, including unseeded random values, wall-clock sleeps, and network calls. Why: Makes performance changes detectable.
- Benchmarks **SHOULD NOT** run in PR gates by default. Why: Performance signals can be unstable.
- BenchmarkDotNet packages **MUST** use Central Package Management. Package references **MUST NOT** declare `Version` attributes. Why: Prevents package-version drift.

## Scope and Audience

Use these rules when you add or run BenchmarkDotNet projects.

## Quick Start

Run one benchmark:

```powershell
dotnet run -c Release --project benchmarks/<Product>.<Feature>.Benchmarks/<Product>.<Feature>.Benchmarks.csproj
```

Run all benchmarks by convention:

```powershell
pwsh ./benchmarks.ps1
```

Pass BenchmarkDotNet arguments after `--`:

```powershell
pwsh ./benchmarks.ps1 -- --filter *Reducers*
```

## Core Principles

- Keep benchmarks isolated and deterministic.
- Keep benchmarks outside PR gates.
- Use Central Package Management and repository SDK defaults.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
