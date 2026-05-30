# JWTNotesAPI

JWTNotesAPI is a secure ASP.NET Core Web API developed for managing personal notes with JWT-based authentication and authorization. The application allows users to register, log in, and securely perform CRUD operations on their notes.

---

## Features

* User Registration and Login
* JWT Token Generation and Authentication
* Secure Password Hashing using BCrypt
* Add, View, Update, and Delete Notes
* User-specific Note Management
* Input Validation and Error Handling
* Swagger API Testing Support

---

## Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* BCrypt.Net
* Swagger / OpenAPI

---

## Folder Structure

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

---

## Authentication APIs

### Register User

Endpoint:
POST /api/auth/register

Sample Request:

{
"username": "notetaker",
"password": "SecureNote123!"
}

---

### Login User

Endpoint:
POST /api/auth/login

Sample Request:

{
"username": "notetaker",
"password": "SecureNote123!"
}

Sample Response:

{
"token": "<jwt-token>",
"expires_in": 3600,
"user": {
"username": "notetaker"
}
}

---

## Notes APIs

### Add Note

Endpoint:
POST /api/notes

Request Body:

{
"title": "Grocery List",
"content": "Eggs, Milk, Bread"
}

---

### Get Notes

Endpoint:
GET /api/notes

---

### Update Note

Endpoint:
PUT /api/notes/{id}

Request Body:

{
"title": "Updated Grocery List",
"content": "Eggs, Milk, Bread, Butter"
}

---

### Delete Note

Endpoint:
DELETE /api/notes/{id}

---

## Security Features

* Passwords are encrypted using BCrypt hashing.
* JWT Authentication secures all Notes APIs.
* Only authenticated users can access protected endpoints.
* Each user can manage only their own notes.

---

## Database Information

Database: SQL Server

Tables Used:

* Users
* Notes

---

## Running the Application

1. Open the project in Visual Studio.

2. Update the SQL Server connection string in appsettings.json.

3. Open Package Manager Console and run:

Add-Migration InitialCreate

Update-Database

4. Run the application using F5.

5. Swagger UI will open automatically for API testing.

---

## JWT Authorization in Swagger

1. Login using the Login API.
2. Copy the generated JWT token.
3. Click the Authorize button in Swagger.
4. Paste the token.
5. Access protected Notes APIs.

---

## Validation Implemented

* Unique Username Validation
* Password Strength Validation
* Required Field Validation

---










