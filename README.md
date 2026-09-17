# IceSync

I built IceSync as a take-home assignment. It keeps a local copy of workflows from the Universal Loader API and lets you view their status or ask the provider to run one.

## What happens in the system

A background worker fetches the provider’s workflows on a configurable interval. The sync handler compares them with the local SQL Server records, inserts new workflows, updates changed ones, and removes records the provider no longer returns. It saves those changes together.

The Razor Pages app reads that local copy. Clicking run sends a separate command to the provider; the next sync picks up the resulting status.

## Why I split it this way

- **Domain** holds the workflow model and the contracts for storage and provider access. The workflow comparison lives here, away from HTTP and EF Core.
- **Application** holds the sync, list, and run use cases behind MediatR handlers. Both the web app and the worker call those handlers.
- **Infrastructure** implements the contracts with EF Core and an HTTP client, including provider authentication. A change to the API or persistence code stays at that boundary.
- **BackgroundServices and WebApp** are separate hosts. One owns the sync schedule; the other handles the UI. Each sync creates a DI scope so its DbContext is disposed when the iteration ends.

That’s a fair amount of structure for a small assignment. It gives each dependency a home and makes the sync logic possible to test without running the UI or calling the provider. The Tests project is still a placeholder, so that test coverage remains work to do.

## Why sync in the background

Fetching every workflow on each page request would tie page latency and availability to the external API. A scheduled sync lets the page read from SQL Server even when the provider is slow or a sync attempt fails. The worker logs failures and tries again on the next interval.

The cost is stale data. The page shows the last successful sync, and a successful run request doesn’t immediately refresh it. The run command itself still calls the provider on the request path.

## What I’d change next

1. Add tests for insert/update/delete reconciliation, failed fetches, and repeated syncs. I’d also check that an incomplete provider response can’t accidentally delete valid local records.
2. Show the last successful sync time and provider errors in the UI. Add bounded retries with backoff for transient sync failures, and define how an uncertain run response should be handled before retrying it.
3. Replace the repeated linear lookup during comparison with a dictionary keyed by workflow ID. If multiple worker instances are needed, add a lease so they can’t reconcile the same data concurrently.
4. Add user authentication and authorization for running workflows, and restore antiforgery validation on the POST handler before exposing the app beyond the exercise.
