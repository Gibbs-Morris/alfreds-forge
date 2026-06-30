# Session artifact store and virtual workspace

## The problem

Mississippi's Brooks subsystem handles structured domain events well — small, typed records appended to event streams per aggregate instance. That is the right model for state transitions, decisions, and lifecycle facts.

But workflow execution produces a different class of content: large semi-structured outputs from each stage — agent responses, plans, architecture decision records, increment frames, Three Amigos outputs, review verdicts. These are not domain events. They are artifacts.

CleanSquad handles this today with a run folder on disk (`state.json`, `event.ndjson`, per-stage output files). That works for a CLI tool. It will not work for a distributed, multi-tenant, auditable cloud platform.

## Brooks expansion note

Brooks currently supports Cosmos DB as its only storage provider. The provider model is pluggable (`IBrookStorageProvider`) and storage-agnostic by design.

If Alfred's Forge needs to extend Brooks capabilities — for example, adding new storage providers or extending the event model — that work would need to happen in the Mississippi repo. This is worth tracking as a future concern as the platform's event storage needs evolve (e.g. additional backend providers for self-hosted deployments).

## The virtual artifact store concept

Alfred's Forge needs a separate, logically addressable artifact store alongside the event stream.

Think of it as a virtual workspace per session/run, with a path-addressed structure:

```
/session/{sessionId}/
  /run/{runId}/
    /stage/{nodeId}/
      input.md        ← assembled context/prompt sent to the agent
      output.md       ← agent response
      verdict.md      ← decision or review output
    /plan.md          ← increment frame
    /architecture/
      solution-design.md
      c4-context.md
    /evidence/
      build-summary.md
      validation.md
```

This gives the control plane a navigable, human-readable record of everything that happened in a session — not just what events fired, but what was produced and consumed at each step.

## What belongs where

| Data type | Storage model |
|---|---|
| State transitions, decisions, lifecycle | Brooks event streams |
| Agent prompt inputs | Artifact store (input artifact per stage) |
| Agent outputs / stage results | Artifact store (output artifact per stage) |
| Architecture documents | Artifact store |
| Evidence documents | Artifact store |
| Cost and billing facts | Brooks events + ledger projection |
| Audit facts | Brooks events |
| Session/run metadata | Brooks aggregate state |

Brooks events can reference artifact store paths so the full picture is reconstructable from either direction.

## Storage backing

The artifact store should be backed by blob storage:

- Azure Blob Storage for SaaS and Azure-hosted deployments
- S3 or equivalent for other cloud deployments
- Local or network storage for self-hosted (configurable)

The virtual path model is an abstraction over the underlying store — the platform addresses artifacts by logical path, the storage provider handles the physical layout.

## Why this matters

1. **Audit completeness** — you can reconstruct exactly what was sent to a model and what it returned, for every stage, for every run, forever
2. **Billing accuracy** — input/output token counts are captured alongside the content that drove them
3. **Resumability** — failed or interrupted runs can reload prior stage outputs without re-running completed stages
4. **Human review** — approval gates can present the actual artifact, not just a status
5. **Governance** — the artifact store is the evidence layer that backs every claim in the audit trail

## Design implications

- artifact store access should be governed by RBAC (not all roles see all artifacts)
- artifacts should have a retention policy (configurable per enterprise/tenant)
- large artifacts should not flow through the event bus — events carry references, not content
- the artifact store path model should be stable enough to be used in audit reports and cost breakdowns
