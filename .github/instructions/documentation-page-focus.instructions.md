---
applyTo: 'docs/Docusaurus/docs/**/*.{md,mdx}'
---

# Documentation Page Focus

Governing thought: Each Alfred's Forge documentation page serves one reader intent and one page type during the migration from feature-oriented layouts.

> Drift check: Keep this file aligned with the canonical page-type model in `docs/Docusaurus/docs/contributing/documentation-guide.md`.

## Rules (RFC 2119)

- Authors **MUST** classify each page as exactly one of `getting-started`, `tutorials`, `how-to`, `concepts`, `reference`, `operations`, `troubleshooting`, `migration`, or `release-notes` before writing. Why: Page type determines structure and evidence.
- Pages **MUST NOT** combine tutorial, how-to, concept, and reference content into one undifferentiated page. Why: Mixed intent harms navigation and maintenance.
- Each page **MUST** answer one primary question and **MUST** state the answer or scope in its opening. Why: Readers need to identify the page's purpose immediately.
- Authors **MUST** treat page type as the writing contract when the physical folder uses an older feature-oriented structure. Why: Alfred's Forge uses a hybrid transition.
- Authors **MUST** split topics that genuinely need multiple page types into separate, cross-linked pages. Why: Readers should not search through irrelevant sections.
- Placement in the docs tree **SHOULD** reinforce page type when practical. Physical placement **MUST NOT** override the page-type contract. Why: Folder layout is migrating incrementally.

## Scope and Audience

All contributors and agents writing or updating public docs under `docs/Docusaurus/docs/`.

## At-a-Glance Quick-Start

- Choose the page type before writing.
- State the outcome or scope in the opening.
- Keep each page focused on one reader intent.
- Split and cross-link topics that span multiple intents.

## Classification Questions

Ask these questions before writing.

- Is the reader trying to reach a first success?
- Is the reader trying to learn through a guided sequence?
- Is the reader trying to complete a specific task quickly?
- Is the reader trying to understand a model, guarantee, or trade-off?
- Is the reader trying to look up exact facts?
- Is the reader trying to run Alfred's Forge safely in production?
- Is the reader diagnosing a symptom?
- Is the reader upgrading between versions?
- Is the reader scanning a release summary?

## Core Principles

- **One Intent**: One page should answer one main question.
- **Page Type Before Placement**: Content contract matters more than folder history.
- **Split Before Stuffing**: If the page wants to do multiple jobs, break it apart.

## References

- Public guide: `docs/Docusaurus/docs/contributing/documentation-guide.md`
- Documentation authoring: `.github/instructions/documentation-authoring.instructions.md`
- Feature documentation structure: `.github/instructions/feature-documentation-structure.instructions.md`
