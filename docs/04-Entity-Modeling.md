# Entity Modeling

## Overview
Entity modeling defines how application data is represented in code and how it maps to the relational database.
In this project, Entity Framework Core (EF Core) is used with a Code First approach, where C# classes define the database schema.

Each entity represents a real-world concept and is designed with clear responsibilities and relationships.


## Entities

### 1. User
Represents a system user who can be assigned tasks and perform status updates.

Key Responsibilities:
- Owns multiple tasks
- Performs task status changes
- Acts as an authenticated system identity

Important Fields:
- Id (Primary Key)
- FullName
- Email
- PasswordHash
- Role
- CreatedAt

Relationships:
- One User → Many Tasks
- One User → Many Task Status History records

Navigation Properties:
- AssignedTasks
- StatusHistories


### 2. TaskItem
Represents a task or production record in the system.

Key Responsibilities:
- Stores task details
- Maintains assignment to a user
- Tracks current status

Important Fields:
- Id (Primary Key)
- Title
- Description
- Status
- AssignedToUserId (Foreign Key)
- CreatedAt
- UpdatedAt

Relationships:
- Many Tasks → One User
- One Task → Many Status History records

Navigation Properties:
- AssignedToUser
- StatusHistory

Note:
The entity is named `TaskItem` to avoid conflict with the built-in `Task` class in C#.


### 3. TaskStatusHistory
Represents an immutable audit log of task status changes.

Key Responsibilities:
- Tracks old and new status values
- Records who made the change
- Preserves historical accuracy

Important Fields:
- Id (Primary Key)
- TaskItemId (Foreign Key)
- OldStatus
- NewStatus
- ChangedAt
- ChangedByUserId (Foreign Key)

Relationships:
- Many History Records → One Task
- Many History Records → One User

Navigation Properties:
- TaskItem
- ChangedByUser


## Relationship Summary

| From Entity | To Entity | Relationship Type |
|-----------|----------|----------------------|
| User      | TaskItem | One-to-Many          |
| TaskItem  | User     | Many-to-One          |
| TaskItem  | TaskStatusHistory | One-to-Many |
| TaskStatusHistory | TaskItem  | Many-to-One |
| User      | TaskStatusHistory | One-to-Many |


## Navigation Properties
Navigation properties are used by EF Core to:
- Load related data
- Generate correct foreign key relationships
- Enable expressive queries using LINQ

They do not create database columns and exist only at the object level.


## Design Considerations
- Clear separation of concerns
- Explicit foreign keys for clarity
- Audit history for traceability
- Scalable structure for future features
