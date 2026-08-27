---
applyTo: '**'
---

# Self-Improvement Learning System

Governing thought: Agents record validated lessons from real-work failures in typed `self-taught-<domain>.instructions.md` files. These files build institutional memory without conflicting with hand-authored rules.

> Drift check: Before adding a lesson, review this file and `.github/instructions/authoring.instructions.md` for conflict rules. Read relevant domain instruction files to check for overlap.

## Rules (RFC 2119)

- When an agent encounters an issue that required a retry, rework, or non-obvious workaround, it **SHOULD** record a concise lesson in the appropriate `self-taught-<domain>.instructions.md` file. Why: Prevents the same mistake in future conversations.
- Self-taught files **MUST** live in `.github/instructions/`. Why: Keeps them discoverable alongside hand-authored instructions.
- Filenames **MUST** use `self-taught-<domain>.instructions.md` with a kebab-case domain. Why: Keeps file names predictable.
- Self-taught files **MUST** follow the standard authoring template. Why: Enables parsing by people and tools.
- The template **MUST** include YAML front matter with `applyTo`, H1, governing thought, Drift check, Rules section, and References. Why: Defines the required instruction structure.
- Each lesson **MUST** be one concise bullet in the Rules section. Why: Keeps lessons dense.
- Each lesson **MUST** use an RFC 2119 keyword. Why: Makes its requirement level clear.
- Each lesson **MUST** end with a `Why:` suffix that cites an observed failure, error message, build output, or specific scenario. Why: Anchors the lesson in evidence.
- Before adding a lesson, the agent **MUST** read every existing instruction file whose `applyTo` scope overlaps with the target self-taught file. Why: Hand-authored rules are the source of truth.
- The agent **MUST** verify that a new lesson does not contradict a hand-authored rule. Why: Self-taught lessons supplement but never override hand-authored rules.
- If a proposed lesson conflicts with a hand-authored rule, the agent **MUST NOT** add it. Why: Protects authoritative policy.
- If a task folder is active, the agent **SHOULD** instead record the conflict in `.thinking/` for human review. Why: Preserves the reason for rejecting the lesson.
- Agents **MUST** treat self-taught lessons as supplementary guidance. Why: Hand-authored instructions remain authoritative.
- When a self-taught lesson conflicts with a hand-authored instruction, the hand-authored instruction **MUST** take precedence. Why: Maintains one authority hierarchy.
- Each self-taught file **SHOULD** stay under 30 items. Why: Keeps the token footprint manageable.
- When a file approaches 30 items, its lessons **SHOULD** be reviewed. Why: Prevents uncontrolled growth.
- After review, lessons **SHOULD** either be promoted to the relevant hand-authored instruction file with human approval or be retired when no longer relevant. Why: Promotes proven guidance and removes obsolete guidance.
- Duplicate or near-duplicate lessons **MUST NOT** be added. Why: Prevents bloat and redundancy.
- Agents **MUST** check existing lessons in the target file before writing. Why: Detects duplicates.
- Self-taught files **MUST NOT** contain opinions, preferences, or speculative guidance. Why: Keeps the evidence empirical.
- Every lesson **MUST** trace to a concrete observed failure or inefficiency. Why: Prevents cargo-cult guidance.
- New domains **MAY** be created when no existing domain covers the lesson. Why: Lets categories grow with the codebase.
- The agent **MUST** select the `applyTo` pattern that best matches the domain scope. Why: Keeps each lesson correctly scoped.

## Scope and Audience

All agents and contributors. Use these rules when an agent encounters a recoverable failure, retry, or non-obvious workaround during any workflow.

## Quick Start

- Capture one concise bullet in `self-taught-<domain>.instructions.md` after resolving a retry, rework, or non-obvious workaround.
- Read overlapping instruction files before adding a lesson.
- Check for conflicts and duplicates before writing.
- Keep lessons evidence-based, concise, and RFC 2119-formatted.
- Treat hand-authored instructions as authoritative and self-taught lessons as supplementary.

## Domain Categories

| Domain | `applyTo` | Covers |
| --- | --- | --- |
| `build` | `'**'` | Build pipeline, MSBuild, project files, NuGet, CI |
| `testing` | `'tests/**'` | Test patterns, coverage, mutation, determinism |
| `csharp` | `'**/*.cs'` | C# idioms, compiler behavior, analyzers |
| `serialization` | `'**/*.cs'` | Orleans serialization, JSON, wire formats |
| `orleans` | `'**/*.cs'` | Grains, activation, lifecycle, hosting |
| `agent-workflow` | `'.github/agents/**'` | Agent design, workflow steps, prompt engineering |
| `documentation` | `'docs/**'` | Docusaurus, page structure, MDX |
| `powershell` | `'**/*.ps*'` | Scripts, engineering tools |
| `blazor` | `'**/*.razor*'` | Blazor UI, components, SignalR |

## Self-Taught File Template

Use this structure for each new `self-taught-<domain>.instructions.md` file:

````markdown
---
applyTo: '<matching pattern for domain>'
---

# Self-Taught Lessons: <Domain>

Governing thought: Empirical lessons learned from real-work failures in <domain area>, captured to prevent repeated mistakes.

> Drift check: Before adding a lesson, read all instruction files whose `applyTo` overlaps with this file's scope; verify no conflict with hand-authored rules per `self-improvement.instructions.md`.

## Rules (RFC 2119)

- <RFC 2119 keyword> <concise lesson>. Why: <evidence from the observed failure>.

## References

- Self-improvement governance: `.github/instructions/self-improvement.instructions.md`
- <relevant domain instruction file(s)>
````

## Conflict Detection Protocol

Adapted from the Rules Manager workflow:

1. **Identify scope.** Determine the `applyTo` of the target self-taught file.
2. **Read overlapping files.** Read every `.github/instructions/*.instructions.md` whose `applyTo` intersects the target scope.
3. **Check contradiction.** Check whether the new lesson **CONTRADICT**s an existing rule.
4. **Check redundancy.** Check whether an existing rule already **COVER**s the lesson.
5. **Decide.** Do not add a lesson that conflicts or duplicates an existing rule. Add a supplementary lesson.

## Lesson Lifecycle

1. **Capture.** After an agent observes a retry, rework, or other qualifying event, it writes a concise bullet in the appropriate file after conflict and duplicate checks.
2. **Accumulate.** Lessons collect over multiple conversations. Each file stays under 30 items.
3. **Promote.** When a lesson proves universally valuable, it can be proposed for the relevant hand-authored instruction file. Human approval is required.
4. **Retire.** A lesson can be removed during periodic review when tooling fixes it or a promoted rule supersedes it.

## Core Principles

- Use evidence, not speculation. Trace every lesson to an observed event.
- Keep self-taught guidance supplementary. Hand-authored rules take precedence.
- Keep token use low with concise lessons, scoped `applyTo`, and file size limits.
- Check conflicts before writing. Use the Rules Manager process.

## References

- Instruction authoring: `.github/instructions/authoring.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
