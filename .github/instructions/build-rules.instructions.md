---
applyTo: '**'
---

# Build Rules and Quality Gates

Governing thought: Every change ships only after a clean build, cleanup, tests, and mutation tests (where required) with zero warnings.

> Drift check: Verify commands in `eng/src/agent-scripts/` (or `./go.ps1`) before use; scripts are the source of truth for switches and order.

## Rules (RFC 2119)

- Builds **MUST** finish with zero compiler/analyzer warnings; agents **MUST NOT** add `NoWarn`, relax severity, or suppress rules without explicit approval. Why: Zero-warnings is a hard gate.
- Agents **MUST** run and pass build, cleanup, and unit tests before calling work complete. Why: Full quality pipeline prevents regressions.
- For local iteration, agents **SHOULD** run targeted `dotnet build`/`dotnet test` commands against changed projects for faster feedback, but full `pwsh ./clean-up.ps1` **MUST** still pass before completion. Why: Faster inner loop without weakening gates.
- Agents **MUST NOT** add `[SuppressMessage]` or `#pragma warning disable` except for explicitly approved, minimal scopes. Why: Suppressions hide defects.
- Agents **MUST** keep StyleCop/ReSharper cleanup clean. Why: Consistent formatting enables readable diffs.
- Solution files **MUST** be edited in `.slnx` form only; `.sln` files **MUST NOT** be hand-edited because automation regenerates them with SlnGen during builds/cleanup for legacy tooling compatibility. Why: Prevents drift between canonical and generated solutions.
- Code changes **MUST** add or update tests appropriate to the behavior touched. Why: Maintains confidence in changed behavior.
- Package versions **MUST** remain in `Directory.Packages.props`; project files **MUST NOT** add `Version` attributes. Why: Central Package Management avoids drift.

## Scope and Audience

All contributors changing this repository.

## At-a-Glance Quick-Start

- Build and cleanup:  
  `pwsh ./eng/src/agent-scripts/build-alfreds-forge-solution.ps1`  
  `pwsh ./eng/src/agent-scripts/clean-up-alfreds-forge-solution.ps1`
- Tests: `pwsh ./eng/src/agent-scripts/unit-test-alfreds-forge-solution.ps1`
- Final gate: `pwsh ./go.ps1`

## Core Principles

- Zero warnings always; fix code rather than suppressing.
- One canonical solution (`alfreds-forge.slnx`) with strict build/test/cleanup gates.
- Use repository scripts for consistent parameters and cleanup.
- Tests accompany behavior changes.

## Procedures

1. Build in Release and fix warnings until clean.
2. Run cleanup script; resolve any reported issues.
3. Add/update tests for changed behavior.
4. Run unit tests and address failures.
5. Re-run build/tests if code changed; finish with `pwsh ./go.ps1` before handoff.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Testing/mutation details: `.github/instructions/testing.instructions.md`

