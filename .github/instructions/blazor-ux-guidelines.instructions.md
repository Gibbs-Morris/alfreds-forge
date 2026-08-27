---
applyTo: '**/*.razor*'
---

# Blazor UX Guidelines

Governing thought: Build atomic, testable Blazor components with separate markup and logic, Redux-style state, accessible defaults, and WebAssembly-compatible dependencies.

> Drift check: Check DI, logging, test settings, and design-system assets before editing components. Treat repository scripts and configs as canonical.

## Rules (RFC 2119)

- Agents **MUST** follow this guide when they create or review Razor components. Why: Keeps UX consistent.
- Components **MUST** use the atomic layers Atoms, Molecules, Organisms, Templates, and Pages. Each component **MUST** have its own folder with `.razor`, `.razor.cs`, styles, and tests. Why: Makes structure predictable.
- Markup and logic **MUST** use separate files. The `.razor.cs` file **MUST** define a partial class. Components **SHOULD** be `sealed` unless they require extension. Why: Improves testability and keeps diffs focused.
- View-only components **MUST** remain presentational. They **MUST** expose `[Parameter]` and `EventCallback` members. Why: Keeps view components focused.
- Child components **MUST NOT** call APIs or manage side effects. Domain logic **MUST** stay outside the UI. Why: Separates presentation from behavior.
- Components **SHOULD** use Redux-style actions, reducers, selectors, and effects. Selectors **MUST** provide component state instead of raw store state. Why: Makes updates predictable.
- Effects **MUST** call interfaces for I/O. Why: Keeps I/O testable.
- Templates **MUST NOT** fetch data. Services **MUST NOT** be injected in Razor markup. Inject services in the partial class. Why: Keeps templates declarative.
- Shared components **MUST NOT** use server-only dependencies. Why: Preserves WebAssembly compatibility.
- `[Parameter]` members **MUST** use PascalCase. Atoms **MUST NOT** depend on global styles. Organisms **MUST NOT** access data stores directly. Why: Preserves component boundaries.
- Interactive atoms **MUST** support keyboard access and required ARIA metadata. Why: Supports accessible interaction.
- Components **MUST** have L0 tests for state transitions and callbacks. Why: Protects interactive behavior.
- Atoms **SHOULD** forward `AdditionalAttributes`. Why: Allows caller-supplied attributes.
- Authors **SHOULD** replace duplicated markup with slots or parameters. Why: Improves reuse.
- Pages **SHOULD** implement `IAsyncDisposable` when they hold resources. Why: Ensures cleanup.
- Global theme overrides **SHOULD NOT** be required. Why: Keeps styles portable.
- Authors **SHOULD** track missing accessibility audits. Why: Makes quality gaps visible.

## Scope and Audience

Use these rules when you create or review Blazor components and pages.

## Quick Start

- Place each component in one folder under the appropriate atomic layer.
- Keep logic in the `.razor.cs` partial class. Inject services there.
- Use Redux-style state and selectors. Send intent through callbacks instead of direct API calls.
- Provide keyboard access, ARIA metadata, isolated styles, and L0 tests.

## Core Principles

- Atomic design supports reuse and consistent composition.
- Separate markup, logic, and state to keep components portable and testable.
- Treat accessibility and WebAssembly readiness as defaults.

## References

- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Testing: `.github/instructions/testing.instructions.md`
