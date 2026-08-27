---
applyTo: '.github/agents/*planner*.agent.md,.github/agents/*build*.agent.md'
---

# Agent Planning Methodology

Governing thought: Planning and building agents use the same verification loop, review roster, and artifact names.

> Drift check: Read `flow-planner.agent.md` and `epic-planner.agent.md` before changing this guidance. Each agent file is authoritative for its workflow.

## Rules (RFC 2119)

- Planning agents **MUST** use the Chain-of-Verification (CoV) loop for every non-trivial claim: hypothesize, question, gather evidence, triangulate with an independent source, conclude with a confidence rating, and record impact. Why: Prevents speculative plans.
- Agents **MUST** verify each non-trivial claim with at least two independent sources. Sources can be files, modules, tests, docs, or configs. Agents **MUST** label a single-source claim `Single-source` and state what would confirm it. Why: Reduces false assumptions.
- Planning agents **MUST** create all artifacts in the canonical order and with the canonical names below. Why: Lets `flow` and `epic` plans use the same structure.
- Persona reviews **MUST** total twelve. Five reviews **MUST** use enterprise generalist personas. Seven reviews **MUST** use Alfred's Forge framework specialist personas. Each reviewer **MUST** act as if they read only the plan and repository. Why: Covers business, architecture, and domain risks.
- Each review item **MUST** state the issue, why it matters, the proposed change, evidence or a marked inference, and a confidence rating. Why: Makes feedback actionable and traceable.
- The synthesis **MUST** deduplicate all twelve reviews. It **MUST** classify items as Must, Should, Could, or Won't. It **MUST** give an Accept or Reject rationale and required edits for each item. Why: Clarifies priority and prevents duplicate work.
- Plans, sub-plans, and instruction extractions **MUST NOT** contain secrets, PII, or internal-only URLs. Why: `epic` plans can be committed to `main` and all plans can be reviewed by many agents.
- Artifact files **MUST** include a short CoV section with key claims, evidence, and confidence when applicable. Why: Preserves traceability.

## Scope and Audience

Use these rules for `flow Planner`, `epic Planner`, `flow Builder`, and `epic Builder` agents in this repository.

## Quick Start

1. Apply CoV to every significant claim.
2. Create artifacts in this order: intake, repository findings, clarifying questions, decisions, draft plan, twelve reviews, synthesis, and final `PLAN.md`.
3. At finalization, move the remaining audit trail into `audit/` with the `audit-` prefix.
4. Keep `PLAN.md` at the plan root.
5. In `epic` plans, also keep required `sub-plans/`, `dependencies.json`, and other execution artifacts at the root.

## Chain of Verification

Record these items for each important claim:

1. Claims or hypotheses.
2. Verification questions.
3. Repository evidence with file paths and line ranges when possible.
4. A second independent source, or `Single-source` with the missing confirmation.
5. A conclusion with High, Medium, or Low confidence and the evidence that would increase confidence.
6. The impact on the plan.

## Canonical Artifacts

| Order | Name | Content |
|---|---|---|
| 1 | `00-intake.md` | Objective, non-goals, constraints, assumptions, and open questions |
| 2 | `01-repo-findings.md` | Repository evidence with two-source verification for each finding |
| 3 | `02-clarifying-questions.md` | Questions answered from the repository and ranked questions for the user |
| 4 | `03-decisions.md` | Decision, chosen option, rationale, evidence, risks, and confidence |
| 5 | `04-draft-plan.md` | Solution plan with architecture, contracts, work, tests, observability, and rollout |
| 6 | `review-01` to `review-12` | Twelve persona reviews |
| 7 | `review-13-synthesis.md` | Deduplicated Must, Should, Could, and Won't feedback |
| 8 | `PLAN.md` | Standalone final plan |

At finalization, move every artifact that is not required at the root into `audit/` and prefix its name with `audit-`. For `flow`, only `PLAN.md` stays at the root. For `epic`, keep `PLAN.md`, `sub-plans/`, `dependencies.json`, and other required execution artifacts at the root.

## Persona Review Roster

### Enterprise generalists: reviews 01 to 05

| Review | Persona | Focus |
|---|---|---|
| 01 | Marketing and Contracts | Public naming, contract discovery, package naming, changelog, and migration communication |
| 02 | Solution Engineering | Business adoption, ecosystem compliance, onboarding, and third-party integration |
| 03 | Principal Engineer | Repository consistency, maintenance, technical risk, SOLID, tests, and backwards compatibility |
| 04 | Technical Architect | Architecture, module boundaries, dependency direction, abstraction layers, and extensibility |
| 05 | Platform Engineer | Telemetry, structured logging, distributed tracing, alerts, failure modes, and deployment safety |

### Alfred's Forge specialists: reviews 06 to 12

| Review | Persona | Focus |
|---|---|---|
| 06 | Distributed Systems Engineer | Orleans lifecycle, reentrancy, single activation, placement, message order, and turn concurrency |
| 07 | Event Sourcing and CQRS Specialist | Event evolution, immutable storage names, pure reducers, aggregate invariants, projection rebuilds, snapshot versions, and idempotency |
| 08 | Performance and Scalability Engineer | Hot-path allocations, grain activation, Cosmos RU use, serialization, N+1 patterns, back-pressure, and throughput |
| 09 | Developer Experience Reviewer | API ergonomics, pit-of-success design, errors, IntelliSense, registration, and migration friction |
| 10 | Security Engineer | Authentication, trust boundaries, claims, tenant isolation, input validation, serialization attacks, and OWASP alignment |
| 11 | Source Generator and Tooling Specialist | Roslyn incremental generators, caching, diagnostics, compile speed, analyzer interaction, and IDE experience |
| 12 | Data Integrity and Storage Engineer | Cosmos partition keys, cross-partition cost, immutable storage names, stream consistency, snapshot correctness, and idempotent writes |

## Core Principles

- Use evidence instead of assumptions. Cite repository paths or mark claims `Single-source`.
- Make plans interchangeable. Any builder agent must be able to execute a plan from either planner family.
- Use twelve reviews to test the plan from five enterprise and seven Alfred's Forge perspectives.

## References

- Instruction authoring: `.github/instructions/authoring.instructions.md`
- RFC keywords: `.github/instructions/rfc2119.instructions.md`
