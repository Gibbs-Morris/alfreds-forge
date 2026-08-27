---
applyTo: '**'
---

# Pull Request Review Guide

Governing thought: Run focused reviews that enforce
small, single-responsibility PRs, proven tests, and build results.

> Drift check: Open the scripts under
> `eng/src/agent-scripts/` before referencing build/test commands. Treat those
> scripts as authoritative.

## Rules (RFC 2119)

- Reviews **MUST** pause when diffs exceed ~600 changed lines.
  Why: Smaller diffs stay reviewable.
- Reviewers **SHOULD** request splits before continuing.
  Why: Smaller diffs stay reviewable.
- Reviews **MUST** fail when new code paths lack L0 tests.
  Why: Behavior changes require tests.
- Reviewers **MUST** verify that the author ran
  `pwsh ./go.ps1` or targeted quality scripts before approval.
  Why: Quality gates must pass.
- Pull requests **MUST** follow single-responsibility.
  Why: Focused pull requests reduce risk.
- Mixed concerns **MUST** be split.
  Why: Separate pull requests prevent bundled refactors/features/cleanup.
- Feedback **SHOULD** be actionable.
  Why: Authors need clear next steps.
- Feedback **SHOULD** include alternatives or slices when useful.
  Why: Authors need practical options.
- Feedback **SHOULD** balance critique with reinforcement.
  Why: Balanced feedback helps authors improve quickly.

## Scope and Audience

PR reviewers in this repository.

## At-a-Glance Quick-Start

- Read the description, links, and screenshots.
  Confirm the single narrative and change type.
- Stop and request a split when the scope or size is too large.
- Check build, test, and mutation evidence.
  Use `./go.ps1` or an equivalent command.
- Inspect architecture boundaries, DI/logging patterns, and tests.
- Summarize must-fix items and notable positives.

## Core Principles

- Small, focused PRs reduce risk.
- Tests and build evidence support approval.
- Clear, direct feedback accelerates iteration.

## References

- Testing: `.github/instructions/testing.instructions.md`
- Logging/DI guardrails: `.github/instructions/logging-rules.instructions.md`,
  `.github/instructions/shared-policies.instructions.md`
- Post-push review polling: `.github/instructions/pr-review-polling.instructions.md`
- Documentation agent: `.github/agents/technical-writer.agent.md`
