# Enterprise-first tiering

## Market posture

Alfred's Forge should target large enterprises from the start.

This means core decisions should assume:

- governance-heavy environments
- multi-team scale
- strict audit and change-control needs
- platform reliability and operational maturity expectations

## Design principle

Build for the hardest enterprise requirements first, then allow graceful simplification.

In other words:

- **enterprise capability is the baseline**
- **downscaling is a supported mode, not a separate product rewrite**

## Scale-down strategy

The platform should be able to reduce complexity for smaller contexts by making enterprise controls optional where appropriate, without breaking the core model.

Examples:

- lighter approval requirements
- simpler workflow packs
- reduced governance layers
- smaller runtime footprint

## Guardrail

Do not compromise enterprise architecture to optimize early for lightweight usage.

Instead, keep enterprise-grade foundations and expose progressive complexity so teams can adopt only what they need.
