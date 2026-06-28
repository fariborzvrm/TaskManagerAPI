# Quick Start Guide - Clean Architecture Setup

## ✅ What Was Completed

Your TaskManager.API project has been successfully refactored to implement **Clean Architecture** with proper layer separation and dependency injection.

---

## 🚀 Running the Application

### 1. Local Development Setup

```bash
# Navigate to project folder
cd C:\Users\User\source\repos\TaskManager.API

# Restore NuGet packages
dotnet restore

# Build the solution
dotnet build

# Apply database migrations (if needed)
dotnet ef database update --project TaskManager.API
```

### 2. Run the API

```bash
# From the TaskManager.API folder
cd TaskManager.API
dotnet run

# Or use Visual Studio
# Press F5 or click the green "Run" button
```

### 3. Access Swagger Documentation

Once running, open:
```
https://localhost:5001/swagger/index.html
```

---

## 📊 Project Architecture

### Four Independent Projects:

1. **Domain** (TaskManager.Domain.csproj)
   - Core business entities
   - NO external dependencies
   - Location: `Domain/Models/TaskItem.cs`

2. **Application** (TaskManager.Application.csproj)
   - Business logic & services
   - Service & Repository interfaces
   - Location: `TaskManager.Application/`

3. **Infrastructure** (TaskManager.Infrastructure.csproj)
   - Database access layer
   - Entity Framework Core
   - Location: `TaskManager.Infrastructure/`

4. **API** (TaskManager.API.csproj)
   - HTTP endpoints
   - Dependency injection setup
   - Location: `TaskManager.API/`

---

## 🔗 Dependency Flow

```
HTTP Request
	↓
TasksController (HTTP Layer)
	↓ injects
ITaskService (Application Interface)
	↓ implemented by
TaskService (Business Logic)
	↓ injects
ITaskRepository (Application Interface)
	↓ implemented by
TaskRepository (Data Access)
	↓ injects
AppDbContext (EF Core)
	↓
SQL Server Database
```

---

## 📝 API Endpoints

### Get All Tasks
```http
GET /api/task
Response: 200 OK
[
  {
	"id": 1,
	"title": "Task 1",
	"isCompleted": false
  }
]
```

### Get Task by ID
```http
GET /api/task/{id}
Response: 200 OK
{
  "id": 1,
  "title": "Task 1",
  "isCompleted": false
}
```

### Create Task
```http
POST /api/task
Content-Type: application/json

{
  "title": "New Task",
  "isCompleted": false
}

Response: 201 Created
Location: /api/task/2
```

### Update Task
```http
PUT /api/task/{id}
Content-Type: application/json

{
  "title": "Updated Title",
  "isCompleted": true
}

Response: 204 No Content
```

### Delete Task
```http
DELETE /api/task/{id}
Response: 204 No Content
```

---

## 🗄️ Database

### Connection String
Currently set to LocalDB in `appsettings.json`:
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskApiDb;Trusted_Connection=True;"
```

### Using Different Database

#### SQL Server (Remote)
```json
"DefaultConnection": "Server=your-server-name;Database=TaskApiDb;User Id=sa;Password=your-password;"
```

#### Azure SQL Database
```json
"DefaultConnection": "Server=your-server.database.windows.net;Database=TaskApiDb;User Id=admin@your-server;Password=your-password;"
```

#### Docker SQL Server
```json
"DefaultConnection": "Server=host.docker.internal,1433;Initial Catalog=TaskApiDb;User Id=SA;Password=YourStrongPassword123;"
```

---

## 🧪 Testing the API

### Using PowerShell (Integrated in VS)

```powershell
# Get all tasks
$response = Invoke-RestMethod -Uri "https://localhost:5001/api/task" -Method Get
$response | ConvertTo-Json

# Create a task
$body = @{
	title = "Buy Groceries"
	isCompleted = $false
} | ConvertTo-Json

$response = Invoke-RestMethod -Uri "https://localhost:5001/api/task" `
	-Method Post `
	-ContentType "application/json" `
	-Body $body
$response | ConvertTo-Json
```

### Using cURL

```bash
# Get all tasks
curl -X GET https://localhost:5001/api/task

# Create a task
curl -X POST https://localhost:5001/api/task \
  -H "Content-Type: application/json" \
  -d '{"title":"Buy Groceries","isCompleted":false}'

# Get task by ID
curl -X GET https://localhost:5001/api/task/1

# Update task
curl -X PUT https://localhost:5001/api/task/1 \
  -H "Content-Type: application/json" \
  -d '{"title":"Buy Groceries","isCompleted":true}'

# Delete task
curl -X DELETE https://localhost:5001/api/task/1
```

### Using REST Client Extension (VSCode)

Create `requests.http`:
```http
@baseUrl = https://localhost:5001

### Get all tasks
GET {{baseUrl}}/api/task

### Create task
POST {{baseUrl}}/api/task
Content-Type: application/json

{
  "title": "Buy Groceries",
  "isCompleted": false
}

### Get task by ID
GET {{baseUrl}}/api/task/1

### Update task
PUT {{baseUrl}}/api/task/1
Content-Type: application/json

{
  "title": "Buy Groceries",
  "isCompleted": true
}

### Delete task
DELETE {{baseUrl}}/api/task/1
```

---

## 📂 File Structure Overview

```
TaskManager.API/
│
├── Domain/                          (Business Entities)
│   ├── TaskManager.Domain.csproj
│   └── Models/
│       └── TaskItem.cs
│
├── TaskManager.Application/         (Business Logic)
│   ├── TaskManager.Application.csproj
│   ├── Interfaces/
│   │   ├── ITaskService.cs
│   │   └── ITaskRepository.cs
│   └── Services/
│       └── TaskService.cs
│
├── TaskManager.Infrastructure/      (Data Access)
│   ├── TaskManager.Infrastructure.csproj
│   ├── Data/
│   │   └── AppDbContext.cs
│   └── Repositories/
│       └── TaskRepository.cs
│
├── TaskManager.API/                 (HTTP API)
│   ├── TaskManager.API.csproj
│   ├── Controllers/
│   │   └── TasksController.cs
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Migrations/
│   │   ├── 20260625091954_InitialCreate.cs
│   │   ├── 20260625091954_InitialCreate.Designer.cs
│   │   └── AppDbContextModelSnapshot.cs
│   └── Properties/
│       └── launchSettings.json
│
├── CLEAN_ARCHITECTURE_SUMMARY.md
├── ARCHITECTURE_GUIDE.md
├── CODE_CHANGES_REFERENCE.md
└── TaskManager.API.slnx

```

---

## 🛠️ Common Development Tasks

### Adding a New Feature

1. **Add to Domain** (Entity/Model)
   ```csharp
   // Domain/Models/NewEntity.cs
   public class NewEntity
   {
	   public int Id { get; set; }
	   public string Name { get; set; }
   }
   ```

2. **Add to Application** (Interfaces & Services)
   ```csharp
   // TaskManager.Application/Interfaces/INewService.cs
   public interface INewService
   {
	   Task<List<NewEntity>> GetAllAsync();
   }
   ```

3. **Implement in Infrastructure** (Repository)
   ```csharp
   // TaskManager.Infrastructure/Repositories/NewRepository.cs
   public class NewRepository : INewRepository
   {
	   // implementation
   }
   ```

4. **Expose in API** (Controller)
   ```csharp
   // TaskManager.API/Controllers/NewController.cs
   [ApiController]
   [Route("api/new")]
   public class NewController : ControllerBase
   {
	   private readonly INewService _service;
	   // implementation
   }
   ```

5. **Register in DI** (Program.cs)
   ```csharp
   builder.Services.AddScoped<INewRepository, NewRepository>();
   builder.Services.AddScoped<INewService, NewService>();
   ```

---

## 🐛 Troubleshooting

### Build Fails
```bash
# Clean and rebuild
dotnet clean
dotnet build
```

### Database Issues
```bash
# Remove existing migrations and database
dotnet ef database drop --project TaskManager.API --force

# Recreate migrations
dotnet ef migrations remove --project TaskManager.API
dotnet ef migrations add InitialCreate --project TaskManager.API
dotnet ef database update --project TaskManager.API
```

### Port Already in Use
Edit `TaskManager.API/Properties/launchSettings.json`:
```json
"https": {
  "commandName": "Project",
  "launchBrowser": true,
  "launchUrl": "swagger",
  "applicationUrl": "https://localhost:5002;http://localhost:5003",
  ...
}
```

### SSL Certificate Issues
```bash
# Trust development certificate
dotnet dev-certs https --trust
```

---

## 📚 Next Steps

1. ✅ **Run the application** and test endpoints
2. ⬜ **Add logging** (Serilog)
3. ⬜ **Add validation** (FluentValidation)
4. ⬜ **Add error handling** middleware
5. ⬜ **Create DTOs** for better API contracts
6. ⬜ **Add unit tests** (xUnit)
7. ⬜ **Implement AutoMapper** for entity mapping
8. ⬜ **Add specifications** for complex queries

---

## 📖 Documentation Files

Three comprehensive guides have been created:

1. **CLEAN_ARCHITECTURE_SUMMARY.md** - Overview and project structure
2. **ARCHITECTURE_GUIDE.md** - Detailed dependency diagrams and responsibilities
3. **CODE_CHANGES_REFERENCE.md** - Before/after code examples

---

## ✅ Build Status

```
✅ Domain builds successfully
✅ Application builds successfully
✅ Infrastructure builds successfully
✅ API builds successfully
✅ All projects reference each other correctly
✅ Dependency injection configured
✅ Database context ready
```

---

## 🎯 Architecture Verified

- ✅ Dependency Rule honored (dependencies flow inward only)
- ✅ SOLID principles applied
- ✅ Clear separation of concerns
- ✅ Proper constructor injection
- ✅ Interface-based design
- ✅ Configuration externalized

---

**Your clean architecture is ready to use!** 🚀
