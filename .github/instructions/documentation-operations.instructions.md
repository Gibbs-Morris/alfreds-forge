---
applyTo: 'docs/Docusaurus/docs/**/*operation*.{md,mdx},docs/Docusaurus/docs/**/*ops*.{md,mdx}'
---

# Operations Guides

Governing thought: Help engineers run Alfred's Forge safely in production with explicit validation, telemetry, failure-mode, and rollback guidance.

> Drift check: Keep this file aligned with `docs/Docusaurus/docs/contributing/documentation-operations.md`.

## Rules (RFC 2119)

- Apply this file only when the page is classified as `operations`. Why: Production guidance needs explicit safety and validation structure.
- Explain when the guidance matters, its assumptions, validation steps, failure modes, rollback steps, and required telemetry. Why: Production changes need operational proof.
- State what is safe live, what requires a maintenance window, and what can affect the whole cluster. Why: Blast radius matters.
- Replace vague recommendations with concrete signals, thresholds, commands, dashboards, or decision criteria when evidence exists. Why: Generic advice is not operational guidance.
- Cover the relevant scaling, deployment order, mixed-version behavior, fault tolerance, disaster recovery, observability, secrets, security, and cost topics. Why: Production owners need these dimensions.
- Do not hide constraints or claim that unverified tuning guidance exists. Why: False confidence is dangerous in production.

## Scope and Audience

Contributors and agents who author operations pages for Alfred's Forge documentation.

## References

- Public guide: `docs/Docusaurus/docs/contributing/documentation-operations.md`
- General authoring: `.github/instructions/documentation-authoring.instructions.md`

