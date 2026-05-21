# Assignment 2 - Customer Details CRUD with Database (Relevantz.Api.CustomerDetailsWithDB)

## Overview
A database-backed CRUD API for managing customer details using ASP.NET Core 8 with SQLite and Entity Framework Core. This extends Assignment 1 by replacing in-memory storage with a real database, adding a proper data access layer (Repository/DAO pattern), service layer for business logic, global exception handling, structured logging, and comprehensive unit and integration tests.

## Project Structure
```
Assignment-2-CustomerDetails_CRUD_With_DB/
├── Relevantz.Api.CustomerDetailsWithDB.slnx
├── README.md
├── Relevantz.Api.CustomerDetailsWithDB/              # Web API project
│   ├── Controllers/
│   │   ├── CustomersController.cs
│   │   ├── CustomerAddressesController.cs
│   │   ├── CustomerBusinessProfilesController.cs
│   │   ├── CustomerTagsController.cs
│   │   └── TagsController.cs
│   ├── Interfaces/
│   │   ├── ICustomerService.cs
│   │   ├── ICustomerAddressService.cs
│   │   ├── ICustomerBusinessProfileService.cs
│   │   ├── ICustomerTagService.cs
│   │   └── ITagService.cs
│   ├── Services/
│   │   ├── CustomerService.cs
│   │   ├── CustomerAddressService.cs
│   │   ├── CustomerBusinessProfileService.cs
│   │   ├── CustomerTagService.cs
│   │   └── TagService.cs
│   ├── Middleware/
│   │   └── GlobalExceptionHandlerMiddleware.cs
│   ├── Program.cs
│   ├── appsettings.json
│   ├── Properties/launchSettings.json
│   └── Relevantz.Api.CustomerDetailsWithDB.csproj
├── Relevantz.Api.CustomerDetailsWithDB.Data/         # Data Access Layer
│   ├── Entities/
│   │   ├── Customer.cs
│   │   ├── CustomerAddress.cs
│   │   ├── CustomerBusinessProfile.cs
│   │   ├── CustomerTag.cs
│   │   └── Tag.cs
│   ├── Context/
│   │   └── CustomerDbContext.cs
│   ├── Interfaces/
│   │   ├── ICustomerRepository.cs
│   │   ├── ICustomerAddressRepository.cs
│   │   ├── ICustomerBusinessProfileRepository.cs
│   │   ├── ICustomerTagRepository.cs
│   │   └── ITagRepository.cs
│   ├── Repositories/
│   │   ├── CustomerRepository.cs
│   │   ├── CustomerAddressRepository.cs
│   │   ├── CustomerBusinessProfileRepository.cs
│   │   ├── CustomerTagRepository.cs
│   │   └── TagRepository.cs
│   └── Relevantz.Api.CustomerDetailsWithDB.Data.csproj
└── Relevantz.Api.CustomerDetailsWithDB.Tests/        # Test project
    ├── Unit/
    │   ├── CustomersControllerTests.cs
    │   └── CustomerServiceTests.cs
    ├── Integration/
    │   └── CustomerRepositoryIntegrationTests.cs
    └── Relevantz.Api.CustomerDetailsWithDB.Tests.csproj
```

## Database Schema

### Tables
| Table | Purpose |
|-------|---------|
| customers | Core customer identity, status, and contact info |
| customer_addresses | Billing/Shipping/Office addresses per customer |
| customer_business_profiles | B2B profile (company, job title, lifecycle stage) |
| tags | Reusable tag definitions |
| customer_tags | Many-to-many relationship between customers and tags |

### Relationships
- `customer_addresses.customer_id` → `customers.customer_id` (One-to-Many)
- `customer_business_profiles.customer_id` → `customers.customer_id` (One-to-One)
- `customer_tags` → composite key (`customer_id`, `tag_id`) (Many-to-Many)

## Setup Steps

### Prerequisites
- .NET 8 SDK
- No external database installation required (SQLite is file-based)

### Run the API
```bash
cd Assignment-2-CustomerDetails_CRUD_With_DB
dotnet run --project Relevantz.Api.CustomerDetailsWithDB
```
The SQLite database file (`customerdetails.db`) is created automatically on first run.

### Swagger UI
Once the application is running, open your browser and navigate to:
```
https://localhost:7263/swagger
```

### Run Tests
```bash
cd Assignment-2-CustomerDetails_CRUD_With_DB
dotnet test
```

### Configuration
Connection string in `Relevantz.Api.CustomerDetailsWithDB/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=customerdetails.db"
  }
}
```

## API Endpoints

### Customers
| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/customers` | Get all customers |
| GET | `/api/customers/{id}` | Get customer by ID |
| GET | `/api/customers/search?query=` | Search customers by name or email |
| POST | `/api/customers` | Create customer |
| PUT | `/api/customers/{id}` | Update customer |
| DELETE | `/api/customers/{id}` | Delete customer |

### Customer Addresses
| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/customers/{customerId}/addresses` | Get all addresses for a customer |
| GET | `/api/customers/{customerId}/addresses/{addressId}` | Get specific address |
| POST | `/api/customers/{customerId}/addresses` | Create address for a customer |
| PUT | `/api/customers/{customerId}/addresses/{addressId}` | Update address |
| DELETE | `/api/customers/{customerId}/addresses/{addressId}` | Delete address |

### Customer Business Profiles
| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/customers/{customerId}/profile` | Get business profile |
| POST | `/api/customers/{customerId}/profile` | Create business profile |
| PUT | `/api/customers/{customerId}/profile` | Update business profile |
| DELETE | `/api/customers/{customerId}/profile` | Delete business profile |

### Tags
| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/tags` | Get all tags |
| POST | `/api/tags` | Create tag |
| DELETE | `/api/tags/{id}` | Delete tag |

### Customer Tags
| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/customers/{customerId}/tags` | Get tags for a customer |
| POST | `/api/customers/{customerId}/tags/{tagId}` | Assign tag to customer |
| DELETE | `/api/customers/{customerId}/tags/{tagId}` | Remove tag from customer |

## Validations
- **FirstName / LastName**: Required, 2–100 characters
- **Email**: Required, valid email format, unique in database
- **Status**: Must be one of: Active, Inactive, Archived, Pending
- **CustomerType**: Must be B2C or B2B
- **AddressType**: Must be Billing, Shipping, or Office
- **AddressLine1, City, PostalCode**: Required
- **CountryCode**: Required, max 3 characters
- **LifecycleStage**: Must be Lead, Opportunity, Customer, or Churned
- **TagName**: Required, max 50 characters, unique

## Architecture

### Layered Design
```
HTTP Request → Controller → Service → Repository → Database (SQLite)
```

- **Controllers**: Thin HTTP layer; handle routing, model binding, and HTTP status codes only.
- **Services**: Contain business logic and validation rules. Each entity has a dedicated service interface and implementation with `ILogger<T>` for structured logging.
- **Repositories**: Encapsulate all EF Core database operations behind interfaces for testability.
- **Middleware**: Global exception handler catches unhandled exceptions and returns consistent JSON error responses.

### Key Design Decisions
| Concern | Approach |
|---------|----------|
| Persistence | EF Core 8 with SQLite (code-first, auto-created DB) |
| DI | All repositories and services registered as scoped |
| Exception Handling | `GlobalExceptionHandlerMiddleware` maps exception types to HTTP status codes |
| Logging | `ILogger<T>` injected in all services; key operations logged at Information level |
| Testing | Unit tests mock services (Moq); integration tests use SQLite in-memory |
| API Documentation | Swagger/OpenAPI via Swashbuckle (development mode) |

### Exception Handling Strategy
| Exception Type | HTTP Status Code | Use Case |
|---------------|-----------------|----------|
| `ArgumentException` | 400 Bad Request | Invalid input parameters |
| `KeyNotFoundException` | 404 Not Found | Resource does not exist |
| `InvalidOperationException` | 409 Conflict | Business rule violation |
| Unhandled `Exception` | 500 Internal Server Error | Unexpected failures (generic message returned) |

## Tests

### Unit Tests (Moq-based)
- **CustomersControllerTests**: Verifies controller returns correct HTTP status codes by mocking `ICustomerService`.
- **CustomerServiceTests**: Verifies validation logic and repository delegation by mocking `ICustomerRepository`.

### Integration Tests (SQLite In-Memory)
- **CustomerRepositoryIntegrationTests**: End-to-end repository tests against a real SQLite in-memory database to verify EF Core queries, relationships, and CRUD operations.

## Technology Stack
| Technology | Purpose |
|-----------|---------|
| .NET 8 | Target framework |
| ASP.NET Core Web API | REST API framework |
| Entity Framework Core 8 | ORM (code-first) |
| SQLite | Lightweight file-based database |
| Swashbuckle (Swagger) | API documentation |
| xUnit | Test framework |
| Moq | Mocking library for unit tests |
| Microsoft.Extensions.Logging | Structured logging |
