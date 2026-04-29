# PlanszówkaPlusPlus

![Logo](PlanszowkaPlusPlus/PlanszowkaPlusPlus/wwwroot/planszowkaPlusPlusLogov2.png)

![.NET](https://img.shields.io/badge/.NET-8-blue)
![Database](https://img.shields.io/badge/Database-MySQL-orange)
![Status](https://img.shields.io/badge/status-university--project-lightgrey)
![License](https://img.shields.io/badge/license-MIT-green)

A university project for a Databases course, developed by a team of 3 people to a full web application. 

The goal is to support the organization of a hypothetical board game store/club, with functionality such as keeping track of stock for staff and information about reservations for both staff and members.

## Tech Stack

- ASP.NET Core (Razor Pages)
- Entity Framework Core
- MySQL

## Quick Start

```bash
git clone [repo]
cd PlanszowkaPlusPlus/PlanszowkaPlusPlus
dotnet restore
dotnet ef database update
dotnet run
```

## User Notes

If using Visual Studio remember to open `PlanszowkaPlusPlus.sln` after cloning the repository.

Make sure MySQL is running and update the connection string in `appsettings.json`:
- `User` – your MySQL username (e.g. `root`)
- `Password` – the password set during MySQL installation

The application will be available at: https://localhost:5042

After launching the app with a fresh database, click `Admin Login` to set up the app.

## Troubleshooting
If you encounter errors such as "table already exists", drop the database and run migrations again:

```sql
DROP DATABASE PlanszowkaPlusPlus;
```