---
applyTo: '**/*.md'
---

# Markdown and markdownlint

Governing thought: Configured markdownlint rules and fix-at-source content keep Markdown consistent.

> Drift check: Lint configs live in `.markdownlint-cli2.jsonc` and `.github/linters/.markdown-lint.yml`; open them before changing guidance.

## Rules (RFC 2119)

- Markdown **MUST** comply with all active markdownlint rules (MD001–MD059 except MD013 per config). Why: Ensures consistent, accessible docs.
- Lint warnings **MUST** be treated as build blockers. Why: Ensures consistent, accessible docs.
- Contributors **MUST NOT** disable markdownlint rules unless explicitly instructed for a single case. Why: Prevents standards erosion.
- Contributors **MUST NOT** suppress markdownlint rules unless explicitly instructed for a single case. Why: Prevents standards erosion.
- Contributors **MUST NOT** reconfigure markdownlint rules unless explicitly instructed for a single case. Why: Prevents standards erosion.
- Contributors **MUST NOT** use inline `markdownlint-disable` unless explicitly instructed for a single case. Why: Prevents standards erosion.
- Content **MUST** use GitHub Flavored Markdown. Why: Primary consumption channel.
- Content **MUST** render correctly on GitHub. Why: Primary consumption channel.
- Authors **MUST** run markdownlint locally before submitting. Why: Catches issues early.
- Authors **MUST** fix all findings before submitting. Why: Catches issues early.
- Authors **SHOULD** prefer plain Markdown over inline HTML. Why: Portability and accessibility.
- Authors **SHOULD** keep content accessible with descriptive links, alt text, and semantic headings. Why: Portability and accessibility.

## Scope and Audience

All Markdown authors/reviewers.

## At-a-Glance Quick-Start

- Structure with a single top-level heading.
- Keep lists, tables, and fences separated by blank lines.
- Provide alt text.
- Use meaningful link text.
- Lint with `npx markdownlint-cli2 "**/*.md"` or use the configured runner.
- Fix findings instead of suppressing them.

## Core Principles

- Fix-at-source keeps docs clean and CI reliable.
- Accessibility is part of quality.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`

