# Relevantz.API.CustomerDetails

A production-ready RESTful ASP.NET Core Web API (.NET 8) for managing customer details with full CRUD operations, comprehensive unit testing, and Swagger/OpenAPI documentation. All data is stored in-memory using seeded mock data — no external database is required.

---

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Project Architecture](#project-architecture)
- [Testing](#testing)
- [Technologies](#technologies)  
- [Configuration](#configuration)
- [Development](#development)

---

## 🎯 Overview

This project demonstrates a well-architected ASP.NET Core Web API following industry best practices including:

- **Clean Architecture**: Separation of concerns with Controllers, Services, and Repositories
- **Dependency Injection**: Scoped services registered in the DI container
- **Interface-based Design**: All services and repositories use interfaces for testability
- **Comprehensive Testing**: Full unit test coverage using xUnit and Moq
- **API Documentation**: Interactive Swagger/OpenAPI documentation
- **XML Documentation**: Complete XML comments for all public APIs
- **Constants Management**: Centralized constant values for maintainability
- **Async/Await Pattern**: Full asynchronous operation support

---

## ✨ Features

- ✅ **Full CRUD Operations**: Create, Read, Update, and Delete customers
- 🔍 **Advanced Search**: Search customers by name, email, or phone (case-insensitive, partial match)
- 📝 **In-Memory Data Store**: 5 pre-seeded customer records for immediate testing
- 📚 **Swagger UI**: Interactive API documentation and testing interface
- 🧪 **Comprehensive Unit Tests**: 100% coverage of controllers and services
- 🔄 **RESTful Design**: Proper HTTP verbs, status codes, and resource routing
- 📊 **Detailed API Responses**: Meaningful error messages and validation
- 🕒 **Automatic Timestamps**: CreatedDate and UpdatedDate tracking
- 🎯 **Explicit Usings**: No implicit usings for better code clarity

---

## 📦 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (version 8.0 or later)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/) (for cloning the repository)

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/Relevantz-Training/AI-training-CustomerDetails-SekharChaganti.git
cd AI-training-CustomerDetails-SekharChaganti
```

### 2. Build the solution

```bash
dotnet build
```

### 3. Run the API

```bash
dotnet run --project Relevantz.API.CustomerDetails
```

Or press **F5** in Visual Studio to run with debugging.

### 4. Access the API

- **HTTPS**: `https://localhost:7292`
- **HTTP**: `http://localhost:5048`
- **Swagger UI**: `https://localhost:7292/swagger`
- **IIS Express**: `http://localhost:7107` (HTTPS: `https://localhost:44366`)

The API will automatically open Swagger UI in your default browser.

---

## 📚 API Documentation

### Endpoints Overview

| Method | Route | Description | Response Codes |
|--------|-------|-------------|----------------|
| GET | `/api/customers` | Retrieve all customers | 200 OK |
| GET | `/api/customers/{id}` | Retrieve a customer by ID | 200 OK, 404 Not Found |
| GET | `/api/customers/search?query={query}` | Search customers by name, email, or phone | 200 OK, 400 Bad Request |
| POST | `/api/customers` | Create a new customer | 201 Created, 400 Bad Request |
| PUT | `/api/customers/{id}` | Update an existing customer | 200 OK, 400 Bad Request, 404 Not Found |
| DELETE | `/api/customers/{id}` | Delete a customer | 204 No Content, 404 Not Found |

### Sample Requests & Responses

#### GET /api/customers
Returns all customers in the system.

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "firstName": "Alice",
    "lastName": "Johnson",
    "email": "alice.johnson@example.com",
    "phoneNumber": "555-0101",
    "address": "123 Maple Street, Springfield, IL 62701",
    "createdDate": "2024-01-10T09:00:00Z",
    "updatedDate": "2024-01-10T09:00:00Z"
  },
  {
    "id": 2,
    "firstName": "Bob",
    "lastName": "Smith",
    "email": "bob.smith@example.com",
    "phoneNumber": "555-0102",
    "address": "456 Oak Avenue, Columbus, OH 43004",
    "createdDate": "2024-02-15T11:30:00Z",
    "updatedDate": "2024-02-15T11:30:00Z"
  }
]
```

#### GET /api/customers/{id}
Retrieves a specific customer by ID.

**Response (200 OK):**
```json
{
  "id": 1,
  "firstName": "Alice",
  "lastName": "Johnson",
  "email": "alice.johnson@example.com",
  "phoneNumber": "555-0101",
  "address": "123 Maple Street, Springfield, IL 62701",
  "createdDate": "2024-01-10T09:00:00Z",
  "updatedDate": "2024-01-10T09:00:00Z"
}
```

**Response (404 Not Found):**
```json
"Customer not found."
```

#### GET /api/customers/search?query=alice
Searches customers by partial, case-insensitive match on FirstName, LastName, Email, or PhoneNumber.

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "firstName": "Alice",
    "lastName": "Johnson",
    "email": "alice.johnson@example.com",
    "phoneNumber": "555-0101",
    "address": "123 Maple Street, Springfield, IL 62701",
    "createdDate": "2024-01-10T09:00:00Z",
    "updatedDate": "2024-01-10T09:00:00Z"
  }
]
```

**Response (400 Bad Request) - Empty query:**
```json
"Search query parameter is required."
```

#### POST /api/customers
Creates a new customer. The ID is auto-generated.

**Request Body:**
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phoneNumber": "555-9999",
  "address": "1 Main Street, Boston, MA 02101"
}
```

**Response (201 Created):**
```json
{
  "id": 6,
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phoneNumber": "555-9999",
  "address": "1 Main Street, Boston, MA 02101",
  "createdDate": "2024-12-15T10:30:00Z",
  "updatedDate": "2024-12-15T10:30:00Z"
}
```

#### PUT /api/customers/{id}
Updates an existing customer. The ID in the URL must match the ID in the request body.

**Request Body:**
```json
{
  "id": 1,
  "firstName": "Alice",
  "lastName": "Johnson-Smith",
  "email": "alice.jsmith@example.com",
  "phoneNumber": "555-0101",
  "address": "456 New Address, Springfield, IL 62701"
}
```

**Response (200 OK):**
```json
{
  "id": 1,
  "firstName": "Alice",
  "lastName": "Johnson-Smith",
  "email": "alice.jsmith@example.com",
  "phoneNumber": "555-0101",
  "address": "456 New Address, Springfield, IL 62701",
  "createdDate": "2024-01-10T09:00:00Z",
  "updatedDate": "2024-12-15T11:00:00Z"
}
```

**Response (400 Bad Request) - ID mismatch:**
```json
"Customer ID in URL does not match the request body."
```

**Response (404 Not Found):**
```json
"Customer not found."
```

#### DELETE /api/customers/{id}
Deletes a customer by ID.

**Response (204 No Content)** - No response body

**Response (404 Not Found):**
```json
"Customer not found."
```

---

## 🏗️ Project Architecture

The solution follows a **layered architecture** pattern with clear separation of concerns:

### Solution Structure

```
Relevantz.API.CustomerDetails/          # Main API project
├── Constants/                          # Centralized constant values
│   └── CustomerConstants.cs           # API routes, messages, Swagger config
├── Controllers/                        # API controllers (presentation layer)
│   └── CustomerController.cs          # RESTful customer endpoints
├── Interfaces/                         # Service & repository contracts
│   ├── ICustomerRepository.cs         # Data access contract
│   └── ICustomerService.cs            # Business logic contract
├── MockData/                           # Seed data for in-memory store
│   └── CustomerMockData.cs            # 5 pre-seeded customers
├── Models/                             # Domain entities
│   └── Customer.cs                    # Customer model with 8 properties
├── Properties/                         # Launch profiles and settings
│   └── launchSettings.json            # HTTP, HTTPS, IIS Express profiles
├── Repositories/                       # Data access layer
│   └── CustomerRepository.cs          # In-memory CRUD implementation
├── Services/                           # Business logic layer
│   └── CustomerService.cs             # Service orchestration
├── appsettings.json                   # Production configuration
├── appsettings.Development.json       # Development configuration
├── Program.cs                          # App bootstrap, DI, middleware
└── Relevantz.API.CustomerDetails.http # HTTP test requests

Relevantz.API.CustomerDetails.Tests/    # xUnit test project
├── Controllers/                        # Controller unit tests
│   └── CustomerControllerTests.cs     # 14 test cases
├── Mocks/                              # Test data helpers
│   └── MockCustomerData.cs            # Sample test customers
└── Services/                           # Service unit tests
    └── CustomerServiceTests.cs        # 8 test cases
```

### Architecture Layers

#### 1. **Presentation Layer** (Controllers)
- `CustomerController.cs`: Handles HTTP requests/responses
- Maps HTTP verbs to service operations
- Validates input and returns appropriate status codes
- Full XML documentation for Swagger

#### 2. **Business Logic Layer** (Services)
- `CustomerService.cs`: Orchestrates business operations
- Implements `ICustomerService` interface
- Delegates to repository for data access
- Currently a pass-through but designed for future business rules

#### 3. **Data Access Layer** (Repositories)
- `CustomerRepository.cs`: Manages in-memory data
- Implements `ICustomerRepository` interface
- Handles CRUD operations and search logic
- Auto-generates IDs and timestamps

#### 4. **Domain Layer** (Models)
- `Customer.cs`: Core domain entity
- Properties: Id, FirstName, LastName, Email, PhoneNumber, Address, CreatedDate, UpdatedDate

#### 5. **Cross-Cutting Concerns**
- `CustomerConstants.cs`: Centralized constants (routes, messages, Swagger)
- `CustomerMockData.cs`: Seed data with 5 sample customers

### Dependency Injection

Services are registered in `Program.cs`:

```csharp
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
```

**Why Scoped?** Each HTTP request gets its own service instance, perfect for managing in-memory state per request.

---

## 🧪 Testing

The project includes comprehensive unit tests using xUnit and Moq.

### Running Tests

```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --verbosity detailed

# Run with code coverage (requires coverlet)
dotnet test /p:CollectCoverage=true
```

### Test Coverage

#### CustomerControllerTests (14 test cases)
- ✅ **GetAll**: Returns 200 with customers list
- ✅ **GetById**: Returns 200 with customer / 404 if not found
- ✅ **Search**: Returns 200 with results / 200 with empty list / 400 for null/empty query
- ✅ **Create**: Returns 201 with created customer
- ✅ **Update**: Returns 200 with updated customer / 404 if not found / 400 for ID mismatch
- ✅ **Delete**: Returns 204 on success / 404 if not found

#### CustomerServiceTests (8 test cases)
- ✅ Validates delegation to repository for all operations
- ✅ Tests GetAll, GetById, Search, Create, Update, Delete
- ✅ Verifies null handling and edge cases

### Test Pattern

All tests follow the **Arrange-Act-Assert (AAA)** pattern:

```csharp
[Fact]
public async Task GetById_ExistingId_ReturnsOk()
{
    // Arrange
    var customer = new Customer { Id = 1, FirstName = "Alice" };
    _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(customer);

    // Act
    var result = await _controller.GetById(1);

    // Assert
    var ok = Assert.IsType<OkObjectResult>(result);
    Assert.Equal(customer, ok.Value);
}
```

---

## 🛠️ Technologies

### Core Framework
- **ASP.NET Core 8.0 Web API** - High-performance, cross-platform web framework
- **.NET 8 SDK** - Latest long-term support (LTS) version

### NuGet Packages (API Project)
- **Swashbuckle.AspNetCore 6.5.0** - Swagger/OpenAPI documentation and UI

### NuGet Packages (Test Project)
- **xUnit 2.5.3** - Modern, extensible unit testing framework
- **xunit.runner.visualstudio 2.5.3** - Visual Studio test adapter
- **Moq 4.20.70** - Mocking framework for interfaces
- **Microsoft.AspNetCore.Mvc.Testing 8.0.0** - Integration testing support
- **Microsoft.NET.Test.Sdk 17.8.0** - Test platform SDK
- **coverlet.collector 6.0.0** - Code coverage collector

### Development Tools
- **Visual Studio 2022** (or later) / Visual Studio Code
- **Git** - Version control
- **PowerShell** - Terminal shell

---

## ⚙️ Configuration

### Application Settings

#### appsettings.json (Production)
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

#### appsettings.Development.json (Development)
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Launch Profiles

Three profiles are available (configured in `launchSettings.json`):

1. **http** - HTTP only (`http://localhost:5048`)
2. **https** - HTTPS with HTTP fallback (`https://localhost:7292`, `http://localhost:5048`)
3. **IIS Express** - IIS Express hosting (`http://localhost:7107`, `https://localhost:44366`)

All profiles:
- Launch browser to Swagger UI
- Set `ASPNETCORE_ENVIRONMENT=Development`
- Enable detailed error pages and Swagger in development mode

---

## 💻 Development

### Seeded Data

The API comes pre-loaded with 5 sample customers:

| ID | Name | Email | Phone | Location |
|----|------|-------|-------|----------|
| 1 | Alice Johnson | alice.johnson@example.com | 555-0101 | Springfield, IL |
| 2 | Bob Smith | bob.smith@example.com | 555-0102 | Columbus, OH |
| 3 | Carol Williams | carol.williams@example.com | 555-0103 | Austin, TX |
| 4 | David Brown | david.brown@example.com | 555-0104 | Denver, CO |
| 5 | Eva Davis | eva.davis@example.com | 555-0105 | Seattle, WA |

### Adding New Features

1. **Add a new property to Customer model**:
   - Update `Customer.cs`
   - Update `CustomerMockData.cs` seed data
   - Update repository `CreateAsync` and `UpdateAsync` logic

2. **Add a new endpoint**:
   - Add method to `ICustomerService` and `ICustomerRepository`
   - Implement in `CustomerService` and `CustomerRepository`
   - Add controller action in `CustomerController`
   - Add constants to `CustomerConstants.cs`
   - Write unit tests in both test projects

3. **Switch to a real database**:
   - Replace `CustomerRepository` with EF Core implementation
   - Add database connection string to `appsettings.json`
   - Register DbContext in `Program.cs`
   - Keep interfaces unchanged for minimal impact

### Best Practices

✅ **Always use interfaces** for services and repositories  
✅ **Write tests first** (TDD approach)  
✅ **Add XML comments** for all public APIs  
✅ **Use constants** instead of magic strings  
✅ **Follow async/await** patterns consistently  
✅ **Validate input** at controller level  
✅ **Return proper HTTP status codes**  
✅ **Keep controllers thin** - delegate to services  

---

## 📄 License

This project is part of Relevantz AI Training Program.

---

## 👤 Author

**Sekhar Chaganti**  
Relevantz Training Repository

---

## 🤝 Contributing

This is a training project. For questions or issues, please contact your training instructor.

---

## 📞 Support

For technical support or questions about this project, please reach out through the Relevantz training portal.
