# Workflow triggers

## Overview

Workflows in Alfred's Forge are not only started by a human picking up a task from the backlog. Triggers are a first-class platform primitive — they define the conditions under which a workflow is initiated, and they operate at multiple scopes.

This is the mechanism that makes the platform a true dark factory: work starts, runs, and completes without a human initiating every cycle.

## Trigger types

### Manual

A user or Alfred selects a Commission from the backlog and initiates the workflow. This is the default interaction model for user-driven delivery work — an engineer or business user says "let's build this now."

### Scheduled

A workflow is started at a defined time or on a recurring cadence. Configured as a cron expression or a simple schedule (daily, weekly, at a specific time).

Examples:
- InfoSec scan across all repositories every morning at 08:00
- Dependency audit every Monday at 06:00
- Weekly engineering alignment report generated on Friday at 17:00
- Monthly cost summary sent to stakeholders on the first of the month

### Event-driven

A workflow is started in response to an event occurring in the connected estate or within the platform itself. The event is observed, matched against trigger rules, and a workflow is initiated if the conditions are met.

**Source event categories:**

| Source | Example events |
|---|---|
| GitHub / SCM | PR opened, PR merged, PR closed, push to branch, tag created, release published, issue opened, issue labelled |
| Alfred's Forge platform | Commission completed, Warrant approved, Campaign closed, build failed, cost threshold crossed |
| External webhook | Any third-party system pushing a structured event to a registered endpoint |

### Threshold / condition

A workflow is started when a monitored metric crosses a defined threshold. This is a more complex form of event trigger — rather than reacting to a discrete event, it reacts to a derived state.

Examples:
- Trigger a dependency review when the number of outdated packages exceeds a limit
- Trigger an architecture review when code complexity in a module exceeds a threshold
- Alert and trigger a remediation workflow when test coverage drops below a configured floor

Threshold triggers are evaluated continuously (via telemetry and platform metrics) or on a scheduled polling basis where continuous evaluation is not practical.

## Trigger scope

This is the critical dimension. Every trigger has a scope that determines which repositories, teams, or parts of the estate it applies to.

### Repository scope

The trigger applies to a single, named repository. The most granular level — appropriate for repo-specific workflows (e.g., "when a PR is merged to this repo's main branch, run its post-merge checks").

### Organisation scope

The trigger applies across all repositories within an organisation (or a defined subset, based on filter rules such as repo tags, teams, or naming patterns).

This is where the platform's value at scale becomes real:
- "Run an InfoSec scan across every repository in the org every morning at 08:00"
- "When any PR is merged across the org, run the standard post-merge alignment check"
- "Audit all repos for Standards compliance every Monday"

Organisation-scope triggers are managed centrally, not per-repo. They are configured in the Armory at the org level and require appropriate RBAC permissions to create or modify.

### Tenant scope

The trigger applies across all organisations within the tenant. Used for platform-wide housekeeping, billing events, cross-org reporting workflows, and administrative automation.

## Trigger configuration

Triggers are defined in the Armory (the platform's configuration layer) as structured definitions. Like all platform configuration, they follow the GitOps model — stored as versioned configuration, promoted through environments, subject to change control.

A trigger definition includes:
- **Type**: scheduled / event-driven / threshold
- **Scope**: repo / org / tenant, with optional filter criteria
- **Condition**: the event type, schedule, or threshold expression
- **Workflow**: which workflow to start, and with what initial context
- **Parameters**: any initial inputs to pass to the workflow at start
- **Active window**: optional time-of-day or day-of-week constraint (e.g., only fire on weekdays)
- **Throttle / deduplication**: minimum interval between firings to prevent flooding on high-frequency events

## RBAC and trigger governance

Trigger creation and modification is permission-controlled:

| Permission | What it governs |
|---|---|
| `trigger.repo.manage` | Create/modify/delete triggers scoped to a specific repository |
| `trigger.org.manage` | Create/modify/delete triggers scoped to an entire organisation |
| `trigger.tenant.manage` | Create/modify/delete tenant-wide triggers |
| `trigger.view` | View trigger definitions and run history |

Organisation-scope triggers are a significant operational lever — they can initiate work across hundreds of repositories simultaneously. These permissions should be tightly controlled and assigned to platform or security team roles, not granted broadly.

## Trigger execution and the event bus

When a trigger fires, it publishes an event to the platform's event bus (see `03-runtime-and-orchestration.md`). The orchestration layer picks this up and starts a workflow execution in the Crucible — the same as any other workflow start. The trigger is simply the entry point.

This means:
- All triggered workflows have the same audit trail, Chronicle entries, and artifact storage as manually initiated ones
- Rate limiting, queuing, and backpressure apply — a trigger that fires on every PR across a 500-repo org does not flood the cluster
- Trigger fire history is retained: when it fired, what it started, what the outcome was

## Org-level trigger use cases

These are the highest-value patterns at enterprise scale:

| Trigger | Scope | Purpose |
|---|---|---|
| Daily 08:00 | Org | InfoSec and dependency scan across all repos |
| PR merged (any repo) | Org | Post-merge alignment check, documentation update prompt |
| Weekly Monday 06:00 | Org | Engineering health report — test coverage, complexity, Standards compliance |
| Tag created matching `release/*` | Org | Trigger release readiness checklist |
| Cost threshold crossed | Org | Budget alert and optional spend review workflow |
| Campaign closed | Platform | Lessons-learned summary prompt, value tracking update |

## What this is not

Triggers are not a replacement for CI/CD pipelines at the repository level. A push-triggered infosec scan in Alfred's Forge is a higher-level workflow — it may observe CI results, run agentic analysis, produce a Commission for remediation work, and issue a Warrant for sign-off. It is not a build step.

The distinction: Alfred's Forge triggers orchestrate *delivery work*, not *build mechanics*. Build mechanics belong in the existing CI/CD estate (GitHub Actions, Azure DevOps, etc.) and Alfred's Forge observes their outputs.
