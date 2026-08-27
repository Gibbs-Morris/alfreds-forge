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
- Developers **MUST** inject every dependency, including `IGrainContext`, through a constructor. Why: Makes dependencies explicit.
- Developers **MUST** store injected dependencies in DI get-only properties. Why: Aligns with shared guardrails and testability.
- Developers **MUST NOT** use private readonly fields for DI. Why: Aligns with shared guardrails and testability.
- `using Orleans.Runtime;` **MUST** be included. Why: Provides access to grain helpers.
- Developers **MUST** call Orleans extension methods with `this.` qualification. Why: Ensures access to grain helpers.
- Grain interfaces **MUST** be public only when external callers need them. Otherwise, keep them internal. Why: Controls API surface.
- Developers **SHOULD** migrate existing grains that inherit from `Grain` to POCO. Why: Keeps patterns consistent and discoverable.
- Abstract classes that inherit from `IGrainBase` **MUST** end with `Base`. Why: Keeps abstract grain names discoverable.
- Developers **SHOULD** track deferred migrations in `.scratchpad/tasks`. Why: Makes deferred work visible.
- When converting from `Grain<TState>`, developers **SHOULD** inject `IPersistentState<TState>` instead. Why: POCO patterns handle state through DI.

## Scope and Audience

Developers implementing Orleans grains and grain interfaces.

## At-a-Glance Quick-Start

- Implement `IGrainBase`.
- Add `public IGrainContext GrainContext { get; }`.
- Inject dependencies through the constructor.
- Store injected dependencies in get-only DI properties.
- Include `using Orleans.Runtime;`.
- Qualify Orleans helper calls with `this.`, such as `this.GetPrimaryKeyString()` and `this.DeactivateOnIdle()`.
- Keep concrete grains `sealed`.
- Expose grain interfaces publicly only for external callers.
- Track deferred legacy `Grain` migrations in `.scratchpad/tasks`.

## Core Principles

- Prefer composition over inheritance. POCO grains are easier to test and refactor.
- Use explicit DI and extension methods. They keep behavior clear and analyzer-friendly.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Serialization: `.github/instructions/orleans-serialization.instructions.md`
