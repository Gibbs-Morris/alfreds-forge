# Observability and OpenTelemetry

## Stance

Every component in Alfred's Forge must be instrumented with OpenTelemetry from the start.

This is not optional telemetry — it is a core operational requirement for both SaaS and self-hosted deployments.

## OpenTelemetry as the standard

OpenTelemetry (OTel) is the instrumentation standard across the platform:

- traces for distributed request flows across control plane, workers, and integrations
- metrics for performance, throughput, queue depths, worker utilization, and error rates
- logs structured and correlated with trace context

All three signals should flow to a configurable OTLP endpoint.

## Configurable endpoint per environment

The OTel exporter endpoint must be configurable per environment, not hardcoded.

This covers:

- SaaS production → platform-managed observability backend (e.g. Azure Monitor, Grafana, Honeycomb, or similar)
- SaaS staging/dev → separate platform-managed endpoint
- Self-hosted → customer-specified endpoint (their own Grafana, Datadog, Dynatrace, New Relic, Jaeger, etc.)

The endpoint, protocol (OTLP/gRPC or OTLP/HTTP), and any auth headers should all be injectable via environment configuration — part of the externalised config model, not baked into builds.

## What must be instrumented

| Component | Required signals |
|---|---|
| Control plane (Mississippi/Orleans) | Traces, metrics, logs |
| Event backbone (Kafka / queue) | Metrics (lag, throughput), traces |
| Worker containers | Traces (per job/stage), metrics, logs |
| AI harness (model calls) | Traces with token counts, latency, model/provider tag |
| Chat/session layer | Traces per session/turn |
| GitHub integration | Traces, error rates |
| Artifact store | Metrics (read/write latency, size) |
| RBAC/auth layer | Metrics (auth failures, token validation) |
| Billing/metering pipeline | Traces, metrics |

## AI harness instrumentation specifically

Model calls deserve special attention:

- every model invocation should emit a span with provider, model, input tokens, output tokens, latency, and outcome
- this feeds both observability (performance) and billing (usage metering)
- cost attribution per span should be possible for internal unit economics

## Self-hosted customer benefit

For self-hosted customers this means:

- they can route telemetry into their own existing observability stack
- no data leaves their estate via a telemetry channel they do not control
- they can alert, dashboard, and SLO on Alfred's Forge the same way they do any other internal platform

## Design implications

- OTel SDK configuration must be part of the standard container startup path
- no component should ship without baseline instrumentation
- trace propagation must cross component boundaries (HTTP headers, Kafka message headers) so distributed traces are complete
- sampling strategy should be configurable per environment
- sensitive data (prompts, outputs) must not appear in trace attributes — artifact references only
