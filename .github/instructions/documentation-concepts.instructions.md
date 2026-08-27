---
applyTo: 'docs/Docusaurus/docs/**/*concept*.{md,mdx},docs/Docusaurus/docs/**/*concepts*.{md,mdx}'
---

# Concept Documentation

Governing thought: Explain how Alfred's Forge works, what it guarantees, and which limits and trade-offs readers must understand.

> Drift check: Keep this guidance aligned with `docs/Docusaurus/docs/contributing/documentation-concepts.md`.

## Rules (RFC 2119)

- This file **MUST** apply only when the page type is `concepts`. Why: Concept pages explain ideas instead of giving task steps.
- Concept pages **MUST** use this structure: a direct explanation, `## The problem this solves`, `## Core idea`, `## How it works`, `## Guarantees`, `## Non-guarantees` or `## Limits`, `## Trade-offs`, and `## Related tasks and reference`. Why: Gives readers a predictable path.
- Concept pages **MUST** explain ordering, concurrency, durability, state-change visibility, failure boundaries, cancellation, and versioning when relevant. Why: Distributed-systems semantics shape Alfred's Forge concepts.
- Comparisons **MUST** use evidence. They **MUST NOT** imply equivalence to Orleans or another system without proof. Why: Similar systems are not identical.
- Concept pages **MUST NOT** become task guides, reference dumps, release notes, or marketing pages. Why: Keeps explanation pages focused.

## Scope and Audience

Use these rules for contributors and agents who write Alfred's Forge concept pages.

## References

- Public guide: `docs/Docusaurus/docs/contributing/documentation-concepts.md`
- General authoring: `.github/instructions/documentation-authoring.instructions.md`
