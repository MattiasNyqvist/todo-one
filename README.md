# TodoOne

Demo application for the blog series **From Zero to Azure** — a minimal todo list built with ASP.NET Core, deployed to Azure App Service through an Azure DevOps pipeline.

The point of this repository is not the application. It is the path from an empty folder to a running app in the cloud, with automated build, test and deployment.

## The series

| Part | What it covers |
| --- | --- |
| [Part 1 — Set up the environment](/blog/from-zero-to-azure-part-one) | Tools, accounts, Azure SQL database |
| [Part 2 — Keep an eye on costs](/blog/from-zero-to-azure-part-two) | Budgets, cost alerts, anomaly detection |
| [Part 3 — Build the app](/blog/from-zero-to-azure-part-three) | This repository, from empty folder to running locally |
| [Part 4 — Set up the services](/blog/from-zero-to-azure-part-four) | GitHub, Azure App Service, Azure DevOps |
| [Part 5 — Build the pipeline](/blog/from-zero-to-azure-part-five) | YAML pipeline, PR validation, deployment |

Written in Swedish. Code, comments and resource names are in English.

## What it does

- Razor Page at `/todos` — add, complete and delete items
- REST API at `/api/todo` — full CRUD
- Entity Framework Core against Azure SQL, migrations applied at startup
- Unit tests running against an in-memory provider

## Stack

ASP.NET Core 10 · Razor Pages · Entity Framework Core · Azure SQL · Azure App Service · Azure DevOps Pipelines · xUnit

## Running it locally

You need the .NET 10 SDK and an Azure SQL database.

```bash
git clone https://github.com/<username>/todo-one.git
cd todo-one/src/TodoOne
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your connection string>"
dotnet run
```

The app applies pending migrations on startup, so the schema is created on first run.

## About the connection string

There is no connection string in this repository, and there never will be. `appsettings.json` holds an empty `DefaultConnection` key; the real value lives in [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) locally and in App Service configuration in Azure.

Keeping credentials out of source control is one of the things the series sets out to demonstrate.

## Pipeline

`azure-pipelines.yml` defines two stages:

- **Build** — restore, build, test, publish, upload artifact
- **Deploy** — download artifact, deploy to Azure App Service

Pull requests run the build stage only. Deployment happens on `main`.

## License

MIT — see [LICENSE](LICENSE).
