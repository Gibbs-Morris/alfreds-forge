# Onboarding, tutorials, and feedback

## Core principle

The platform has no separate "help system." Alfred is the help system. Tutorials, guidance, contextual explanation, and feedback collection all flow through the same chat interface the user already operates. A second surface for help creates cognitive overhead and feels inconsistent with the product's identity.

## Onboarding

### First-run experience

When a user joins the platform for the first time, Alfred runs a guided introduction — a structured conversation, not a modal wizard or a tour overlay. He asks what the user is here to do, explains the platform in the user's own terms, and walks them through creating their first task.

The first-run flow is not a tutorial about features. It is Alfred learning the user's context and the user learning how to communicate with Alfred. Both directions matter.

### Progressive disclosure

Not everything is shown at once. The first experience is simple: talk to Alfred, see a task created, watch it move. Approval gates, policy packs, cost views, initiative-level grouping — these are introduced when they become relevant, not upfront.

Alfred surfaces new concepts in context: "You've got a few things in flight now — want me to show you how grouping related tasks into an initiative helps track progress?"

### Role-based onboarding paths

Business users, engineers, and platform administrators have different first-run experiences, shaped by their role. A business user is onboarded into the task/initiative model and the approval flow. A developer is onboarded into the repository connection, policy pack configuration, and build output reading. An admin is onboarded into the configuration store, RBAC setup, and environment configuration.

Alfred adapts his introduction based on the role assigned at provisioning.

## Contextual help

### In-flow guidance

At any point in the interface, a user can ask Alfred a question about what they are looking at or what they should do next. This is not a FAQ — Alfred gives a specific, contextual answer based on the user's current state and history.

"What does this card mean?" should yield an explanation of that specific card, that specific state, in that specific workflow — not a generic help article.

### Signposting without intruding

For new or infrequently-used features, Alfred may offer a brief, dismissible prompt within the chat flow: "This is your first time setting a Standard — want a quick walkthrough?" The user can accept, decline, or defer. Alfred does not repeat the offer unless the user returns to the same feature after significant time.

## User feedback

### In-context feedback

Users can rate any Alfred response, any card, or any workflow outcome directly in the interface — a lightweight signal (helpful / not helpful / incorrect) attached to the specific moment. This is low-friction: a small control on the card or message, not a separate form.

This feedback is captured in the Chronicle as a typed event and surfaced in the platform's own analytics.

### Structured feedback prompts

At key lifecycle moments — after a Commission is completed, after a Warrant is resolved, after a Campaign closes — Alfred may ask a single focused question: "Did the output match what you expected?" One question, not a survey. The answer is optional and dismissible.

### Bug and issue reporting

Users can tell Alfred "something went wrong" or "this doesn't look right" in plain language. Alfred captures context automatically (what was on screen, what workflow was active, what stage the session was in) and logs a structured report without asking the user to describe technical details. The user adds their description; Alfred gathers the rest.

## A/B testing and experimentation

### What can be tested

The platform is designed to support controlled experimentation on its own UX and behaviour:

- Alfred response style variants (more concise vs more explanatory)
- Card layout and information hierarchy
- Onboarding flow variants
- Herald coaching prompt frequency and timing
- Workflow default configurations

### How it works

Feature flags govern which variant a user sees. Flags are managed in the Armory (the platform's configuration layer) and can be scoped to: tenant, role, user cohort, or random split. The platform itself is a consumer of its own configuration model — no separate experimentation infrastructure required.

Feedback signals, interaction events, and workflow outcomes are tagged with the active variant at time of capture. This allows comparison across variants using the Chronicle and analytics surfaces.

### Data ethics and transparency

Users in an experiment see no difference in the interface — no banners, no labels. Experimentation is a product-layer concern, not a user-facing disclosure requirement at the interaction level. Enterprise contracts may include provisions around experimentation scope.

## What this is not

Tutorials are not a documentation replacement. The platform should have proper documentation and API references — those live outside the chat interface. Alfred handles in-context, in-moment guidance. Documentation handles reference, depth, and integration guidance.

Help should never feel like an interruption. Alfred's guidance is offered, not imposed.
