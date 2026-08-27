---
applyTo: '**'
---

# Build Rules and Quality Gates

Governing thought: Ship changes only after a clean build, cleanup, unit tests, and required mutation tests. Builds must have zero warnings.

> Drift check: Verify commands in `eng/src/agent-scripts/` or `./go.ps1` before use. Those scripts define the switches and order.

## Rules (RFC 2119)

- Builds **MUST** finish with zero compiler and analyzer warnings. Agents **MUST NOT** add `NoWarn`, lower severity, or add unapproved suppressions. Why: Zero warnings is a hard gate.
- Agents **MUST** run and pass the build, cleanup, and unit tests before they complete work. Agents **MUST** run required mutation tests. Why: The full quality pipeline prevents regressions.
- Agents **SHOULD** use targeted `dotnet build` and `dotnet test` commands during local iteration. They **MUST** run `pwsh ./clean-up.ps1` before completion. Why: Supports fast iteration without skipping the final cleanup.
- Agents **MUST NOT** add `[SuppressMessage]` or `#pragma warning disable` without explicit approval and a minimal scope. Why: Suppressions hide defects.
- Agents **MUST** keep StyleCop and ReSharper cleanup clean. Why: Keeps formatting consistent.
- Authors **MUST** edit solutions in `.slnx` form. Authors **MUST NOT** hand-edit `.sln` files. SlnGen regenerates `.sln` files during builds and cleanup. Why: Prevents solution drift.
- Code changes **MUST** add or update tests for the behavior they change. Why: Protects changed behavior.
- Package versions **MUST** stay in `Directory.Packages.props`. Project files **MUST NOT** add `Version` attributes. Why: Enforces Central Package Management.

## Scope and Audience

Use these rules for all contributors who change this repository.

## Quick Start

Build and clean:

```powershell
pwsh ./eng/src/agent-scripts/build-alfreds-forge-solution.ps1
pwsh ./eng/src/agent-scripts/clean-up-alfreds-forge-solution.ps1
```

Run unit tests:

```powershell
pwsh ./eng/src/agent-scripts/unit-test-alfreds-forge-solution.ps1
```

Run the final pipeline:

```powershell
pwsh ./go.ps1
```

## Core Principles

- Keep zero warnings. Fix code instead of suppressing warnings.
- Use `alfreds-forge.slnx` as the canonical solution.
- Use repository scripts for consistent parameters and cleanup.
- Add tests with behavior changes.

## Procedure

1. Build in Release. Fix all warnings.
2. Run cleanup. Fix all reported issues.
3. Add or update tests for changed behavior.
4. Run unit tests. Fix all failures.
5. Rerun build and tests when code changed.
6. Run `pwsh ./go.ps1` before handoff.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Testing and mutation details: `.github/instructions/testing.instructions.md`
