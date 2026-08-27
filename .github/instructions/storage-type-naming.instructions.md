---
applyTo: '**/*.cs'
---

# Storage Type Naming with Attributes

Governing thought: Use stable, versioned attribute names
(`APP.MODULE.NAME.Vn`) for persisted types. This lets you refactor code without
breaking stored data.

> Drift check: Check current registries and serialization code before changing
> attributes or registries.

## Rules (RFC 2119)

- Persisted types, such as events, snapshots, and commands, **MUST** have a
  naming attribute, such as `[EventStorageName]` or `[SnapshotStorageName]`.
  The attribute **MUST** set an explicit `version` parameter. Why: Gives each
  type a stable, versioned identity.
- The computed name **MUST** use `APPNAME.MODULENAME.NAME.Vn`. The `app`,
  `module`, and `name` parts **MUST NOT** change once persisted. Schema
  evolution **MUST** increment `version`. Why: Keeps stored data compatible.
- Each attribute value **MUST** be globally unique. Registries, such as
  `IEventTypeRegistry`, **MUST** resolve names to types and types to names. All
  persisted types **SHOULD** be registered at startup through scanning. Why:
  Enables deterministic resolution.
- Developers **MAY** freely refactor class and record names when the attribute
  stays the same. Why: Separates code identity from storage identity.
- When developers remove members from types backed by persisted data in a real
  (non-test) store, they **SHOULD** keep prior versions and types available for
  reads. Before version 1.0, developers **MAY** skip this when no persisted
  production data exists. See
  `.github/instructions/backwards-compatibility.instructions.md`. Why:
  Supports backward compatibility for deployed stores. This guidance does not
  apply to pre-release iteration.

## Scope and Audience

These rules apply to developers who create or consume persisted types in
event-sourced or storage components.

## At-a-Glance Quick-Start

- Add `[EventStorageName("ORDER","FULFILLMENT","SHIPPED", version: 1)]` (or the
  appropriate attribute) to persisted types.
- Keep the app, module, and name parts stable. Increase `version` for breaking changes.
- Use registries to resolve names to types and types to names. Scan assemblies at
  startup.

## Core Principles

- Attribute values define storage identity. Code names can change.
- Explicit versions make schema evolution safe and easy to audit.

## References

- Orleans serialization: `.github/instructions/orleans-serialization.instructions.md`
