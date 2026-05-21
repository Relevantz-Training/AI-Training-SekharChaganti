# Sekhar-Chaganti

This repository contains all training assignments by **Sekhar Chaganti** as part of the Relevantz AI Training Program. Each assignment is organized in its own subfolder with independent solution files, documentation, and tests.

---

## 📂 Repository Structure

```
AI-Training-SekharChaganti/
├── Assignment_1_CustomerDetails_CRUD/                # In-memory CRUD API
│   ├── Relevantz.API.CustomerDetails/                # Main API project
│   ├── Relevantz.API.CustomerDetails.Tests/          # Unit tests
│   ├── Relevantz.API.CustomerDetails.slnx            # Solution file
│   └── README.md                                     # Assignment docs
├── Assignment-2-CustomerDetails_CRUD_With_DB/        # Database-backed CRUD API
│   ├── Relevantz.Api.CustomerDetailsWithDB/          # Web API (Controllers, Services, Middleware)
│   ├── Relevantz.Api.CustomerDetailsWithDB.Data/     # Data layer (Entities, Repositories, EF Core)
│   ├── Relevantz.Api.CustomerDetailsWithDB.Tests/    # Unit & Integration tests
│   ├── Relevantz.Api.CustomerDetailsWithDB.slnx      # Solution file
│   └── README.md                                     # Assignment docs
├── .gitignore
└── README.md                                         # This file
```

---

## 📝 Assignments

| # | Folder | Description | Tech Stack |
|---|--------|-------------|------------|
| 1 | [Assignment_1_CustomerDetails_CRUD](./Assignment_1_CustomerDetails_CRUD/) | RESTful Web API for managing customer details with in-memory CRUD operations | ASP.NET Core 8, xUnit, Moq, Swagger |
| 2 | [Assignment-2-CustomerDetails_CRUD_With_DB](./Assignment-2-CustomerDetails_CRUD_With_DB/) | Database-backed CRUD API with Repository pattern, Service layer, exception handling, and logging | ASP.NET Core 8, EF Core 8, SQLite, xUnit, Moq, Swagger |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Running an Assignment

Navigate to any assignment folder and use the solution file:

```bash
# Assignment 1 - In-memory CRUD
cd Assignment_1_CustomerDetails_CRUD
dotnet build
dotnet run --project Relevantz.API.CustomerDetails
dotnet test

# Assignment 2 - Database-backed CRUD with SQLite
cd Assignment-2-CustomerDetails_CRUD_With_DB
dotnet build
dotnet run --project Relevantz.Api.CustomerDetailsWithDB
dotnet test
```

---

## 🏗️ Architecture Highlights (Assignment 2)

- **Layered Architecture**: Controllers → Services → Repositories → SQLite Database
- **Global Exception Handling**: Custom middleware for consistent JSON error responses
- **Structured Logging**: `ILogger<T>` in all service classes
- **Comprehensive Tests**: Unit tests (Moq) + Integration tests (SQLite in-memory)

---

## 👤 Author

**Sekhar Chaganti**  
Relevantz AI Training Program
