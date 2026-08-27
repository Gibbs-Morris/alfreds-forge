---
applyTo: 'docs/Docusaurus/docs/**/*release-notes*.{md,mdx},docs/Docusaurus/docs/**/*releases*.{md,mdx}'
---

# Release Notes Documentation

Governing thought: Release notes tell Alfred's Forge users what changed, why it matters, and what action to take.

> Drift check: Keep this file aligned with `docs/Docusaurus/docs/contributing/documentation-release-notes.md`.

## Rules (RFC 2119)

- This file **MUST** apply only when the page is classified as `release-notes`. Why: Release notes summarize changes, not broad documentation.
- Release notes **MUST** include the exact version and release date, a concise summary, breaking changes, features, fixes, deprecations, security notes when relevant, upgrade guidance, and links. Why: Readers need a standard release summary shape.
- Release notes **MUST** lead with user impact. Why: Readers need to understand the change before its details.
- Release notes **MUST** include exact identifiers when relevant, such as version numbers, issue numbers, PR numbers, config keys, removed APIs, or changed defaults. Why: Precise identifiers make release notes actionable.
- Authors **MUST** call out breaking changes even when a workaround exists. Why: Breaking changes belong in the breaking-changes section, not buried in prose.
- Release notes **SHOULD** link to migration, how-to, or reference pages for detail instead of duplicating that detail. Why: Release notes should stay concise.
- Release notes **MUST NOT** restate commit messages verbatim. Why: Release notes are engineering change summaries.
- Release notes **MUST NOT** use marketing language. Why: Release notes are engineering change summaries.

## Scope and Audience

Contributors and agents authoring release notes for Alfred's Forge documentation.

## References

- Public guide: `docs/Docusaurus/docs/contributing/documentation-release-notes.md`
- General authoring: `.github/instructions/documentation-authoring.instructions.md`

