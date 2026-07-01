# TaskManager.API

A modern RESTful API for managing tasks, built with .NET 10 and following clean architecture principles. This project demonstrates best practices in API development, including dependency injection, validation, error handling, and database persistence.

## 📋 Table of Contents

- [Project Overview](#project-overview)
- [Architecture](#architecture)
- [Technologies](#technologies)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Features](#features)
- [License](#license)

## 📌 Project Overview

TaskManager.API is a backend service that provides CRUD operations for managing tasks. It's designed with a layered architecture that separates concerns and follows SOLID principles to ensure maintainability, testability, and scalability.

## 🏗️ Architecture

This project follows **Clean Architecture** with clear separation of concerns across four layers:

### 1. **Domain Layer** (`TaskManager.Domain`)
- Contains core business entities and models
- No external dependencies (pure C# classes)
- Defines `TaskItem` entity with properties: Id, Title, IsCompleted

### 2. **Application Layer** (`TaskManager.Application`)
- Implements business logic and use cases
- Contains DTOs (Data Transfer Objects) for API contracts
- Houses service interfaces and implementations
- Includes FluentValidation validators
- Custom exception handling
- Object mapping (DTO ↔ Domain Model)

### 3. **Infrastructure Layer** (`TaskManager.Infrastructure`)
- Handles data persistence with Entity Framework Core
- Implements repository pattern
- Contains database context configuration
- Unit of Work pattern for transaction management
- SQL Server integration

### 4. **Presentation Layer** (`TaskManager.API`)
- ASP.NET Core REST API
- API controllers handling HTTP requests
- Middleware for cross-cutting concerns
- Exception handling middleware
- Dependency injection configuration
- Swagger/OpenAPI documentation

### Architecture Diagram

```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│              (TaskManager.API - Controllers)             │
└─────────────────────────────────────────────────────────┘
						  ↑
┌─────────────────────────────────────────────────────────┐
│                   Application Layer                      │
│         (Services, Validators, DTOs, Interfaces)        │
└─────────────────────────────────────────────────────────┘
						  ↑
┌─────────────────────────────────────────────────────────┐
│                  Infrastructure Layer                    │
│          (DbContext, Repositories, Unit of Work)        │
└─────────────────────────────────────────────────────────┘
						  ↑
┌─────────────────────────────────────────────────────────┐
│                      Domain Layer                        │
│                  (Pure Business Models)                  │
└─────────────────────────────────────────────────────────┘
```

## 🛠️ Technologies

| Category | Technology | Version |
|----------|-----------|---------|
| **Framework** | .NET | 10.0 |
| **Language** | C# | 12 |
| **Database** | SQL Server | - |
| **ORM** | Entity Framework Core | 10.0.9 |
| **Validation** | FluentValidation | 12.1.1 |
| **API Documentation** | Swagger/Swashbuckle | 10.2.3 |
| **Web API** | ASP.NET Core | 10.0.9 |

### Key NuGet Packages
- `FluentValidation` - Data validation
- `FluentValidation.AspNetCore` - ASP.NET Core integration
- `FluentValidation.DependencyInjectionExtensions` - Dependency injection
- `Microsoft.EntityFrameworkCore` - ORM
- `Microsoft.EntityFrameworkCore.SqlServer` - SQL Server provider
- `Swashbuckle.AspNetCore` - Swagger UI & generation

## 📂 Project Structure

```
TaskManager.API/
├── Domain/                          # Core business models
│   └── Models/
│       └── TaskItem.cs             # Task entity
│
├── TaskManager.Application/         # Business logic & use cases
│   ├── DTOs/
│   │   ├── CreateTaskDto.cs
│   │   ├── UpdateTaskDto.cs
│   │   └── TaskResponseDto.cs
│   ├── Exceptions/                 # Custom exceptions
│   ├── Interfaces/
│   │   ├── ITaskService.cs
│   │   ├── ITaskRepository.cs
│   │   └── IUnitOfWork.cs
│   ├── Services/
│   │   └── TaskService.cs          # Business logic
│   ├── Validators/
│   │   ├── CreateTaskValidator.cs
│   │   └── UpdateTaskValidator.cs
│   ├── Mappings/
│   │   └── TaskMappings.cs         # DTO mappings
│   └── DependencyInjection.cs      # Application DI configuration
│
├── TaskManager.Infrastructure/      # Data access & external services
│   ├── Data/
│   │   └── AppDbContext.cs         # EF Core DbContext
│   ├── Persistence/
│   │   └── UnitOfWork.cs           # Transaction management
│   ├── Repositories/
│   │   └── TaskRepository.cs       # Data access layer
│   └── DependencyInjection.cs      # Infrastructure DI configuration
│
└── TaskManager.API/                # REST API & Presentation
	├── Controllers/
	│   └── TasksController.cs      # API endpoints
	├── Middleware/
	│   └── ExceptionHandlingMiddleware.cs  # Global exception handler
	├── Program.cs                  # Application setup & configuration
	└── appsettings.json           # Configuration
```

## 🚀 Getting Started

### Prerequisites
- .NET 10 SDK or later
- SQL Server (local or remote)
- Visual Studio 2025 or Visual Studio Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/fariborzvrm/TaskManager.API.git
   cd TaskManager.API
   ```

2. **Configure database connection**
   - Update `appsettings.json` with your SQL Server connection string:
   ```json
   {
	 "ConnectionStrings": {
	   "DefaultConnection": "Server=YOUR_SERVER;Database=TaskManagerDB;Trusted_Connection=true;"
	 }
   }
   ```

3. **Restore packages**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet watch run
   ```

   The API will be available at `https://localhost:5001`

6. **Access Swagger UI**
   - Navigate to `https://localhost:5001/swagger` to explore and test API endpoints

## 📚 API Documentation

### Base URL
```
https://localhost:5001/api
```

### Endpoints

#### Get All Tasks
```http
GET /task
```
**Response:** `200 OK`
```json
[
  {
	"id": 1,
	"title": "Sample Task",
	"isCompleted": false
  }
]
```

#### Get Task by ID
```http
GET /task/{id}
```
**Response:** `200 OK` or `404 Not Found`

#### Create Task
```http
POST /task
Content-Type: application/json

{
  "title": "New Task"
}
```
**Response:** `201 Created`
```json
{
  "id": 1,
  "title": "New Task",
  "isCompleted": false
}
```

#### Update Task
```http
PUT /task/{id}
Content-Type: application/json

{
  "title": "Updated Task",
  "isCompleted": true
}
```
**Response:** `204 No Content`

#### Delete Task
```http
DELETE /task/{id}
```
**Response:** `204 No Content` or `404 Not Found`

### Validation

The API implements FluentValidation to ensure data integrity:

- **Title Validation**
  - Required field (cannot be empty)
  - Minimum length: 3 characters
  - Maximum length: 100 characters

Invalid requests return `400 Bad Request` with validation errors.

## ✨ Features

### ✅ Implemented
- **RESTful API** with standard HTTP methods (GET, POST, PUT, DELETE)
- **Data Validation** using FluentValidation
- **Error Handling** with custom exceptions and middleware
- **Dependency Injection** configuration for loose coupling
- **Entity Framework Core** for database operations
- **Repository Pattern** for data access abstraction
- **Unit of Work Pattern** for transaction management
- **Async/Await** for non-blocking operations
- **Swagger/OpenAPI** documentation
- **Clean Architecture** principles
- **SOLID Principles** adherence

### 🔄 Design Patterns Used
1. **Repository Pattern** - Abstract data access logic
2. **Service Pattern** - Encapsulate business logic
3. **Dependency Injection** - Loose coupling between layers
4. **Unit of Work** - Manage database transactions
5. **DTO Pattern** - Separate API contracts from domain models
6. **Middleware Pattern** - Cross-cutting concerns (logging, error handling)

## 🛡️ Error Handling

The application includes comprehensive error handling:

- **Global Exception Middleware** - Catches all unhandled exceptions
- **Custom Exceptions** - Domain-specific exceptions:
  - `NotFoundException` - Resource not found
  - `DuplicateTaskTitleException` - Duplicate task titles
  - `NotExistingTaskTitleException` - Invalid task operation
  - `UnauthorizedException` - Authorization failures

## 🧪 Testing Your API

Use tools like Postman, curl, or Swagger UI to test:

```bash
# Create task
curl -X POST https://localhost:5001/api/task \
  -H "Content-Type: application/json" \
  -d '{"title":"My Task"}'

# Get all tasks
curl https://localhost:5001/api/task

# Update task
curl -X PUT https://localhost:5001/api/task/1 \
  -H "Content-Type: application/json" \
  -d '{"title":"Updated Task","isCompleted":true}'

# Delete task
curl -X DELETE https://localhost:5001/api/task/1
```

## 📖 Key Concepts

### Clean Architecture
This project implements Clean Architecture to ensure:
- Independence from databases (easily swap SQL Server for PostgreSQL)
- Independence from frameworks (decoupled from ASP.NET Core specifics)
- Testability (each layer can be tested independently)
- Maintainability (clear separation of concerns)

### Dependency Inversion
- High-level modules depend on abstractions (interfaces)
- Low-level modules depend on abstractions
- Configured via `DependencyInjection.cs` in each layer

### SOLID Principles
- **S**ingle Responsibility - Each class has one reason to change
- **O**pen/Closed - Open for extension, closed for modification
- **L**iskov Substitution - Subtypes are substitutable for base types
- **I**nterface Segregation - Specific interfaces over general ones
- **D**ependency Inversion - Depend on abstractions, not concretions

## 🚢 Deployment

1. **Build**
   ```bash
   dotnet build -c Release
   ```

2. **Publish**
   ```bash
   dotnet publish -c Release -o ./publish
   ```

3. **Deploy to Azure, Docker, or IIS** as needed

## 📝 Configuration

All configuration is managed in `appsettings.json`:

```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=localhost;Database=TaskManagerDB;..."
  },
  "Logging": {
	"LogLevel": {
	  "Default": "Information"
	}
  }
}
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is open source and available under the MIT License.

## 👨‍💻 Author

**Farborz Varmostin**
- GitHub: [@fariborzvrm](https://github.com/fariborzvrm)
- Repository: [TaskManager.API](https://github.com/fariborzvrm/TaskManager.API)

## 🔗 Resources

- [Clean Architecture - Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [FluentValidation Documentation](https://docs.fluentvalidation.net/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html)
- [Unit of Work Pattern](https://martinfowler.com/eaaCatalog/unitOfWork.html)

---

**Last Updated:** December 2025  
**Status:** Active Development ✅
