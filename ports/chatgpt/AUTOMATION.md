# Automation brief

Use after the workflow passes a manual trial. This file does not create a task.

```text
Create a scheduled task called {NAME}.
Workflow: {EXACT ACTION AND ACCEPTANCE CRITERIA}.
Sources: {LINKS / IDS / QUERY}.
Schedule: {CADENCE}, time zone {IANA TIME ZONE}.
Or supported event trigger: {SERVICE / EVENT / FILTER}; confirm support first.
Destination: {THIS CHAT / SEPARATE RUN / EXPLICITLY AUTHORIZED DESTINATION}.
Allowed writes or sends: {EXACT SCOPE, OR NONE}.
State: {CHECKPOINT LOCATION}; record processed IDs and last successful run.
No change: {NO SUBSTANTIVE ALERT IF SUPPORTED / BRIEF STATUS}.
Failure: report missing coverage or access; retry at most {N} times.
Stop: {END DATE / COMPLETION CONDITION / FAILURE LIMIT}.
Confirm the saved task, trigger, time zone, destination and next run if scheduled.
If you cannot save it, say so and return the prepared configuration.
```

Choose one trigger approach supported by the host. Do not assume arbitrary
webhooks or a particular interval. Review the first run's source coverage,
artifact and delivery. A tool reporting a completed run does not prove the
business outcome was correct.
