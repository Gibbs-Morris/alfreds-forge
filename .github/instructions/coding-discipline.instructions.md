---
applyTo: 'src/**'
---

# Coding Discipline

Governing thought: Repository applications use Redux on the client, aggregates and projections on the server, and schema-first generation. Refuse deviations.

> Drift check: Review `src/` for reference patterns. Review `src/Reservoir/` for client state and `src/DomainModeling.Runtime/` for server patterns.

## Rules (RFC 2119)

### Client

- Client state **MUST** use Redux. Actions carry intent. State flows down. Reducers transform state. Effects handle side effects. Why: Makes state changes predictable.
- Components **MUST** remain presentational. Components **MUST NOT** call APIs or dispatch actions. Components **MUST** emit events through `EventCallback`. Why: Separates presentation from orchestration.
- Pages **MUST** inherit `StoreComponent`, or `InletComponent` for SignalR. Pages **MUST** dispatch actions and read state through `GetState<T>()`. Why: Pages integrate UI with the store.
- Feature folders **MUST** contain `*State.cs`, `*Action.cs`, `*Reducers.cs`, and `*FeatureRegistration.cs`. Client effects **MAY** use `*ActionEffect.cs` types in `ActionEffects/`, as in Spring. Why: Gives features a consistent structure.
- Ad hoc state management outside Reservoir **MUST NOT** manage domain state. Why: Preserves predictable state flow.

### Server

- Server logic **MUST** live in the domain project under `Aggregates/` or `Projections/`. Why: Keeps business logic in one boundary.
- All write operations **MUST** use aggregates. Every type that writes state **MUST** be an aggregate. Why: Aggregates own state transitions.
- Read-optimized views **MUST** use projections that consume brook events. Why: Preserves CQRS separation.
- Server code **MUST** use actions, reducers, state, and effects:
  - Commands represent intent.
  - Handlers validate commands and emit events.
  - Reducers apply events to state.
  - Event effects handle server side effects.
- Extensions that add behavior **MUST** use event effects or other event-based patterns. Why: Preserves the event-driven architecture.
- Sagas and workflows **MUST** be aggregates with additional orchestration. They **MUST NOT** bypass the aggregate model. Why: Aggregates and effects can coordinate cross-aggregate work.
- Code outside `Domain/` **MUST NOT** contain business rules. Runtime and Gateway hosts **MUST** contain only DI and configuration. Why: Keeps hosts thin and the domain rich.

### Schema First

- New features **MUST** start with domain types: aggregates, commands, events, and projections. These types **MUST** use generation attributes. Why: Makes domain types the source of truth.
- Types **MUST** use available framework generation attributes, such as `[GenerateAggregateEndpoints]`, `[GenerateCommand]`, and `[GenerateProjectionEndpoints]`. Review `src/Inlet.Generators.Abstractions/` for current attributes. Why: Automates boilerplate.
- Generators **MUST** produce client actions, DTOs, and registrations. Manual implementations **MAY** exist only when no generator supports the scenario. Why: Preserves DRY and consistent output.

### Enforcement

- Code that violates these patterns **MUST** be refused or refactored. Why: Architecture consistency is mandatory.
- Pull requests **MUST** be rejected when they add non-aggregate writes, non-Redux client state, or business logic outside Domain. Why: Core projects are reference implementations.
- Feature extensions **MUST** use event effects or new projections instead of imperative additions. Why: Keeps the architecture composable.

## Scope and Audience

Use these rules for all contributors who build Alfred's Forge applications. Core projects are reference implementations and **MUST** demonstrate correct usage.

## Quick Start

### Client pattern

```text
User action -> Component EventCallback -> Page dispatches an action -> Store
Store -> Reducers update state -> Effects call APIs -> New actions
Component reads state through GetState<T>() <- Store
```

### Server pattern

```text
HTTP request -> Controller -> Aggregate grain -> Command handler
Handler validates -> Emits events -> Brook persists events
Brook -> Projection reducers -> Projection state
Projection -> SignalR -> Client subscription
```

### Feature folders

```text
Client/Features/{Feature}/
├── {Feature}State.cs
├── {Action}Action.cs
├── {Feature}Reducers.cs
├── {Feature}FeatureRegistration.cs
└── ActionEffects/
    └── {Action}ActionEffect.cs

{DomainProject}/Aggregates/{Aggregate}/
├── {Aggregate}Aggregate.cs
├── Commands/{Command}.cs
├── Events/{Event}.cs
├── Handlers/{Command}Handler.cs
├── Reducers/{Event}Reducer.cs
└── Effects/{Effect}Effect.cs

{DomainProject}/Projections/{Projection}/
├── {Projection}Projection.cs
└── Reducers/{Event}ProjectionReducer.cs
```

## Core Principles

- Use actions, reducers, state, and effects on both client and server.
- Aggregates own writes. Projections optimize reads.
- Domain attributes define schemas. Generators implement the surrounding code.
- Refuse non-compliant code.

## References

- Framework patterns: `.github/instructions/framework-patterns.instructions.md`
- Alfred's Forge framework: `.github/instructions/alfreds-forge-framework.instructions.md`
- Domain modeling: `.github/instructions/domain-modeling.instructions.md`
- Blazor UX: `.github/instructions/blazor-ux-guidelines.instructions.md`
