---
applyTo: '**'
---

# Legacy Test Improvement Loop

Governing thought: Improve legacy code by adding tests only. Keep production code unchanged unless explicit approval allows a change.

> Drift check: Confirm command flags in repository test scripts before use. Treat script behavior as authoritative.

## Rules (RFC 2119)

- Agents **MUST** keep work under `tests/` unless explicit approval allows changes to production code. Why: Assumes existing behavior is correct until tests prove otherwise.
- Agents **MUST** fix new warnings and errors immediately. Why: Zero warnings applies to tests.
- Test code **MUST NOT** add suppressions or `NoWarn`. Why: Zero warnings applies to tests.
- Changed code paths **MUST** aim for high coverage without regressions. Why: Protects quality while improving legacy areas.
- After the first clean build, agents **SHOULD** use focused project-level test loops. Why: Keeps iteration fast.
- After the first clean build, agents **MUST** still run a build with `-warnaserror`. Why: Keeps quality gates intact.

## Scope and Audience

Agents who improve tests in legacy or non-TDD areas.

## At-a-Glance Quick-Start

- Restore tools with `dotnet tool restore`.
- Use `dotnet test ./tests/<Name>/<Name>.csproj -c Release --no-build` as the fast loop.
- Run repository tests with `pwsh ./eng/src/agent-scripts/unit-test-alfreds-forge-solution.ps1`.
- Use project-scoped loops after the first build to speed up iteration.
- Still run `dotnet build ... -warnaserror`.

## Core Principles

- Keep tests deterministic and isolated.
- Keep test loops tight and quality gates intact.
- Use script output to track results instead of manual drift.

## References

- Canonical testing guidance: `.github/instructions/testing.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
