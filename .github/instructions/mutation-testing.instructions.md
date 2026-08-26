---
applyTo: '**'
---

# Mutation Testing Playbook

Governing thought: Mutation testing is optional until repository mutation tooling is added; when enabled, run it after a clean build and close survivors through targeted tests without altering production behavior.

> Drift check: Verify mutation scripts exist under `eng/src/agent-scripts/` before use; if they do not exist, treat this document as policy guidance only.

## Rules (RFC 2119)

- Agents **MUST** run `dotnet tool restore` and a clean build before mutation tests. Why: Prevents invalid runs.
- Mutation scripts, when present, **MUST** be allowed to finish (plan for long runs); cancelling early **MUST NOT** happen. Why: Scores are invalid otherwise.
- Mutation reports/paths, when generated, **MUST** be recorded from script output. Why: Provides traceability.
- Production code **MUST NOT** be changed solely to kill mutants unless the mutant is provably unkillable via tests; any such change **MUST** be justified. Why: Protects intended behavior.
- Build warnings/test failures **MUST** be fixed before continuing mutation work. Why: Keeps gates stable.
- Survivors **SHOULD** be prioritized by score/impact and addressed with targeted tests. Why: Maximizes value per run.

## Scope and Audience

Mutation testing for this repository when mutation tooling is available.

## At-a-Glance Quick-Start

- Baseline run (when available): execute the repository mutation test script under `eng/src/agent-scripts/`.
- Iterate: use generated mutation report artifacts to prioritize high-impact survivors.
- If mutation tooling is unavailable, use coverage and targeted test strengthening as the interim quality mechanism.

## Core Principles

- Let mutation runs finish when executed.
- Kill mutants with tests, not behavior changes.
- Keep reports and scratchpad tasks in sync.

## References

- Canonical testing guidance: `.github/instructions/testing.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`

