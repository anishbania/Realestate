# Realestate

A .NET application for a real estate platform built using Clean Architecture principles.

## Prerequisites
- .NET 9 SDK
- SQL Server (LocalDB or full instance)

## Setup
1. Clone the repository.
2. Update the connection string in `RealEstate/appsettings.json` to match your SQL Server instance.
3. Apply database migrations:
   ```bash
   dotnet ef database update --project RealEstate.Infrastructure --startup-project RealEstate
