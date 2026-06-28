# Clean Architecture Dependency Diagram

## Project References Flow

```
┌─────────────────────────────────────────────────────────┐
│                  TaskManager.API (Presentation)         │
│                      ↓ depends on ↓                      │
│  - TasksController (HTTP Endpoints)                     │
│  - Program.cs (DI Configuration)                        │
│  - Migrations (EF Core)                                 │
│                                                          │
│  References: Application + Infrastructure               │
└─────────────┬────────────────────────────────┬──────────┘
			  │                                │
			  ↓                                ↓
	┌─────────────────────────┐    ┌──────────────────────────┐
	│ TaskManager.Application │    │ TaskManager.Infrastructure│
	│   (Business Logic)      │    │   (Data Access)          │
	│      ↓ depends on ↓     │    │      ↓ depends on ↓      │
	│ - ITaskService          │    │ - AppDbContext           │
	│ - TaskService           │    │ - TaskRepository         │
	│ - ITaskRepository       │    │ - EF Core Configuration  │
	│                         │    │                          │
	│ References: Domain      │    │ References: Application  │
	└────────────┬────────────┘    └──────────┬───────────────┘
				 │                            │
				 └──────────────┬─────────────┘
								↓
					┌─────────────────────────┐
					│  TaskManager.Domain     │
					│   (Business Entities)   │
					│      ↓ contains ↓       │
					│  - TaskItem             │
					│  - Validation Logic     │
					│                         │
					│ References: NONE        │
					└─────────────────────────┘
```

---

## Detailed Layer Responsibilities

### 🏛️ Domain Layer (Innermost - Most Stable)
```csharp
// TaskManager.Domain
Location: Domain/Models/TaskItem.cs

Responsibilities:
- Define core business entities
- Apply business validation rules
- NO external dependencies
- Pure C# classes and interfaces
```

### 🔧 Application Layer
```csharp
// TaskManager.Application
Locations:
- Interfaces/ITaskService.cs
- Interfaces/ITaskRepository.cs
- Services/TaskService.cs

Responsibilities:
- Define business logic
- Orchestrate operations
- Define repository contracts
- Use case implementations
```

### 💾 Infrastructure Layer
```csharp
// TaskManager.Infrastructure
Locations:
- Data/AppDbContext.cs
- Repositories/TaskRepository.cs

Responsibilities:
- Implement repository interface
- Configure Entity Framework Core
- Handle data persistence
- External service integration
```

### 🌐 API/Presentation Layer (Outermost - Most Volatile)
```csharp
// TaskManager.API
Locations:
- Controllers/TasksController.cs
- Program.cs (Startup configuration)
- Migrations/ (EF Core)

Responsibilities:
- Handle HTTP requests/responses
- Route mapping
- Dependency injection setup
- Cross-cutting concerns (logging, error handling)
```

---

## Dependency Injection Chain

```
HTTP Request
	↓
TasksController(ITaskService)
	↓
TaskService(ITaskRepository)
	↓
TaskRepository(AppDbContext)
	↓
AppDbContext(DbContextOptions)
	↓
SQL Server Database
```

---

## SOLID Principles Applied

### S - Single Responsibility
- TasksController: Only handles HTTP
- TaskService: Only business logic
- TaskRepository: Only data access
- AppDbContext: Only DB configuration

### O - Open/Closed
- ITaskService interface allows adding new implementations
- Can extend functionality without modifying existing code

### L - Liskov Substitution
- TaskRepository implements ITaskRepository
- Can replace with alternative implementations (mock, different DB, etc.)

### I - Interface Segregation
- ITaskService: Only task-related operations
- ITaskRepository: Only repository operations
- Small, focused interfaces

### D - Dependency Inversion
- TasksController depends on ITaskService (abstraction)
- TaskService depends on ITaskRepository (abstraction)
- All dependencies injected through constructor

---

## Data Flow Example: Get All Tasks

```
1. GET /api/task
   └→ HTTP Request arrives at TasksController

2. TasksController.GetAll()
   └→ Calls: _taskService.GetAllTasksAsync()

3. TaskService.GetAllTasksAsync()
   └→ Calls: _taskRepository.GetAllTasksAsync()

4. TaskRepository.GetAllTasksAsync()
   └→ Calls: _context.TaskItems.ToListAsync()

5. AppDbContext.TaskItems
   └→ Executes SQL Query against database

6. SQL Server Database
   └→ Returns TaskItem entities

7. Return chain (upward)
   └→ Repository → Service → Controller → HTTP Response (JSON)

8. HTTP 200 OK [List of TaskItems]
```

---

## Project File Structure

```
TaskManager.API/
├── Domain/
│   ├── TaskManager.Domain.csproj
│   └── Models/
│       └── TaskItem.cs
│
├── TaskManager.Application/
│   ├── TaskManager.Application.csproj
│   ├── Interfaces/
│   │   ├── ITaskService.cs
│   │   └── ITaskRepository.cs
│   └── Services/
│       └── TaskService.cs
│
├── TaskManager.Infrastructure/
│   ├── TaskManager.Infrastructure.csproj
│   ├── Data/
│   │   └── AppDbContext.cs
│   └── Repositories/
│       └── TaskRepository.cs
│
└── TaskManager.API/
	├── TaskManager.API.csproj
	├── Controllers/
	│   └── TasksController.cs
	├── Program.cs
	├── appsettings.json
	└── Migrations/
		├── 20260625091954_InitialCreate.cs
		├── 20260625091954_InitialCreate.Designer.cs
		└── AppDbContextModelSnapshot.cs
```

---

## Next Architecture Enhancements

### Phase 1: Immediate Improvements
- [ ] Add FluentValidation for request validation
- [ ] Add Error handling middleware
- [ ] Add logging (Serilog)
- [ ] Create DTOs for API responses

### Phase 2: Advanced Patterns
- [ ] Add AutoMapper for entity-to-DTO mapping
- [ ] Implement Unit of Work pattern
- [ ] Add specification pattern for complex queries
- [ ] Add event sourcing for audit trail

### Phase 3: Testing
- [ ] Create xUnit test project for Application layer
- [ ] Create Moq for mocking dependencies
- [ ] Add integration tests
- [ ] Add API endpoint tests

---

## Build & Deployment

### Current Status
✅ All projects compile successfully
✅ Clean architecture implemented
✅ Dependency injection configured
✅ Database context ready for migrations

### Ready for:
✅ Local development
✅ Unit test implementation
✅ Docker containerization
✅ CI/CD pipeline setup
