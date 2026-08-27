---
applyTo: 'docs/Docusaurus/docs/**/*getting-started*.{md,mdx}'
---

# Getting-Started Documentation

Governing thought: Lead new Alfred's Forge users to their first success through the shortest verified path with minimal theory.

> Drift check: Keep this guidance aligned with `docs/Docusaurus/docs/contributing/documentation-getting-started.md`.

## Rules (RFC 2119)

- This file **MUST** apply only when the page type is `getting-started`. Why: This page type has distinct scope and safety needs.
- Getting-started pages **MUST** use the shortest verified path to success. Why: New users need early progress.
- Getting-started pages **MUST** use one happy path. They **MUST NOT** include optional variants unless a variation is unavoidable. Why: Fewer choices reduce onboarding effort.
- Getting-started pages **MUST** follow the public guide structure: prerequisites, verified path, verification step, and next steps. Why: Gives first-time readers a predictable sequence.
- Commands and expected results **MUST** be executable and verified as written. Why: First-run failures reduce trust.
- Getting-started pages **MUST NOT** become long tutorials, concept essays, reference pages, or production deployment guides. Why: Their purpose is first success.

## Scope and Audience

Use these rules for contributors and agents who write Alfred's Forge getting-started pages.

## References

- Public guide: `docs/Docusaurus/docs/contributing/documentation-getting-started.md`
- General authoring: `.github/instructions/documentation-authoring.instructions.md`
