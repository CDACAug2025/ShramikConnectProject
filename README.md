

---


# ShramikConnect

## Role-Based Job & Contract Management Platform

---

## 📌 Project Overview

**ShramikConnect** is a full-stack web application designed to digitally connect **workers, clients, organizations, and administrators** on a single secure platform. The system enables job posting, job applications, contract creation, escrow-based payments, and role-specific dashboards, ensuring transparency, accountability, and trust in short-term and contract-based work.

The application is built using a **modern decoupled architecture**, with a **React.js frontend** and an **ASP.NET Core Web API backend**, making it scalable, maintainable, and suitable for real-world deployment.

---

## 🛠️ Technology Stack

### Frontend
- React.js
- React Router (role-based routing)
- Context API (authentication & user state)
- Axios (API communication)
- Modular component-based architecture

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- Role-based authorization
- RESTful API design

---

## 🏗️ System Architecture

ShramikConnect follows a **client–server architecture**:

- **React Frontend**
  - Handles UI rendering, routing, and role-based dashboards
  - Communicates with backend APIs using secure authentication
- **ASP.NET Core Web API**
  - Manages authentication, business logic, and data persistence
  - Exposes REST APIs for all frontend operations

---

## 👥 User Roles & Capabilities

### 🔑 Admin
- Manage users (workers, clients, organizations)
- Monitor jobs, contracts, payments, and disputes
- View system reports and analytics
- Resolve disputes and oversee platform activity

### 👷 Worker
- Browse available jobs
- Apply for jobs
- Track application status
- View contracts and earnings
- Manage personal profile and skills

### 🧑‍💼 Client
- Post jobs
- View applicants
- Create contracts with selected workers
- Manage escrow-based payments
- Track job progress

### 🏢 Organization
- Manage multiple workers
- Create and manage contracts
- View analytics and performance insights
- Oversee large-scale job assignments

---

## 📂 Frontend Folder Structure (React)

```text
src/
├── assets/
├── components/
│   ├── common/
│   ├── admin/
│   ├── worker/
│   ├── client/
│   └── organization/
├── pages/
│   ├── common/
│   ├── auth/
│   ├── admin/
│   ├── worker/
│   ├── client/
│   ├── organization/
│   ├── jobs/
│   ├── profile/
│   ├── chat/
│   └── payment/
├── services/
├── context/
├── routes/
├── utils/
├── App.jsx
└── main.jsx
````

---

## 📂 Backend Folder Structure (ASP.NET Core Web API)

```text
ShramikConnectWebApi
├── Controllers
├── Data
│   ├── AppDbContext.cs
│   └── Seed
├── Models
│   ├── Core
│   ├── Profiles
│   ├── Jobs
│   ├── Contracts
│   ├── Chat
│   ├── Commerce
│   ├── Audit
│   └── Kyc
├── Services
├── Shared
│   ├── Enums
│   ├── Constants
│   └── Helpers
├── Program.cs
└── appsettings.json
```

---

## ⚙️ Key Features

* User registration and authentication
* Role-based dashboards and access control
* Job posting and job application workflow
* Contract creation between clients and workers
* Escrow-based payment lifecycle
* Dispute handling and admin resolution
* Chat system per contract (planned)
* Analytics and reporting (planned)

---

## 🚀 Scalability & Future Enhancements

* Mobile application support
* Third-party payment gateway integration
* Notification services (email/SMS)
* Advanced analytics and AI-based job matching
* Real-time chat and dispute tracking

---

## 📌 Conclusion

**ShramikConnect** demonstrates a real-world, production-ready system design with:

* Clean architecture principles
* Secure role-based access control
* Scalable frontend and backend separation
* Practical implementation of contracts and payments

The platform is suitable for workforce management systems, job marketplaces, and contract-based service platforms.

---

## 📄 License

This project is for educational and learning purposes.

```

