---
applyTo: '**/*.ps*'
---

# PowerShell Scripting

Governing thought: Run scripts with strict mode, explicit parameters, deterministic exit codes, and shared helpers to mirror our C# quality bar.

> Drift check: Review `eng/src/agent-scripts/RepositoryAutomation.psm1` and test scripts before changing patterns.

## Rules (RFC 2119)

- Scripts **MUST** use this opening sequence: `#!/usr/bin/env pwsh`, `Set-StrictMode -Version Latest`, then `$ErrorActionPreference='Stop'`. Why: These settings make scripts fail fast across platforms.
- Scripts **MUST NOT** relax these settings. Why: Relaxed settings can hide failures.
- Scripts **MUST** use explicit exit codes. Why: Explicit codes make automation and CI reliable.
- Scripts **MUST** use `exit 0` for success. Why: Automation can identify successful completion.
- Scripts **MUST** use a non-zero exit code for failure. Why: Automation can identify failed completion.
- Scripts **MUST NOT** rely on implicit success. Why: Implicit success can misreport automation results.
- Scripts **MUST NOT** introduce hidden global state. Why: Hidden state makes composition unpredictable.
- Helper functions **MUST** bubble errors. Why: Callers must receive failures.
- Helper functions **MUST NOT** swallow errors. Why: Swallowed errors hide failures.
- Parameters and outputs **SHOULD** be typed. Why: Types make contracts clear.
- Parameters and outputs **SHOULD** be validated. Why: Validation catches invalid input early.
- Scripts **SHOULD** use shared helpers from `RepositoryAutomation.psm1` instead of duplicating logic. Why: Shared helpers provide consistency and reuse.
- Scripts **SHOULD** use the cross-platform cmdlets `Join-Path`, `Resolve-Path`, and `Test-Path`. Why: These cmdlets improve portability.
- Scripts **SHOULD** return structured data when automation consumes results. Why: Structured data improves machine readability.

## Scope and Audience

Authors/reviewers of PowerShell scripts/modules in this repo.

## At-a-Glance Quick-Start

- Use this template sequence: shebang → `[CmdletBinding()]` + `param(...)` → strict mode → import helpers → try/catch with explicit exit.
- Validate parameters.
- Avoid implicit output.
- Keep module scope clean.
- Run `pwsh ./eng/tests/orchestrate-powershell-tests.ps1` to validate changes.

## Core Principles

- Fail fast.
- Be explicit.
- Stay cross-platform.
- Reuse shared automation instead of ad hoc scripts.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
