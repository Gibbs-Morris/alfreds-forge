---
applyTo: '**/*.{cs,razor,css}'
---

# Namespace and Folder Placement

Governing thought: Deterministic, vertical, repeatable file and namespace layouts keep behavior discoverable across frameworks, tests, and applications.

> Drift check: Review `Directory.Build.props`, `.github/instructions/naming.instructions.md`, and project role guidance in `.github/instructions/projects.instructions.md` before changing placement rules.

## Rules (RFC 2119)

- File placement **MUST** derive deterministically from project identity, namespace segments, and approved archetype patterns. Contributors **MUST NOT** use ad-hoc placement. Why: Keeps navigation predictable.
- Applicable source-file namespace segments **MUST** mirror folder segments. Explicitly approved root exceptions, such as `GlobalUsings.cs` and assembly-info style files, are allowed. Why: Preserves one-to-one discoverability.
- Namespace roots **MUST** come from the effective project `RootNamespace`. Contributors **MUST NOT** assume the repository root `Directory.Build.props` applies when a nearer `Directory.Build.props` overrides it. Why: Actual project identity determines placement.
- Projects **MUST** use the namespace root implied by their own effective `Directory.Build.props`. Contributors **MUST NOT** prepend `Alfred's Forge` by default. Why: Projects define deterministic namespace roots.
- Folder density limits **MUST** enforce **20 files per file type/extension**, such as `.cs`, `.css`, and `.razor`. Mixed types **MAY** exceed 20 total when each type remains <=20. Why: Prevents over-dense folders while allowing mixed UI/component colocation.
- Vertical splitting **MAY** occur before the 20-per-type limit when it materially improves discoverability and repeated pattern alignment. A folder/type exceeding 20 files **MUST** use a vertical split. Why: Enables proactive organization while keeping a hard ceiling.
- Horizontal technical-bucket splits (`Services`, `Models`, `Helpers`, `Utils`, `Common`) **MUST NOT** be introduced unless the segment is an established, explicitly justified domain feature name. Why: Avoids non-domain junk-drawer structures.
- Aggregate, Projection, and Saga code **MUST** use consistent archetype patterns across `src/**` and `tests/**`. Tests **SHOULD** mirror production vertical structure under test namespace roots. Why: Repetition improves onboarding and maintenance.
- Archetype-conformance deviations **MUST** include a reason, approval metadata, and evidence. Contributors **MUST NOT** accept deviations for convenience or preference. Why: Allows edge cases without policy erosion.
- Exception usage **SHOULD** remain <=1% of mapped files. Execution **MUST** stop for rules reassessment and a recorded decision when usage exceeds 1%. Why: Keeps exceptions rare and intentional.
- Conflict resolution precedence **MUST** be valid bounded-context naming, then archetype consistency, then per-type cap splitting inside the archetype branch, then deterministic qualifiers. Why: Prevents inconsistent tie-break behavior.
- Deterministic placement enforcement **MUST** include machine-readable inventories/reconciliation outputs. Gates **MUST** fail for unresolved collisions, cap breaches, missing approvals, or missing mappings. Why: Ensures no file is silently skipped.

## Scope and Audience

Contributors moving or adding source files under `src/` and `tests/` where namespace/folder consistency is required.

## At-a-Glance Quick-Start

- Start with project identity and namespace, then map matching folder segments.
- Keep each folder at <=20 files per extension.
- Split vertically by behavior context before creating horizontal utility buckets.
- Use Aggregate/Projection/Saga archetypes consistently across framework and tests.
- Record and approve any true edge-case deviation.

## Placement Decision Tree (Deterministic)

1. Exclude generated/intermediate files (`obj/**`, `bin/**`, generated outputs).
2. Determine the base namespace from project identity and role.
3. Apply the archetype branch (`Aggregates`, `Projections`, `Sagas`) when the artifact kind matches.
4. Enforce the per-type cap and split vertically within the branch when needed or proactively when clearer.
5. Validate conformance, collisions, and approvals, then fail gates on unresolved issues.

## Archetype Segment Naming (Canonical)

| Artifact | Canonical Segments |
|----------|--------------------|
| Aggregate | `Aggregates/<Name>/Commands`, `Events`, `Reducers`, `Effects`, `State`, `Handlers`, `Registrations` |
| Projection | `Projections/<Name>/Reducers`, `Effects`, `State`, `Handlers`, `Contracts`, `Registrations` |
| Saga | `Sagas/<Name>/Commands`, `Events`, `Reducers`, `Effects`, `State`, `Compensation`, `Registrations` |

## Core Principles

- Determinism beats preference.
- Vertical slices beat horizontal technical buckets.
- Edge cases are allowed, but governance must stay strict.

## References

- Root namespace sources:
  - `Directory.Build.props`
  - nearest project-local `Directory.Build.props` (if present)
- Naming policy: `.github/instructions/naming.instructions.md`
- Project roles and boundaries: `.github/instructions/projects.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
