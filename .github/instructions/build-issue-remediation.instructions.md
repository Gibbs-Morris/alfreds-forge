---
applyTo: '**'
---

# Build Issue Remediation Protocol

Governing thought: Fix each warning or error with the smallest safe edit. Try one issue no more than five times before you defer it with context.

> Drift check: Read the build, cleanup, and test scripts in `eng/src/agent-scripts/` before running them. Those scripts define the switches and order.

## Rules (RFC 2119)

- Each warning or error at one location **MUST** receive no more than five focused fix attempts. Why: Prevents thrashing.
- When an issue remains after five attempts, agents **MUST** leave the code compiling and consistent. Why: Keeps the tree usable.
- When agents defer an issue, they **MUST** create or update `.scratchpad/tasks/...` with `status=deferred`. Why: Records the blocker for later work.
- Agents **MUST NOT** broaden the issue scope. They **MUST** change only the required lines. Large refactors **MUST NOT** be part of remediation. Why: Limits regression risk.
- Agents **MUST NOT** relax analyzers, add `NoWarn`, edit generated code, or add `[SuppressMessage]` or `#pragma` without explicit approval. Why: Zero-warning policy is mandatory.
- Project files **MUST NOT** add package versions. Package changes **MUST** use Central Package Management. Why: Prevents NU10xx errors and version drift.
- Agents **MUST** follow `.editorconfig` and `Directory.Build.props`. Why: Keeps formatting and settings consistent.

## Scope and Audience

Use these rules when you fix build, analyzer, or style issues in this repository.

## Quick Start

1. Run the relevant build and cleanup scripts. Record warning and error codes.
2. Choose one issue and one file.
3. Apply the smallest safe fix. Prefer code changes over suppressions.
4. Rerun build and cleanup.
5. Stop after five attempts for one issue. If it still fails, record its code, path, reason, and next step in a deferred scratchpad task.

## Core Principles

- Prefer precision over breadth. Do not reformat unrelated code.
- Count one attempt for each edit and verification cycle.
- Apply the shared zero-warning, Central Package Management, DI, and logging guardrails.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Quality gates: `.github/instructions/build-rules.instructions.md`
