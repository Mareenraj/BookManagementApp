# 📚 Book Management System

A full-stack web application to manage a list of books with full CRUD (Create, Read, Update, Delete) functionality.

## Tech Stack

| Layer    | Technology                          |
| -------- | ----------------------------------- |
| Frontend | Angular 21, TypeScript, Tailwind CSS |
| Backend  | ASP.NET (.NET 10), C#               |
| API Docs | Swagger / OpenAPI                    |
| Storage  | In-Memory List (no database needed)  |

## Project Structure

```
book_management_system/
├── BookApi/          # ASP.NET Backend
│   └── BookApi/
│       ├── Controllers/    # API endpoints
│       ├── Models/         # Entities & DTOs
│       ├── Services/       # Business logic
│       ├── Repositories/   # Data access
│       └── Program.cs      # App entry point
├── BookUi/           # Angular Frontend
│   └── src/app/
│       ├── books/
│       │   ├── book-list/       # List & delete books
│       │   ├── book-form/       # Add & edit books
│       │   ├── book-api.service.ts
│       │   └── book.model.ts
│       └── app.routes.ts
└── README.md
```

## Book Model

| Property        | Type     |
| --------------- | -------- |
| id              | int      |
| title           | string   |
| author          | string   |
| isbn            | string   |
| publicationDate | DateTime |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js (v18+)](https://nodejs.org/)
- [Angular CLI](https://angular.dev/) — install with `npm install -g @angular/cli`

## Getting Started

### 1. Run the Backend

```bash
cd BookApi/BookApi
dotnet run
```

The API will start at **http://localhost:5282**. Swagger UI is available at `/swagger` in development mode.

### 2. Run the Frontend

```bash
cd BookUi
npm install
ng serve
```

The app will open at **http://localhost:4200**.

## API Endpoints

| Method | Endpoint         | Description       |
| ------ | ---------------- | ----------------- |
| GET    | `/api/books`     | Get all books     |
| GET    | `/api/books/{id}`| Get a book by ID  |
| POST   | `/api/books`     | Add a new book    |
| PUT    | `/api/books/{id}`| Update a book     |
| DELETE | `/api/books/{id}`| Delete a book     |

## Features

- ✅ View list of all books
- ✅ Add new books via form
- ✅ Edit existing book details
- ✅ Delete books
- ✅ Form validation
- ✅ Responsive UI with dark/light mode
- ✅ Swagger API documentation

## Notes

- Data is stored **in-memory** — it resets when the backend restarts.
- CORS is configured to allow requests from `http://localhost:4200`.
