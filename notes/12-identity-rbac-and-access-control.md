# Identity, RBAC, and access control

## Model

The platform must implement a managed RBAC model with a clear three-tier hierarchy:

```
Permission → Role → Group / User assigned to Role
```

- **Permissions** are granular capability grants (e.g. "approve plan", "trigger build", "view cost data", "manage instruction packs")
- **Roles** are named collections of permissions (e.g. "Developer", "Delivery Lead", "Enterprise Admin", "Auditor")
- **Groups and Users** are assigned to one or more roles

This gives administrators fine-grained control without having to manage permissions per individual.

## Identity provider integration

The platform must support external identity providers rather than maintaining its own user directory.

Priority targets:

- Azure Active Directory / Entra ID
- other OAuth2 / OIDC-compliant providers
- SAML for enterprise SSO scenarios

The key capability is **group mapping**: AD groups (or equivalent) should be mappable directly to platform roles, so access control follows the customer's existing organisational structure without requiring manual duplication.

## Group-to-role mapping

When an AD group is assigned to a role, all members of that group inherit its permissions automatically.

This means:

- joiner/mover/leaver access is managed in the identity provider, not in Alfred's Forge
- no shadow user management required
- access changes propagate without manual platform admin steps

## Scope of RBAC

Permissions should cover all major platform surfaces:

| Domain | Example permissions |
|---|---|
| Backlog | view, create, edit, approve, delete work items |
| Build | trigger, cancel, view results |
| Approvals | approve/reject plans, architecture, experiments |
| Workflow config | view, edit, deploy workflow definitions |
| Instruction packs | view, edit, publish enterprise/repo-type packs |
| Cost and billing | view cost reports, manage billing config |
| Governance/audit | view audit logs, export reports |
| Admin | manage roles, manage identity mappings, manage secrets references |

## Enterprise and multi-tenant considerations

- enterprise-wide roles should be manageable at org level
- team or repo-level roles should be possible for finer-grained delegation
- role assignments should themselves be auditable
- permission changes should be captured in the audit trail

## Self-hosted considerations

- customers should be able to bring their own identity provider
- group sync should work with on-prem AD via LDAP or Azure AD Connect where needed

## Open questions

- whether permission scoping extends to individual repositories or workflow definitions
- how role delegation works (can a team admin grant roles within their scope only?)
- whether time-limited or approval-gated role escalation is needed for sensitive operations
