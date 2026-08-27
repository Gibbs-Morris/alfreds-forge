---
applyTo: '**'
---

# Pull Request Description Authoring

Governing thought: PR descriptions should communicate business value, explain the holistic design, and enable reviewers and future maintainers to understand both what changed and why.

> Drift check: Open `.github/PULL_REQUEST_TEMPLATE.md` before writing descriptions; the template defines the expected structure.

## Rules (RFC 2119)

### PR Titles

- PR titles **MUST** be human-readable descriptions of what the change accomplishes (not implementation details). Why: Titles appear in changelogs, release notes, and git history.
- PR titles **MUST** be updated on each commit/push when the scope or nature of the change evolves. Why: Stale titles mislead reviewers and pollute history.
- PR titles **MUST** end with a semver bump suffix for GitVersion (`+semver: <type>`). Why: Repository uses `commit-message-incrementing: Enabled` to derive versions from squash-merge commit messages.

**Semver suffix patterns (choose one):**

| Change Type | Suffix | When to Use |
|-------------|--------|-------------|
| Breaking change | `+semver: breaking` | Removes/renames public APIs, changes behavior in incompatible ways |
| New feature | `+semver: feature` | Adds new capabilities without breaking existing behavior |
| Bug fix | `+semver: fix` | Corrects defects without adding features |
| No version bump | `+semver: skip` | Docs-only, CI config, refactors with no public API changes |

**Title examples:**

- `Add fire-and-forget event effects for async side effects +semver: feature`
- `Fix null reference in aggregate grain activation +semver: fix`
- `Remove deprecated ILegacyEventStore interface +semver: breaking`
- `Update PR template and authoring instructions +semver: skip`

### PR Descriptions

- PR descriptions **MUST** be updated on each commit/push when a PR exists for the branch. Why: Keeps the description synchronized with the actual changes.
- Authors **MUST** compare the branch against `main` to understand all changes before writing or updating the description. Why: Ensures the description reflects the complete diff.
- Authors **MUST** lead with first-principles context explaining the problem or opportunity, the change, the expected impact, and the scope/non-goals. Why: Reviewers need the intended outcome before evaluating implementation details.
- Authors **MUST** include a How to Read This PR and Code section naming the review path, starting point, and areas requiring human judgment. Why: Directs attention to the code and decisions that matter most.
- Authors **MUST** include a Business Value section explaining why the change matters. Why: Reviewers and future maintainers need context beyond "what" to understand "why".
- Authors **MUST** include a How It Works section with architectural overview. Why: Holistic understanding prevents incorrect usage and aids code review.
- Authors **MUST** include Story Context describing what the PR builds on, delivers, enables, and defers. Why: A sequence of PRs should remain understandable as one evolving product story.
- Authors **MUST** include Testing and Evidence describing tests added or changed, commands and results, manual validation, and known gaps. Why: A claimed change is not trustworthy without reproducible evidence.
- AI-assisted PRs **MUST** identify AI's scope, the human decisions and verification performed, and unresolved uncertainty. Why: Human reviewers must be able to distinguish generated work from verified behavior.
- Authors **SHOULD** include Common Use Cases with concrete examples across different domains. Why: Helps users understand applicability to their problems.
- Authors **SHOULD** include architecture diagrams (ASCII or Mermaid) for non-trivial changes. Why: Visual representations accelerate understanding.
- Authors **MUST** list all new and modified files with brief descriptions. Why: Provides a roadmap for reviewers and documents the change scope.
- Authors **MUST** document breaking changes with before/after code examples. Why: Enables users to migrate without guessing.
- Risk, rollout, rollback, migration, security, and performance sections **SHOULD** be kept when relevant and removed when they are not. Why: Conditional prompts surface meaningful risk without making routine PRs bureaucratic.
- Authors **SHOULD** include code examples that demonstrate typical usage. Why: Copy-paste examples reduce adoption friction.
- Descriptions **MUST NOT** contain stale information from previous iterations. Why: Outdated content misleads reviewers.

## Scope and Audience

All contributors creating or updating pull requests.

## At-a-Glance Quick-Start

- Title format: `<Human-readable summary> +semver: <feature|fix|breaking|skip>`
- Before writing: `git diff main...HEAD --stat` to see all changes
- Use the PR template in `.github/PULL_REQUEST_TEMPLATE.md`
- Update title and description on every push if PR exists
- Start with first-principles change and impact, then guide the code review

## Procedure

### Initial PR Description

1. Run `git diff main...HEAD --stat` to list all changed files
2. Read through each changed file to understand what was done
3. Identify the problem, desired outcome, and concrete impact
4. Write the code-reading guide so reviewers can navigate the change
5. Write the Business Value section
6. Add Story Context for preceding, current, and follow-up PRs
7. Document How It Works with architecture overview
8. Record tests, commands, results, manual validation, and known gaps
9. List all files changed with brief descriptions
10. Add code examples for new APIs or patterns
11. Document relevant risk and any breaking changes with migration guidance
12. Disclose AI contribution and human verification when applicable

### Updating on Subsequent Commits

1. After each commit/push, review changes since last description update
2. Update first-principles impact, scope, and the code-reading guide if the change evolved
3. Update Story Context if dependencies, sequencing, or follow-up work changed
4. Update file lists if files were added/removed/renamed
5. Update code examples if APIs changed
6. Update testing evidence and AI/human-review notes when verification changes
7. Verify Business Value still accurately reflects the PR scope
8. Remove any stale information that no longer applies

## Template Structure

The PR template includes these core sections:

1. **First Principles: What Changes and What Is the Impact?** - Problem, outcome, impact, and non-goals (required)
2. **How to Read This PR and Code** - Review route and human-focus areas (required)
3. **Business Value** - Who benefits and why it matters (required)
4. **How It Works** - Architecture, flow, boundaries, and trade-offs (required for non-trivial changes)
5. **Story Context** - Place in the larger product or technical sequence (required)
6. **Testing and Evidence** - Tests, commands, results, validation, and gaps (required)
7. **Files Changed** - Complete file manifest (required)
8. **Checklist** - Merge-readiness confirmations (required)

The template also provides conditional sections for common use cases, observability, risk/rollout/rollback, AI contribution, breaking changes/migration, and related issues/dependencies.

## Good vs Bad Examples

### Bad: Implementation-focused

> "Added FireAndForgetEffectWorkerGrain.cs that implements IFireAndForgetEffectWorkerGrain interface"

### Good: Value-focused

> "Commands now return immediately without waiting for external API calls. Effects like sending notifications or updating analytics run asynchronously in background worker grains, reducing p99 latency by up to 500ms for commands with I/O-heavy side effects."

### Bad: Missing context

> "Changed HandleAsync signature to include brookKey parameter"

### Good: Complete context

> "**Breaking change**: The `HandleAsync` method on `EventEffectBase` now requires `brookKey` and `eventPosition` parameters. This enables effects to correlate with specific aggregate instances and event positions for debugging and idempotency. Existing effects must add these parameters (they can be ignored if not needed)."

## Core Principles

- Lead with first-principles change and impact, not implementation details
- Make the review path explicit so humans can focus their judgment
- Write for the reviewer who has no context
- Make each PR self-contained while connecting it to the larger story
- Include enough detail that someone could implement similar functionality
- Treat tests and results as evidence, not a checkbox
- Keep descriptions synchronized with actual code changes
- Use tables and diagrams to convey complex relationships

## References

- PR template: `.github/PULL_REQUEST_TEMPLATE.md`
- Review guidelines: `.github/instructions/pull-request-reviews.instructions.md`
- Markdown conventions: `.github/instructions/markdown.instructions.md`
