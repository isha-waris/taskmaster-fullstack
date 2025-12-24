## Step 8: DTO Layer (Data Transfer Objects)
## Purpose of This Step

The DTO layer is introduced to separate API contracts from database entities.

At this stage:

Entities are stable
Repositories work
Controllers work
But API responses are too large and unsafe

DTOs solve this problem.

1. What Problem DTOs Solve

Without DTOs:

API returns navigation properties

Nested entities appear in responses

Swagger forces clients to send unwanted fields

Over-posting becomes possible

Entities leak internal structure

Entities ≠ API models

2. What Is a DTO?

A DTO (Data Transfer Object) is:

A plain C# class

Used only for API input/output

Contains only required fields

Has no navigation properties

No EF Core behavior

DTOs define what the client can send and receive

3. Why Repositories Should NOT Return DTOs

Repositories:

Work with Entities

Represent database logic

Must remain persistence-focused

Controllers:

Handle DTO ↔ Entity mapping

Define API contracts

Correct flow:

## Controller (DTO) → Repository (Entity) → DB
## Where DTOs Live in the Project

## Recommended structure:

/DTOs
  /User
    CreateUserDto.cs
    UserResponseDto.cs
  /Task
    CreateTaskDto.cs
    TaskResponseDto.cs
Migration Rules with DTOs

<!-- DTO changes: -->

❌ Do NOT require migrations

<!-- Entity changes: -->

✅ Require migrations

DTOs are API-level, not database-level.

## End of Step 8 (Conceptual)

At the end of this step:

➡️API input/output is controlled
➡️Swagger schemas are clean
➡️Entities are protected
➡️Over-posting is prevented
➡️System is ready for auth & business logic