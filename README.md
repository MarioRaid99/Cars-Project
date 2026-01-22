# Car (Garage Dashboard) — ASP.NET Core MVC

A layered ASP.NET Core MVC application for managing cars (CRUD) with a custom "Garage Dashboard" UI theme.  
The project follows a clean architecture approach with separate layers for UI, services, domain, and data access.

## Tech Stack
- ASP.NET Core MVC
- Entity Framework Core (SQL Server / LocalDB)
- xUnit (service-level tests)
- Bootstrap + custom CSS theme (`garage.css`)

## Solution Structure
- **Car** — MVC UI (Controllers + Views)
- **Car.ApplicationServices** — DTOs + Services (validation, mapping, timestamps)
- **Car.Core** — Domain entities + interfaces
- **Car.Data** — EF Core DbContext + repository implementations
- **Car.CarTest** — xUnit tests (InMemory EF Core)

## Key Features
- Full CRUD for cars (Create, Read, Update, Delete)
- Required timestamps:
  - `CreatedAt`
  - `ModifiedAt`
- Input validation in service layer (clean separation of concerns)
- Garage Dashboard UI:
  - Card grid layout (not a default table)
  - Search + Fuel filter on Index page
  - Styled forms and details view

## Domain Model (Car)
The `Car` entity includes the required fields for the assignment:
- Make
- Model
- Year
- Price
- Mileage
- FuelType
- Transmission
- CreatedAt
- ModifiedAt

## Getting Started

### Prerequisites
- Visual Studio 2022
- .NET SDK (same version as the solution)
- SQL Server LocalDB (typically installed with Visual Studio)

### Configure Database
The default connection string is in `Car/appsettings.json`:

```json
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=CarDb;Trusted_Connection=True;MultipleActiveResultSets=true"
