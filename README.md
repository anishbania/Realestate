# Realestate

A .NET application for a real estate platform built using Clean Architecture principles.

## Prerequisites
- .NET 6 SDK
- SQL Server (LocalDB or full instance)

## Setup
1. Clone the repository.
2. Update the connection string in `Presentation/appsettings.json` to match your SQL Server instance.
3. Apply database migrations:
   ```bash
   dotnet ef database update --project RealEstatePlatform.Infrastructure --startup-project RealEstatePlatform.Presentation
