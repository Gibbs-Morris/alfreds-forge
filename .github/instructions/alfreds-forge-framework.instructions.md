---
applyTo: 'src/**'
---

# Alfred's Forge Framework Usage

Governing thought: Build applications with Alfred's Forge, source generation by default, four projects, Redux-style Reservoir state, and small componentized projections over brooks.

> Drift check: Review `src/` and existing Domain implementations before adding features. Established patterns are authoritative.

## Rules (RFC 2119)

### Instruction Maintenance

- When repository applications gain patterns such as effects, attributes, or source-generation options, this file **MUST** be updated in the same PR or immediately after it. Why: Keeps reference guidance synchronized.
- New capabilities in `src/` that affect application design **MUST** be documented here before broad adoption. Why: Contributors need the new patterns before they use them.

### Source Generation

- Contributors **SHOULD** use generators for every supported concern: DTOs, actions, action effects, endpoints, and mappers. Manual implementations **MAY** be used only when no generator supports the scenario. Why: Generators reduce boilerplate and keep code consistent while preserving an escape hatch.
- Generators consume Domain types—aggregates, commands, events, and projections—and emit Client artifacts—actions, action effects, feature registrations, and DTOs. See the Generator Inputs table and Inlet and Client-Server Integration. Why: Domain types remain the source of truth and generated artifacts serve every target.
- Types marked `[PendingSourceGenerator]` in `src/Inlet.Generators.Abstractions/` **MUST** be used only as reference implementations for generator validation. They support test comparisons between generated and expected code and **MUST NOT** guide new development. Why: They belong to generator test infrastructure.
- Contributors **SHOULD** review `src/Inlet.Client.Generators/`, `src/Inlet.Gateway.Generators/`, `src/Inlet.Generators.Abstractions/`, and `src/Reservoir/`. The first two contain generator implementations, the third contains attribute definitions, and the last contains state management. Why: Framework internals explain correct usage; abstractions define the attribute surface.

#### Generator Inputs by Project

| Input Project | Generator Input | Output Artifacts |
|---------------|-----------------|------------------|
| Domain | Aggregates with `[GenerateAggregateEndpoints]` | Runtime registration, Gateway controller, Client feature/state/reducers, feature registration (`Add{Aggregate}Feature()`) |
| Domain | Commands with `[GenerateCommand]` | DTOs, mappers, HTTP endpoints, client actions, action effects, command state |
| Domain | Projections with `[GenerateProjectionEndpoints]` | Gateway controller, Client subscription, DTOs |
| Domain | Event effects extending `EventEffectBase` or `SimpleEventEffectBase` | Runtime registration (`AddEventEffect<TEffect, TAggregate>()`) |

### Solution Structure

- New repository applications **MUST** use four projects: Runtime host in an Orleans silo, ASP.NET Gateway, Blazor WebAssembly Client, and Domain. See Scope and Audience. Why: Separates concerns and enables source generation.
- An Aspire AppHost project **SHOULD** provide local development orchestration. Why: Simplifies Cosmos, Azure Storage, and Orleans emulator setup.
- Runtime and Gateway host projects **MUST** contain only configuration, options, dependency wiring, and framework registration. They **MUST NOT** contain domain logic. Why: Keeps hosts thin.
- The Domain project **MUST** contain all server-side domain state: aggregates, projections, commands, events, handlers, and reducers. Why: Centralizes domain logic for source generation.
- The WebAssembly Client project **MUST** contain all front-end code, including UX, UI, and local state management. Why: Separates client concerns from server domain.

### State Management (Reservoir)

- Client-side domain and business state **MUST** use the Reservoir store with actions and reducers. Ephemeral UI state, such as hover, focus, and temporary form input, **MAY** remain in a component. See `.github/instructions/blazor-ux-guidelines.instructions.md`. Why: Provides predictable Redux/Flux state while allowing practical UI patterns.
- Contributors **SHOULD** review `src/Reservoir/` before building features. Why: The implementation explains the store pattern.
- Dispatching actions and obtaining feature state **MUST** go through the store. Ad hoc or component-local state management **MUST NOT** be used for domain state. Why: Keeps important state inspectable and replayable.
- Third-party component libraries **MUST** fit Reservoir's state model. Libraries that require incompatible internal state **SHOULD NOT** serve as general-purpose UI components. Why: Preserves state consistency.
- Manual actions **SHOULD** use `{Command}Action`, `{Command}ExecutingAction`, `{Command}SucceededAction`, and `{Command}FailedAction`. Generated actions use generator naming conventions. See Inlet and Client-Server Integration. Why: Makes the action lifecycle visible while respecting generated names.

### UX Component Guidelines

- Components **MUST** follow atomic design: Atoms, Molecules, Organisms, Templates, and Pages. Why: Enables composition and reuse.
- State flows down the component tree through parameters, including cascading parameters for shared context. Domain events flow up through `EventCallback`. See `.github/instructions/blazor-ux-guidelines.instructions.md`. Why: Creates predictable unidirectional flow.
- Presentational components (Atoms and Molecules) **MUST NOT** call APIs or dispatch actions. They **MUST** emit events for container components (Organisms and Pages) to handle. Containers dispatch store actions; action effects respond to them. See Action Effects and Reservoir State Management. Why: Keeps presentational components pure and testable while containers coordinate.
- See `.github/instructions/blazor-ux-guidelines.instructions.md` for detailed component patterns. Why: It contains the complete UX guidance.

### Inlet and Client-Server Integration

- Inlet spans domain logic and the front end and generates code for both sides. Why: It unifies client-server communication.
- Inlet **MUST** generate client actions from aggregate commands marked `[GenerateCommand]`. Why: Automates the HTTP pipeline.
- Generated actions map directly to aggregate commands; the framework performs the mapping. Why: Removes manual wiring.
- When a generated action is dispatched, generated infrastructure follows this flow:

  **Success path:**

  1. Construct and issue an HTTP request.
  2. Handle the request on the server.
  3. Activate or retrieve the aggregate grain in the silo. See Orleans Grain Considerations.
  4. Validate the command and produce events.
  5. Append events to the brook and update the snapshot when snapshot storage is configured.
  6. Let projection grains consume events and update their state.
  7. Notify only subscribed clients through SignalR asynchronously and near-real-time, not instantly. See Projection Subscriptions.

  **Failure path:** Command validation failures return an error code. They produce no events, and projections stay unchanged.

- Projection updates are asynchronous and eventual. See Consistency Model Separation.
- Client features **MUST** use generated `Add{Aggregate}Feature()` extension methods. Runtime and Gateway projects use `Add{Aggregate}()`, following `.github/instructions/service-registration.instructions.md`. Why: Distinguishes client and host registration while keeping feature registration scalable.

### Projection Subscriptions

- UX screens **SHOULD** subscribe to many small projections instead of one monolithic projection. Why: Limits unnecessary updates.
- Projections **MUST** use Inlet subscription APIs for both subscription and unsubscription. Why: Manages SignalR connections consistently.
- Each UI surface **SHOULD** subscribe only to the projections it needs. One event **MAY** update multiple projections. Why: Reduces updates and re-renders while supporting the one-to-many brook-to-projection pattern.
- Client projection DTOs **MUST** use `[ProjectionPath]` that matches the server projection path. Why: Lets Inlet route subscription requests correctly.

### Domain Modeling (Aggregates)

- Contributors **SHOULD** review Domain implementations under `src/`. Why: They are the detailed reference pattern.
- Aggregates, commands, and events **MUST** be `internal sealed record` types with `[GenerateSerializer]` and `[Id(n)]` on every property. See `.github/instructions/domain-modeling.instructions.md` and Framework Attributes Reference. Why: Ensures Orleans serialization and visibility.
- Aggregates **MUST** define commands. Command handlers **MUST** validate business logic before raising events. Why: Enforces invariants.
- Aggregates **MUST** use `[BrookName]`, `[SnapshotStorageName]`, `[GenerateSerializer]`, and `[Alias]`. Aggregates exposed through an API **MUST** also use `[GenerateAggregateEndpoints]`. See Framework Attributes Reference. Why: Enables event sourcing and stable serialization; endpoint generation depends on API exposure.
- Commands exposed to UX **MUST** use `[GenerateCommand(Route = "...")]`. Why: Starts endpoint and action generation.
- Command handlers **MUST** return `OperationResult<IReadOnlyList<object>>`. Success returns events; failure returns an error code. Use `AggregateErrorCodes.InvalidCommand` for validation failures and `AggregateErrorCodes.InvalidState` for state-based rejections. Why: Enables consistent typed error handling.
- Command handlers **MUST** validate commands against current state and return events. The framework handles persistence and snapshotting. See Inlet and Client-Server Integration. Why: Separates business logic from infrastructure.
- **Pre-1.0 event evolution:** While the repository is pre-1.0, as described in `.github/instructions/backwards-compatibility.instructions.md`, event shapes **MAY** change freely. V2 event types and compatibility shims **MUST NOT** be introduced for patterns that exist only on the current branch. Why: Pre-release speed outweighs ceremony; only contracts on `main` define compatibility.
- **Post-1.0 event immutability:** Once the repository reaches 1.0+ or events are persisted in a real, non-test store, events **MUST NOT** change after writing. Existing event property names and types **MUST NOT** change. Events are immutable facts in an append-only log. Adding properties **MAY** be possible but is not always advisable; significant schema changes **SHOULD** use a new type such as `{Event}V2` alongside the original. Why: Post-release rolling updates need compatibility and gradual migration.

### Domain Modeling (Projections)

- Multiple projections **MAY** use the same brook (event stream). Why: Supports different read-optimized views.
- Projections **SHOULD** be small and highly componentized. Why: Supports reuse across UX contexts.
- Projections **MUST** use `[BrookName]`, `[SnapshotStorageName]`, `[GenerateSerializer]`, and `[Alias]`. Client-facing projections **MUST** also use `[ProjectionPath]` and `[GenerateProjectionEndpoints]`. See Projection Subscriptions and Framework Attributes Reference. Why: Core attributes enable event sourcing; client-facing attributes depend on exposure.
- Projection reducers **MUST** inherit `EventReducerBase<TEvent, TProjection>` and return new instances with `with` expressions or constructors. Why: Enforces immutability.

### Brooks (Event Streams)

- Brooks **MUST** use `[BrookName("APPNAME", "MODULENAME", "NAME")]`. Why: Gives each stream a stable string identity.
- Developers **MUST NOT** work with brooks directly. The framework aligns aggregates and projections by matching `[BrookName]` values. Why: Simplifies event-sourcing configuration.
- Aggregates and brooks have a one-to-one relationship. Each aggregate **MUST** have exactly one brook. Orleans grains are single-threaded, so one aggregate per brook preserves update consistency and keeps aggregate state correct. Why: Maintains the aggregate consistency boundary.
- Brooks and UX projections have a one-to-many relationship. Multiple projections **MAY** subscribe to one brook. Projections are eventually consistent read models for optimized views of one event stream (CQRS). Why: Supports different read models without changing the stream.

### Orleans Grain Considerations

- Aggregate grains are single-threaded. Contributors **MUST** avoid bottlenecks caused by “master” grains that do too much. Why: Protects throughput.
- When scalability requires it, contributors **MAY** create a family of grains or aggregates that share a business identifier but store different aspects. Each aggregate still has its own brook; the one-to-one invariant applies per aggregate type, not per identifier. Why: Distributes load across activations while preserving event-stream isolation.
- Grain operations **SHOULD** be fast and avoid long-running work. Why: Protects grain throughput.

### Action Effects (Client-Side Side Effects)

- Action effects **MAY** trigger client behavior in response to actions, such as notifications, navigation, and follow-up actions. Why: Supports reactive client workflows.
- Action effects run after reducers complete and can emit multiple actions over time, including asynchronous success and failure actions. Why: Lets side effects drive further state changes.
- Action effects **SHOULD** dispatch follow-up actions or call client services instead of containing complex inline logic. Why: Keeps effects light and predictable.
- Action effects run on the client. They **MUST NOT** be confused with server-side event effects, which respond to domain events in grains. Why: Keeps client and server effect patterns distinct.

### Event Effects (Server-Side Side Effects)

- Event effects **MAY** trigger server behavior after persisted domain events, including cross-aggregate commands, external notifications, and audit logging. Why: Supports reactive server workflows without coupling aggregates.
- Event effects run synchronously in the grain context after event persistence and block the grain until completion. Why: Ensures completion before the next command.
- Event effects **MUST** inherit `EventEffectBase<TEvent, TAggregate>` when they yield more events, or `SimpleEventEffectBase<TEvent, TAggregate>` when they perform side operations only. Why: Provides typed handling and correct async-enumerable support.
- Event effects **SHOULD** live in an `Effects` sub-namespace under the aggregate, such as `Aggregates/BankAccount/Effects/`. Why: Source generators discover effects by namespace convention.
- Event effects can yield more events through `IAsyncEnumerable<object>`. The framework persists them immediately. This supports streaming such as LLM token streaming and progressive data fetch. Why: Lets effects produce follow-up events for real-time projection updates.
- Event effects **SHOULD** finish quickly, typically in under one second. The framework logs a warning when one exceeds one second. Why: Long effects block grain throughput.
- For long-running background work triggered by events, event effects **SHOULD** dispatch commands to other grains or use Orleans reminders or timers instead of inline processing. Why: Avoids blocking the originating grain.
- Event effects **MUST** be stateless and registered as transient services. The framework auto-registers them with `AddEventEffect<TEffect, TAggregate>()`. Why: Creates each effect per invocation with the correct DI scope.
- Event effects can inject Orleans services such as `IAggregateGrainFactory` and `IGrainContext` to dispatch commands to other aggregates. Why: Supports cross-aggregate workflows such as the Spring sample's `HighValueTransactionEffect`.

### Fire-and-Forget Event Effects

- Fire-and-forget effects **MAY** run asynchronous side effects without blocking the aggregate grain, including external API calls, notifications, and analytics. Why: Offloads background work without increasing command latency.
- Fire-and-forget effects **MUST** inherit `FireAndForgetEventEffectBase<TEvent, TAggregate>`. Why: Provides typed handling through a dedicated worker grain.
- Fire-and-forget effects run in a separate worker grain, not the aggregate grain. They provide Orleans single-threaded guarantees but are otherwise infrastructure. Why: Keeps aggregate grains fast while effects take longer.
- Fire-and-forget effects **MUST NOT** yield more events. If state must change, the effect **MUST** dispatch commands through the normal aggregate command API. Why: Preserves event-stream integrity and aggregate ownership of state transitions.
- Fire-and-forget effects **SHOULD** live in an `Effects` sub-namespace under the aggregate, as regular event effects do. Why: Source generators discover effects by namespace convention.
- Source generators register fire-and-forget effects with `AddFireAndForgetEventEffect<TEffect, TEvent, TAggregate>()`. Why: Reuses the discovery pattern for regular event effects.
- Use fire-and-forget effects when significant work, such as external HTTP calls or third-party integrations, makes aggregate blocking unacceptable. Why: Offloading slow side effects improves p99 latency.

### Storage Providers

- Cosmos DB **SHOULD** be the default provider for brooks (events) and snapshots. It suits append-only event-sourcing writes and Aspire integration. Why: It provides scalable, globally distributed storage and a strong developer experience.
- Custom providers **MAY** be used when Cosmos is unsuitable. Framework storage abstractions support pluggable backends. Why: Supports different deployment scenarios.
- New projects **SHOULD** use Aspire with emulators for local development. Why: Provides a consistent local experience.
- The Spring sample uses Cosmos for event sourcing and Azure Storage for Orleans clustering and grain state. Why: It demonstrates the reference storage setup.
- Storage client registrations **MUST** use keyed services and follow `Spring.Runtime/Program.cs`. Why: Supports multiple storage accounts for different purposes.

### Framework Attributes Reference

Contributors **SHOULD** review all custom attributes under `src/`, especially `Inlet.Generators.Abstractions/` and `Brooks.Abstractions/Attributes/`, to understand their behavior.

| Attribute | Purpose | When to Use | Relates To |
|-----------|---------|-------------|------------|
| `[BrookName]` | Identifies the event stream through hierarchical `(APP, MODULE, NAME)` | Required on all aggregates and projections that share an event stream | Event-stream alignment; matching names share events; names are immutable after deployment; use uppercase alphanumeric segments. See `.github/instructions/storage-type-naming.instructions.md`. |
| `[SnapshotStorageName]` | Gives snapshot storage a stable, versioned identity `(APP, MODULE, NAME, version)` | Required on aggregates and projections that persist state | Snapshot naming and storage; versioning enables schema evolution; names are immutable after deployment |
| `[EventStorageName]` | Gives event storage a stable, versioned identity `(APP, MODULE, NAME, version)` | Required on every event type | Event versioning; safe refactoring without breaking stored events; names are immutable after deployment |
| `[GenerateAggregateEndpoints]` | Generates runtime registration, Gateway controller, and Client feature code | Required on aggregate records exposed through an API | Endpoint generation; creates `Add{Aggregate}()` extension methods |
| `[GenerateProjectionEndpoints]` | Generates a read-only GET endpoint and SignalR subscription code | Required on projections exposed to clients | Endpoint generation; creates a projection controller and client subscription |
| `[GenerateCommand]` | Exposes a command through an HTTP POST endpoint and generated client action | Required on commands callable from UX | Command exposure; `Route` controls the endpoint path |
| `[ProjectionPath]` | Defines a projection subscription and API path | Required on server projections and matching client DTOs | Subscription routing; server and client paths must match |
| `[GenerateSerializer]` | Provides Orleans serialization support | Required on types crossing grain boundaries | Orleans serialization; pair with `[Id(n)]` on properties; IDs start at 0 and are unique at each inheritance level |
| `[Alias]` | Provides stable Orleans type identity across refactoring | Required on all serialized types | Type versioning; use the fully qualified name format |

## Scope and Audience

These rules apply to all contributors who build repository applications or new Alfred's Forge features. They keep applications consistent and idiomatic and make them reference implementations for framework consumers.

## At-a-Glance Quick-Start

### Project Structure

Contributors write domain logic—aggregates, commands, events, and projections—in Domain. They write UI components, custom actions, and action effects in Client. Source generators produce Client actions, DTOs, feature registrations, runtime registrations, and Gateway endpoints from Domain types.

```text
{Sample}/
├── {Sample}.AppHost/           # Aspire orchestration (local dev)
├── {Sample}.Runtime/           # Runtime host (runs in Orleans silo)
│   └── Program.cs              # Configuration and framework registration
├── {Sample}.Gateway/           # ASP.NET API host (thin)
│   └── Program.cs              # Configuration and framework registration
├── {Sample}.Client/            # Blazor WebAssembly
│   └── Program.cs              # Feature registration and Inlet setup
└── {Sample}.Domain/            # Domain logic (this is where you write code)
    ├── Aggregates/
    │   └── {Aggregate}/
    │       ├── {Aggregate}Aggregate.cs   # Aggregate state record
    │       ├── Commands/                  # Command records
    │       ├── Events/                    # Event records
    │       ├── Effects/                   # EventEffectBase implementations
    │       ├── Handlers/                  # CommandHandlerBase implementations
    │       └── Reducers/                  # EventReducerBase implementations
    └── Projections/
        └── {Projection}/
            ├── {Projection}Projection.cs  # Projection state record
            └── Reducers/                   # Projection reducers
```

### Typical Workflow

Contributors write code only in Domain. Client actions, effects, DTOs, and registrations are generated automatically.

1. Define an aggregate with `[BrookName]`, `[SnapshotStorageName]`, and `[GenerateAggregateEndpoints]` when the API exposes it.
2. Create commands with `[GenerateCommand(Route = "…")]`.
3. Create events with `[EventStorageName]`.
4. Implement command handlers that extend `CommandHandlerBase`.
5. Implement aggregate reducers that extend `EventReducerBase`.
6. Define projections with `[ProjectionPath]` and `[GenerateProjectionEndpoints]` when clients expose them.
7. Implement projection reducers.
8. **Build** so source generators create runtime registrations, Gateway controllers, and Client features.
9. Have Client subscribe through Inlet and dispatch generated actions to trigger commands.

## Core Principles

- **Source generation first:** Use generators for boilerplate; use manual code only for advanced cases.
- **Thin hosts, rich domain:** Runtime and Gateway contain configuration; Domain contains behavior.
- **Redux-style state:** Client state flows through Reservoir with actions and reducers.
- **Small projections:** Prefer many focused projections to one monolithic view.
- **Event immutability:** Events are facts; never modify them, only version them.
- **Orleans awareness:** Design grains for scalability and avoid single-point bottlenecks.

## Holistic Design Benefits

Alfred's Forge patterns work together. Contributors should use these strengths when designing features.

### Consistency Model Separation

The one-to-one aggregate-to-brook relationship and Orleans' single-threaded grain model provide strong consistency within one aggregate activation: commands run serially and state transitions are atomic. Cross-aggregate consistency needs saga patterns or eventual consistency. See Brooks and Orleans Grain Considerations. The one-to-many brook-to-projection relationship provides eventual consistency for reads and permits optimized views without blocking writes. Contributors can reason about aggregate correctness and projection performance separately.

### End-to-End Traceability

The framework generates every step from UI dispatch through HTTP, Orleans grain processing, event persistence, projection updates, and SignalR push-back:

- Debugging follows a predictable path.
- Source generators expose breaking changes at compile time.
- Every layer is testable.

### Scalability by Design

Orleans distributes grain load. Aggregates built around natural business identity, such as an account or order, scale horizontally through correct aggregate boundaries. Contributors do not implement sharding or load balancing.

### Time-Travel and Auditability

Event sourcing gives the brook a complete history. Immutable events and versioned storage names support:

- Replaying events to debug issues.
- Auditing every state change.
- Building projections over historical data.
- Rolling back by replaying to a point in time.

**Caveat:** Time travel depends on event retention policies and serializer compatibility across versions. Contributors should plan retention windows and test event schema evolution.

### Developer Velocity

Source generation removes hand-written DTOs, mappers, controllers, SignalR hubs, and client actions. Contributors focus on:

1. Domain modeling: aggregates, commands, and events.
2. Business logic: command handlers and reducers.
3. UX: components consuming projections.

Everything between these areas is generated. This reduces bug surface area and keeps the codebase DRY.

### Testability at Every Layer

The separation enables:

- **L0 tests:** Pure unit tests for reducers, handlers, and domain logic, with no infrastructure.
- **L1 tests:** Light infrastructure tests with in-memory stores.
- **L2 tests:** Integration tests through Aspire with real emulators.
- **Mutation testing:** High-confidence assertions for business logic.

Contributors should maximize L0 coverage by placing complexity in pure functions that are easy to test and reason about.

### Zero External Infrastructure

Alfred's Forge uses Orleans for everything, including real-time updates through Aqueduct, the Orleans-backed SignalR backplane. By default, applications require:

- **No Redis** for SignalR scale-out.
- **No Azure SignalR Service** for managed real-time.
- **No external message brokers** for cross-server communication.

These are defaults, not hard constraints. Production deployments can integrate external services when needed.

Inlet uses Aqueduct automatically; contributors do not configure or manage the backplane. Orleans grains deliver messages across servers, so real-time scales horizontally with the Orleans cluster. Adding silo nodes increases compute capacity and real-time throughput without infrastructure changes.

### Enterprise-Ready with Keyed Services

Keyed DI services support multiple Cosmos databases, blob accounts, and other coexisting services:

- Brooks events can write to one Cosmos account.
- Snapshots can persist to another.
- Locking can use a dedicated blob account.
- Each service uses only the resources it's configured for via module-owned keyed service defaults.

This supports enterprise solutions with isolated storage for teams or tenants that share application infrastructure. See `.github/instructions/keyed-services.instructions.md` for registration patterns and naming conventions.

### Enterprise Benefits with Startup Speed

Alfred's Forge provides event sourcing, CQRS, real-time updates, and horizontal scaling with the development speed of a simple CRUD application:

- Define a command, event, and reducer to get event sourcing after configuring storage providers and DI registrations. See Storage Providers.
- Add `[GenerateAggregateEndpoints]` to get an API.
- Subscribe from a component to get real-time updates.

Contributors get auditability, time travel, decoupled read/write models, and horizontal scaling without the usual complexity tax. The framework handles infrastructure; contributors focus on business value.

### Extensibility via Orleans Grains

Generated flows are not the limit. Because aggregates live in a silo, contributors can add custom Orleans grains for timers, reminders, stream consumers, schedulers, and long-running workflows. These grains can orchestrate complex logic and invoke aggregate commands directly, or use HTTP when needed. The aggregate model remains authoritative while contributors use full Orleans capabilities.

**Scenario examples:**

| Scenario | Pattern | Flow |
|----------|---------|------|
| **Reminder-driven game combat** | Grain → Aggregate | `CombatReminderGrain` uses Orleans reminders to trigger "attack landed" every hour, then calls `AttackLandedCommand` on the battle aggregate; a follow-up event effect updates the unit-count aggregate. |
| **Stream consumer for IoT telemetry** | Stream → Grain → Aggregate | `TelemetryIngestGrain` subscribes to Orleans streams, buffers device events, and emits `RecordTelemetryCommand` to the device aggregate; projections update dashboards in near-real-time. |
| **Kafka ingestion bridge** | External Stream → Grain → Aggregate | `KafkaListenerGrain` reads a Kafka topic, normalizes messages, and issues `ApplyShipmentUpdateCommand` to logistics aggregates. Equivalent HTTP endpoints can also be exposed for replay/backfill. |
| **Batch ETL drop** | Batch Process → HTTP → Aggregate | A nightly batch job drops a CSV to blob storage, a processing grain picks it up, and posts commands via the API (`ImportInventoryCommand`) for large-scale rehydration. |
| **Enterprise cross-system workflow** | HTTP + Grain Orchestration | `ClaimsOrchestratorGrain` accepts external HTTP callbacks from a third-party claims system, applies validation, and triggers multiple aggregate commands (claims, payouts, fraud checks), using Orleans timers for SLA escalation. |

These patterns show that generated flows are a starting point, not a ceiling. Contributors can use full Orleans features while keeping aggregates the single source of business state.

### Opinionated Patterns for AI and Human Developers

The framework gives each concern one clear pattern:

- **Commands** go to aggregates through generated endpoints.
- **Aggregates** validate commands and produce events in their brook (one-to-one).
- **Brooks** are append-only event logs that aggregates write and projections read.
- **Projections** subscribe to brooks and build optimized read views (one-to-many).
- **State** lives in Reservoir on the client and in projections on the server.
- **Side effects** use action effects on the client or event effects on the server.

This approach means:

- AI models can follow established patterns from user requirements without inventing solutions.
- Contributors need not over-engineer or debate choices already made by the framework.
- Code reviews focus on business-logic correctness rather than structure.
- New human or AI team members become productive quickly by following existing patterns.

The result is an event-based architecture with a predictable command → event → projection → UI update flow.

## References

- Coding discipline: `.github/instructions/coding-discipline.instructions.md`
- Framework patterns (src): `.github/instructions/framework-patterns.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Orleans conventions: `.github/instructions/orleans.instructions.md`
- Domain modeling: `.github/instructions/domain-modeling.instructions.md`
- Blazor UX guidelines: `.github/instructions/blazor-ux-guidelines.instructions.md`
- Keyed services: `.github/instructions/keyed-services.instructions.md`
- Testing: `.github/instructions/testing.instructions.md`
