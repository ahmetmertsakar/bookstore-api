
# 📚 Bookstore API

A simple RESTful API built with .NET Core and PostgreSQL for managing a bookstore’s inventory and orders.

---

## 🔧 Tech Stack

- **.NET 8** (ASP.NET Core Web API)
- **Entity Framework Core**
- **PostgreSQL**
- **Swagger** for API documentation

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/ahmetmertsakar/bookstore-api.git
cd bookstore-api
```

### 2. Set up PostgreSQL

- Create a database named `BookstoreDb`
- Update your `appsettings.json` connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=BookstoreDb;Username=postgres;Password=yourpassword"
}
```

### 3. Apply the database migration

```bash
dotnet ef database update
```

### 4. Run the API

```bash
dotnet run
```

- Visit Swagger UI at:  
  `http://localhost:{port}/swagger`

---

## 📘 API Endpoints

### 📚 Books

| Method | Endpoint                    | Description               |
|--------|-----------------------------|---------------------------|
| GET    | `/api/books`                | List all books            |
| GET    | `/api/books/{id}`           | Get book by ID            |
| POST   | `/api/books`                | Add a new book            |
| GET    | `/api/books/search?title=`  | Search books by title     |

### 🛒 Orders

| Method | Endpoint                            | Description               |
|--------|-------------------------------------|---------------------------|
| POST   | `/api/orders`                       | Create a new order        |
| GET    | `/api/orders/{id}`                  | Get order details         |
| PUT    | `/api/orders/{id}/status`           | Update order status       |

---

## 📦 Sample Requests & Responses

### ➕ Create Book

**POST** `/api/books`

```json
{
  "title": "Book 1",
  "author": "Author 1",
  "isbn": "123456789",
  "price": 19.99,
  "stockQuantity": 10,
  "categoryId": 1,
  "publicationYear": 2024
}
```

**Response:**
```json
{
  "id": 1,
  "title": "Book 1",
  "author": "Author 1",
  "isbn": "123456789",
  "price": 19.99,
  "stockQuantity": 10,
  "publicationYear": 2024,
  "categoryName": "Fiction"
}
```

---

### 🛒 Create Order

**POST** `/api/orders`

```json
{
  "bookId": 1,
  "quantity": 2
}
```

**Response:**
```json
{
  "id": 1,
  "bookTitle": "Book 1",
  "quantity": 2,
  "totalPrice": 39.98,
  "orderDate": "2025-04-06T22:00:00Z",
  "status": "PENDING"
}
```

---

## ❗ Error Handling

| Code | Scenario                  | Message Example                  |
|------|---------------------------|----------------------------------|
| 404  | Book or Order not found   | `"Book not found."`              |
| 400  | Invalid request data      | `"Quantity must be greater than 0."` |
| 409  | Insufficient stock        | `"Insufficient stock."`          |

---

## 🗂️ Project Structure

```
BookstoreAPI/
├── Controllers/
├── Data/
├── Models/
├── Requests/
├── Responses/
├── Program.cs
├── appsettings.json
└── README.md
```

---

## 🧠 Notes

- DTOs (`BookResponse`, `OrderResponse`) are used to prevent circular references and expose only relevant data.
- Swagger UI is enabled for interactive testing.
- All core features outlined in the technical assessment are implemented.
