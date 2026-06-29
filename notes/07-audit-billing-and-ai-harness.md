# Audit, billing, and AI harness

## Why this is core

If Alfred's Forge is pay-per-use and enterprise-grade, economics must be first-class, not an afterthought.

The platform needs to explain both:

- **governance truth** (what happened and why)
- **cost truth** (what it cost and why)

## Metering model

The platform should meter usage at multiple levels:

- model usage (provider, model, input tokens, output tokens, retries)
- workflow/node usage (which stages consumed what)
- execution/runtime usage (worker/container time and supporting services)
- integration usage (GitHub/API calls where relevant)

Each run needs a durable cost record (the **Ledger** in forge naming) that can be queried by:

- PR
- task/ticket
- workflow run
- repository
- team/business unit

## Pricing and reporting surfaces

The product should provide cost views that answer practical questions:

- cost per PR
- cost per task/ticket
- cost per workflow run
- cost per stage/node

And support both:

- customer-facing billing (invoice-ready usage)
- internal unit economics (margin and optimization visibility)

## Provider pass-through and pricing strategy

Given OpenAI and Claude model usage, pricing should support:

- direct pass-through style pricing
- marked-up managed pricing
- blended/hybrid models over time

Whatever model is chosen, every billed line should be auditable back to measured usage.

## AI harness ownership

Alfred's Forge should own the AI harness and context-engineering layer.

That includes:

- provider abstraction and model routing
- BYOM support — customers can point to their own model endpoints (Azure OpenAI, private LLMs, on-prem)
- prompt/context assembly by workflow stage and instruction layer
- policy and guardrail injection
- deterministic usage capture for billing and audit
- quality/cost controls (for example model tier selection by task type)

Owning this layer is strategic because it controls:

- delivery quality
- policy compliance
- unit economics
- vendor flexibility
- enterprise portability (BYOM, private network, data sovereignty)

## Design implications

- audit and billing events should be part of the same evented backbone, not separate bolt-ons — audit facts flow to the **Chronicle**, cost facts flow to the **Ledger**
- every meaningful execution step should emit both governance and cost-relevant facts
- the user-facing app should show cost and progress together so value and spend stay linked
- GitOps-driven settings changes should carry the same audit/billing traceability context as UI-driven changes
