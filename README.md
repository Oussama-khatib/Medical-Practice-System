# Medical Practice System Backend

## 📖 Overview
The **Medical Practice System** is a backend application built with **C#**, **Entity Framework**, and **SQL Server**, exposing a secure **REST API** with **JWT authentication**.  
It enables doctors, nurses, and administrators to manage patients, appointments, diagnoses, treatments, and chronic diseases efficiently.

---

## ⚙️ Tech Stack
- **C# / .NET Web API**
- **Entity Framework Core**
- **SQL Server**
- **JWT Authentication**

---

## 🗄 Database Design
The system uses a relational SQL Server database with the following key tables:

- **Users**: Doctors, nurses, admins, and staff  
- **UserType**: Roles (doctor, nurse, admin)  
- **Patients**: Patient records and contact info  
- **Appointments**: Scheduling, diagnosis, treatment, chronic condition tracking  
- **Diseases**: Chronic disease records  

👉 Appointment statuses include: Pending, Confirmed, Canceled, Rescheduled, Completed.

---

## 🔑 Features Implemented
- **Authentication & Authorization**  
  - Secure login with JWT tokens  
  - Role-based access (doctor, nurse, admin)

- **CRUD Operations**  
  - Insert, update, delete for all tables (Users, Patients, Appointments, Diseases)

- **Stored Procedures**  
  - Close appointment (mark completed, record diagnosis/treatment)  
  - Patients with disease counts  
  - Patients with more than 5 chronic diseases  
  - Appointments between two dates  
  - Patients with no history  
  - Appointments for a specific doctor  
  - Paginated appointment retrieval

- **Triggers**  
  - Automatically add a new disease when a completed appointment is marked as chronic

- **Functions**  
  - Scalar function to calculate average number of diseases per patient

---
   ```bash
   git clone https://github.com/yourusername/Medical-Practice-System.git
