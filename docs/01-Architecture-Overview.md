# Architecture Overview

## Overview
➡️TaskMaster is a full-stack task tracking system designed using a client-server architecture. 
➡️The system separates concerns between frontend, backend, authentication, and data persistence 
➡️to ensure scalability, maintainability, and security.

## High-Level Architecture

Frontend (React)
↓
REST APIs (ASP.NET Core Web API)
↓
Business Logic & Validation
↓
Entity Framework Core
↓
SQL Server Database

## Frontend Layer
- Built using React
- Responsible for UI, routing, and state management
- Communicates with backend via RESTful APIs
- Stores JWT token securely for authenticated requests

## Backend Layer
- Built using ASP.NET Core Web API
- Exposes REST endpoints for authentication and task management
- Implements business rules, validation, and error handling
- Uses JWT-based authentication

## Database Layer
- SQL Server is used for data persistence
- Entity Framework Core handles ORM
- Proper relational modeling ensures data integrity

## Authentication
- JWT-based authentication
- Stateless API design
- Role-based authorization (Admin, User)

## Design Principles
- Separation of concerns
- Stateless backend
- Secure authentication
- Scalable and maintainable architecture
