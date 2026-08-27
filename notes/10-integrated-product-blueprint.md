# Integrated product blueprint

## North-star definition

Alfred's Forge is an enterprise-first, opinionated dark factory that converts structured intent into tested pull requests with evidence, using Mississippi as the control-plane foundation and workflow-engine concepts from CleanSquad as execution/process scaffolding.

## Product stack (integrated)

1. **Experience layer**
   - chat-first, business-usable Blazor WASM interface with full session ownership (PWA interim; MAUI Hybrid mobile future)
   - board-style work intake and backlog (owned by the platform, not pass-through)
   - value and progress tracking across SDLC and evidence state
   - Herald coaching layer to help users write better prompts and get more from the system
   - two-layer language model: plain English for business users, forge-world naming for developers/APIs
   - onboarding via Alfred — structured guided conversation, not modal wizards
   - notification system with four urgency classes (action required / alert / update / digest), delivered through Alfred and configurable external channels

2. **Policy and process layer**
   - layered Standards model (enterprise → domain → repo-type → repo → task)
   - configurable workflow definitions with strong default SDLC packs
   - policy-bounded execution loops and explicit decision routing
   - workflow triggers as a first-class primitive: manual, scheduled (cron), event-driven (SCM events, platform events, webhooks), and threshold/condition — all operable at repo, org, or tenant scope

3. **Control and orchestration layer**
   - Mississippi-based coordination core
   - event/queue backbone (Kafka reference standard) for dispatch and lifecycle signalling
   - agentic worker behaviour bounded by explicit orchestration rules
   - Warrant gates (human approval) for pre-code artifacts; GitHub PR for code review
   - Enquiry system (Knowledge Requests): agents can park mid-workflow and reach out to humans via the People Directory

4. **Knowledge and memory layer**
   - shared memory at three scopes: org (architecture principles, domain map), campaign/project (decisions, goals, team), repo (business logic, tribal knowledge, hotspots)
   - People Directory: expertise routing model (distinct from RBAC) — who knows what, at org/project/repo granularity
   - memory accumulates across sessions; agents inject relevant context at workflow start
   - memory is versioned, attributed, and change-controlled at org level

5. **Execution layer**
   - prebuilt .NET worker containers (Wrights)
   - AKS-oriented SaaS execution fabric with elastic scaling (KEDA-style direction)
   - standardised runtime shapes instead of bespoke runners
   - same container images deployable in self-hosted mode
   - Assayer review agents run in parallel specialist pattern with master synthesis

6. **Governance and economics layer**
   - Chronicle: full audit trail for decisions, actions, outcomes, and notifications
   - Ledger: usage metering and cost record for pay-per-use charging, per PR/task/workflow/node
   - GitOps configuration path with PR approvals, CAB alignment, and rollback (Armory)
   - secrets excluded from GitOps — dedicated secrets management (Key Vault, Vault, K8s secrets)
   - managed RBAC: Permission → Role → Group/User, with AD/OIDC/SAML identity provider integration
   - multi-region control plane, geo-redundant backups, HA event backbone
   - OpenTelemetry across all components with configurable OTLP endpoint per environment
   - BYOM: AI harness abstraction supports customer model endpoints, keeps traffic within customer network boundary

## Platform stance

- Enterprise requirements are the baseline; simpler adoption is a controlled scale-down mode, not a separate architecture.
- Configuration flexibility must not collapse product opinionation.
- Governance, quality, and economic traceability are part of delivery, not optional extras.
- Human review of code lives in GitHub. The platform owns everything before and after the PR — not the review itself.
- Human approval of pre-build artifacts (plans, architecture, experiments) lives inside the platform via Warrants.
- For non-SDLC business processes, in-app Warrants are the only approval surface — no SCM involved.
- SaaS and self-hosted are deployment modes of the same product, not divergent builds.
- Secrets never live in GitOps — hard infosec boundary.
- The platform is a learning system: every Commission enriches the shared memory, making future work faster and more accurate.

## Strategic extension path

Start with SDLC as the flagship domain, then extend the same contract → execution → evidence model to adjacent enterprise processes over time.

As the platform moves into general business process automation, in-app Warrants become the primary approval surface and the shared knowledge/memory layer becomes the institutional memory of how the organisation's processes work.

## Full note index

| Note | Topic |
|---|---|
| `01-core-vision.md` | Product identity, dark factory definition, design stance |
| `02-operating-concepts.md` | Ownership boundaries, approval model, coaching, alignment tasks |
| `03-runtime-and-orchestration.md` | AKS, Kafka, KEDA, worker containers, agentic execution |
| `04-governance-and-instruction-model.md` | Instruction hierarchy (Standards), Chronicle, logical barriers |
| `05-workflow-engine-and-process-model.md` | Crucible / workflow engine direction, CleanSquad concepts |
| `06-mississippi-and-clean-squad-capability-brief.md` | Capability digest from source repos |
| `07-audit-billing-and-ai-harness.md` | Ledger, metering, BYOM, AI harness ownership |
| `08-gitops-configuration-and-change-control.md` | Armory / GitOps config, secrets exclusion, CAB alignment |
| `09-enterprise-first-tiering.md` | Enterprise-first, controlled scale-down |
| `11-deployment-models.md` | SaaS + GitHub App, self-hosted, BYOM, externalised config |
| `12-identity-rbac-and-access-control.md` | RBAC model, AD/OIDC integration, group mapping |
| `13-operational-reliability-and-resilience.md` | Backup, multi-region, HA |
| `14-session-artifact-store.md` | Virtual artifact workspace, Brooks boundary, blob storage |
| `15-observability-and-opentelemetry.md` | OTel, configurable OTLP endpoints |
| `16-design-language-and-visual-identity.md` | Blazor WASM, visual identity, OLED palette, balance |
| `17-alfred-and-naming-world.md` | Alfred's character, forge metaphor, two-layer language model |
| `18-onboarding-tutorials-and-feedback.md` | Onboarding, contextual help, A/B experimentation |
| `19-notification-system.md` | Notification taxonomy, delivery channels, Alfred as notification layer |
| `20-workflow-triggers.md` | Trigger types, org-scope triggers, RBAC, event bus integration |
| `21-agent-knowledge-memory-and-outreach.md` | Shared memory, People Directory, Enquiry / Knowledge Requests |
| `22-development-patterns.md` | Agreed implementation patterns (Blazor RCL-first, etc.) |
