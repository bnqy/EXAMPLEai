# extoaido

A simple RESTful API [ed purp], asp core, ef core, sql server.

## Features

- CRUD operations for items (`title`, `description`)
- EF Core Code-First persistence
- Layered architecture (Controller, Service, Repository)
- DTO mapping
- Unit tests (>80% coverage) -
- Code quality and style checks (e.g., with dotnet-format, Code Coverage)
- Ready-to-run with provided scripts/configs

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Git](https://git-scm.com/)

### Running the API

```bash
git clone
cd [folder]
dotnet ef database update
dotnet run
```
