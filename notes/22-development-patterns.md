# Development patterns

A running record of agreed patterns and architectural decisions to carry forward into implementation.

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
