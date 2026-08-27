---
applyTo: 'docs/Docusaurus/docs/**/*migration*.{md,mdx},docs/Docusaurus/docs/**/*upgrade*.{md,mdx}'
---

# Migration Guides

Governing thought: Move readers from one exact Alfred's Forge version range to another with explicit compatibility, validation, and rollback guidance.

> Drift check: Keep this file aligned with `docs/Docusaurus/docs/contributing/documentation-migration.md` and compatibility-related repo rules.

## Rules (RFC 2119)

- Apply this file only when the page is classified as `migration`. Why: Migration pages are versioned operational contracts.
- Include the exact version scope in each migration page title. Why: Readers need immediate clarity about applicability.
- Document the relevant source and target versions, mixed-version support, wire, storage, and serialization compatibility, config renames, default changes, removed and deprecated APIs, and rollout order. Why: Upgrade risk lives in the details.
- Include exact validation steps. State whether rollback is possible and what to back up first. Why: A successful migration needs proof and a contingency.
- Verify every before-and-after code or configuration example. Why: Migration instructions cannot rely on stale examples.
- Do not move migration detail into release notes. Why: Readers need a dedicated upgrade surface.

## Scope and Audience

Contributors and agents who author migration pages for Alfred's Forge documentation.

## References

- Public guide: `docs/Docusaurus/docs/contributing/documentation-migration.md`
- General authoring: `.github/instructions/documentation-authoring.instructions.md`
- Backwards compatibility: `.github/instructions/backwards-compatibility.instructions.md`
- Storage naming: `.github/instructions/storage-type-naming.instructions.md`

