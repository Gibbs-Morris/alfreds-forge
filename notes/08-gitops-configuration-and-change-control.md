# GitOps configuration and change control

## Direction

Alfred's Forge should support GitOps-style configuration as a first-class operating mode.

This allows organizations to manage settings in Git instead of forcing every change through interactive UI actions.

## Why this matters

Git-backed configuration gives enterprises:

- versioned configuration history
- straightforward backup and duplication
- pull-request based review and approval
- explicit rollback via revert/cherry-pick workflows
- compatibility with existing platform governance practices

## Change-control alignment

A GitOps model can align with enterprise CAB/change workflows by making settings changes:

- reviewable
- approvable
- attributable
- reversible

This is especially useful for organizations that already rely on GitHub or Bitbucket approval flows as governance evidence.

## Product stance

The UI should still exist for discoverability and ease-of-use, but it should not be the only control surface.

For governed environments, Git should be a primary source-of-truth option for configuration.

## Scope ideas for configuration-as-code

Potential settings domains to support in Git:

- instruction-layer definitions
- workflow and policy definitions
- model-routing and cost-control policies
- environment/runtime defaults
- governance gates and approval requirements
- BYOM model endpoint configuration

## Design implications

- every config change should map to a commit and an audit trace
- import/export and environment promotion should follow repository semantics
- settings deployment should be policy-checked before activation
- rollback should be operationally simple and explicit

## Hard boundary: secrets do not belong in GitOps

GitOps manages configuration, not secrets.

Secrets (API keys, model provider credentials, integration tokens, signing keys, database credentials) must be managed through a dedicated secrets management layer, not stored in Git repositories.

The practical split:

| Lives in Git (GitOps) | Lives in secrets management |
|---|---|
| Instruction layer definitions | API keys for AI providers |
| Workflow and policy definitions | Integration tokens (GitHub App, Bitbucket) |
| Model routing rules | Database credentials |
| Governance gates | Encryption/signing keys |
| Environment/runtime defaults | Billing provider secrets |
| BYOM endpoint URLs | BYOM auth credentials |

GitOps config may reference secrets by name or path but must never contain secret values.

## Information security requirement

Top-level infosec controls are required:

- secrets management must be a first-class platform concern, not customer-delegated
- supported paths include environment injection, Kubernetes secrets, and external vaults (e.g. Azure Key Vault, HashiCorp Vault)
- secret rotation must not require code or config changes
- audit logging must cover secret access events as well as config changes
- self-hosted deployments must support customer-owned secrets stores
