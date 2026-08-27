---
applyTo: 'docs/Docusaurus/docs/**/*troubleshoot*.{md,mdx}'
---

# Troubleshooting Documentation

Governing thought: Troubleshooting pages help readers diagnose and resolve Alfred's Forge failures with evidence, not guesses.

> Drift check: Align this file with `docs/Docusaurus/docs/contributing/documentation-troubleshooting.md`.

## Rules (RFC 2119)

- Authors **MUST** apply this file only when the page is classified as `troubleshooting`. Why: Troubleshooting starts from symptoms, not subsystem tours.
- Authors **MUST** organize troubleshooting pages by symptom. Why: Symptoms define the diagnostic path.
- Troubleshooting pages **MUST** include symptoms, meaning, probable causes, confirmation steps, resolution, verification, prevention, and related content. Why: Readers need a complete evidence-driven diagnostic path.
- Troubleshooting pages **MUST** use real error messages, real metrics, or real log signatures when available. Why: Readers need reliable evidence.
- Troubleshooting pages **MUST NOT** fabricate stack traces. Why: Fake evidence misleads readers.
- Probable causes **MUST** explain how to confirm or rule each one out. Why: A cause list without confirmation steps is guesswork.
- Resolution steps **MUST** state whether the fix is safe live. Why: Recovery guidance has operational consequences.
- Resolution steps **MUST** state whether restart, rollout, or state repair is required. Why: Recovery guidance has operational consequences.
- Troubleshooting pages **SHOULD** include concrete prevention guidance such as tests, alerts, rollout sequencing, or compatibility checks. Why: Good troubleshooting reduces repeat failures.

## Scope and Audience

Contributors and agents authoring Alfred's Forge troubleshooting pages.

## References

- Public guide: `docs/Docusaurus/docs/contributing/documentation-troubleshooting.md`
- General authoring: `.github/instructions/documentation-authoring.instructions.md`
