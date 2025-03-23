# Steno
An enhanced notepad with tagging, and searching capabilities to keep notes organized.

## Project structure

The project consists of the following components:

* Mobile - User-facing mobile application (.NET MAUI, Android only).
* API - Azure Functions serverless REST API used by the mobile application to retrieve and save content.
* Migrations - Changes to apply to the database schema (SQL Server).

## Requirements

* .NET 9
* .NET MAUI workload
* Azure development workload
* SQL Server
* Azure subscription
* Blob storage (Azure storage account)

## Getting started

Open solution in Visual Studio.

### Run the migrations

1. Set the `Migrations` project as startup project
1. Specify the database connection string as `Database` in the application settings. A default is provided in `appsettings.Development.json` that targets the local SQL Server instance provided by Visual Studio.
2. _(Optional)_ To import notes from another source:
    1. Export the notes in a `.txt` file. Each note should have the following format: `2025-03-22 2:30 PM: note content`. Notes may be split into multiple files.
    2. Upload the files to the blob storage under a container named `import-files`.
    3. Specify the blob storage connection string as `BlobStorage` in the application settings.
    4. Add the `import` command line argument to the `Migrations` project.
3. Run the `Migrations` project.

This will run every migration script contained under _Migrations > Scripts_ and optionally import the data from the blob storage.

### Run the API and mobile app

1. Set multiple startup project: `API` and `Mobile`.
2. Run the projects.