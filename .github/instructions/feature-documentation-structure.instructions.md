---
applyTo: 'docs/Docusaurus/docs/**/*.{md,mdx}'
---

# Feature Documentation Structure

Governing thought: Alfred's Forge is moving to page-type-driven documentation. Feature-oriented folders still need consistent structure during the transition.

> Drift check: Align this file with `docs/Docusaurus/docs/contributing/documentation-guide.md` and the current Docusaurus sidebar behavior.

## Rules (RFC 2119)

- Feature documentation **MUST** follow the selected page type before historical folder shape. Why: Page type is the current authoring contract.
- Existing feature-oriented folders **MAY** remain until authors touch them. Why: The repo is in a hybrid migration.
- New public folders **SHOULD** use `_category_.yml`. Why: The repo is in a hybrid migration.
- New public folders **SHOULD** prefer generated indexes for section navigation. Why: The repo is in a hybrid migration.
- Feature folders **MUST NOT** mix getting-started, tutorial, reference, troubleshooting, and internals content into one long page. Why: The new governance model is page-type-driven.
- Authors **MUST** split content into separate pages when a feature needs multiple page types. Why: Readers should not wade through unrelated material.
- Authors **MUST** cross-link the separate pages. Why: Readers should not wade through unrelated material.
- Content placement within a feature area **SHOULD** make the page type obvious from the filename or neighboring docs when practical. Why: Discoverability improves when filenames and purpose line up.
- Feature-level entry pages **SHOULD** orient readers with links to narrower child pages. Why: Entry pages should guide, not sprawl.
- Feature-level entry pages **SHOULD NOT** absorb all content categories. Why: Entry pages should guide, not sprawl.
- Migration guides **MUST** remain isolated from release notes. Why: Upgrade risk deserves its own surface.
- Migration guides **MUST** remain isolated from generic feature overviews. Why: Upgrade risk deserves its own surface.
- Troubleshooting content **MUST** remain symptom-driven when nested under a feature folder. Why: Troubleshooting should start from the failure, not the subsystem.

## Scope and Audience

Contributors and agents updating feature-oriented docs during the transition to stronger page-type governance.

## At-a-Glance Quick-Start

- Keep existing feature folders when that avoids unnecessary churn.
- Split content by page type instead of stuffing every concern into one feature page.
- Prefer `_category_.yml` for new public folders.
- Use orientation pages plus cross-links instead of giant all-in-one pages.

## Transitional Placement Model

During the current transition:

- Feature folders remain allowed.
- Page type governs structure and content rules.
- New sections should prefer generated-index category metadata.
- Touched legacy pages should move closer to the new model when safe.

## Core Principles

- **Page Type Wins**: Structure follows reader intent first.
- **Transition Without Thrash**: Keep stable folders when they are not the problem.
- **Cross-Link Instead Of Collapse**: Use multiple smaller pages with clear adjacency.

## References

- Documentation guide: `docs/Docusaurus/docs/contributing/documentation-guide.md`
- Documentation page focus: `.github/instructions/documentation-page-focus.instructions.md`
- Documentation authoring: `.github/instructions/documentation-authoring.instructions.md`

