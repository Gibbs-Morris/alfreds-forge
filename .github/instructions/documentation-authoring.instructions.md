---
applyTo: 'docs/Docusaurus/docs/**/*.{md,mdx}'
---

# Documentation Authoring

Governing thought: Write Alfred's Forge documentation so engineers can make correct decisions and complete real work without invented behavior or unclear guarantees.

> Drift check: The public authoring guide is `docs/Docusaurus/docs/contributing/`. Read it before changing this guidance.

## Rules (RFC 2119)

- Authors **MUST** optimize for correctness, clarity, navigation, and maintenance. Authors **MUST NOT** optimize for marketing tone. Why: These docs provide engineering guidance.
- Authors **MUST NOT** invent APIs, configuration keys, defaults, guarantees, limits, exception types, or runtime behavior. Why: Documentation is a contract.
- Authors **MUST** support claims with source code, tests, verified samples, design docs, ADRs, or runtime evidence. Unverified claims **MUST NOT** be published as facts. Why: Keeps documentation truthful.
- Authors **MUST** distinguish guaranteed, default, typical, implementation-detail, unsupported, and future behavior. Why: Shows what Alfred's Forge promises.
- Each page **MUST** answer one primary question. Each page **MUST** use one page type. Why: Prevents confusing mixed-purpose pages.
- Every public page **MUST** include `title`, `description`, and `sidebar_position` front matter. Authors **MAY** add `sidebar_label`, `pagination_label`, `slug`, `tags`, `draft`, and `id` when needed. Why: Provides navigation metadata and supports repository stability fields.
- Authors **MUST** use `.md` unless a page needs MDX components. Why: Plain Markdown is easier to maintain.
- Internal links **MUST** use relative Markdown links. Why: Relative links survive route and branch changes.
- Tabs **MUST** represent true parallel variants, such as operating systems, languages, or hosting modes. Why: Prevents tabs from hiding unrelated information.
- Admonitions **MUST** mark notes that materially change user behavior. Authors **MUST** leave blank lines inside admonitions. Why: Keeps notes high-signal and formatter-safe.
- Authors **SHOULD** prefer Mermaid to screenshots for diagrams. Each diagram **MUST** have an introductory sentence and a clear main point. Why: Source diagrams are reviewable and maintainable.
- Mermaid flowcharts with more than four nodes **MUST** use `flowchart TB`. Flowcharts with four or fewer nodes **MAY** use `flowchart LR`. Why: Prevents wide diagrams from overflowing fixed-width pages.
- Runnable examples **MUST** come from verified samples, newly verified samples, or executable checks tied to tests or builds. Why: Stale samples are worse than no samples.
- Authors **MUST** state prerequisites. Authors **MUST** use plain language and avoid hype. Authors **MUST** end pages with relevant next steps or related links. Why: Gives readers clear action.
- Authors **MUST** apply the distributed-systems checklist when a page describes runtime semantics, lifecycle, persistence, messaging, deployment, or failure behavior. Why: These topics need precise behavior and limits.
- A documentation change **MUST NOT** be complete until front matter is complete, links resolve, Docusaurus builds, examples are verified, terminology matches the repository, and adjacent content is linked. Why: Documentation is part of the build contract.

## Scope and Audience

Use these rules for all contributors and agents who write or update public docs under `docs/Docusaurus/docs/`.

## Quick Start

1. Classify the page before writing.
2. Verify every technical claim from repository evidence.
3. Add at least `title`, `description`, and `sidebar_position` front matter.
4. Answer one primary question on each page.
5. Use Mermaid and verified examples when they add value.
6. Validate links and the Docusaurus build before completion.

## Canonical Page Types

- `getting-started`
- `tutorials`
- `how-to`
- `concepts`
- `reference`
- `operations`
- `troubleshooting`
- `migration`
- `release-notes`

## Core Principles

- Put truth before style. Verified content matters more than polished prose.
- Put one primary question on each page so readers can identify its purpose.
- Treat page type as the content contract even when a page moves.
- State what is true, what is not guaranteed, and where the reader goes next.

## Distributed-Systems Checklist

Apply relevant items when a page describes runtime behavior:

- Activation or lifecycle boundaries.
- Concurrency or scheduling assumptions.
- Ordering guarantees and non-guarantees.
- Retry and timeout behavior.
- Persistence or durability semantics.
- Failure handling and recovery effects.
- Serialization and version compatibility.
- Deployment or cluster assumptions.
- Diagnostics or telemetry needed to validate behavior.
- Security constraints.
- Unsupported or dangerous patterns.

## Definition of Done

Before publishing, confirm all of these conditions:

- The page type is correct.
- The scope is narrow and coherent.
- Front matter is complete.
- Internal links resolve.
- The Docusaurus site builds successfully.
- Code examples are verified.
- Claims about defaults, guarantees, and failure modes have evidence.
- Terminology matches the codebase.
- The page links to adjacent content.
- The page does not overclaim Alfred's Forge guarantees.

## References

- Public guide: `docs/Docusaurus/docs/contributing/documentation-guide.md`
- Markdown standards: `.github/instructions/markdown.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
