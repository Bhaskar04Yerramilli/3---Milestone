# JWTNotesAPI

A secure ASP.NET Core Web API for note management using JWT Authentication and SQL Server.

---

# Features

* User Registration
* User Login
* JWT Token Authentication
* Password Hashing using BCrypt
* Add Notes
* Retrieve Notes
* Update Notes
* Delete Notes
* User-specific Note Access
* Input Validation
* Error Handling
* Swagger API Documentation

---

# Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* BCrypt.Net
* Swagger/OpenAPI

---

# Project Structure

```text
JWTNotesAPI
│
├── Controllers
│   ├── AuthController.cs
│   └── NotesController.cs
│
├── Models
│   ├── User.cs
│   └── Note.cs
│
├── DTOs
│   ├── RegisterDto.cs
│   ├── LoginDto.cs
│   └── NoteDto.cs
│
├── Data
│   └── AppDbContext.cs
│
├── Migrations
│
├── Program.cs
├── appsettings.json
```

---

# API Endpoints

## Authentication APIs

### Register User

http
POST /api/auth/register
```

Request Body:

json
{
  "username": "notetaker",
  "password": "SecureNote123!"
}
```

---

Login User

```http
POST /api/auth/login
```

Request Body:

```json
{
  "username": "notetaker",
  "password": "SecureNote123!"
}
```

Response:

```json
{
  "token": "<jwt-token>",
  "expires_in": 3600,
  "user": {
    "username": "notetaker"
  }
}
```

---

# Notes APIs

Add Note

```http
POST /api/notes
```

Request Body:

```json
{
  "title": "Grocery List",
  "content": "Eggs, Milk, Bread"
}
```

---

Get Notes

```http
GET /api/notes
```

---

Update Note

```http
PUT /api/notes/{id}
```

Request Body:

```json
{
  "title": "Updated Grocery List",
  "content": "Eggs, Milk, Bread, Butter"
}
```

---

 Delete Note

```http
DELETE /api/notes/{id}
```

---

# Security Features

* Passwords are securely hashed using BCrypt.
* JWT Authentication is implemented for secure API access.
* Only authenticated users can manage notes.
* Users can only access their own notes.

---

# Database

Database Used:

```text
SQL Server
```

Tables:

* Users
* Notes










