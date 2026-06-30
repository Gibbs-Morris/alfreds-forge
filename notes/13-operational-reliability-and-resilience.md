# Operational reliability and resilience

## Platform reliability expectations

Alfred's Forge is targeting large enterprises. That means the platform itself must meet enterprise-grade operational expectations, not just deliver enterprise features.

## Control plane resilience

The Mississippi-based control plane must be designed for high availability:

- multi-region deployment for the SaaS control plane
- no single point of failure in the orchestration or event backbone
- active/active or active/passive regional failover depending on component
- regional isolation so a zone or region failure does not take down the full platform

## Data backup

All platform-owned data must be backed up:

- backlog, work items, and session state
- audit and billing ledgers
- workflow definitions and instruction packs
- run history, evidence artifacts, and cost records
- RBAC/identity configuration

Backup requirements:

- regular automated backups with defined retention
- point-in-time recovery for critical data stores
- backup verification (not just write — confirm restorability)
- geo-redundant backup storage separate from primary

## Event backbone resilience

The event bus (Kafka or equivalent) must be treated as a critical infrastructure component:

- replicated across availability zones minimum
- durable message retention to allow replay on partial failure
- consumer group offsets preserved through failure scenarios
- dead-letter handling for events that fail processing

## Worker execution resilience

Worker containers should be stateless where possible so they can restart cleanly:

- failed jobs should be retryable without side effects
- partially completed workflow runs should resume from last known state (CleanSquad run-state model applies here)
- worker node failure should not lose in-flight work

## Multi-region considerations

| Concern | Direction |
|---|---|
| Control plane | Multi-region active/passive or active/active |
| Event backbone | At minimum multi-AZ, ideally multi-region replication |
| Data stores | Geo-redundant with failover |
| Worker execution | Can run regionally, closer to customer estate |
| Config and GitOps | Git-backed inherits SCM redundancy |
| Secrets | Must use HA-capable secrets store (e.g. Key Vault with geo-redundancy) |

## Self-hosted implications

For self-hosted customers, the platform should:

- document minimum HA deployment topology clearly
- support deployment on multi-node Kubernetes clusters
- not assume single-node operation for production workloads
- allow customers to define their own RTO/RPO targets through configuration

## Open questions

- whether the SaaS control plane is active/active or active/passive across regions
- RTO and RPO targets for the managed SaaS offering
- whether tenant data is region-pinned or replicated (data sovereignty vs resilience trade-off)
- how partial regional degradation surfaces to users vs full failover
