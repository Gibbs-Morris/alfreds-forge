---
applyTo: '**/*.instructions.md'
---

# Instruction Authoring Guide

Governing thought: Use one concise structure for every instruction file: front matter, purpose, rules, and only the supporting detail that readers need.

> Drift check: Open referenced scripts under `eng/src/agent-scripts/` before quoting commands or behavior. Treat those scripts as canonical.

## Rules (RFC 2119)

- Every instruction file **MUST** include YAML front matter with `applyTo`, an H1 title, a one-sentence governing thought, a Drift check note, and one consolidated `Rules (RFC 2119)` section. Why: Enables predictable parsing.
- RFC 2119 keywords **MUST** stay uppercase. They **MUST NOT** appear outside the Rules section unless they occur in a quoted example. Why: Prevents accidental policy changes.
- Instruction files **MUST** live in `.github/instructions/`. Filenames **MUST** use kebab-case with the form `<topic>.instructions.md`. Each file **SHOULD** cover one cohesive topic. Why: Improves discovery.
- Authors **MUST** use concise, factual US English. Each Rules bullet **MUST** contain one requirement per sentence. Each rule **SHOULD** include a short `Why` when the reason is not obvious. Why: Reduces ambiguity and token use.
- Command examples **MUST** use real scripts or tools. Content and examples **MUST NOT** contain secrets. Why: Keeps guidance safe and actionable.
- Instruction changes **MUST** follow repository review policy. Authors **MUST** update a matching Cursor `.mdc` file only when that mirror exists. Why: Preserves parity without requiring unused files.

## Scope and Audience

Use these rules when you create or update `*.instructions.md`.

## Quick Start

Use this section order:

1. Front matter.
2. H1 title and governing thought.
3. Drift check.
4. Rules.
5. Scope and Audience.
6. Quick Start.
7. Core Principles.
8. Procedures, Examples, and References when needed.

Keep RFC 2119 keywords in the Rules section. Keep prose concise. Link to authoritative scripts and configs instead of copying their details.

## Core Principles

- Use a predictable structure so humans and automation can read instructions.
- Use concise, factual wording to reduce tokens and misinterpretation.
- Treat canonical scripts and configs as more authoritative than narrative text.

## References

- RFC keywords: `.github/instructions/rfc2119.instructions.md`
