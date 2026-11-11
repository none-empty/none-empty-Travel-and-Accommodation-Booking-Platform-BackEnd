# Travel and Accommodation Booking Platform

A REST API for hotel discovery, user authentication, reservation creation, and administrative management of cities, hotels, and rooms.

The backend uses ASP.NET Core 9, Entity Framework Core with SQL Server, MediatR, and FluentValidation. JWT access tokens and refresh tokens provide authentication. Redis stores access-token revocations, and Papercut SMTP captures reservation emails during development.

## Features

- User registration, login, refresh-token rotation, and logout.
- Hotel search, hotel details, featured deals, trending destinations, and recently visited hotels.
- Reservation creation, price calculation, and confirmation emails.
- Role-based access to city, hotel, and room administration.
- Swagger UI for exploring and calling the API in Development mode.

## Solution structure

| Project | Responsibility |
| --- | --- |
| [API](Travel-and-Accommodation-Booking-Platform-BackEnd) | Controllers, request DTOs, authentication, middleware, and application startup. |
| [Application](Travel-and-Accommodation-Booking-Platform-BackEnd.Application) | Commands, queries, handlers, validation, and service interfaces. |
| [Domain](Travel-and-Accommodation-Booking-Platform-BackEnd.Domain) | Entities and domain enums. |
| [Infrastructure](Travel-and-Accommodation-Booking-Platform-BackEnd.Infrastructure) | Database access, migrations, repositories, JWT generation, Redis, and email services. |

The editable [database schema diagram](docs/database-schema.drawio) opens in draw.io Desktop or [diagrams.net](https://app.diagrams.net/). It includes all 13 tables, columns, SQL types, primary and foreign keys, nullable columns, unique indexes, and the 14 foreign-key relationships. Its second page documents check constraints and enum storage. The diagram follows the [migration snapshot](Travel-and-Accommodation-Booking-Platform-BackEnd.Infrastructure/Migrations/AppDbContextModelSnapshot.cs), cross-checked against the domain entities and [EF Core mappings](Travel-and-Accommodation-Booking-Platform-BackEnd.Infrastructure/AppDbContextFiles/AppDbContext.OnModelCreating.cs); update it when the schema changes.

## Requirements

| Run mode | Requirements |
| --- | --- |
| Full development stack | Docker Engine or Docker Desktop with Linux containers and Docker Compose. |
| Docker with host SQL Server | Docker and Compose, an accessible SQL Server instance on the host, and a populated local settings file. |
| API from the .NET CLI | .NET 9 SDK, ASP.NET Core 9 runtime, SQL Server, Redis, an SMTP endpoint, and a populated local settings file. Docker can provide the dependencies. |

All commands below run from the repository root. The Compose files are standalone alternatives and share published ports; one stack runs at a time.

## Run the full development stack

```bash
docker compose -f compose.development.yaml up -d --build --wait --wait-timeout 180
```

This stack builds the API and pulls SQL Server, Redis, and Papercut images. Connection settings and development credentials are supplied by Compose; an existing service instance or local settings file is not required.

The API waits for SQL Server and Redis readiness, then applies the existing migrations before accepting requests. Initial image downloads and builds can take several minutes. Migrations create the database schema without sample data.

| Service | Address |
| --- | --- |
| API | <http://localhost:5151> |
| Swagger UI | <http://localhost:5151/swagger> |
| SQL Server | `localhost,1433` |
| Redis | `localhost:6379` |
| SMTP | `localhost:2525` |
| Papercut web UI | <http://localhost:37408> |

The SQL login and public development-only defaults are defined in [compose.development.yaml](compose.development.yaml). Private credentials and signing keys belong in the ignored `appsettings.Development.json` used by the host-SQL and local API modes. The complete container stack supplies its own disposable defaults. Published ports bind to localhost. SQL Server, Redis, and captured email use persistent Docker volumes.

The API and SQL Server container use matching development passwords. Changing this password after a SQL data volume has been initialized also requires updating that SQL login's password; changing the container environment alone does not update an existing database login.

## Run Docker with an existing host SQL Server

The [default Compose file](compose.yaml) runs the API, Redis, and Papercut alongside an existing SQL Server instance on the host.

This mode requires `Travel-and-Accommodation-Booking-Platform-BackEnd/appsettings.Development.json` with the database name, credentials, and remaining application settings described below. The SQL login requires permission to apply migrations and, when necessary, create the database.

```bash
docker compose -f compose.yaml up -d --build --wait --wait-timeout 180
```

The settings file is mounted read-only at runtime. `Database__Server` overrides only the SQL server address with `host.docker.internal,1433`; the database name, credentials, and other connection options remain in the private settings file. The host SQL instance must accept connections from Docker on that address and port.

Redis and Papercut pass readiness checks before the API starts. Development mode enables Swagger and automatic migrations. The API health check verifies that Swagger responds after startup. Redis uses append-only persistence, Papercut messages use a named volume, and all services have restart policies. Redis and Papercut images are pinned to immutable digests.

The API, Swagger, Redis, SMTP, and Papercut addresses are the same as in the full development stack.

## Local configuration

[appsettings.json](Travel-and-Accommodation-Booking-Platform-BackEnd/appsettings.json) is a tracked template with empty values. Actual local values belong in `appsettings.Development.json` in the same directory. That file is ignored by Git and excluded from Docker image builds.

Both JSON files contain the same configuration keys. New local options are also represented in the tracked template with empty string or null values.

For first-time local configuration, when the development settings file does not already exist:

```bash
cp Travel-and-Accommodation-Booking-Platform-BackEnd/appsettings.json \
   Travel-and-Accommodation-Booking-Platform-BackEnd/appsettings.Development.json
```

The local settings file requires these values:

| Section | Configuration |
| --- | --- |
| `Logging:LogLevel` | Log levels such as `Information` for `Default` and `Warning` for `Microsoft.AspNetCore`. |
| `AllowedHosts` | Allowed host names, or `*` for local development. |
| `ConnectionStrings:sqlserver` | SQL Server connection string containing the server, database, login, password, and connection options. |
| `Redis:connection` | Redis connection, such as `localhost:6379,abortConnect=false`. |
| `JwtSettings` | A signing key of at least 32 UTF-8 bytes, issuer, audience, and a positive `ExpiresInMinutes`. |
| `RefreshTokenSettings:ExpiresInDays` | Positive refresh-token lifetime in days. |
| `Email` | Sender email, sender name, SMTP host, and SMTP port. Papercut uses `localhost` and port `2525` for a local API process. |
| `Database:Server` | Optional server-address override; empty for a local API process. |
| `Database:ApplyMigrationsOnStartup` | Boolean migration flag. Automatic migrations also require the Development environment. |

Environment variables override JSON values. Both Compose files supply their container-specific server addresses. The full development stack also supplies its own shared JWT and SQL credentials independently of private local configuration.

## Run the API from the .NET CLI

This mode uses the populated development settings file and the published localhost service endpoints.

The dependency containers start with:

```bash
docker compose -f compose.development.yaml up -d --wait --wait-timeout 180 sqlserver redis papercut-smtp
```

If the full development stack's API container is already running, its port is released with:

```bash
docker compose -f compose.development.yaml stop travel-and-accommodation-booking-platform-backend
```

Manual migrations require the EF Core CLI. If the tool is not already installed:

```bash
dotnet tool install --global dotnet-ef --version 9.0.11
```

The database is created or updated with:

```bash
ASPNETCORE_ENVIRONMENT=Development dotnet ef database update \
  --project Travel-and-Accommodation-Booking-Platform-BackEnd.Infrastructure \
  --startup-project Travel-and-Accommodation-Booking-Platform-BackEnd
```

The API starts from a Bash terminal with:

```bash
ASPNETCORE_ENVIRONMENT=Development dotnet run \
  --project Travel-and-Accommodation-Booking-Platform-BackEnd \
  --urls http://localhost:5151
```

Swagger is available at <http://localhost:5151/swagger>. Access tokens returned by registration or login can be entered through Swagger's **Authorize** control. Administrative operations require the `Admin` role.

## Service management

For the full development stack:

```bash
# Service status
docker compose -f compose.development.yaml ps

# Live logs
docker compose -f compose.development.yaml logs -f

# Stop and remove containers while preserving persistent data
docker compose -f compose.development.yaml down
```

The same commands apply to the host-SQL stack with `-f compose.yaml`. Named volumes survive `down`; `down --volumes` deletes the stack's stored data.
