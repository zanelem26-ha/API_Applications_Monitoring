# Application Monitoring API

ASP.NET Core Web API for monitoring applications, incidents, incident comments, and application health checks.

## Overview

The Application Monitoring API provides the backend services for an application monitoring system. It exposes RESTful endpoints that allow the frontend to retrieve and manage application monitoring data.

The API uses Entity Framework Core to communicate with SQL Server and model relationships between applications, incidents, incident comments, and health checks.

## Technologies

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- REST APIs
- Swagger / OpenAPI
- LINQ
- .NET 8

## Architecture

The API follows a simple layered structure:

```text
Controller
    ↓
Entity Framework Core
    ↓
SQL Server