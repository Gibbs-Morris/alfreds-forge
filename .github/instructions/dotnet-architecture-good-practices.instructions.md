---
description: "DDD and .NET architecture guidelines"
applyTo: '**/*.cs,**/*.csproj,**/Program.cs,**/*.razor'
---

# DDD and Architecture Checklist

Governing thought: Start every domain change with explicit DDD/SOLID analysis. Keep logic in the right layer. Verify tests/observability before shipping.

> Drift check: Open referenced scripts or configs (build/test/logging) first. Treat them as authoritative.

## Rules (RFC 2119)

- Domain-sensitive work **MUST** start with a written analysis of the bounded context, aggregates/value objects/services/events, applicable patterns, and security/compliance impacts. Why: Prevents ad-hoc design.
- Before coding, agents **MUST** plan which aggregates/value objects/domain services/events and tests will change. Why: Aligns ubiquitous language and verification.
- Domain logic **MUST** stay inside aggregates/value objects/domain services. Why: Preserves clean layering.
- Application services **MUST** remain orchestration. Why: Preserves clean layering.
- Infrastructure concerns **MUST** stay isolated per service-registration guidance. Why: Preserves clean layering.
- Test strategy **MUST** follow testing instructions: PascalCase test names, L0-first, coverage >=80% overall/target 95-100% where feasible, 100% on touched code, and Alfred's Forge mutation gate. Why: Ensures consistent verification.
- Financial rules **MUST** use decimal-based value objects with explicit rounding and recorded domain events. Why: Protects audit/compliance.
- After implementation, agents **MUST** confirm SOLID adherence, event publication, security boundaries, and documentation/tasks before marking done. Why: Enforces exit criteria.

## Scope and Audience

Engineers modifying domain/application/infrastructure/UI shells where DDD or SOLID choices matter.

## At-a-Glance Quick-Start

- Map bounded context, aggregates, value objects, services, events, and invariants.
- Identify impacted layers and dependencies (logging, DI, serialization, options).
- Plan tests and observability updates alongside code changes.
- Implement with clean layering.
- Validate with `pwsh ./go.ps1` plus targeted scripts.

## Core Principles

- Ubiquitous language and bounded contexts reduce coupling.
- Composition/DI beats inheritance. Keep options/config external.
- Observability (logging/events) and testing are part of design, not afterthoughts.

## References

- C#: `.github/instructions/csharp.instructions.md`
- Service registration: `.github/instructions/service-registration.instructions.md`
- Testing/mutation: `.github/instructions/testing.instructions.md`
- Logging: `.github/instructions/logging-rules.instructions.md`
