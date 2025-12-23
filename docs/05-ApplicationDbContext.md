# ApplicationDbContext

## Overview
`ApplicationDbContext` is the central database context for the application.  
It acts as the bridge between the domain entities and the underlying relational database using **Entity Framework Core (EF Core)**.

All database interactions, including querying, saving data, and tracking changes, are coordinated through this context.


## Why ApplicationDbContext Exists

Entity Framework Core requires a `DbContext` to:
- Track entity changes
- Translate LINQ queries into SQL
- Manage relationships between entities
- Apply migrations and schema updates
- Control database connections per request

In this project, `ApplicationDbContext` represents a **single unit of work** for each API request.

## DbSet<T> Explanation

### What is DbSet<T>?
A `DbSet<T>` represents a collection of entities of a specific type and maps directly to a database table.

Example:
- `DbSet<User>` → `Users` table
- `DbSet<TaskItem>` → `TaskItems` table

### Why We Define DbSet<T>
Defining a `DbSet<T>` tells EF Core:
- Which entities should be tracked
- Which tables should be created in the database
- How LINQ queries should be translated

Without a `DbSet<T>`, EF Core will not generate a table for that entity.

---

## DbSets Used in This Project

| DbSet | Purpose |
|-----|--------|
| Users | Stores system users |
| TaskItems | Stores tasks assigned to users |
| TaskStatusHistories | Stores audit history for task status changes |

## Relationship Configuration

Relationships between entities are configured using the **Fluent API** inside the `OnModelCreating` method.

This approach is preferred over data annotations because:
- It keeps entities clean
- It centralizes relationship logic
- It allows fine-grained control over delete behavior and constraints

Examples of configured relationships:
- One User → Many TaskItems
- One TaskItem → Many TaskStatusHistory records
- One User → Many TaskStatusHistory records

Explicit configuration prevents accidental cascade deletes and ensures audit data integrity.


## OnModelCreating Method

`OnModelCreating(ModelBuilder modelBuilder)` is used to:
- Configure entity relationships
- Define foreign keys
- Control delete behaviors
- Override EF Core conventions when needed

This method executes once when the model is created and is critical for database consistency.

## Functions That Can Be Defined in ApplicationDbContext

While `DbContext` is primarily used for data access, it can also include:

### 1. Overridden Methods
- `SaveChanges()`
- `SaveChangesAsync()`

Used to:
- Add auditing fields (CreatedAt, UpdatedAt)
- Enforce business rules before saving
- Apply soft-delete logic


### 2. Transaction Management
`DbContext` supports database transactions to ensure data consistency during complex operations.


### 3. Query Filters
Global filters can be applied for:
- Soft deletes
- Multi-tenant applications
- Role-based data access


### 4. Database Configuration
- Index definitions
- Unique constraints
- Default values
- Precision for numeric fields


## EF Core Packages Used (.NET 8)

To enable Entity Framework Core with SQL Server in a .NET 8 project, the following packages are required:

### Required Packages

| Package Name | Purpose |
|-------------|--------|
| Microsoft.EntityFrameworkCore | Core EF functionality |
| Microsoft.EntityFrameworkCore.SqlServer | SQL Server database provider |
| Microsoft.EntityFrameworkCore.Tools | Migration and tooling support |
| Microsoft.EntityFrameworkCore.Design | Design-time services for EF Core |

### Versioning Rule
All EF Core packages must match the target framework version:
Mixing versions can cause runtime and migration errors.

## Design Considerations

- DbContext is registered as **scoped** to align with API request lifecycle
- Explicit relationship configuration improves maintainability
- Database logic is centralized for clarity
- Prepared for scalable schema evolution through migrations


## Summary
`ApplicationDbContext` is the foundation of data access in this application.  
It defines entity tracking, relationships, schema behavior, and acts as the gateway between application logic and persistent storage.



