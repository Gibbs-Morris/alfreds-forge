---
applyTo: '**/*.cs'
---

# Service Registration Pattern

Governing thought: Use hierarchical `ServiceRegistration` extension methods with
options-based overloads, synchronous registration, and deferred async work in
hosted services or Orleans lifecycle participants.

> Drift check: Review DI settings in `Directory.Build.props` and any referenced
> scripts or config before editing registration code.

## Rules (RFC 2119)

- Each feature **MUST** expose a
  `public static class {Feature}Registrations`. Why: Keeps DI discoverable and
  consistent.
- `Add{Feature}()` extension methods **MUST** follow the feature namespace
  structure. Examples include `AggregateRegistrations`, `ReducerRegistrations`,
  and `InletSiloRegistrations`. Why: Keeps DI discoverable and consistent.
- Parent registrations **MUST** call child registrations. Why: Preserves the
  registration hierarchy.
- Sub-feature registrations **MUST** remain `internal`. Why: Minimizes the
  public surface.
- Public registration **MUST** exist only at product or feature boundaries.
  Why: Minimizes the public surface.
- Public registration **MUST** include XML docs. Why: Documents the public
  surface.
- Registration methods **MUST** be synchronous. Why: DI building is
  synchronous.
- Async calls such as DB or HTTP calls **MUST NOT** occur during registration.
  Why: DI building is synchronous.
- Async initialization **MUST** be deferred to `IHostedService` or Orleans
  lifecycle participants. Why: Avoids startup deadlocks.
- Async factories **MUST** be registered for deferred work instead of blocking
  registration. Why: Avoids startup deadlocks.
- Classes **MUST NOT** inject `IServiceProvider` directly. Why: Service locators
  hide dependencies, complicate testing, and break static analysis.
- Classes **MUST** use explicit dependencies or `Lazy<T>` to break circular
  dependencies. Why: Makes dependencies clear and testable.
- Factory patterns that resolve services at runtime **MAY** be the only
  exception to the direct `IServiceProvider` rule. Why: Supports deferred
  resolution without general service-locator use.
- Constructors **MUST NOT** take raw configuration primitives. Why: Supports
  predictable configuration.
- Registration **MUST** offer overloads for `Action<TOptions>`,
  `IConfiguration`, and explicit parameters. Why: Supports predictable
  configuration.
- Options classes **MUST** use the name `{Feature}Options`. Why: Makes feature
  options discoverable.
- Options classes **MUST** provide sensible defaults. Why: Supports predictable
  configuration.
- Options classes **MUST** provide validation through `ValidateOnStart` or
  `IValidateOptions`. Why: Detects configuration errors early.
- Connection strings and external clients **MUST** be accepted or configured
  through factories. Why: Keeps registration testable.
- Project files **MUST NOT** add package version entries. Why: Keeps registration
  CPM-compliant.
- Registered services **MUST** use the DI get-only property pattern. Why: Aligns
  with shared guardrails and logging patterns.
- Registration classes **SHOULD** be sealed. Why: Keeps the DI surface tight.
- Registration classes **SHOULD** stay minimal. Why: Keeps the DI surface tight.
- Configuration **SHOULD** be externalized according to cloud-native principles.
  Why: Keeps configuration environment-friendly.

## Scope and Audience

Developers who add or modify DI registration in Alfred's Forge/Samples,
including Orleans integrations.

## At-a-Glance Quick-Start

- Put `services.Add{Feature}()` in `{Feature}Registrations.cs` under the feature
  namespace. Examples include `AggregateRegistrations.cs` and
  `InletSiloRegistrations.cs`.
- Keep registration sync-only. Move setup to `IHostedService` or Orleans
  lifecycle participants.
- Provide overloads for explicit parameters, `Action<TOptions>`, and
  `IConfiguration`. Validate options on start.
- Call child registrations instead of duplicating service lists.

## Core Principles

- Hierarchical DI keeps features composable.
- Options and validation catch configuration errors early.
- Async work belongs in hosted services or lifecycle hooks, not registration.
- Internal-by-default access reduces public API churn.

## Domain Registration Patterns (Event Sourcing)

For domain models that use Alfred's Forge event sourcing:

- `Add{Domain}Domain()` is the public entry point. Example:
  `AddContosoDomain()`.
- `Add{Aggregate}Aggregate()` is private per aggregate. Example:
  `AddChannelAggregate()`.
- `Add{Projection}Projection()` is private per projection. Example:
  `AddUserProfileProjection()`.

Registration order within aggregates/projections:

1. `AddEventType<TEvent>()` - Register event types.
2. `AddCommandHandler<TCommand, TAggregate, THandler>()` - Register handlers.
3. `AddReducer<TEvent, TState, TReducer>()` - Register reducers.
4. `AddSnapshotStateConverter<TState>()` - Register snapshot converter.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Orleans lifecycle: `.github/instructions/orleans.instructions.md`
- Domain modeling: `.github/instructions/domain-modeling.instructions.md`
- Keyed services for storage: `.github/instructions/keyed-services.instructions.md`
