# Assignment 2 - Customer Details CRUD with Database (Relevantz.Api.CustomerDetailsWithDB)

## Overview
A database-backed CRUD API for managing customer details using ASP.NET Core 8 with SQLite and Entity Framework Core. This extends Assignment 1 by replacing in-memory storage with a real database, adding a proper data access layer (Repository/DAO pattern), service layer, global exception handling, and comprehensive tests.

## Project Structure
```
Assignment-2-CustomerDetails_CRUD_With_DB/
+-- Relevantz.Api.CustomerDetailsWithDB.slnx
+-- README.md
+-- Relevantz.Api.CustomerDetailsWithDB/              # Web API project
|   +-- Controllers/
|   |   +-- CustomersController.cs
|   |   +-- CustomerAddressesController.cs
|   |   +-- CustomerBusinessProfilesController.cs
|   |   +-- CustomerTagsController.cs
|   |   +-- TagsController.cs
|   +-- Interfaces/
|   |   +-- ICustomerService.cs
|   |   +-- ICustomerAddressService.cs
|   |   +-- ICustomerBusinessProfileService.cs
|   |   +-- ICustomerTagService.cs
|   |   +-- ITagService.cs
|   +-- Services/
|   |   +-- CustomerService.cs
|   |   +-- CustomerAddressService.cs
|   |   +-- CustomerBusinessProfileService.cs
|   |   +-- CustomerTagService.cs
|   |   +-- TagService.cs
|   +-- Middleware/
|   |   +-- GlobalExceptionHandlerMiddleware.cs
|   +-- Program.cs
|   +-- appsettings.json
|   +-- Properties/launchSettings.json
|   +-- Relevantz.Api.CustomerDetailsWithDB.csproj
+-- Relevantz.Api.CustomerDetailsWithDB.Data/         # Data Access Layer
|   +-- Entities/
|   |   +-- Customer.cs
|   |   +-- CustomerAddress.cs
|   |   +-- CustomerBusinessProfile.cs
|   |   +-- CustomerTag.cs
|   |   +-- Tag.cs
|   +-- Context/
|   |   +-- CustomerDbContext.cs
|   +-- Interfaces/
|   |   +-- ICustomerRepository.cs
|   |   +-- ICustomerAddressRepository.cs
|   |   +-- ICustomerBusinessProfileRepository.cs
|   |   +-- ICustomerTagRepository.cs
|   |   +-- ITagRepository.cs
|   +-- Repositories/
|   |   +-- CustomerRepository.cs
|   |   +-- CustomerAddressRepository.cs
|   |   +-- CustomerBusinessProfileRepository.cs
|   |   +-- CustomerTagRepository.cs
|   |   +-- TagRepository.cs
|   +-- Relevantz.Api.CustomerDetailsWithDB.Data.csproj
+-- Relevantz.Api.CustomerDetailsWithDB.Tests/        # Test project
    +-- Relevantz.Api.CustomerDetailsWithDB.Tests.csproj
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
- customer_addresses.customer_id -> customers.customer_id (One-to-Many)
- customer_business_profiles.customer_id -> customers.customer_id (One-to-One)
- customer_tags -> composite key (customer_id, tag_id) (Many-to-Many)

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
| POST | `/api/customers` | Create customer |
| PUT | `/api/customers/{id}` | Update customer |
| DELETE | `/api/customers/{id}` | Delete customer |

### Customer Addresses
| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/customeraddresses` | Get all addresses |
| GET | `/api/customeraddresses/{id}` | Get address by ID |
| POST | `/api/customeraddresses` | Create address |
| PUT | `/api/customeraddresses/{id}` | Update address |
| DELETE | `/api/customeraddresses/{id}` | Delete address |

### Customer Business Profiles
| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/customerbusinessprofiles` | Get all business profiles |
| GET | `/api/customerbusinessprofiles/{id}` | Get business profile by ID |
| POST | `/api/customerbusinessprofiles` | Create business profile |
| PUT | `/api/customerbusinessprofiles/{id}` | Update business profile |
| DELETE | `/api/customerbusinessprofiles/{id}` | Delete business profile |

### Tags
| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/tags` | Get all tags |
| GET | `/api/tags/{id}` | Get tag by ID |
| POST | `/api/tags` | Create tag |
| PUT | `/api/tags/{id}` | Update tag |
| DELETE | `/api/tags/{id}` | Delete tag |

### Customer Tags
| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/customertags` | Get all customer-tag mappings |
| GET | `/api/customertags/{id}` | Get mapping by ID |
| POST | `/api/customertags` | Create customer-tag mapping |
| DELETE | `/api/customertags/{id}` | Delete customer-tag mapping |

## Validations
- FirstName / LastName: Required, 2-100 characters
- Email: Required, valid email format, unique in database
- Status: Must be one of: Active, Inactive, Archived, Pending
- CustomerType: Must be B2C or B2B
- AddressType: Must be Billing, Shipping, or Office
- AddressLine1, City, PostalCode: Required
- CountryCode: Required, max 3 characters
- LifecycleStage: Must be Lead, Opportunity, Customer, or Churned
- TagName: Required, max 50 characters, unique

## Architecture
- **Layered Architecture**: Controllers → Services → Repositories → Database
- **Repository Pattern**: All DB access through interfaces (ICustomerRepository, etc.)
- **Service Layer**: Business logic separated from controllers via service interfaces
- **Entity Framework Core 8**: ORM with code-first approach targeting SQLite
- **Dependency Injection**: All services and repositories registered as scoped services
- **Global Exception Handling**: Custom middleware for consistent error responses
- **Async/Await**: All database operations are fully asynchronous
- **Swagger/OpenAPI**: API documentation available at `/swagger` in development mode

## Technology Stack
| Technology | Purpose |
|-----------|---------|
| .NET 8 | Target framework |
| ASP.NET Core Web API | REST API framework |
| Entity Framework Core 8 | ORM |
| SQLite | Database |
| Swashbuckle (Swagger) | API documentation |
| xUnit | Unit testing |
