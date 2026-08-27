---
applyTo: '**/*.cs'
---

# Domain Modeling with Alfred's Forge Event Sourcing

Governing thought: Use consistent, attribute-driven domain models with immutable aggregates, typed handlers, and projection reducers that follow Alfred's Forge patterns.

> Drift check: Review `src/DomainModeling.Abstractions` and `src/Tributary.Abstractions` for base class signatures before implementing handlers or reducers.

## Rules (RFC 2119)

### Aggregate Types

- Aggregates **MUST** be `internal sealed record` types with `[BrookName]`, `[SnapshotStorageName]`, `[GenerateSerializer]`, and `[Alias]` attributes. Why: These attributes enable event sourcing, serialization, and stable storage identity.
- Aggregate properties **MUST** use `[Id(n)]` starting at 0 with unique sequential values. Aggregate types **MUST** use `{ get; init; }` properties with sensible defaults. Why: Orleans serialization requires explicit member ordering.
- Aggregates **SHOULD** include a sentinel property, such as `IsCreated` or `IsInitialized`, to detect first-time creation. Why: Command handlers must distinguish new and existing aggregates.

### Command Types

- Commands **MUST** be `internal sealed record` types with `[GenerateSerializer]` and `[Alias]` attributes. Why: These attributes enable Orleans serialization and stable identity.
- Command properties **MUST** use the `required` modifier and `[Id(n)]` attributes. Command names **SHOULD** be verb phrases, such as `CreateChannel` and `UpdateDisplayName`. Why: This enforces valid construction and clear intent.

### Event Types

- Events **MUST** be `internal sealed record` types with `[EventStorageName]`, `[GenerateSerializer]`, and `[Alias]` attributes. Why: These attributes enable stable event storage and serialization.
- Event names **MUST** use past tense, such as `ChannelCreated` and `MessageSent`. Event properties **MUST** use the `required` modifier and `[Id(n)]` attributes. Why: Events represent facts that have occurred.

### Command Handlers

- Handlers **MUST** inherit from `CommandHandlerBase<TCommand, TAggregate>` and be `internal sealed class` types. Why: The base class provides type-safe dispatch and consistent behavior.
- Handler classes **MUST** be named `{Command}Handler`, such as `CreateChannelHandler`. Handlers **MUST** implement `HandleCore()` returning `OperationResult<IReadOnlyList<object>>`. Why: The naming convention enables discovery, and the return type supports multiple events.
- Handlers **MUST** validate command properties before checking aggregate state. Invalid commands **MUST** return `AggregateErrorCodes.InvalidCommand`. State conflicts **MUST** return `AggregateErrorCodes.InvalidState`. Why: Clear error categories aid debugging.

### Reducers (Aggregate State)

- Reducers **MUST** inherit from `EventReducerBase<TEvent, TAggregate>` and be `internal sealed class` types. Why: The base class provides type-safe reduction and enforces immutability.
- Reducer classes **MUST** be named `{Event}Reducer`, such as `ChannelCreatedReducer`. Reducers **MUST** return a new instance using `with` expressions or record constructors. Why: Runtime checks enforce immutability.
- Reducers **MUST NOT** mutate the input state. The base class throws if a reducer returns the same instance. Why: Event sourcing requires pure functions.

### Projection Types

- Projections **MUST** be `internal sealed record` types with `[BrookName]`, `[SnapshotStorageName]`, `[GenerateSerializer]`, and `[Alias]` attributes. Why: Projections use the same attributes as aggregates for read-optimized views.
- Projection types **SHOULD** be named `{Name}Projection`, such as `UserProfileProjection` and `ChannelMemberListProjection`. Why: The suffix distinguishes projection state from aggregate state.

### Projection Reducers

- Projection reducers **MUST** inherit from `EventReducerBase<TEvent, TProjection>` and be `internal sealed class` types. Why: The base class provides the same dispatch pattern as aggregate reducers.
- Projection reducer classes **MUST** be named `{Event}ProjectionReducer`, such as `UserRegisteredProjectionReducer` rather than `UserRegisteredReducer`. Why: The name distinguishes projection reducers when multiple reducers consume the same event.
- Projection reducers **SHOULD** live under `Projections/{ProjectionName}/Reducers/`. Why: This organizes reducers by projection type.

### Attribute Values

- `[BrookName]` **MUST** use the format `("APPNAME", "MODULENAME", "NAME")` with uppercase alphanumeric values only. Why: The attribute validates this format at runtime.
- `[EventStorageName]` and `[SnapshotStorageName]` **MUST** use the format `("APPNAME", "MODULENAME", "NAME", version: n)` with version defaulting to 1. Storage names **MUST NOT** change once persisted. Why: This supports safe refactoring while preserving storage compatibility.
- `[Alias]` values **MUST** match the fully qualified type name, such as `[Alias("Contoso.Domain.Channel.Events.ChannelCreated")]`. Why: This provides stable Orleans serialization identity across refactoring.

### Registration

- Domain registration **MUST** follow the pattern `Add{Domain}Domain()` as a public entry point. The entry point **MUST** call private `Add{Aggregate}Aggregate()` and `Add{Projection}Projection()` methods. Why: Hierarchical registration keeps DI discoverable.
- Registration order **MUST** be event types (`AddEventType<>`), command handlers (`AddCommandHandler<>`), reducers (`AddReducer<>`), and snapshot converter (`AddSnapshotStateConverter<>`). Why: Dependencies must be registered before dependents.
- Registration classes **MUST** be named `{Domain}Registrations` and declared as `public static class` types. Why: The naming convention enables discovery.

## Scope and Audience

Developers implementing domain models with Alfred's Forge event sourcing in repository applications.

## At-a-Glance Quick-Start

### Folder Structure

```text
{Domain}/
|-- {Aggregate}/
|   |-- {Aggregate}Aggregate.cs
|   |-- Commands/
|   |   `-- {Action}.cs
|   |-- Events/
|   |   `-- {ActionPastTense}.cs
|   |-- Handlers/
|   |   `-- {Action}Handler.cs
|   `-- Reducers/
|       `-- {Event}Reducer.cs
|-- {ProjectionName}Projection/
|   |-- {ProjectionName}Projection.cs
|   `-- Reducers/
|       `-- {Event}ProjectionReducer.cs
`-- {Domain}Registrations.cs
```

### Required Attributes Checklist

| Type | Required Attributes |
|------|---------------------|
| Aggregate | `[BrookName]`, `[SnapshotStorageName]`, `[GenerateSerializer]`, `[Alias]` |
| Command | `[GenerateSerializer]`, `[Alias]` |
| Event | `[EventStorageName]`, `[GenerateSerializer]`, `[Alias]` |
| Projection | `[BrookName]`, `[SnapshotStorageName]`, `[GenerateSerializer]`, `[Alias]` |

### Naming Conventions

| Type | Pattern | Example |
|------|---------|---------|
| Aggregate | `{Name}Aggregate` | `ChannelAggregate` |
| Command | `{Verb}{Noun}` | `CreateChannel` |
| Event | `{Noun}{PastVerb}` | `ChannelCreated` |
| Handler | `{Command}Handler` | `CreateChannelHandler` |
| Reducer | `{Event}Reducer` | `ChannelCreatedReducer` |
| Projection | `{Name}Projection` | `UserProfileProjection` |
| Projection Reducer | `{Event}ProjectionReducer` | `UserRegisteredProjectionReducer` |

## Core Principles

- Immutable record types with explicit serialization ensure deterministic replay.
- Attribute-based storage names decouple code identity from persistence identity.
- Base classes enforce immutability and type dispatch while providing extension points.
- Hierarchical registration mirrors domain structure and enables composability.

## References

- Coding discipline: `.github/instructions/coding-discipline.instructions.md`
- Framework patterns (src): `.github/instructions/framework-patterns.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Orleans serialization: `.github/instructions/orleans-serialization.instructions.md`
- Storage naming: `.github/instructions/storage-type-naming.instructions.md`
- Service registration: `.github/instructions/service-registration.instructions.md`
