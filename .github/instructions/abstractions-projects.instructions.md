---
applyTo: '**'
---

# Abstractions Projects

Governing thought: Put stable cross-assembly contracts in `{Vendor}.{Area}[.{Feature}].Abstractions` projects so consumers avoid implementation dependencies.

> Drift check: Open referenced scripts and templates under `eng/src/` before use. Treat them as canonical.

## Rules (RFC 2119)

- `*.Abstractions` projects **MUST** contain only public contracts: interfaces, justified abstract bases, DTOs, domain exceptions, and CQRS requests. Why: Keeps packages focused.
- `*.Abstractions` projects **MUST NOT** contain infrastructure, persistence, or hosting code. Why: Keeps consumers independent from implementations.
- Dependency injection in an abstractions project **MUST NOT** embed concrete dependencies. Why: Keeps registration opt-in.
- Generic DI helpers **MAY** live in an abstractions project only when they register the abstraction to a caller-supplied implementation type and add no package dependencies. Why: Allows lightweight registration helpers.
- Main projects **MUST** own implementations and infrastructure. Main projects **MUST** reference their abstractions. Why: Preserves dependency direction.
- Abstractions **MUST NOT** depend on implementations. Downstream consumers **SHOULD** reference abstractions unless they need an implementation. Why: Preserves clean layering.
- Contributors **MUST** create an abstractions project before adding or modifying contracts when all of these conditions apply: cross-assembly or service contracts, multiple existing or expected implementations, and a stable public API. Why: Separates stable contracts early.
- Contributors **SHOULD** create an abstractions project when any of these conditions apply: dependency minimization, testing or mocking, cross-team reuse, or versioning flexibility. Contributors **MAY** omit it when they document the reason. Why: Supports reuse when it has clear value.
- Types that describe *what* to do **SHOULD** live in abstractions. Types that describe *how* to do it **MUST** stay in the main project. Why: Keeps the public programming model stable.
- Abstract base classes intended for external inheritance **MUST** end with `Base`. Authors **MUST** document why the base class supports external inheritance. Why: Makes inheritance intent clear.
- Abstraction project names **SHOULD** follow `{Vendor}.{Area}[.{Feature}].Abstractions`. Why: Makes contracts discoverable.

## Scope and Audience

Use these rules when you create or update a library that exposes contracts across assemblies or services.

## Core Principles

- Keep contracts lightweight and reusable.
- Keep implementations flexible and internal.
- Follow the dependency direction used by `Microsoft.Extensions.*` and Orleans packages.

## References

- Naming: `.github/instructions/naming.instructions.md`

