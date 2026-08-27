---
applyTo: '**/*.cs'
---

# Orleans Serialization

Governing thought: Use explicit `[GenerateSerializer]`, `[Id]`, and `[Alias]` with progressive versioning to keep Orleans payloads compatible across deployments.

> Drift check: Review Orleans analyzer settings and package references (`Microsoft.Orleans.Sdk`, `Microsoft.Orleans.CodeGenerator.MSBuild`) before changes.

## Rules (RFC 2119)

- Serializable types **MUST** use `[GenerateSerializer]`. Why: Enables version-tolerant code generation.
- Every serialized member **MUST** use `[Id(n)]`. Why: Enables version-tolerant code generation.
- Types **MUST NOT** use implicit serialization. Why: Enables version-tolerant code generation.
- Each type **MUST** include a globally unique `[Alias]`. Why: Supports safe renames and deterministic layout.
- Member IDs **MUST** start at 0 for each inheritance level. Why: Supports deterministic layout.
- Member IDs **MUST** be unique within each inheritance level. Why: Supports deterministic layout.

### Pre-1.0 serialization freedom

- While the repository is pre-1.0 (see `.github/instructions/backwards-compatibility.instructions.md`), agents **MAY** change member IDs freely. Why: The repository is pre-release and has no rolling deployments to protect.
- While the repository is pre-1.0 (see `.github/instructions/backwards-compatibility.instructions.md`), agents **MAY** change type shapes freely. Why: The repository is pre-release and has no rolling deployments to protect.
- Agents **MUST NOT** add compatibility shims for serialization layouts that exist only on the current branch. Why: The repository is pre-release and has no rolling deployments to protect.

### Post-1.0 serialization stability

- Once the repository reaches 1.0+, existing member IDs **MUST NOT** change. Why: Protects rolling deployments.
- Once the repository reaches 1.0+, existing member IDs **MUST** remain unique and unchanged for their level. Why: Protects rolling deployments.
- Once the repository reaches 1.0+, developers **MUST NOT** make breaking type changes, such as record-to-class or signedness changes. Why: Protects rolling deployments.
- Once the repository reaches 1.0+, new members **MUST** use unused IDs. Why: Protects rolling deployments.
- Post-1.0 versioning **MUST** use additive and compatible changes. Why: Keeps serialized data readable.
- Post-1.0 member removals **SHOULD** use `[NonSerialized]` or `[Obsolete]`. Why: Keeps serialized data readable.
- Post-1.0 changes **SHOULD** widen numeric types and make properties nullable instead of narrowing them. Why: Keeps serialized data readable.

### Analyzer and review requirements

- Projects **MUST** include the required Orleans packages. Why: Catches issues at build time.
- Projects **MUST** build with analyzers as errors. Why: Catches issues at build time.
- Reviewers **SHOULD** verify compatibility. Why: Catches issues at review time.
- Agents **SHOULD** create focused `.scratchpad/tasks/pending` items for analyzer violations or missing attributes that they do not fix immediately. Why: Tracks debt.

## Scope and Audience

Developers creating or changing Orleans-serialized types.

## At-a-Glance Quick-Start

- Add `[GenerateSerializer]`, `[Alias("Namespace.TypeName")]` (fully qualified type name), `[Id(n)]` (starting at 0) to members.
- Keep IDs stable.
- Add new members with new IDs.
- Prefer widening numeric types and making properties nullable.
- Include Orleans SDK/codegen packages.
- Treat analyzer warnings as errors.

## Core Principles

- Explicit, stable identifiers prevent serialization breaks.
- Additive versioning supports rolling upgrades after 1.0.
- Pre-1.0 policy allows free changes (see `.github/instructions/backwards-compatibility.instructions.md`).

## References

- Orleans serialization docs: <https://learn.microsoft.com/dotnet/orleans/serialization>
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Domain modeling: `.github/instructions/domain-modeling.instructions.md`
