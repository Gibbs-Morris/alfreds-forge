# Design language and visual identity

## UX technology

The primary client is a **WebAssembly (WASM) application** — consistent with the .NET/Mississippi stack, this means Blazor WASM. The UI runs entirely in the browser with no server-side rendering dependency for the client shell. This gives desktop-class interactivity, real-time SignalR connectivity via Mississippi's Aqueduct layer, and a consistent experience across browsers without app store distribution.

**Long-term: mobile**

A native mobile application is a future consideration — not in scope for the initial build but the design and component model should not preclude it. Implications to carry forward:

- The design language should be defined in tokens (colour, spacing, typography, component semantics), not browser-only assumptions, so it can be adapted to a mobile surface
- The notification system must support mobile push as a first-class channel when a mobile app exists (see `19-notification-system.md`)
- Core interaction patterns (chat-first, card inserts, action controls inline) translate naturally to mobile — this should be a deliberate constraint, not an afterthought
- Responsive layout thinking should be baked into the component library from the start, even if the first target is desktop

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

## Striking the balance

Futuristic must not mean obscure.

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
