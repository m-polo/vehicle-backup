# Vehicle Backup

Vehicle Backup recovers data from database and stores it CSV files. It starts a console app that will check data updates every second until you stop the process manually.

It includes everything you need, including:

- [.Net 8](https://learn.microsoft.com/es-es/dotnet/core/whats-new/dotnet-8/overview) as runtime
- [Geotab SDK](https://developers.geotab.com/myGeotab/introduction) to access database

It is written 100% in [C#](https://learn.microsoft.com/es-es/dotnet/csharp/whats-new/csharp-12).

## Using this template

Clone the repository:

```sh
git clone https://github.com/m-polo/vehicle-backup.git
```

## What's inside?

It has the following structure:

### Folder structure

- Entities: Main app objects.
- Interfaces: Contract definitions for Entities and Services.
- Processes: Application functionality runners.
- Services: Main app functionalities.

### Build

To build apps, run the following command:

```
dotnet build
```

### Start

To start app, run the following command:

```
dotnet run <user> <password> <server> <database> [filepath]
```

- `user`: Database user.
- `password`: Database user password.
- `server`: Database server.
- `database`: Database name.
- `filepath`: Output directory. Optional.

## Future improvements
- Request required fields only
- Improve caching
- Fix odometer calculation
- Create tests
- Dockerize application