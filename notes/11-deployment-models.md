# Deployment models

## Default stance: SaaS

Alfred's Forge should be designed as a SaaS product first.

The default operating assumption is:

- multi-tenant hosted service
- platform-managed infrastructure (AKS, worker containers, event backbone)
- GitHub App (or equivalent Bitbucket/SCM integration) as the primary connect point into customer estates
- customers configure via UI or GitOps without managing any runtime themselves

## GitHub App integration

The SaaS model needs a GitHub App (and likely a Bitbucket equivalent) so the platform can:

- connect to customer repositories without requiring per-user credentials
- receive webhook events for PR status, CI results, and review feedback
- read and write code, branches, pull requests, and check results on behalf of the customer
- operate within customer-defined permission scopes

This is the standard enterprise integration pattern and keeps the connection model governed and auditable.

## Self-hosted: first-class mode, not an afterthought

The platform must also support self-hosted deployment as a first-class option.

The target shape for self-hosted is:

- container-based deployment (Docker / Kubernetes)
- customers run the full platform inside their own estate
- same feature set as SaaS, with customer-managed infrastructure
- customers can bring their own AI models (BYOM) rather than routing through platform-managed providers

This matters because:

- some large enterprises will not allow external SaaS to touch their code
- regulated industries often require data sovereignty
- internal model deployments (on-prem or private cloud LLMs) are common in large enterprise
- self-hosted is a strong enterprise sales motion alongside SaaS

## Bring Your Own Model (BYOM)

The AI harness must support BYOM as a first-class configuration option.

In self-hosted or enterprise contexts, customers should be able to:

- point the platform at their own model endpoints (Azure OpenAI, private LLMs, on-prem models, etc.)
- configure which model is used per workflow, stage, or repo-type instruction pack
- keep model traffic entirely within their own network boundary if required

This means the AI harness abstraction is not optional — it is the mechanism that makes BYOM possible.

## Configuration lives outside the codebase

All environment-specific and customer-specific configuration must live outside the application codebase.

That includes:

- model routing and provider config
- instruction layers and workflow definitions
- governance and policy settings
- environment variables and secrets
- billing and integration credentials

Configuration is injected at runtime — via environment, mounted volumes, or GitOps-managed config repositories — not baked into builds.

**Secrets are explicitly excluded from the GitOps model.** API keys, credentials, tokens, and signing keys must live in a dedicated secrets management layer (environment injection, Kubernetes secrets, Azure Key Vault, HashiCorp Vault, or equivalent). GitOps config may reference secrets by name but never by value.

This is essential for both SaaS multi-tenancy and self-hosted portability, and is a non-negotiable infosec baseline.

## Design implication

The architecture should treat SaaS and self-hosted as deployment modes of the same product, not divergent builds.

Concretely:

- all runtime components should be containerized and portable
- configuration and policy should be environment-agnostic
- telemetry, billing, and governance hooks must work in both modes
- self-hosted may route billing differently but must still meter usage accurately

## Open questions

- whether self-hosted ships as a single Helm chart / compose bundle or a set of component images
- how licensing and billing enforcement works in self-hosted mode
- whether GitOps configuration management differs between SaaS and self-hosted
- how upgrades are managed for self-hosted customers
