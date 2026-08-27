---
applyTo: 'src/**'
---

# Framework Patterns (Alfred's Forge Core)

Governing thought: Framework code minimizes cognitive overload through consistent primitives—aggregates, projections, commands, events, actions, reducers, and effects—while internal flexibility serves external consistency.

> Drift check: Review existing framework implementations in `src/Reservoir.*/`, `src/DomainModeling.Runtime/`, and `src/Inlet.*/` before adding new patterns.

## Rules (RFC 2119)

### Developer Experience First

- Framework **MUST** minimize cognitive overload by reusing the same primitives everywhere. Why: Developers learn once and apply everywhere.
- New framework features **MUST** express themselves through existing primitives when possible: aggregates, projections, commands, events, actions, reducers, and effects. Why: Consistency reduces the learning curve.
- New primitives **SHOULD** use the same shape when necessary: immutable records, handlers/reducers, and DI registration patterns. Why: Familiarity accelerates adoption.
- Framework APIs **MUST** start with the developer's perspective, then be implemented. Why: Developer experience drives design.

### Testability by Design

- **Developer-authored primitives** **MUST** be testable without infrastructure dependencies: aggregates, projections, command handlers, reducers, and effects. Why: Business logic stays pure.
- Command handlers, reducers, and effects **MUST** be pure functions or simple classes with injected dependencies. Why: They must have no static state or hidden coupling, and this enables isolation.
- The Domain project pattern demonstrates the goal because almost all its business logic is testable without Orleans, Cosmos, HTTP, or SignalR. Why: The reference implementation proves the pattern.
- Framework **SHOULD** provide test harnesses such as `AggregateTestHarness` and `StoreScenario` with Given/When/Then semantics. Why: Harnesses reduce test boilerplate.
- New framework primitives that developers extend **MUST** treat testability as a first-class design constraint. Why: Hard-to-test developer code requires a redesign.
- **Framework infrastructure** (developer tools, diagnostics, and configuration toggles) has different constraints. Users enable or disable these features rather than write code on top of them. Standard testing practices apply, but testability without infrastructure is less critical. Why: These are framework internals, not developer extension points.

### Pattern Consistency

- Framework APIs **MUST** expose the patterns developers use: actions, reducers, state, and effects for the client; commands, handlers, events, and reducers for the server. Why: Developers learn one model.
- Internal framework code **SHOULD** follow these patterns when practical. Why: Consistency aids maintenance.
- Framework code **MAY** deviate when infrastructure requires it, including low-level Orleans integration, source generators, and storage providers. Why: Framework code builds the patterns and cannot always use them.
- Code deviations **MUST** include a justification in comments. Why: Future maintainers need context.

### Extension Points

- Framework **MUST** provide base classes and interfaces that guide correct patterns, including `CommandHandlerBase`, `EventReducerBase`, `ActionEffectBase`, and `StoreComponent`. Why: These create a pit of success.
- Base classes **SHOULD** enforce invariants at runtime, including reducer immutability checks and handler return type validation. Why: Runtime checks catch pattern violations early.
- Framework **SHOULD** expose customization hooks without requiring pattern abandonment. Why: Edge cases should not force developers out of the architecture.

### Source Generators

- Generators **MUST** produce code that follows the patterns developers would write manually. Why: Generated code is the reference implementation.
- Generator output **SHOULD** be readable and match hand-written style. Why: Developers debug generated code.
- `[PendingSourceGenerator]` marks hand-written code awaiting generation. Why: These reference patterns support generator validation and track the generation backlog.

### Abstractions

- Public framework APIs **MUST** live in `*.Abstractions` projects when they define contracts. Why: Consumer packages stay lightweight.
- Implementation details **MUST** stay in main projects. Why: Abstractions **MUST NOT** depend on implementations and layering stays clean.
- Framework **SHOULD** use the DI patterns it prescribes: `private Type Name { get; }` and no service locator. Why: The framework must dogfood its guidance.

### Client State (Reservoir)

- `IStore`, `IAction`, `IFeatureState`, `IActionReducer`, and `IActionEffect` define the client pattern. Why: These are the core contracts.
- Framework **MAY** use internal state management for its own concerns, such as Inlet connection state, but **SHOULD** expose it through the client pattern when beneficial. Why: This balances practicality with consistency.
- Built-in features, including Navigation and Lifecycle, **MUST** follow the actions/reducers pattern. Why: They are reference implementations.

### Server State (Event Sourcing)

- `ICommandHandler`, `IEventReducer`, and `IEventEffect` define the server pattern. Why: These are the core contracts.
- `IRootCommandHandler` and `IRootReducer` compose handlers and reducers for dispatch. Why: Composition provides a single entry point.
- Framework grains **SHOULD** follow aggregate patterns where applicable. Why: Consistency reduces cognitive load.
- Infrastructure grains, including worker grains and coordination grains, **MAY** use custom patterns when orchestrating aggregates. Why: Infrastructure enables the patterns.

### Flexibility for Edge Cases

- Framework code **MAY** implement custom solutions when a pattern cannot support a requirement. Why: The framework must remain complete.
- Custom solutions **SHOULD** expose familiar developer-facing interfaces. Why: Developer-facing API consistency matters more than internal consistency.
- New edge-case patterns **SHOULD** become generalized when reusable. Why: One-offs become patterns if needed twice.

## Scope and Audience

These rules apply to contributors building or extending the Alfred's Forge framework under `src/`. These rules guide the infrastructure that enables strict and repeatable architectural patterns.

## At-a-Glance Quick-Start

### Pattern Hierarchy

```text
Framework exposes patterns → Applications follow patterns strictly
Framework MAY deviate internally → But MUST NOT leak deviations to developers
```

### When to Follow vs Deviate

| Scenario | Guidance |
|----------|----------|
| New feature for developers | MUST follow patterns |
| Internal plumbing | SHOULD follow; MAY deviate with justification |
| Source generators | MUST produce pattern-compliant code |
| Orleans integration | MAY use Orleans patterns directly |
| Storage providers | MAY use provider-specific patterns |

### Framework Contracts

```text
Client: IStore, IAction, IFeatureState, IActionReducer, IActionEffect
Server: ICommandHandler, IEventReducer, IEventEffect, IAggregate, IProjection
Generators: [GenerateCommand], [GenerateAggregateEndpoints], [GenerateProjectionEndpoints], etc.
```

Review `src/Inlet.Generators.Abstractions/` for the latest generation attributes.

## Core Principles

- **Developer experience first**: Minimize cognitive overload.
- **Same primitives everywhere**: Use aggregates, projections, commands, events, actions, reducers, and effects.
- **Testability by design**: Keep business logic testable without infrastructure; see Domain implementations under `src/`.
- Base classes enforce correct usage.
- Generated code matches hand-written style.
- Internal flexibility serves external consistency, and deviations require documentation.

## References

- Coding discipline: `.github/instructions/coding-discipline.instructions.md`
- Abstractions: `.github/instructions/abstractions-projects.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
