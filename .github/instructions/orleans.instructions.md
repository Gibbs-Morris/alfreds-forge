---
applyTo: '**/*.cs'
---

# Orleans POCO Grains

Governing thought: Use Orleans 7+ POCO grains with `IGrainBase`, constructor injection, and extension methods. Never inherit from `Grain`.

> Drift check: Review Orleans settings/packages in `Directory.Build.props` before editing grains.

## Rules (RFC 2119)

- Grains **MUST** implement `IGrainBase` with `public IGrainContext GrainContext { get; }`. Why: Follows Orleans POCO guidance.
- Grains **MUST NOT** inherit from `Grain`. Why: Prevents unintended inheritance.
- Concrete grains **MUST** be `sealed`. Why: Prevents unintended inheritance.
- Constructors **MUST** receive all dependencies, including `IGrainContext`. Why: Makes dependencies explicit.
- Injected dependencies **MUST** use the DI get-only property pattern. Private readonly fields for DI **MUST NOT** be used. Why: Aligns with shared guardrails and testability.
- `using Orleans.Runtime;` **MUST** be included. Why: Provides access to grain helpers.
- Orleans extension methods **MUST** be called with `this.` qualification. Why: Ensures access to grain helpers.
- Grain interfaces **MUST** be public only when external callers need them. Otherwise, keep them internal. Why: Controls API surface.
- Existing grains that inherit from `Grain` **SHOULD** be migrated to POCO. Why: Keeps patterns consistent and discoverable.
- Abstract classes that inherit from `IGrainBase` **MUST** end with `Base`. Why: Keeps abstract grain names discoverable.
- Deferred migrations **SHOULD** be tracked in `.scratchpad/tasks`. Why: Makes deferred work visible.
- When converting from `Grain<TState>`, developers **SHOULD** inject `IPersistentState<TState>` instead. Why: POCO patterns handle state through DI.

## Scope and Audience

Developers implementing Orleans grains and grain interfaces.

## At-a-Glance Quick-Start

- Implement `IGrainBase`; add `GrainContext` property; inject dependencies in the constructor with get-only properties.
- Add `using Orleans.Runtime;` and call helpers with `this.`, such as `this.GetPrimaryKeyString()` and `this.DeactivateOnIdle()`.
- Keep concrete grains sealed; limit public interfaces to external needs.
- Track migrations for legacy `Grain` inheritance in `.scratchpad/tasks` if not fixed immediately.

## Core Principles

- Prefer composition over inheritance. POCO grains are easier to test and refactor.
- Use explicit DI and extension methods. They keep behavior clear and analyzer-friendly.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Serialization: `.github/instructions/orleans-serialization.instructions.md`

