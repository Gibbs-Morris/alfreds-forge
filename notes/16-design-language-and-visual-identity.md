# Design language and visual identity

## Design vision

Alfred's Forge should feel like a sophisticated command interface from a near-future era — precise, calm, quietly powerful — while remaining immediately legible to business users and engineers alike. The governing principle: **the interface earns trust by how well it works, not just how it looks, and clarity is always the first obligation.** Every choice below — the technology, the OLED-first palette, the chat-with-cards pattern, the task-focused discipline — serves that single aim.

## UX technology

The primary client is a **WebAssembly (WASM) application** — consistent with the .NET/Mississippi stack, this means Blazor WASM. The UI runs entirely in the browser with no server-side rendering dependency for the client shell. This gives desktop-class interactivity, real-time SignalR connectivity via Mississippi's Aqueduct layer, and a consistent experience across browsers without app store distribution.

**Long-term: mobile via .NET MAUI Blazor Hybrid**

A native mobile application is a future consideration — not in scope for the initial build. The good news: the .NET ecosystem has a well-supported path that avoids a component rewrite.

The strategy is a **Razor Class Library (RCL)** — a shared component library that is referenced by both the Blazor WASM web app and a future .NET MAUI Blazor Hybrid mobile app. Microsoft's recommended pattern as of .NET 8/9.

```
/Alfred.UI.Shared    ← Razor Class Library — all reusable components, pages, models
/Alfred.UI.Web       ← Blazor WASM host (web)
/Alfred.UI.MAUI      ← .NET MAUI Blazor Hybrid host (iOS / Android) — future
```

The MAUI host embeds a `BlazorWebView` and renders the same Razor components natively. Platform-specific services (push notifications, device APIs, file access) are swapped in via dependency injection — the shared components never know which host they are running in.

**Implications to carry forward from day one:**

- Build all components in the shared RCL from the start — no web-only components in the WASM host
- Keep components platform-neutral; use DI interfaces for anything device-specific
- Design tokens (colour, spacing, typography) rather than hardcoded CSS values — mobile will need adaptation
- The chat-first + card insert pattern translates directly to mobile form factors — preserve this as a deliberate constraint
- The notification system must support mobile push as a first-class channel when the MAUI app exists (see `19-notification-system.md`)

**PWA as an interim mobile option**

Before a native MAUI app ships, Blazor WASM can be deployed as a **Progressive Web App (PWA)** — installable on mobile home screens, with offline support and basic push notification capability via browser APIs. This gives a reasonable mobile experience with zero additional build effort, and acts as a bridge until a native app is warranted.

## Design era and feeling

The visual identity should feel like a sophisticated command interface from a near-future era — not retrofuturism, not glossy consumer tech, but the kind of interface an elite operational crew would use in a highly capable vessel or facility. Precision, calm authority, and quiet power. The UI should feel like it belongs to people who are serious about what they are doing.

## OLED-first palette

The interface is designed for OLED screens where true black is a display advantage.

- **Background**: true black or very near-black — not dark grey, not navy
- **Primary UI colour**: a single cool blue, used for chrome, borders, interactive elements, type hierarchy, and ambient glow
- **Glow and depth**: elements should emit subtle luminous halos and edge glows consistent with the blue register — the interface should feel like it is lit from within, not backlit
- **Monochromatic discipline**: the vast majority of the UI lives within a single-hue palette — blue on black, with depth created through luminosity and opacity rather than additional colours

## Semantic colour use — strict and minimal

Only three accent colours are used outside the core palette, and only for specific functional meanings:

| Colour | Use |
|---|---|
| **Green** | Go, confirmed, approved, ready, passing |
| **Amber / orange** | Warning, attention required, degraded, in-flight caution |
| **Red** | Error, blocked, failed, rejected, critical |

These colours appear only where they carry semantic meaning. They are never used decoratively.

## Typography

Fonts should feel engineered, not editorial.

- space-age, geometric, or technical character — clean letterforms with a slightly futuristic edge
- high legibility at small sizes (this is a dense operational UI)
- monospace or semi-monospace for data, identifiers, timestamps, and code-adjacent content
- a clear typographic hierarchy: primary/secondary/tertiary levels, not a flat wall of similar weights

## Primary interface: chat with card inserts

The core UX pattern is a chat-first conversational interface.

Within the chat stream, structured content surfaces as **cards** — inline inserts that present:

- workflow stage status and progress
- evidence summaries
- approval requests
- cost breakdowns
- build results
- lifecycle position indicators

Cards should feel like information panels integrated into the conversation — not popups, not separate views, but part of the flow. They should inherit the same dark/blue design language as the surrounding chrome.

## Secondary interface: task-based board and management

Alongside the primary chat interface, a **task board** surface provides a traditional work management view for users who prefer scanning and filtering over conversation.

The board shows:
- **Column structure**: backlog, in-progress, in-review, blocked, completed (or custom workflow states)
- **Task cards**: one card per task/Commission, with status, assignee, priority, due date, cost, evidence summary
- **Filtering and sorting**: by status, assignee, priority, cost, date, team, repository
- **Quick actions**: drag-to-change-status, inline approval, cost drill-down, evidence view
- **Search**: find tasks by name, description, or connected artifact
- **Bulk operations**: batch approve, batch close, batch reassign

The board is **not** the primary interface — it's a secondary surface for power users and project leads. Alfred in the chat remains the primary interaction model for most users.

The board and chat are **always synchronized** — actions in one surface instantly reflect in the other. A task moved to "in-review" in the board immediately surfaces that state in the chat. An approval given in chat reflects on the task card.

**Design language:** The board inherits the same OLED/blue/semantic-accent palette and typography. Task cards use the same visual language as chat cards — ambient depth, glow on active states, semantic colour for status at a glance.

## Motion and animation

- transitions should be purposeful and fast — no decorative flourishes
- status changes, stage completions, and approval events should animate with brief, informative transitions
- glow intensity can pulse subtly to indicate active processing — never distractingly

## Design language as a living system

As the product is built out, a formal design language should be established around these principles:

- component library with consistent tokens (colour, spacing, radius, shadow/glow, typography)
- each component documents its semantic meaning — no ambiguous use of accent colours
- dark/OLED optimisation tested as a first-class concern, not a theme toggle afterthought

## Language and labelling

The visual design principle extends to language: **clarity is the first obligation**.

Business users must be able to operate the interface without learning a new vocabulary. UI labels use plain, functional language. The platform's internal naming world (forge metaphors, forge-world terms) surfaces only as secondary identity — subtle, learnable over time, never required upfront.

Alfred speaks plain English in conversation. The futuristic aesthetic and the naming depth are experienced through familiarity, not demanded on first encounter.

See `17-alfred-and-naming-world.md` for the two-layer language model.

## Design discipline: task-focused surfaces

Every screen and interaction should be designed around the specific task at hand, not generic data presentation.

This means:

| Anti-pattern | Correct approach |
|---|---|
| Generic data grid with 10 right-click options | Context-specific view showing exactly what's needed for the current task |
| Bloated settings panel with every option visible | Progressive disclosure: show defaults, advanced options hidden until needed |
| Menu with 15 actions all equally prominent | Action hierarchy: primary action obvious, secondary actions available but not intrusive |
| Dense information overload | Signal hierarchy: what matters now is prominent, context lives in cards/tooltips |
| Right-click menus with unclear options | Inline actions that make sense in context; destructive actions require explicit confirmation |

**Principle:** If a user is reviewing evidence for approval, show the evidence and approval controls — not a generic evidence browser with 20 filtering options visible by default. If they're creating a task, guide them through the refinement with Alfred — not a form with 30 fields.

The interface should feel like a capable tool shaped to the work, not a generic platform that forces users to navigate and construct their own experience.

## Striking the balance

The interface should feel ahead of its time, but every interaction must be immediately legible. Business users and engineers alike need to operate it without a learning curve on the chrome itself — the cognitive load belongs to the work, not the UI.

The balance to strike:

| Lean into | Avoid |
|---|---|
| Precise, purposeful visual language | Style that obscures meaning |
| Ambient depth and glow that communicates state | Animation that distracts or delays |
| Technical typography that feels engineered | Fonts so stylised they hurt legibility |
| Cards and structured information in the flow | Dense raw data with no hierarchy |
| A calm, authoritative aesthetic | Cold or alienating emptiness |
| Futuristic refinement | Futuristic novelty for its own sake |

The north star: a user should feel they are working with a highly capable, serious tool — and that feeling should come from how well it works, not just how it looks.

The aesthetic earns trust by being coherent and unambiguous. Every visual choice should have a reason.
