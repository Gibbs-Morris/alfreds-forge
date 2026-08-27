# Workflow engine and process model

## Core product stance

The workflow-style engine is a core capability of Alfred's Forge, not a side feature.

The first-principles reason: a dark factory is only as flexible as the process it can express. If the lifecycle is hardcoded, the platform serves one team's way of working. If the lifecycle is a governed, definable graph, the same platform serves many teams and, eventually, many kinds of process.

So the engine must let users define, adapt, and govern their own SDLC shape, while Alfred's Forge still provides a strong default out-of-the-box path.

## SDLC definition model

The product should support two modes at once:

- opinionated defaults for teams that want fast adoption
- configurable workflow definitions for teams that need their own lifecycle and controls

This lets Alfred's Forge be usable immediately while still scaling to enterprise variation.

## Why this matters

If workflow definition is first-class, the dark factory is not locked to one process template.

Long-term, this opens the door to:

- SDLC automation
- adjacent engineering governance workflows
- broader enterprise delivery or operational processes that follow similar contract -> execution -> evidence patterns

## Platform implication

This reframes Alfred's Forge as:

- an AI engineering concierge
- a governed execution platform
- a configurable workflow/process engine with SDLC as the initial flagship domain

## Design guardrail

Even with process flexibility, the system should keep strong product opinionation.

Configuration should not devolve into a blank orchestration tool with no stance.

The value is in combining:

- configurable process structure
- enforced governance and instruction boundaries
- opinionated delivery quality and evidence expectations

## Grounding in prior work

This direction is not greenfield speculation. There is prior workflow-engine thinking from `Gibbs-Morris/clean-squad` that should be pulled forward into Alfred's Forge. The goal is not to copy implementation details blindly, but to carry over the strongest proven concepts. Capability evidence from both source repositories is summarized in `06-mississippi-and-clean-squad-capability-brief.md`.

### CleanSquad capabilities to carry forward

Based on current CleanSquad materials, important capabilities to preserve include:

- graph-based workflow definitions with explicit node kinds (Stage, Fork, Join, Wait, Decision, Exit)
- multiple named entry points so one workflow can start from different operational contexts
- persisted run state plus structured event history for resume and auditability
- shared asset layering (persona, rules, general instructions, repository instructions) for controlled behavior composition
- policy controls that limit review/rebuild loops and bound execution
- specialist parallel review patterns with master synthesis and rules-based routing
- built-in wait/poll loops for external system lag (for example, GitHub comments and CI status)
