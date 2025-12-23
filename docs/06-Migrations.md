## Handling Migrations & Entity Mismatches

## Overview

During development, it’s common to encounter issues where your Entity Framework Core (EF Core) entity models and SQL Server database schema become out of sync. This can prevent saving changes, applying migrations, or even starting the application.

This document explains how we handled migration issues in the TaskMaster project.

## Problem Encountered

After updating entity models, previous migrations no longer matched the database schema.
SaveChanges() failed due to type mismatches between entities and database columns.Attempting to remove migrations initially caused errors due to connection issues or missing database.
## Root Cause

Database schema was different from entity definitions.

➡️EF Core migrations were inconsistent or partially applied.
➡️Connection string issues (e.g., wrong server, LocalDB vs SQL Server instance) also contributed.
Key Lesson: EF Core trusts the database schema; if it does not match your entities, migrations fail.

## Solution Steps
Step 1: Confirm Connection String
Ensure appsettings.json points to the correct SQL Server instance:

"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TaskMaster;Trusted_Connection=True;"

Step 2: Remove Old Migrations
Remove-Migration
Removes the last migration.

Only works if no changes have been applied to the database yet.

Step 3: Delete Existing Database
Using SQL Server Object Explorer or dotnet ef database drop.
Ensures the database is recreated cleanly with the correct schema.

Step 4: Create Fresh Migration
Add-Migration InitialCreate
Generates migration code based on current entity models.
Stored in the Migrations folder.
Ensures entity properties, types, and relationships match the database schema.

Step 5: Apply Migration
Update-Database
Creates the database and tables exactly as defined in the entities.
Aligns EF Core context with SQL Server.

## Why This Is Important
Keeps database schema and entity models synchronized.
Ensures SaveChanges() and CRUD operations work without runtime errors.
Prepares the backend for adding real endpoints and business logic safely.

