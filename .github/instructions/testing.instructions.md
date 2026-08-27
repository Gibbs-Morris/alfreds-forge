---
applyTo: '**'
---

# Testing, Coverage, and Mutation Strategy

Governing thought: Default to fast, deterministic L0 tests with high coverage and follow the repository's current scripts and gates.

> Drift check: Confirm script parameters in `eng/src/agent-scripts/` or `./go.ps1` before running tests. Treat the scripts as authoritative for order and options.

## Rules (RFC 2119)

- Test projects **MUST** follow level naming (`<Feature>.L0Tests`…`<Feature>.L4Tests`). Legacy `*.Tests` projects **MUST** migrate when touched. Why: Keeps analyzers and InternalsVisibleTo aligned.
- New tests **MUST** default to L0. L1 **SHOULD** be used only when light infra is required. Why: Keeps feedback fast and deterministic.
- When L0 cannot cover a behavior, authors **SHOULD** attempt L1 before moving to L2. Why: Preserves fast feedback and limits infra reliance.
- L2 tests **SHOULD** be used only when real infrastructure is required (HTTP APIs, SignalR, Cosmos/Blob storage, etc.). Why: Keeps lower levels pure and deterministic.
- Each implementation solution **SHOULD** include separate L0Tests, L1Tests, and L2Tests projects. Why: Keeps scopes clear and enables targeted pipelines.
- Each L2 test project **SHOULD** have a companion Aspire AppHost project that provisions required dependencies and emulators. Why: Makes integration tests repeatable and self-contained.
- Tests **MUST** be deterministic and isolated. Tests **MUST NOT** use sleeps or shared mutable state. L0 tests **MUST NOT** use real network. Tests **MUST** use `FakeTimeProvider` from `Microsoft.Extensions.TimeProvider.Testing` when production code injects `TimeProvider`. Random seeds **SHOULD** be fixed or injected. Why: Prevents flakiness and enables reproducible assertions.
- Changed code **MUST** aim for 100% coverage. It **MUST NOT** regress coverage on touched files. Solution-wide coverage **MUST** stay >=80%. The solution **SHOULD** target 95-100% coverage where feasible. Why: Protects behavior and gates.
- If mutation tooling is added for this repository, agents **MUST** run `dotnet tool restore` and a clean build before mutation tests. Why: Keeps future mutation runs valid and repeatable.
- Mutation work **MUST NOT** change production code solely to kill mutants unless tests prove a mutant unkillable. Authors **MUST** justify any such change. Why: Preserves intended behavior.
- Test code **MUST** honor the zero-warnings policy. It **MUST NOT** use suppressions, `#pragma`, or `NoWarn`. Why: Test quality equals production quality.
- Legacy improvement tasks **MUST NOT** edit production code without approval. They **MUST** keep work inside `tests/`. Warnings and failures **MUST** be fixed immediately. Why: Assumes existing behavior is correct until tests prove otherwise.
- Package references in tests **MUST** follow Central Package Management. They **MUST NOT** include `Version` attributes. Why: Prevents drift and NU10xx noise.

## Scope and Audience

Applies to all test authors in this repository.

## At-a-Glance Quick-Start

- Restore tools once: `dotnet tool restore`
- Build only during iteration: `dotnet build ./tests/<Name>/<Name>.csproj -c Release -warnaserror`
- Run repository unit tests: `pwsh ./eng/src/agent-scripts/unit-test-alfreds-forge-solution.ps1`
- Run the full local pipeline: `pwsh ./go.ps1`

## Core Principles

- Prefer L0 (pure, in-memory) for speed. Step to L1, then L2, when needed. Use real infra via Aspire for L2.
- Put determinism first. Use `FakeTimeProvider` for time. Fix random seeds. Isolate the file system and ports. Avoid sleeps.
- Keep coverage high on changed code. Prevent regressions.
- Use repository scripts as the source of truth for current test execution behavior.

## Test Levels Snapshot

| Level | Scope | Dependencies | Typical Run |
| ----- | ----- | ------------ | ----------- |
| L0 | Pure unit, no IO | In-memory only | Always (PR/local) |
| L1 | Light infra | Temp FS, in-proc DB/mocks | Often (PR/local) |
| L2 | Functional vs test deployment | Aspire AppHost + emulators/services | Scheduled/on-demand |
| L3 | End-to-end/prod-like UI/API | Full stack, Playwright | Release/controlled |
| L4 | Synthetic prod checks | Live endpoints (read-only) | Post-deploy/monitoring |

## Workflows

### Baseline and Coverage

1. Run project-level tests for the target test project. For example, run `dotnet test ./tests/<Name>/<Name>.csproj -c Release --no-build`.
2. Add tests for behavior, edges, and branches. Keep them deterministic.
3. If coverage < target, inspect Cobertura output under `.scratchpad/coverage-test-results/<Project>/`.

### Legacy Test Improvements

1. Work only under `tests/` unless explicitly approved to change production code.
2. Keep loops tight with direct `dotnet test` or project-level runs. Still run a build with `-warnaserror`.
3. Fix warnings immediately and maintain coverage targets.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Build rules: `.github/instructions/build-rules.instructions.md`
