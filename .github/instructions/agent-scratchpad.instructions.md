---
applyTo: '**'
---

# Agent Scratchpad

Governing thought: Use `.scratchpad/` as an untracked workspace for task handoff. Never store secrets there or couple code to it.

> Drift check: Verify the current `.scratchpad` layout and task files before changing these rules.

## Rules (RFC 2119)

- Secrets and PII **MUST NOT** live in `.scratchpad/`. Why: The scratchpad is ephemeral and ignored by Git.
- Source and test code **MUST NOT** reference `.scratchpad/` paths. Why: Scratchpad content is not part of the product.
- Agents **MUST** claim tasks by atomically moving files from `tasks/pending` to `tasks/claimed`. Why: Prevents races.
- Only the task owner **MUST** edit a claimed task file. Why: Prevents conflicting edits.
- Task timestamps **MUST** use UTC ISO-8601. Why: Gives all agents one time standard.
- Each task file **MUST** use `<yyyyMMddHHmmss>_<slug>_<ulid>.json`. Why: Makes task files predictable.
- Task producers **SHOULD** split work into tasks of about 15 minutes. Why: Keeps tasks easy to claim and finish.
- Task workers **SHOULD** process tasks by priority and then FIFO order. Why: Gives selection a deterministic order.
- Attempts for one task **MUST** stop at five. After five attempts, move the task to `tasks/deferred/` with context. Why: Prevents thrashing.
- Files **SHOULD** remain small text files. Large binaries **SHOULD NOT** be stored. Why: Keeps the scratchpad easy to manage.
- Agents **MAY** prune old runs and completed tasks at any time. Why: Scratchpad data is disposable.

## Scope and Audience

Use these rules for all agents that coordinate work locally.

## Quick Start

- Claim a task by moving its JSON file from `tasks/pending` to `tasks/claimed`. Add `claimedBy`, `claimedAt`, and `attempts`.
- Complete a task by updating its status and result, then moving it to `tasks/done`.
- After five attempts, defer the task with the reason and next steps.
- Use `<yyyyMMddHHmmss>_<slug>_<ulid>.json` for names. Keep JSON keys stable and timestamps in UTC.

## Core Principles

- One file represents one task.
- The containing folder represents task ownership.
- Atomic moves prevent conflicts.
- Do not share mutable task files.
- Keep scratchpad data out of builds and product code.

## References

- Build remediation attempt cap: `.github/instructions/build-issue-remediation.instructions.md`
