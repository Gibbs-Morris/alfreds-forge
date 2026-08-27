---
applyTo: '**'
---

# Backwards Compatibility Policy

Governing thought: Before version 1.0.0, allow breaking changes and do not preserve patterns that exist only on the current branch.

> Drift check: Check `GitVersion.yml` and its `next-version` before applying this policy.

## Rules (RFC 2119)

- Before version 1.0.0, backwards compatibility **MUST NOT** constrain changes. Public APIs, contracts, event shapes, serialization layouts, and behavior **MAY** change when CI/CD passes its build and test gates. Why: The framework is pre-release.
- Agents **MUST NOT** add compatibility shims, wrappers, adapters, or V2 types for patterns introduced earlier on the same feature or chore branch. Only contracts on `main` define the compatibility baseline. Why: Branch-local patterns will not exist after merge.
- When a branch contract change breaks repository applications or tests, the same pull request **MUST** update those consumers. Why: CI/CD must stay green.
- After version 1.0.0, serialization member IDs and storage attribute names **MUST NOT** change without a versioned migration path. This rolling-update rule applies only after version 1.0.0. Why: Production deployments need orderly transitions.
- A compatibility wrapper, adapter, or shim added for a rolling update **MUST** have `[Obsolete("Remove in vX.0")]`. Authors **SHOULD** remove it in the next planned major release. Why: Keeps compatibility code short-lived.
- `[EventStorageName]` and `[SnapshotStorageName]` values **MUST NOT** change after a real, non-test store persists data. This rule applies at every version. Why: A changed storage name can orphan existing data.
- When unsure whether an old pattern exists on `main`, agents **MUST** compare the branch with `main` before adding compatibility code. Why: Prevents unnecessary shims.

## Scope and Audience

Use these rules for all contributors and agents who change code in this repository. Before version 1.0.0, this policy overrides guidance that requires backwards compatibility by default.

## Quick Start

- Check `GitVersion.yml`. If `next-version` is below `1.0.0`, do not treat compatibility as a constraint.
- Preserve only contracts that exist on `main`.
- Update all repository consumers in the same pull request when a contract change breaks them.
- Mark rolling-update shims with `[Obsolete]` and plan their removal.
- Never change persisted event or snapshot storage names.

## Core Principles

- Prefer shipping quality over compatibility ceremony while the project is pre-release.
- Treat branch work as temporary. Use `main` as the compatibility baseline.
- Keep compatibility shims short-lived.
- Treat persisted storage identity as a data-integrity rule, not an API-compatibility rule.

## References

- GitVersion config: `GitVersion.yml` (`next-version`)
- Orleans serialization: `.github/instructions/orleans-serialization.instructions.md`
- Storage naming: `.github/instructions/storage-type-naming.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
