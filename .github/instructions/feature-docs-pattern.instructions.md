---
applyTo: 'docs/Docusaurus/docs/**/event-sourcing-*.md'
---

# Feature Documentation Pattern

Governing thought: Alfred's Forge features with source-generated and manual registration paths need one subordinate documentation pattern.

> Drift check: Keep this file aligned with `docs/Docusaurus/docs/contributing/documentation-guide.md` and the page-type-specific authoring pages.

## When This Pattern Applies

Use this pattern only when the page has a valid primary page type and the topic includes both source-generated and manual registration paths.

Common examples include:

- sagas
- aggregates
- UX projections
- other Inlet-generated features with a real manual alternative

This pattern does not replace page-type rules. It refines them.

## Rules (RFC 2119)

- Pages using this pattern **MUST** still choose one primary page type before applying the branching structure. Why: The source-generation pattern is subordinate guidance.
- Shared setup **MUST** appear before the source-generated versus manual branching point. Why: Readers learn common mechanics once.
- The source-generated path **MUST** appear first. It **MUST** be marked as recommended when it is the preferred repo path. Why: Readers see the least error-prone path first.
- Manual registration **MUST** explain why a reader would choose it. It **MUST** explain what it makes explicit. Why: Manual branches are for understanding or customization, not noise.
- Both branches **MUST** describe equivalent runtime intent. They **MUST NOT** imply different guarantees unless evidence shows real behavioral differences. Why: Readers need accurate trade-offs.
- A branching callout such as `:::tip Registration Options` **SHOULD** introduce the divergence. Why: Readers need a clear signal when the document splits.
- This pattern **MUST NOT** stuff concept, tutorial, and reference content into one giant feature page. Why: It is a local refinement, not a license to mix page types.

## Scope and Audience

Authors documenting Alfred's Forge features that expose both source-generated and manual registration paths.

## At-a-Glance Quick-Start

- Choose the page type first.
- Teach shared setup before branching.
- Show the recommended generated path first.
- Explain the manual path only when it adds real value.

## Core Principles

- **Subordinate Pattern**: Page type remains the top-level contract.
- **Shared Before Split**: Avoid duplicated setup.
- **Trade-Off Honesty**: Explain both paths without inventing differences.

## References

- Documentation guide: `docs/Docusaurus/docs/contributing/documentation-guide.md`
- Documentation authoring: `.github/instructions/documentation-authoring.instructions.md`
- Documentation page focus: `.github/instructions/documentation-page-focus.instructions.md`
