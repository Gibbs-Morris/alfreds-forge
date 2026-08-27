# Pull Request Narrative

<!--
Write for a reviewer who has no context beyond this PR. Keep the completed
description concise, remove sections that do not apply, and replace every
placeholder with evidence or an explicit "None" / "Not applicable".
-->

## First Principles: What Changes and What Is the Impact?

<!--
Start with the underlying problem or opportunity, not the files changed.
Explain the behavior before and after this PR and the concrete user, business,
operator, or developer impact.
-->

- **Problem or opportunity:**
- **Change:**
- **Expected impact:**
- **Scope and non-goals:**

## How to Read This PR and Code

<!--
Give reviewers a short route through the change. Name the best starting file,
the suggested reading order, the decisions that need human judgment, and any
generated or boilerplate files that do not need detailed review.
-->

- **Suggested reading order:**
- **Start here:**
- **Review focus:**
- **Generated or boilerplate files:**

## Business Value

<!--
State who benefits, what value is delivered, and how the outcome will be
recognized or measured. Do not restate the implementation summary.
-->

- **Who benefits:**
- **Value delivered:**
- **Success signal:**

## Common Use Cases (if applicable)

<!-- Give concrete examples of where this change is useful. Remove when not applicable. -->

- **Use case:**
- **Example:**

## How It Works

<!--
Describe the design and flow at the level needed to understand the change.
Include a small diagram or code example when it makes a non-trivial boundary
clearer.
-->

- **Design and flow:**
- **Boundaries and dependencies:**
- **Important trade-offs:**

## Observability (if applicable)

<!-- Describe new or changed logs, metrics, traces, dashboards, and alerts. Remove when not applicable. -->

- **Signals:**
- **Operational follow-up:**

## Story Context

<!--
Make this PR useful when read beside the next nine PRs. Explain where it fits
in the larger product or technical story, including stacked or dependent PRs.
For a standalone change, say that it starts or completes the story.
-->

- **Builds on:**
- **This PR establishes or delivers:**
- **Enables or follows:**
- **Intentionally deferred:**

## Testing and Evidence

<!--
Describe the tests built or changed as part of this PR, the existing tests
that were run, the exact commands and results, and any manual validation.
State meaningful gaps instead of implying that untested behavior is covered.
-->

- **Tests added or changed:**
- **Commands and results:**
- **Manual or UX validation:**
- **Coverage and known gaps:**

## Risk, Rollout, and Rollback (if applicable)

<!--
Keep this section for runtime, API, data, deployment, security, performance,
or user-facing changes. Otherwise remove it.
-->

- **Risk level:**
- **Rollout or deployment plan:**
- **Rollback or kill switch:**
- **Migration, configuration, security, or performance notes:**

## AI Contribution and Human Review (if applicable)

<!--
Keep this section when AI tools or agents contributed to the PR. Identify
where AI generated, edited, or reviewed work; state the human decisions and
verification performed; and call out unresolved uncertainty.
-->

- **AI assistance and scope:**
- **AI-produced or modified areas:**
- **Human decisions and verification:**
- **Open uncertainty or follow-up:**

## Files Changed

<!--
List every new, modified, renamed, and deleted file with a brief purpose.
Account for generated files, lock files, and unusually large diffs.
-->

- `path/to/file`: purpose of the change.

## Breaking Changes and Migration (if applicable)

<!--
Keep this section for public API, contract, storage, or behavior changes.
State "None" or remove it when not applicable. For a breaking change, include
before and after examples plus clear migration steps.
-->

- **Breaking change:** Yes / No
- **Before:**
- **After:**
- **Migration steps:**

## Related Issues and Dependencies

<!-- Link issues, dependent PRs, design documents, or follow-up work. -->

- **Related issue or work item:**
- **Dependencies or follow-up:**

## Checklist

- [ ] The title is human-readable and ends with the correct `+semver:` suffix.
- [ ] The PR has one focused responsibility and a clear scope.
- [ ] The first-principles impact, code-reading guide, business value, and story context are complete.
- [ ] Tests were added or updated for changed behavior, or the reason is documented above.
- [ ] Quality-gate commands and results are recorded above.
- [ ] Documentation, release notes, and generated files are updated or explicitly not applicable.
- [ ] Risk, rollout, rollback, and migration details are included or their sections were removed as not applicable.
- [ ] AI contribution and human verification are disclosed when applicable.
