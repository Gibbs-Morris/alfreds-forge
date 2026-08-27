# Agent knowledge, shared memory, and human outreach

## The core idea

Agents in Alfred's Forge are not isolated workers. They operate within a shared knowledge fabric — a structured, scoped memory that persists across sessions and accumulates over time. When an agent encounters a knowledge gap, it can query that fabric, and if the answer is not there, it can reach out to a human who holds it.

This turns the platform from a task executor into a learning system. Every Commission, every question answered, every piece of tribal knowledge surfaced adds to a shared body of understanding that makes future work faster and more accurate.

## The three capabilities

### 1. Shared memory

Alfred and all agents share a memory store that is scoped at three levels. These are distinct stores with different lifetimes, update mechanisms, and access rules.

#### Org-level memory

Persistent knowledge about the organisation as a whole. This is the slowest-changing and highest-authority layer.

Contains:
- Architectural principles and standards — the agreed patterns the org builds toward
- Strategic direction — what the organisation is trying to achieve technically and commercially
- Banned patterns and known anti-patterns — things explicitly prohibited and why
- Key decisions log — significant choices made at org level, with rationale and date
- Domain map — a high-level model of the business domains the codebase serves
- Key stakeholders and their areas of authority

Populated by: platform admin configuration, synthesis from completed Campaigns, architectural review outputs, explicit curation by authorised roles.

#### Project / campaign level memory

Knowledge scoped to a specific Campaign (epic) or project. This layer lives as long as the Campaign is active and is archived (not deleted) when it closes.

Contains:
- Campaign goals and success criteria
- Decisions made during the Campaign — architectural, product, technical trade-offs
- Rejected approaches and why they were rejected
- The team involved — who is working on this, in what capacity
- Dependencies on other Campaigns or external systems identified during the work
- Domain knowledge surfaced during discovery that is specific to this body of work

Populated by: Alfred's synthesis of the Commission conversation, agent-surfaced discoveries, Warrant decisions and their rationale, human corrections during outreach.

#### Repo / codebase level memory

Knowledge scoped to a specific repository. This is the layer most relevant to agents doing technical work — discovery, analysis, review.

Contains:
- Business logic explanations — what this code does in business terms, not just technical terms
- Known complexities, hotspots, and technical debt — areas that require extra care
- Domain ownership mapping — which modules are owned or best understood by which people
- Historical context — why key architectural choices were made, what was tried before
- Tribal knowledge surface — things not in the code or documentation that experienced contributors know
- Test coverage notes and known gaps

Populated by: agent discovery during Commissions, human answers to outreach questions (see below), repository analysis workflows, explicit curation by repository owners.

---

### 2. People directory

The people directory is a structured model of the humans connected to the platform — not just their permissions (that is RBAC), but their knowledge, domain, and reachability. It exists to enable intelligent routing: when an agent needs an answer, the system knows who to ask.

#### What is stored per person

| Field | Description |
|---|---|
| Identity | Linked to the platform identity (SSO, AD) |
| Role and team | Organisational role and team membership |
| Domain expertise | Tagged areas of knowledge (e.g., "payments domain", "infosec", "checkout service") |
| Repo ownership / familiarity | Which repositories this person has deep knowledge of |
| Preferred contact channel | In-app, Slack, Teams, email — in priority order |
| Availability preferences | Do-not-disturb windows, maximum outreach frequency |
| Outreach history | Past questions asked, response rate, response time |

#### Expertise tagging

Domain expertise is not self-declared only — it is also inferred. The platform can build expertise signals from:
- Who has reviewed, authored, or commented on Commissions in a domain
- Who has answered previous outreach questions and had those answers accepted
- Who is listed as a code owner or team lead for a repository
- Explicit curation by a team lead or admin

Expertise tags are visible to the person and can be corrected or supplemented. They are never surfaced publicly as a ranking system — they are routing signals, not performance indicators.

#### Scope levels

Like memory, the people directory is scoped:

- **Org level**: all people in the organisation, with org-wide expertise tags
- **Project level**: who is assigned to this Campaign, and in what capacity
- **Repo level**: who has deep knowledge of this repository (owners, frequent contributors, domain SMEs)

---

### 3. Agent-to-human outreach

When an agent is mid-workflow and encounters a knowledge gap it cannot resolve from the shared memory, it can raise a **Knowledge Request** — a structured question directed at one or more specific people identified via the people directory.

#### How it works

1. The agent identifies a gap (e.g., "I cannot determine the intended business behaviour of this module from the code or documentation alone")
2. The agent queries the people directory for who has relevant expertise (repo ownership, domain tag match, prior answers on similar topics)
3. Alfred formulates a clear, specific question — the agent provides the content, Alfred shapes it for human consumption
4. The question is delivered to the target person via their preferred channel, with full context: what is being built, why the question is needed, what the agent already understands
5. The recipient responds — in-app, or via their connected channel
6. The response is captured, stored in the relevant memory layer (repo or project), and the workflow resumes

#### Workflow behaviour during outreach

A Knowledge Request is a park-and-resume pattern. The workflow does not block — it suspends the stage that requires the answer, continues any parallel stages it can, and resumes the suspended stage when the response arrives.

If no response is received within a configured timeout, Alfred follows up once. If still no response, Alfred either escalates to an alternative contact (if defined) or surfaces the unresolved gap as a Warrant — a human must decide how to proceed.

#### This is not an approval gate

A Knowledge Request (outreach) and a Warrant (approval gate) are distinct:

| | Knowledge Request | Warrant |
|---|---|---|
| Purpose | Gather missing information | Approve or reject a decision |
| Initiated by | Agent (autonomous) | Workflow definition |
| Response type | An answer, a clarification, a correction | Approve / reject / request changes |
| Effect on workflow | Resumes with new knowledge | Proceeds or stops based on decision |
| Stored in memory | Yes — the answer enriches the memory store | The decision is stored in the Chronicle |

#### Consent and expectation management

People who receive Knowledge Requests need to understand:
- Who is asking (Alfred / the platform, acting on behalf of a Commission)
- Why their input is needed
- How their response will be used
- That their answer will be stored and may inform future work

Outreach preferences (frequency limits, channel preferences, do-not-disturb) set in the people directory are always respected. A person should never feel spammed by agents.

---

## Memory lifecycle

### How memory is written

Memory is written from multiple sources:
- **Agent synthesis**: at the end of a workflow stage, the agent summarises what it learned about the domain, codebase, or business logic and writes structured entries to the appropriate memory level
- **Human answers**: responses to Knowledge Requests are stored verbatim and also summarised into structured entries
- **Explicit curation**: authorised users can write, edit, or delete memory entries directly
- **Commission outcomes**: when a Commission closes, Alfred synthesises key learnings into the campaign and org memory layers

### How memory is read

Memory is injected into agent context at workflow start, scoped to the relevant level. An agent working on a Commission in a specific repo gets:
- The relevant sections of org-level memory (architecture principles, domain map)
- The campaign-level memory for the current Campaign
- The repo-level memory for the target repository

Memory is not dumped wholesale — Alfred selects relevant entries based on the current task context.

### Memory governance

Memory entries are versioned and attributed. Every write records who (or which agent) wrote it, when, and in what context. Entries can be challenged and corrected by authorised users.

Memory at the org level is change-controlled — modifications require appropriate RBAC permissions and are logged in the Chronicle. See `12-identity-rbac-and-access-control.md` for the permission model.

**Memory vs artifacts:** Memory is structured, queryable knowledge that persists and accumulates. Artifacts are point-in-time records of what a specific workflow stage consumed and produced. Both are needed; neither replaces the other. See `14-session-artifact-store.md` for the artifact model.

---

## Why this matters at enterprise scale

In a large organisation, the most valuable knowledge is the knowledge that is not written down anywhere — the business logic that exists only in the heads of experienced engineers, the architectural decisions whose rationale has been forgotten, the domain understanding that makes the difference between a change that works and one that breaks things subtly.

Alfred's Forge is designed to surface and preserve that knowledge. Every agent interaction is an opportunity to capture something that would otherwise be lost. Over time, the shared memory becomes a genuine institutional asset — not a document repository, but a live, queryable, structured model of how the organisation's software works and why.
