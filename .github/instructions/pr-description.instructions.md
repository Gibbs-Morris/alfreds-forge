---
applyTo: '**'
---

# Pull Request Description Authoring

Governing thought: PR descriptions explain what changed and why to reviewers and
future maintainers. They communicate business value, explain the holistic
design, and show evidence clearly.

> Drift check: Open `.github/PULL_REQUEST_TEMPLATE.md` before writing a
> description. The template defines the expected structure.

## Rules (RFC 2119)

### PR Titles

- PR titles **MUST** describe what the change accomplishes in human-readable
  terms. Why: Titles appear in changelogs, release notes, and git history.
- PR titles **MUST NOT** describe only implementation details. Why:
  Implementation details do not explain the change to reviewers or users.
- Authors **MUST** update PR titles on each commit or push when the scope or
  nature of the change evolves. Why: Stale titles mislead reviewers and pollute
  history.
- PR titles **MUST** end with a semver bump suffix for GitVersion
  (`+semver: <type>`). Why: The repository uses
  `commit-message-incrementing: Enabled` to derive versions from squash-merge
  commit messages.

**Semver suffix patterns (choose one):**

1. Breaking change: `+semver: breaking`. Use it when the change removes or
   renames public APIs, or changes behavior in incompatible ways.
2. New feature: `+semver: feature`. Use it when the change adds new
   capabilities without breaking existing behavior.
3. Bug fix: `+semver: fix`. Use it when the change corrects a defect.
4. No version bump: `+semver: skip`. Use it for docs-only changes, CI config,
   and refactors with no public API changes.

**Title examples:**

- `Add fire-and-forget event effects for async side effects +semver: feature`
- `Fix null reference in aggregate grain activation +semver: fix`
- `Remove deprecated ILegacyEventStore interface +semver: breaking`
- `Update PR template and authoring instructions +semver: skip`

### PR Descriptions

- Authors **MUST** update the PR description on each commit or push when the
  branch has an open PR. Why: The description must match the current changes.
- Authors **MUST** compare the branch with `main` before writing or updating
  the description. Why: The description must reflect the complete diff.
- Authors **MUST** lead with first-principles context. Why: Reviewers need the
  intended outcome before they assess implementation details.
- First-principles context **MUST** explain the problem or opportunity. Why:
  Reviewers need to know why the change is needed.
- First-principles context **MUST** explain the change. Why: Reviewers need to
  know what the PR does.
- First-principles context **MUST** explain the expected impact. Why:
  Reviewers need to know the expected result.
- First-principles context **MUST** explain the scope and non-goals. Why: Clear
  boundaries prevent incorrect assumptions.
- Authors **MUST** include a `How to Read This PR and Code` section. Why:
  Reviewers need a clear route through the change.
- The `How to Read This PR and Code` section **MUST** name the review path.
  Why: Reviewers need to know how to inspect the change.
- The `How to Read This PR and Code` section **MUST** name the starting point.
  Why: Reviewers need a clear place to begin.
- The `How to Read This PR and Code` section **MUST** name the areas that need
  human judgment. Why: Reviewers need to focus on important decisions.
- Authors **MUST** include a `Business Value` section that explains why the
  change matters. Why: Reviewers and future maintainers need context beyond
  "what".
- Authors **MUST** include a `How It Works` section with an architectural
  overview. Why: The design must be clear enough to prevent incorrect use and
  support review.
- Authors **MUST** include a `Story Context` section. Why: Each PR must remain
  understandable within the larger product story.
- `Story Context` **MUST** describe what the PR builds on. Why: Reviewers need
  to understand prior work.
- `Story Context` **MUST** describe what the PR delivers. Why: Reviewers need to
  understand the current result.
- `Story Context` **MUST** describe what the PR enables. Why: Reviewers need to
  understand the next steps.
- `Story Context` **MUST** describe what the PR defers. Why: Reviewers need to
  understand the remaining scope.
- Authors **MUST** include a `Testing and Evidence` section. Why: Reviewers
  need reproducible evidence.
- `Testing and Evidence` **MUST** describe tests added or changed. Why:
  Reviewers need to see behavior coverage.
- `Testing and Evidence` **MUST** describe commands and results. Why:
  Reviewers need to reproduce the checks.
- `Testing and Evidence` **MUST** describe manual validation. Why: Automated
  tests do not cover every user-facing behavior.
- `Testing and Evidence` **MUST** describe known gaps. Why: Reviewers need to
  understand untested behavior.
- AI-assisted PRs **MUST** identify the AI scope. Why: Human reviewers must
  distinguish generated work from verified behavior.
- AI-assisted PRs **MUST** identify the human decisions and verification
  performed. Why: Human reviewers must know what a person decided and checked.
- AI-assisted PRs **MUST** identify unresolved uncertainty. Why: Human
  reviewers must know what remains unverified.
- Authors **SHOULD** include `Common Use Cases` with concrete examples across
  different domains. Why: Examples show where the change applies.
- Authors **SHOULD** include ASCII or Mermaid architecture diagrams for
  non-trivial changes. Why: Diagrams make complex designs easier to understand.
- Authors **MUST** list every new and modified file with a brief description.
  Why: The file list gives reviewers a change map.
- Authors **MUST** document breaking changes with before and after code
  examples. Why: Users need clear migration guidance.
- Authors **SHOULD** keep risk, rollout, rollback, migration, security, and
  performance sections when they are relevant. Why: These sections expose
  meaningful delivery risks.
- Authors **SHOULD** remove risk, rollout, rollback, migration, security, and
  performance sections when they are not relevant. Why: Routine PRs should not
  contain unnecessary process.
- Authors **SHOULD** include code examples that demonstrate typical usage. Why:
  Copy-paste examples reduce adoption friction.
- Descriptions **MUST NOT** contain stale information from previous iterations.
  Why: Outdated content misleads reviewers.

## Scope and Audience

All contributors creating or updating pull requests.

## At-a-Glance Quick-Start

- Title format: `<Human-readable summary> +semver: <feature|fix|breaking|skip>`
- Before writing, run `git diff main...HEAD --stat` to see all changes.
- Use the PR template in `.github/PULL_REQUEST_TEMPLATE.md`.
- Update the title and description on every push when the branch has an open PR.
- Start with first-principles change and impact.
- Then guide the code review.

## Procedure

### Initial PR Description

1. Run `git diff main...HEAD --stat` to list all changed files.
2. Read each changed file to understand the change.
3. Identify the problem, desired outcome, and concrete impact.
4. Write the code-reading guide so reviewers can navigate the change.
5. Write the `Business Value` section.
6. Add `Story Context` for preceding, current, and follow-up PRs.
7. Document `How It Works` with an architectural overview.
8. Record tests, commands, results, manual validation, and known gaps.
9. List all changed files with brief descriptions.
10. Add code examples for new APIs or patterns.
11. Document relevant risk and breaking changes with migration guidance.
12. Disclose AI contribution and human verification when applicable.

### Updating on Subsequent Commits

1. After each commit or push, review changes since the last description update.
2. Update first-principles impact, scope, and the code-reading guide if the
   change evolved.
3. Update `Story Context` if dependencies, sequencing, or follow-up work changed.
4. Update the file list if files were added, removed, or renamed.
5. Update code examples if APIs changed.
6. Update testing evidence and AI or human review notes when verification changes.
7. Verify that `Business Value` still reflects the PR scope.
8. Remove stale information that no longer applies.

## Template Structure

The PR template includes these core sections:

1. **First Principles: What Changes and What Is the Impact?** - Problem,
   outcome, impact, and non-goals. Required.
2. **How to Read This PR and Code** - Review route and human-focus areas. Required.
3. **Business Value** - Who benefits and why it matters. Required.
4. **How It Works** - Architecture, flow, boundaries, and trade-offs. Required
   for non-trivial changes.
5. **Story Context** - Place in the larger product or technical sequence. Required.
6. **Testing and Evidence** - Tests, commands, results, validation, and gaps. Required.
7. **Files Changed** - Complete file manifest. Required.
8. **Checklist** - Merge-readiness confirmations. Required.

The template also provides conditional sections for `Common Use Cases`,
`Observability`, `Risk, Rollout, and Rollback`, `AI Contribution`, `Breaking
Changes and Migration`, and `Related Issues and Dependencies`.

## Good vs Bad Examples

### Bad: Implementation-focused

> "Added FireAndForgetEffectWorkerGrain.cs that implements
> IFireAndForgetEffectWorkerGrain interface"

### Good: Value-focused

> "Commands now return immediately without waiting for external API calls.
> Effects like sending notifications or updating analytics run asynchronously
> in background worker grains, reducing p99 latency by up to 500ms for commands
> with I/O-heavy side effects."

### Bad: Missing context

> "Changed HandleAsync signature to include brookKey parameter"

### Good: Complete context

> "**Breaking change**: The `HandleAsync` method on `EventEffectBase` now
> requires `brookKey` and `eventPosition` parameters. This enables effects to
> correlate with specific aggregate instances and event positions for debugging
> and idempotency. Existing effects must add these parameters (they can be
> ignored if not needed)."

## Core Principles

- Lead with first-principles change and impact.
- State the review path clearly.
- Write for reviewers without prior context.
- Connect each PR to the larger story.
- Include enough detail for another person to implement similar functionality.
- Treat tests and results as evidence.
- Keep the description synchronized with the code changes.
- Use tables and diagrams for complex relationships.

## References

- PR template: `.github/PULL_REQUEST_TEMPLATE.md`
- Review guidelines: `.github/instructions/pull-request-reviews.instructions.md`
- Markdown conventions: `.github/instructions/markdown.instructions.md`
