# Runtime and orchestration

## Deployment shape

The product needs a real execution layer behind the experience.

For the SaaS version, one likely direction is to run Alfred's Forge on our own AKS cluster, with Kubernetes-native scaling and control over how work is executed.

There may also be a need to ship some form of helper runtime, build server, or worker component so work can be carried out reliably where the platform needs it.

The current direction is to use one or more **prebuilt Docker containers** that include our own .NET application runtime. These containers would take work from the main Mississippi servers and execute the assigned jobs.

This implies a model where the control plane and execution plane are intentionally separated:

- Mississippi-based servers coordinate and dispatch work
- prebuilt worker containers receive and run that work
- execution environments are standardized rather than assembled ad hoc per task

## Orchestration requirement

If this is a true dark factory, then work cannot just be conversational. It needs an orchestration model that can:

- accept work from the main product interface
- queue it
- decide what needs to spin up
- start the right workers or runtimes
- track state as execution progresses

## Event bus / queue backbone

An event bus or queueing layer seems central to the architecture.

This layer would manage how:

- work items are handed off from intake to execution
- background tasks are scheduled
- workers are triggered or scaled
- state changes are propagated across the system
- progress and evidence updates are reported back to the user-facing interface

For enterprise-grade positioning, a standard such as **Kafka** may be the right default framing, even if the exact implementation choice evolves over time.

That matters because the messaging backbone should feel:

- standard
- governable
- scalable
- familiar to enterprise platform teams

## Agentic execution model

The build server or agent server should be **agentic** in how it works.

That means execution nodes are not just dumb workers consuming jobs. They may need bounded autonomy to:

- interpret delivery intent
- choose or sequence sub-steps
- recover within policy
- produce evidence as part of execution

But that agentic behavior should sit on top of a standard event-stream or queue backbone, not replace it.

In other words:

- worker behavior can be intelligent and adaptive
- orchestration and coordination still need explicit, observable infrastructure primitives

This separation is important if the system is meant to be operationally serious.

## Worker container model

The worker side likely needs to be productized as a consistent runtime image or small set of runtime images.

Those images would:

- contain our .NET worker application
- know how to receive work from the Mississippi control plane
- execute jobs in a standard runtime shape
- emit status, evidence, and lifecycle events back into the platform

This creates a cleaner operating model than treating every job runner as bespoke infrastructure.

It also supports:

- repeatable deployment
- versioned worker capabilities
- controlled dependency footprints
- easier enterprise operations and governance

## KEDA and elastic execution

KEDA-style scaling is a strong fit for the SaaS runtime shape if the system uses queues or event streams as its operational backbone.

That would support a model where:

- incoming demand drives worker scale
- different work types can fan out to specialized executors
- infrastructure cost can better follow actual workload
- the factory can feel always-on without requiring everything to run all the time

## Architectural implication

This suggests Alfred's Forge is not just:

- a chat product
- a task tracker
- an automation wrapper

It is a coordinated delivery system with:

- a conversational front end
- a control plane powered by Mississippi
- an orchestration layer
- an execution fabric
- a feedback loop that returns status, evidence, and progress to users

## Open design questions

- what the minimum viable prebuilt worker image contains
- where jobs execute in open-source versus SaaS modes
- whether the queue backbone is domain-event-oriented, job-oriented, or both
- how much of the orchestration model belongs in Mississippi versus Alfred's Forge itself
- whether Kafka is the initial standard or simply the enterprise reference point
- how much autonomy agentic workers are allowed before orchestration loses predictability
- whether there is one general-purpose worker image or several specialized container types
