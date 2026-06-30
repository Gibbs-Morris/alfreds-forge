# Development patterns

A running record of agreed patterns and architectural decisions to carry forward into implementation.

---

## Pattern: Diátaxis documentation structure

**Context:** All documentation — from developer guides to user manuals — must be discoverable, reusable, and serve distinct reader needs.

**The pattern:**

All documentation follows the **Diátaxis framework**, which divides docs into four orientations:

1. **Tutorials** — Learning-oriented. Goal: help a user get started with a hands-on walkthrough. Always goal-directed, step-by-step, concrete examples. Reader is assumed to have minimal context.

2. **How-to guides** — Task-oriented. Goal: help a user accomplish a specific task. Assumes the reader already understands the basics; focuses on the "recipe" for that job.

3. **Reference** — Information-oriented. Goal: provide technical specifications, API contracts, data models, configuration options, etc. Exhaustive and systematic; used as lookup material.

4. **Explanation** — Understanding-oriented. Goal: help a reader understand *why* something works the way it does, the reasoning behind a design, or the broader context. Discursive and conceptual.

**In Alfred's Forge:**
- Tutorials and How-to guides live in a `/docs/guides/` tree, organized by feature or role
- Reference material lives in `/docs/reference/`, organized by component or subsystem
- Explanations live in `/docs/explanation/`, organized by topic
- Vision notes and architecture decisions remain in `/notes/` and are not public-facing (internal only)

**Why it matters:** Readers searching for answers won't find them in a flat wall of docs. Diátaxis creates a clear path for each reader's intent. A tutorial reader, a troubleshooting reader, and a developer learning the architecture all find the right content in the right place.

**Tooling:** Use front-matter or directory structure to tag every doc with its orientation (e.g., `type: tutorial`, `type: reference`). Build a navigation index that groups by orientation.

---

## Pattern: Blazor Razor Class Library (RCL) first

**Context:** The primary UI is Blazor WASM. A native mobile app (.NET MAUI Blazor Hybrid) is a future target. We want to share components across both without a rewrite.

**The pattern:**
- All UI components live in a shared **Razor Class Library (RCL)** — never in the web host project directly
- The Blazor WASM host and the future MAUI Blazor Hybrid host both reference the RCL
- Platform-specific services (push notifications, device APIs, file access) are abstracted behind interfaces and injected per host via DI
- The RCL has no knowledge of which host it is running in

**Project structure to follow:**
```
/Alfred.UI.Shared    ← Razor Class Library — all components, pages, models, shared services
/Alfred.UI.Web       ← Blazor WASM host (web)
/Alfred.UI.MAUI      ← .NET MAUI Blazor Hybrid host (iOS / Android) — future
```

**Why it matters:** If this discipline is followed from the first line of UI code, the mobile app is a new host project referencing the existing library — not a rewrite. If it is not followed, components built into the web host must be manually migrated later.

**Interim mobile:** Blazor WASM can be deployed as a PWA (Progressive Web App) before the MAUI app ships — installable on mobile home screens, basic push notification support, zero additional build cost.

---
