# Notification system

## Design principle

Alfred is the notification system. Every notification a user receives arrives through the same chat interface they already use — as a card, a message, or a brief update from Alfred. There is no separate notification tray, no badge count on a bell icon, no parallel system to manage.

This means notifications feel like Alfred keeping you informed, not a system firing alerts at you. The voice, the format, and the context are consistent with everything else in the interface.

## Notification taxonomy

### By urgency class

| Class | Meaning | How Alfred delivers it |
|---|---|---|
| **Action required** | Something is blocked waiting for the user | Alfred surfaces this immediately, with a card and a clear call to action |
| **Alert** | Something has gone wrong or needs attention | Alfred flags it clearly; red semantic colour in the card |
| **Update** | A workflow stage has progressed; informational | Delivered as a card in the chat flow, or batched into a digest |
| **Digest** | A summary of recent activity | Alfred sends a periodic summary at a cadence the user controls |

### By trigger type

The following events may generate a notification:

**Action required**
- Warrant pending — a plan, architecture review, or business process approval needs sign-off
- Enquiry pending — an agent has raised a Knowledge Request and needs a human answer to continue (see `21-agent-knowledge-memory-and-outreach.md`)
- Commission blocked — a workflow has paused and needs human input to proceed
- Budget threshold approaching — spend is near a configured limit
- Rework requested — an Assayer review has returned the work

**Alert**
- Commission failed — workflow reached an unrecoverable state
- Build or test failure that exceeds retry limits
- Integration error (GitHub connection lost, model endpoint unavailable)
- Security or compliance rule violation flagged

**Update**
- Commission status changed (moved to a new stage)
- Warrant approved or rejected by another approver
- Campaign milestone reached
- Assayer review completed (pass)
- Commission completed — evidence available for review

**Digest**
- Daily or weekly summary of activity across owned Commissions and Campaigns
- Herald coaching suggestions (batched, not per-event)
- Cost report (daily/weekly spend vs budget)

## Who gets what

Notifications are scoped to the user's role and the work they are connected to. The platform does not broadcast everything to everyone.

| Role | Default scope |
|---|---|
| Business user | Warrants they must approve, Commissions they own, Campaign progress |
| Developer / engineer | Build results, review outcomes, rework requests on their Commissions |
| Platform admin | System health alerts, integration failures, budget threshold alerts |
| All roles | Action-required events for anything directly assigned to them |

Notification scope rules are configurable at the tenant level and can be further refined per user.

## Delivery channels

### Primary: in-app via Alfred

All notifications arrive in-app as part of the chat flow. Action-required events interrupt the current state; informational updates are queued and appear naturally in the conversation. Alfred batches low-urgency updates rather than fragmenting the conversation.

### Secondary: external channels

Users may configure delivery of notifications to external channels:

- **Slack or Teams**: direct message or channel post, configurable by notification class and event type
- **Email**: digest mode only — not real-time event-by-event delivery; configurable cadence (daily, weekly)
- **Webhook**: for custom integrations, enterprise automation, or alerting pipelines

External delivery is opt-in per channel, per notification class. Action-required events should be strongly recommended to at least one external channel in addition to in-app, since users may not have Alfred open at all times.

### Future: mobile push

Reserved for a future mobile surface. Same taxonomy and user preferences apply.

## User control

Users manage their notification preferences through a dedicated settings surface (accessible via Alfred or the settings panel). Preferences are:

- **Per event type**: on / off / digest only
- **Per channel**: which events go to which delivery channel
- **Quiet hours**: a time window during which only action-required and alert class events are delivered; all others are held for the next active period
- **Digest cadence**: daily at a chosen time, or weekly on a chosen day

Default preferences are set by role at provisioning. Tenant admins can set mandatory minimums — for example, Warrant notifications cannot be disabled for users with approval responsibilities.

## Alfred's role in notification delivery

When Alfred delivers a notification in-app, he does so with context. He does not send a bare status update. He explains what changed, why it matters, and what (if anything) the user needs to do next. The notification card contains the structured data; Alfred's message provides the framing.

For action-required events, Alfred gives the user everything they need to act without leaving the chat: the relevant context, the card, and the action controls inline.

For digest summaries, Alfred writes a brief natural-language summary of what happened in the period — not a list of raw events, but a shaped account of progress.

## Notification volume and trust

A notification system fails when it generates too much noise. The default configuration should err strongly toward fewer notifications, not more. Users who are not doing anything should not receive a steady stream of FYI updates.

The rule: **notify when the user needs to know, not when the system has something to say.**

Alfred applies this judgement. Low-significance status changes (e.g., an internal workflow stage transition with no user-facing implication) do not surface as notifications. Alfred knows the difference between a user-relevant event and a system-internal one.

## Notification history

All delivered notifications are retained in the Chronicle as typed events — what was sent, when, to whom, and via which channel. This supports audit requirements and allows users to review past notifications in context.

The in-app notification history is accessible as a filtered view of the Chronicle, surfaced through Alfred on request: "What did I miss while I was away?"
