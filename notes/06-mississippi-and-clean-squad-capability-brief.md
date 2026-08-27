# Mississippi and CleanSquad capability brief

## Purpose

This note captures the practical capability read-in from:

- `Gibbs-Morris/mississippi`
- `Gibbs-Morris/clean-squad`

The goal is to ground Alfred's Forge decisions in what these systems already prove.

## Mississippi: key capability picture

From the Mississippi README and documentation set, the core pattern is an opinionated end-to-end architecture for event-driven, stateful systems:

- event sourcing + CQRS as the foundation
- Orleans-based execution model for stateful/concurrent behavior
- generated HTTP, SignalR, and client surfaces around domain types
- real-time projection delivery through Inlet/Aqueduct
- Redux-style client state via Reservoir
- composable subsystem model (Brooks, Tributary, DomainModeling, Inlet, Aqueduct, Reservoir, Refraction)

Important implication for Alfred's Forge:

- Mississippi is not only a library source; it already defines a coherent control-plane architecture and consistency model we can build on.

## CleanSquad: key capability picture

From README, docs, and workflow definitions, CleanSquad demonstrates a workflow engine with practical governance and execution features:

- JSON graph workflow model with explicit node kinds (Stage/Fork/Join/Wait/Decision/Exit)
- parallel branch execution with joins and wave-style progression
- persistent run folders (`state.json`, event logs, stage outputs) with resume capability
- decision routing by rules or agent
- policy limits (`maxReviewCycles`, `maxRebuilds`, node/parallelism limits)
- workflow packages combining orchestration, personas, instructions, and RFC 2119 rules
- default clean-agile delivery loop with story/epic routing, architecture gates, three-amigos, specialist reviews, implementation, GitHub sync/poll, wait cycles, and rework loops

Important implication for Alfred's Forge:

- CleanSquad already exercises many of the process-shaping mechanics we need for configurable SDLC and beyond-SDLC process automation.

## Combined reading for Alfred's Forge

The two repos are strongly complementary:

- Mississippi contributes the evented control-plane architecture and runtime coherence
- CleanSquad contributes explicit workflow/process orchestration patterns and policy-enforced execution loops

Together they support the direction we are defining:

- an opinionated dark factory
- with enterprise governance and auditability
- running configurable, policy-bounded processes
- with SDLC as the initial flagship process, not the long-term limit

## Adoption intent (current)

1. Use Mississippi as the foundational control-plane substrate.
2. Pull forward CleanSquad workflow-engine concepts as a first-class product capability.
3. Keep strong default SDLC workflows while allowing configurable process definitions.
4. Preserve policy layering and auditable execution as non-negotiable architecture properties.
