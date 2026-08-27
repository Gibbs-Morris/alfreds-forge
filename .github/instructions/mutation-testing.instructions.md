---
applyTo: '**'
---

# Mutation Testing Playbook

Governing thought: Mutation testing is optional until repository tooling exists. When enabled, run it after a clean build and close survivors with targeted tests without changing production behavior.

> Drift check: Verify mutation scripts under `eng/src/agent-scripts/` before use. If none exist, treat this document as policy guidance only.

## Rules (RFC 2119)

- Agents **MUST** run `dotnet tool restore` and a clean build before mutation tests. Why: Prevents invalid runs.
- Mutation scripts, when present, **MUST** be allowed to finish. Why: Scores are invalid otherwise.
- Agents **MUST** plan for long mutation runs. Why: Scores are invalid otherwise.
- Agents **MUST NOT** cancel mutation scripts early. Why: Scores are invalid otherwise.
- Agents **MUST** record generated mutation reports/paths from script output. Why: Provides traceability.
- Production code **MUST NOT** be changed solely to kill mutants unless the mutant is provably unkillable via tests. Why: Protects intended behavior.
- Authors **MUST** justify any production-code change that addresses a provably unkillable mutant. Why: Protects intended behavior.
- Build warnings/test failures **MUST** be fixed before continuing mutation work. Why: Keeps gates stable.
- Survivors **SHOULD** be prioritized by score/impact. Why: Maximizes value per run.
- Survivors **SHOULD** be addressed with targeted tests. Why: Maximizes value per run.

## Scope and Audience

Use these rules for mutation testing in this repository when tooling is available.

## At-a-Glance Quick-Start

- When available, execute the repository mutation test script under `eng/src/agent-scripts/` for a baseline run.
- Use generated mutation report artifacts to prioritize high-impact survivors.
- If mutation tooling is unavailable, use coverage and targeted test strengthening as the interim quality mechanism.

## Core Principles

- Let mutation runs finish when executed.
- Kill mutants with tests, not behavior changes.
- Keep reports and scratchpad tasks in sync.

## References

- Canonical testing guidance: `.github/instructions/testing.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
