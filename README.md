# Relevantz.API.CustomerDetails

A RESTful ASP.NET Core Web API (.NET 8) for managing customer details with full CRUD operations. All data is stored in-memory using seeded mock data — no external database is required.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

---

## Getting Started

### 1. Clone the repository

```bash
git clone <repository-url>
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

The API will be available at `https://localhost:5001` (or `http://localhost:5000`).  
Swagger UI is available in development mode at: `https://localhost:5001/swagger`

---

## Running Unit Tests

```bash
dotnet test
```

---

## API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/customers` | Get all customers |
| GET | `/api/customers/{id}` | Get a customer by ID |
| GET | `/api/customers/search?query={query}` | Search customers by name, email, or phone |
| POST | `/api/customers` | Create a new customer |
| PUT | `/api/customers/{id}` | Update an existing customer |
| DELETE | `/api/customers/{id}` | Delete a customer |

### Sample Request & Response

**GET /api/customers**
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

**POST /api/customers** — Request body:
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phoneNumber": "555-9999",
  "address": "1 Main Street, Boston, MA 02101"
}
```

**GET /api/customers/search?query=alice** — Returns all customers whose FirstName, LastName, Email, or PhoneNumber contains "alice" (case-insensitive).

---

## Project Structure

```
Relevantz.API.CustomerDetails/          # Main API project
├── Controllers/                        # API controllers
│   └── CustomerController.cs
├── Constants/                          # All constant values
│   └── CustomerConstants.cs
├── Interfaces/                         # Service & repository contracts
│   ├── ICustomerRepository.cs
│   └── ICustomerService.cs
├── MockData/                           # In-memory seeded data
│   └── CustomerMockData.cs
├── Models/                             # Domain model
│   └── Customer.cs
├── Repositories/                       # Data access layer
│   └── CustomerRepository.cs
├── Services/                           # Business logic layer
│   └── CustomerService.cs
└── Program.cs                          # App bootstrap & DI registration

Relevantz.API.CustomerDetails.Tests/    # xUnit test project
├── Controllers/                        # Controller unit tests
│   └── CustomerControllerTests.cs
├── Mocks/                              # Shared test mock helpers
│   └── MockCustomerData.cs
└── Services/                           # Service unit tests
    └── CustomerServiceTests.cs
```

---

## Technologies

- ASP.NET Core 8 Web API
- Swashbuckle.AspNetCore (Swagger / OpenAPI)
- xUnit (unit testing framework)
- Moq (mocking library)
