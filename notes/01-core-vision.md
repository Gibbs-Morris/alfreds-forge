# Core vision

## Product definition

Alfred's Forge is an enterprise-first, opinionated **dark factory** for software delivery: it turns structured intent into tested pull requests with evidence.

Stated plainly, it is:

- a SaaS-first platform with self-hosted deployment as a first-class option
- an AI engineering concierge
- a system that turns structured delivery contracts into tested pull requests with evidence
- a platform built to operate at enterprise scale with governed execution and observable delivery
- enterprise-first from day one, while still designed to support simpler adoption contexts

## Strategic identity

The reasoning from first principles: software delivery is a flow from intent to proven outcome. If that flow is made explicit, governed, and observable, it can run as a factory rather than a series of manual handoffs. That is the dark factory stance:

- work enters through structured intent
- the system refines, challenges, and operationalizes that intent
- delivery moves through an observable software lifecycle
- outputs are not just code, but code plus proof, traceability, and progress
- the same operating model can later support non-SDLC workflows

SDLC is the flagship domain first, with a path to broader process execution over time.

## Design stance

This product should not be neutral.

It should be highly opinionated about:

- what good delivery looks like
- how ideas should be refined before implementation
- how engineering work should align with architecture over time
- what evidence is required before something is considered done

## Foundation

Alfred's Forge is built on the Mississippi framework as its core control plane, with the relevant NuGet packages pulled in as the operational backbone. This choice follows from the identity above: an event-sourced, stateful control plane is what an observable, governed delivery factory requires.
