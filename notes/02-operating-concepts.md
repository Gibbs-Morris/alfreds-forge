# Operating concepts

## Product ownership boundaries

Alfred's Forge has clear ownership over specific surfaces. Being explicit about this matters.

| Owned by Alfred's Forge | Owned by GitHub (or SCM) |
|---|---|
| Backlog / work intake | Human code review |
| Value and progress tracking | PR approvals |
| Build execution and evidence | CI/CD pipelines |
| Chat agent and sessions | Branch protection rules |
| Coaching layer | Merge controls |
| Human approval gates (pre-build) | |

## Human approval gates

The platform should support human approval as a first-class concept within the app — for pre-build artifacts that are not code.

Examples of things that should be approvable inside Alfred's Forge:

- plans and increment frames before build starts
- architecture decisions and solution designs
- experiment definitions before execution
- epic and story decomposition before stories are actioned
- alignment task proposals

This allows teams to gate the start of build work on explicit human sign-off, without those approvals needing to happen in GitHub.

When work reaches the SDLC execution phase and produces code, human review defers to the SCM (GitHub, Bitbucket, etc.) PR process. That is where code approval lives.

The rule of thumb:

- **pre-code artifacts** → approve inside Alfred's Forge
- **code** → approve in GitHub/SCM via PR
- **non-SDLC business processes** → approve entirely inside Alfred's Forge, no SCM involved

## In-app approval as a long-term platform capability

When the platform extends beyond SDLC into general business process execution, the in-app approval model becomes the primary and only approval surface.

In those contexts there is no GitHub PR to defer to. The platform must be capable of:

- defining approval gates as part of any workflow definition
- routing approval requests to the right people or roles
- blocking process progression until approval is received
- recording approvals as part of the audit trail
- supporting approval via the chat interface or a dedicated review surface

This means in-app human approval is not just a pre-build convenience — it is a core workflow primitive that becomes load-bearing as the platform grows into general process automation.

## Work intake and backlog

Alfred's Forge owns the backlog. This is not a pass-through to Jira or a thin integration layer.

The backlog is the primary surface for:

- capturing ideas, stories, and experiments
- refining work through the concierge agent
- tracking what has been built, what is in progress, and what is pending
- managing alignment tasks alongside feature delivery

## Value tracking

The platform should track value and progress, not just task status.

That means showing:

- where an item is in the SDLC
- how close it is to its projected delivery
- what evidence exists for its current state
- what the item has cost so far

## Build ownership

Alfred's Forge owns the build process end-to-end:

- receiving approved work from the backlog
- executing through the workflow engine
- producing code, tests, and structured evidence
- pushing the result to GitHub as a pull request ready for human review

The platform does not own what happens after the PR is raised — that is GitHub's domain.

## Chat agent and sessions

The primary user interaction model is chat-first.

Alfred's Forge owns this fully:

- session state and history
- agent behavior and persona
- refinement loops before work enters the build
- progress updates and evidence surfacing back to users

## Coaching layer

The platform should include a coaching agent or mode that helps users get more from the system.

This coach would:

- observe how users phrase requests and prompts
- suggest better framing when requests are vague or likely to produce weak output
- explain why a refinement or push-back is happening
- help users build mental models of what makes a good story or experiment
- surface patterns across sessions to improve over time

The coach is not a gatekeeper — it is a collaborator that makes users progressively more effective.

## Concierge behavior

The agent should act like an entrepreneurial delivery partner:

- helping users work out what they actually want
- shaping vague asks into well-refined stories or experiments
- pushing back on weak, confused, or low-value ideas
- steering users toward clearer intent and better delivery framing

This means refinement is part of the product, not just task execution.

## Alignment tasks

One important concept is **alignment tasks**:

- work items whose purpose is to move the codebase in small steps toward a more aligned engineering design
- tasks that are justified not only by feature delivery, but by long-term system coherence

This gives the product a way to improve architecture continuously, not only react to incoming features.

## Product character

This is not just a wrapper around ticket execution.

It is a dark factory with explicit views on:

- intake quality
- idea refinement
- delivery discipline
- architectural alignment
- progress transparency
- enterprise governance boundaries enforced in the product experience
