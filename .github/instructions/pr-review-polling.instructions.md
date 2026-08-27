---
applyTo: '**'
---

# Post-Push PR Review Polling

Governing thought: After pushing code to a branch with an open PR, agents wait
for human review and handle each new comment separately until none remain.

> Drift check: If GitHub MCP tools are unavailable, use GitHub CLI (`gh`).
> Confirm that `gh` is installed with `Get-Command gh` before use.

## Rules (RFC 2119)

- After pushing code to a branch that already has an open PR, agents **MUST**
  sleep for 300 seconds before polling for new review comments. For example,
  use `Start-Sleep -Seconds 300` in PowerShell. Why: Reviewers need time to
  inspect the pushed changes.
- Local commits that have not been pushed **MUST NOT** start the polling loop.
  Why: Reviewers cannot comment on changes that are not visible on the PR.
- Agents **MUST** poll for unresolved PR review comments and threads with
  GitHub MCP tools by default. Why: MCP is the preferred integration.
- If MCP tools are unavailable and `gh` is installed, agents **MUST** fall
  back to GitHub CLI using `gh api` and `gh api graphql` for polling and thread
  actions. Why: The CLI provides the required fallback workflow.
- If neither MCP nor `gh` is available, agents **MUST** stop and report the
  blocker. Why: Agents cannot complete the required workflow without one of
  these tools.
- When agents find new unaddressed comments, they **MUST** handle each comment
  one at a time. Why: Separate handling limits regression risk.
- Agents **MUST** follow this exact order for each comment:
  1. Read and understand the comment.
  2. Apply the minimal focused fix.
  3. Commit with a message scoped to that single comment.
  4. Push the branch.
  5. Reply to the comment thread with what changed and the commit SHA.
  6. Resolve the thread.
  Why: Isolated commits make review history auditable.
- Agents **MUST NOT** batch unrelated fixes into one commit. Why: Each fix
  must remain traceable to its review feedback.
- Agents **MUST** create one commit for each comment. Why: One commit per
  comment prevents unrelated fixes from being grouped together.
- Agents **MUST NOT** resolve a thread before pushing the fix and replying with
  evidence. Why: Premature resolution hides unfinished work.
- If an agent declines a comment because it disagrees or is out of scope, it
  **MUST** reply with its rationale. Why: The reviewer needs the author's
  reasoning.
- If an agent declines a comment, it **MUST** leave the thread open for the
  reviewer. Why: Only the reviewer or author should close a declined thread.
- Agents **SHOULD** skip review threads where `isOutdated` is `true` during the
  polling loop. Why: GitHub does not permit normal resolution of outdated
  threads.
- Agents **SHOULD** record each skipped outdated thread in the remediation
  ledger as `SKIPPED (outdated)`. Why: The ledger preserves the reason for
  skipping it.
- Agents **SHOULD** leave skipped outdated threads open for human review. Why:
  Attempts to resolve them can cause API errors or confusing state.
- If the current machine cannot complete the exact thread reply or resolution
  action with MCP or `gh`, agents **MUST** stop and report the blocker. Why:
  The required per-thread action is unavailable.
- In that case, agents **MUST NOT** substitute a top-level PR comment. Why: A
  top-level comment does not satisfy the required per-thread audit trail.
- After addressing all found comments, agents **MUST** sleep for another 300
  seconds and poll again. Why: Reviewers may add follow-up comments after
  fixes land.
- Agents **MUST** repeat the loop until either a poll returns zero new
  unaddressed comments or the configured maximum iteration cap is reached.
  Why: The cap bounds the workflow in adversarial scenarios.
- Agents **SHOULD** log each addressed thread in a running remediation ledger
  in their output. Why: The ledger provides an auditable action record.
- Agents **SHOULD** include the thread ID, status, and commit SHA in each
  ledger entry. Why: These fields make each action auditable.
- Agents **SHOULD** configure a reasonable maximum-iteration cap, such as 20
  iterations. Why: A cap prevents runaway polling.
- If the maximum iteration cap is reached, agents **MUST** log the remaining
  unresolved threads in the ledger. Why: Human reviewers need to see unfinished
  work.
- If the maximum iteration cap is reached, agents **MUST** stop with a summary
  for human review. Why: Human review is required when the bounded loop ends
  with unresolved threads.

## Scope and Audience

All agents that push code to branches associated with open pull requests.

## At-a-Glance Quick-Start

1. Start when code is pushed to a branch with an open PR.
2. Sleep 300 seconds.
3. Poll for new unresolved review comments with GitHub MCP or `gh` CLI.
4. For each comment, fix, commit, push, reply, and resolve it.
5. Sleep 300 seconds and poll again.
6. Repeat until a poll returns zero new comments or the iteration cap is
   reached.

## Procedure

### Poll and Remediate Loop

```text
[TRIGGER: code pushed to a branch with an open PR]
IF no open PR THEN EXIT
LOOP (max 20 iterations)
  SLEEP 300 seconds  (e.g. Start-Sleep -Seconds 300)
  POLL for unresolved review comments/threads
  IF no new unaddressed comments THEN EXIT LOOP
  FOR EACH unaddressed comment (one at a time)
    READ the comment and understand the request
    APPLY the minimal focused fix
    COMMIT with a message referencing the comment
    PUSH the branch
    REPLY to the thread with: what changed, commit SHA, rationale
    IF fix applied THEN RESOLVE the thread
    ELSE reply with decline rationale and LEAVE thread open
  END FOR
  IF iteration cap reached THEN LOG remaining threads and EXIT LOOP
END LOOP
```

### GitHub MCP (preferred)

Use MCP tools for:

- Fetching PR review comments and threads
- Replying to comment threads
- Resolving threads

### GitHub CLI Fallback

If MCP tools are unavailable, use these commands:

- Fetch review states and general PR discussion:

  ```text
  gh pr view <number> --json reviews,comments
  ```

  This command fetches review states (approved/changes-requested) and general
  PR discussion. It does NOT return inline review thread comments.

- List review comments on the PR:

  ```text
  gh api repos/{owner}/{repo}/pulls/{pull_number}/comments
  ```

- Fetch thread IDs and top-level comment IDs:

  ```text
  gh api graphql -f query='query($owner:String!, $repo:String!, $number:Int!) { repository(owner:$owner, name:$repo) { pullRequest(number:$number) { reviewThreads(first:100) { nodes { id isResolved isOutdated comments(first:100) { nodes { databaseId url body path line } } } } } } }' -F owner=<owner> -F repo=<repo> -F number=<pull_number>
  ```

- Reply to a top-level review comment in the thread:

  ```text
  gh api -X POST repos/{owner}/{repo}/pulls/{pull_number}/comments/{comment_id}/replies -f body='<reply>'
  ```

- Resolve a review thread:

  ```text
  gh api graphql -f query='mutation($threadId:ID!) { resolveReviewThread(input:{threadId:$threadId}) { thread { id isResolved } } }' -F threadId='<thread-node-id>'
  ```

- If any required thread action cannot be completed with `gh`, stop and
  report the blocker instead of posting a top-level PR comment.

## Core Principles

- Sleep before polling. Do not race reviewers.
- Use one comment, one commit, one reply, and one resolution.
- Prefer MCP and fall back to CLI. Never skip the feedback loop.
- Keep a ledger of actions for traceability.

## References

- PR review guide: `.github/instructions/pull-request-reviews.instructions.md`
- PR description authoring: `.github/instructions/pr-description.instructions.md`
- Shared guardrails: `.github/instructions/shared-policies.instructions.md`
- Build issue remediation:
  `.github/instructions/build-issue-remediation.instructions.md`
