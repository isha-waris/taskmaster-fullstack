# Database Design

## Overview
The database is designed using a relational model to support task tracking, user management, 
and audit history. Proper relationships and constraints ensure data consistency and integrity.

## Tables

### 1. Users
Stores system users.

Fields:
- Id (PK)
- FullName
- Email (Unique)
- PasswordHash
- Role
- CreatedAt

### 2. Tasks
Stores tasks or production records.

Fields:
- Id (PK)
- Title
- Description
- Status
- AssignedToUserId (FK → Users.Id)
- CreatedAt
- UpdatedAt

Relationship:
- One User can have many Tasks
- Each Task is assigned to one User

### 3. TaskStatusHistory
Tracks status changes for auditing.
Fields:
- Id (PK)
- TaskId (FK → Tasks.Id)
- OldStatus
- NewStatus
- ChangedAt

Relationship:
- One Task can have many StatusHistory records

## Relationships Summary

| From | To | Type |
|----|----|----|
| Users | Tasks | One-to-Many |
| Tasks | TaskStatusHistory | One-to-Many |
| Tasks | Users | Many-to-One |

## Design Considerations
- Foreign keys enforce referential integrity
- History table provides audit trail
- Timestamp fields support reporting and debugging
