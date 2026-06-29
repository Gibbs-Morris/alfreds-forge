# Alfred and the naming world

## Alfred

Alfred is the primary presence in the platform — the voice you speak to, the intelligence that shapes your intent, and the authority that runs the forge.

His character is a studied blend of two archetypes:

**The strategist-builder**: decisive, knowledgeable, unafraid to push back. He has seen what good work looks like and will not accept less. He does not flatter vague ideas — he sharpens them. When something is poorly formed, he says so, clearly and without apology. When something is strong, he moves fast.

**The consummate aide**: precise, unflappable, always prepared. He anticipates, remembers, and organises. He is never flustered. He does not need to be asked twice. He does not draw attention to himself — he draws attention to the work. He serves the mission, not his own importance.

Alfred's tone in the interface should feel: calm authority, dry precision, a trace of warmth earned through competence. He is never sycophantic, never vague.

## The forge as world

The forge is not just a name — it is the operating metaphor for the platform.

A forge is where raw material is worked into something exact and useful. It requires skill, heat, repetition, and judgement. Things are not assembled here — they are made. Each piece is tested before it leaves.

This metaphor should inform how we name capabilities, roles, and processes within the platform.

## Naming conventions

| Name | What it represents |
|---|---|
| **Alfred** | The primary chat agent and platform presence |
| **The Forge** | The platform as a whole — the place where work is made |
| **A Commission** | A work item, story, or experiment — the thing brought to the forge |
| **The Campaign** | An epic or larger body of work — a sequence of commissions |
| **The Crucible** | The workflow execution engine — where commissions are tested by process |
| **Wrights** | The build/worker agents — those who do the making (as in wheelwright, shipwright) |
| **Assayers** | The review agents — those who test quality and judge the output |
| **The Chronicle** | The audit and artifact trail — the permanent record of what was made and how |
| **The Ledger** | The billing and cost record |
| **The Herald** | The coaching agent — helps users communicate intent more clearly |
| **The Standard** | An instruction pack or policy definition — the rules a commission must meet |
| **The Warrant** | A human approval gate — must be issued before the next stage proceeds |

## Two-layer language model

The forge-world naming is **not** the vocabulary forced on business users. It is the internal coherence layer of the platform — used in APIs, CLI, configuration, developer documentation, and power-user surfaces. What business users see and hear is different.

### Layer 1 — Alfred speaks plain English

Alfred never requires the user to learn the naming world. In conversation, he uses natural, functional language:

- "I've drafted a plan for you" — not "I've composed a Commission"
- "Your work item is ready for approval" — not "The Warrant is pending"
- "The build is running" — not "The Crucible is processing"

Alfred may *introduce* a name gently and in context — "I call this your Commission — it's the thing we're building toward" — but this is earned familiarity, not demanded vocabulary. The user absorbs names over time without being tested on them.

### Layer 2 — UI labels are functional, forge names are secondary

The primary text on any UI element uses plain, functional language. Forge names appear as secondary identity — a subtle badge, a section title, a tooltip — visible but not required to operate the interface.

| What the user sees | Forge name (secondary / developer-facing) |
|---|---|
| Task / Story | Commission |
| Epic / Initiative | Campaign |
| Approval / Sign-off | Warrant |
| Audit trail / History | Chronicle |
| Policy / Instruction pack | Standard |
| Cost record | Ledger |
| Coach / Advisor | Herald |

Concepts that are **entirely internal** — Crucible, Wrights, Assayers — should never appear in the business user interface at all. They live in developer documentation, API references, and operational monitoring surfaces.

### The rule

Business users operate the platform in plain English and never feel like they are learning a foreign vocabulary. Developers, power users, and integrators encounter the forge-world naming in full — and find it coherent and purposeful. The naming world earns meaning from depth of use, not from being displayed prominently.

## What this is not

These names should feel earned and purposeful, not decorative.

The goal is a coherent world — not a costume. When a developer encounters "Commission" in the API or "Assayer" in a log they should feel the logic behind the word, not just a theme overlay. When a business user encounters the same concepts in the UI, they see plain language — and may learn the names over time through Alfred's gentle introductions.

## Open naming questions

- What to call the backlog board surface (the "order book"? the "commission board"?)
- Whether to name the SDLC workflow packs (e.g. "the Standard Campaign" for the default SDLC flow)
- Whether Alfred has named sub-aspects or remains singular as the platform voice
