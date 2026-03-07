# Online Shop (ASP.NET Core MVC)

## Introduction
Full-stack e-commerce web application built with ASP.NET Core MVC.
The project includes authentication, role-based access, product and order management, and database-backed workflows.

## Tech Stack
- C# / ASP.NET Core MVC
- Razor Views
- Entity Framework Core
- MySQL
- ASP.NET Core Identity
- HTTP API integration (Groq AI assistant service)

## Core Features
- User authentication and authorization with roles (`Admin`, `Collaborator`, `User`).
- Product catalog, categories, cart, wishlist, reviews, and order flows.
- Admin dashboard for product moderation, user role management, and order status updates.
- Database integration via EF Core (`ApplicationDbContext`) and seeded startup data.
- AI assistant integration through an external HTTP API for product-related Q&A.

## Repository Structure
- `Controllers/` - MVC controllers for user/admin workflows.
- `Models/`, `ViewModels/` - domain and presentation models.
- `Data/` - EF Core context and seeding.
- `Services/` - business logic and external service integration.
- `Views/` - Razor pages.
- `wwwroot/` - static assets.

## Run Instructions
```bash
dotnet restore
dotnet build
dotnet run
```

## Scope
Portfolio-ready academic project demonstrating practical full-stack development in .NET.
