<div align="center">

# 🎓 Student Course Registration System (SCRS)

![.NET](https://img.shields.io/badge/.NET-9.0-purple?style=for-the-badge&logo=dotnet)
![Status](https://img.shields.io/badge/Status-Active-success?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-blue?style=for-the-badge)

**A robust ASP.NET Core Web API solution for managing academic enrollments with ease.**

DATA-DRIVEN • SECURE • EFFICIENT

</div>

---

## 📖 About
The **Student Course Registration System** is a backend API designed to handle the complex relationships between students, courses, and instructors. Built with **Entity Framework Core (Code-First)** on **.NET 9.0**, it ensures data integrity, performance, and type safety.

### Key Features
- **Robust CRUD Operations**: Full Create, Read, Update, Delete support for all entities.
- **Safe Updates**: Implements safe `PUT` methods with null checks and validation (e.g., verifying Instructor existence before updating a Course).
- **Strict Integrity**: Prevents duplicate student enrollments automatically.
- **Smart Data Management**: Implements "Soft Delete" to preserve course history while decluttering views.
- **Performance Optimized**: Uses Eager Loading (`.Include()`) and Async/Await patterns for efficiency.

---

## ⚙️ Tech Stack
| Component | Technology | Description |
|-----------|------------|-------------|
| **Core Framework** | ASP.NET Core 9.0 | High-performance cross-platform framework |
| **Database** | SQL Server (LocalDB) | Reliable relational database suitable for development |
| **ORM** | Entity Framework Core 9.0 | Logic-first database management |
| **Architecture** | REST API | Standardized HTTP interface |

---

## 🛠️ Installation & Setup

### 1. Prerequisites
Ensure you have the following installed:
- [.NET SDK 9.0](https://dotnet.microsoft.com/download)
- [SQL Server / LocalDB](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### 2. Clone & Prepare
```powershell
# Navigate to the backend folder
cd Backend
```

### 3. Database Setup
The project uses EF Core Migrations. The connection string in `appsettings.json` defaults to LocalDB.
```powershell
# Create database and tables
dotnet ef database update
```

### 4. Run Application
You can run the application using the standard `dotnet run` command.
```powershell
# Start the API server on default ports
dotnet run
```
*   **Default Default URL**: `http://localhost:5268` (or port determined by `launchSettings.json`)
*   **Kestrel Default URL**: `http://localhost:5000` (if forced or default profile fails)

---

## 📡 API Reference

### 🎓 Students
| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/Students` | Retrieve a list of all students |
| `POST` | `/api/Students` | Register a new student |
| `PUT` | `/api/Students/{id}` | **Update student details** (Safe update with validation) |
| `GET` | `/api/Students/{id}` | Get specific student profile |
| `GET` | `/api/Students/{id}/courses` | View courses a student is enrolled in |

<details>
<summary><b>JSON Example: Create/Update Student</b></summary>

```json
{
  "name": "Rohan Verma",
  "email": "rohan.verma@example.com"
}
```
</details>

### 📚 Courses
| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/Courses` | List all active courses |
| `POST` | `/api/Courses` | Create a new course |
| `PUT` | `/api/Courses/{id}` | **Update course details** (Validates Instructor ID) |
| `DELETE` | `/api/Courses/{id}` | Soft delete a course (mark inactive) |
| `GET` | `/api/Courses/{id}/students` | View class roster |

<details>
<summary><b>JSON Example: Create/Update Course</b></summary>

```json
{
  "title": "Advanced Astrophysics",
  "credits": 4,
  "instructorId": 1
}
```
</details>

### 👨‍🏫 Instructors
| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/Instructors` | List all instructors |
| `POST` | `/api/Instructors` | Add a new instructor |
| `PUT` | `/api/Instructors/{id}` | **Update instructor details** |

### 📝 Enrollments
| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/Enrollments` | Enroll a student in a course |

<details>
<summary><b>JSON Example: Enroll</b></summary>

```json
{
  "studentId": 1,
  "courseId": 5
}
```
</details>

---

## 📂 Project Structure
```text
Backend
 ├── Controllers       # API Request Handlers (Students, Courses, Enrollments, Instructors)
 ├── Models            # Database Entities
 ├── DTOs              # Data Transfer Objects (Create/Read/Update models)
 ├── Data              # DbContext & Database Configuration
 ├── Migrations        # EF Core Migration Files
 └── Program.cs        # Application Entry Point & Configuration
```

---

<div align="center">
Made with ❤️ by .NET Developers
</div>