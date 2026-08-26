---
applyTo: '**'
---

# Legacy Test Improvement Loop

Governing thought: Raise coverage on legacy code by adding tests only, keeping production code untouched unless explicitly approved.

> Drift check: Confirm command flags in repository test scripts before use; script behavior is authoritative.

## Rules (RFC 2119)

- Work **MUST** stay under `tests/` unless explicit approval is given to change production code. Why: Assumes existing behavior is correct until tests prove otherwise.
- New warnings/errors **MUST** be fixed immediately; test code **MUST NOT** add suppressions or `NoWarn`. Why: Zero-warnings applies to tests.
- Changed code paths **MUST** aim for high coverage with no regressions. Why: Protects quality while improving legacy areas.
- After the first clean build, agents **SHOULD** use focused project-level test loops but **MUST** still run a build with `-warnaserror`. Why: Keeps iteration fast without skipping gates.

## Scope and Audience

Agents improving tests on legacy/non-TDD areas.

## At-a-Glance Quick-Start

- Restore tools: `dotnet tool restore`
- Fast loop: `dotnet test ./tests/<Name>/<Name>.csproj -c Release --no-build`
- Repository tests: `pwsh ./eng/src/agent-scripts/unit-test-alfreds-forge-solution.ps1`
- Speed up after first build: keep project-scoped loops; still run `dotnet build ... -warnaserror`

## Core Principles

- Tests-only edits; deterministic, isolated tests.
- Tight loops with quality gates intact.
- Use script outputs for deterministic tracking rather than manual drift.

## References

- Canonical testing guidance: `.github/instructions/testing.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`

