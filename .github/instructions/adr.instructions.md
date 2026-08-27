---
applyTo: 'docs/Docusaurus/docs/adr/[0-9][0-9][0-9][0-9]-*.md'
---

# Architecture Decision Records (MADR)

Governing thought: Use the MADR 4.0.0 template for immutable ADRs in `docs/Docusaurus/docs/adr/`.

> Drift check: Read the MADR 4.0.0 specification at <https://adr.github.io/madr/> before changing this template. Read `docs/key-principles/architecture-decision-records.md` for the project's foundational guidance.

## Rules (RFC 2119)

- ADRs **MUST** live in `docs/Docusaurus/docs/adr/`. ADRs **MUST** follow the MADR 4.0.0 template in this file. Why: Gives every published decision one discoverable format.
- ADR filenames **MUST** use `NNNN-title-with-dashes.md`. `NNNN` **MUST** be a zero-padded sequential number. The title **MUST** use lowercase dashes. Why: Gives cross-references stable names.
- Numbers on a feature branch **MUST** remain provisional until merge preparation. Before merging, the author **MUST** rebase onto the latest `main`. The author **MUST** renumber new ADRs as one contiguous block after the highest number on `main`. Why: Parallel pull requests can assign conflicting numbers.
- Before merging renumbered ADRs, the author **MUST** update the filenames, `ADR-NNNN` titles, `sidebar_position` values, and relative ADR links. Why: Keeps every identifier and link aligned.
- ADR front matter **MUST** include `title`, `description`, `sidebar_position`, `status`, and `date`. Authors **SHOULD** add `decision_makers`, `consulted`, and `informed` when applicable. Why: Supports Docusaurus and MADR metadata.
- ADRs **MUST** contain `Context and Problem Statement`, `Considered Options`, and `Decision Outcome`. `Decision Outcome` **MUST** contain a `Chosen option:` sentence. Why: These sections are the MADR 4.0.0 minimum.
- Authors **SHOULD** add `Decision Drivers`, `Consequences`, `Confirmation`, `Pros and Cons of the Options`, and `More Information` when they add value. Authors **MAY** omit these sections for simple decisions. Why: Keeps simple ADRs small and complex ADRs complete.
- New ADRs, substantially revised mutable ADRs, and new superseding ADRs **SHOULD** include Mermaid when they explain a multi-step flow or a multi-component structure that prose alone would make materially harder to understand. Authors **SHOULD NOT** add diagrams for trivial, metadata-only, or link-only edits. Why: Matches diagram effort to comprehension value.
- When an ADR includes Mermaid, its prose **MUST** remain authoritative. The diagram **MUST** match the prose. The diagram **SHOULD** appear directly below the section it explains. Why: Prevents diagrams from conflicting with the decision.
- When a qualifying ADR omits Mermaid, the author **SHOULD** add a short omission rationale near the relevant discussion or in a clearly labeled note. Why: Makes the omission intentional and reviewable.
- Authors **SHOULD** use `sequenceDiagram` for interactions over time, `flowchart` for process or decision flow, and simple architecture or C4-style Mermaid for structures. Why: Gives common relationships consistent diagram types.
- Accepted ADRs **MUST NOT** change their `Context and Problem Statement` or `Decision Outcome`. When a decision changes, the author **MUST** create a new ADR. The original **MUST** use status `superseded by [ADR-NNNN](NNNN-title.md)`. Why: Preserves historical reasoning.
- Status **MUST** be `proposed`, `accepted`, `deprecated`, or `superseded by [ADR-NNNN](NNNN-title.md)`. Why: Defines a traceable lifecycle.
- ADR cross-references **MUST** use relative Markdown links. Why: Keeps links valid in every publishing environment.
- Authors **SHOULD** write ADRs during or before the decision. Why: Retrospective records lose context.
- Authors **SHOULD** record only decisions that affect structure, are hard to reverse, cross component boundaries, involve significant trade-offs, or set a precedent. Why: Prevents trivial decisions from filling the log.

## Scope and Audience

Use these rules when you create or modify an ADR. The cs ADR Keeper agent is the primary author in the Clean Squad workflow.

## Quick Start

1. Copy the template to `docs/Docusaurus/docs/adr/NNNN-title-with-dashes.md`.
2. Treat the branch number as provisional until merge preparation.
3. Set `sidebar_position` to the `NNNN` number.
4. Complete the three required sections.
5. Add Mermaid when both qualifying conditions apply:
   - The ADR explains a multi-step flow or a multi-component structure.
   - Prose alone would make that relationship materially harder to understand.
6. Add an omission rationale when a qualifying ADR does not include Mermaid.
7. Set status to `proposed`. Set it to `accepted` after approval.

## Mermaid Qualifying Test

An ADR qualifies for Mermaid when both conditions apply:

1. It documents a multi-step flow or a multi-component structure.
2. Prose alone would make that relationship materially harder to understand.

Mermaid remains optional when either condition is absent.

## MADR 4.0.0 Template

````markdown
---
title: "ADR-NNNN: Title of Decision"
description: One-sentence summary of the decision
sidebar_position: NNNN
status: "proposed"
date: YYYY-MM-DD
decision_makers:
  - Name or role
consulted:
  - Name or role
informed:
  - Name or role
---

# ADR-NNNN: Title of Decision

## Context and Problem Statement

Describe the context and problem in two or three sentences.
State the problem as a question when possible.

When the ADR qualifies for Mermaid, put the diagram directly below the section it explains.
Keep the prose authoritative.

## Decision Drivers

- Driver 1 (for example, performance requirement)
- Driver 2 (for example, team expertise)

## Considered Options

- Option 1
- Option 2
- Option 3

## Decision Outcome

Chosen option: "Option N", because [justification].

### Consequences

- Good, because [positive consequence]
- Bad, because [negative consequence]

### Confirmation

Describe how the team will confirm compliance with this ADR.
Examples include code review, an architecture test, and a CI check.

## Pros and Cons of the Options

### Option 1

Description or link to more information.

- Good, because [argument]
- Neutral, because [argument]
- Bad, because [argument]

### Option 2

Description or link to more information.

- Good, because [argument]
- Bad, because [argument]

## More Information

Link to related ADRs, RFCs, design documents, or external references.

If the ADR qualifies for Mermaid but omits it, add a brief omission rationale here.
````

## Core Principles

- Use MADR 4.0.0. Keep the mandatory minimum small.
- Use optional sections when they add value.
- Preserve historical reasoning. Create a superseding ADR instead of editing an accepted decision.
- Publish ADRs in Docusaurus so the team can find them.
- Use sequential numbers for stable identifiers.
- Use Mermaid to clarify complex flows and structures. Do not add decorative diagrams.

## References

- MADR 4.0.0: <https://adr.github.io/madr/>
- Key principles: `docs/key-principles/architecture-decision-records.md`
- ADR Keeper agent: `.github/agents/cs-adr-keeper.agent.md`
- Documentation authoring: `.github/instructions/documentation-authoring.instructions.md`
