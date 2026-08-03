# ASP.NetCoreWebAPI-E-Commerce-APIs

## E-Commerce Web APIs
This project consists of a set of Web APIs developed using ASP.NET Core for an E-Commerce platform. It follows a layered architecture with separate layers for API, Business Logic, and Data Access. The project utilizes Microsoft SQL Server for data storage and Entity Framework Core for database operations. Additionally, Identity User is employed for user authentication and authorization.

## 📑 Table of Contents
1. Introduction
2. Technologies Used
3. Architecture
4. Project Structure
5. API Endpoints
6. Contributors

---

# 🛒 Introduction

The **E-Commerce Web API** is a scalable RESTful API built with **ASP.NET Core** following the **Onion Architecture** and **Repository Pattern**. It provides a complete backend solution for an online shopping platform, including authentication, product management, categories, shopping cart, orders, payments, and user management.

---

# 🛠 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- ASP.NET Core Identity
- JWT Authentication
- Redis
- AutoMapper
- FluentValidation
- Visual Studio 2022

---

# 🏛 Architecture

This project follows the **Onion Architecture**, which separates the application into independent layers to improve maintainability, scalability, and testability.

### API Layer
- Controllers
- Dependency Injection
- Authentication & Authorization

### Application Layer
- Managers
- DTOs
- Mapping Profiles
- Validators
- Specifications
- Interfaces

### Domain Layer
- Entities
- Domain Interfaces

### Infrastructure Layer
- DbContext
- Entity Configurations
- Migrations
- Identity
- Repositories
- Unit of Work
- Data Seeding
- Payment Services

---

# 📂 Project Structure

```text
E_Commerce
│
├── E_Commerce.Api
│   └── Controllers
│
├── E_Commerce.Application
│   ├── DTOs
│   ├── Managers
│   ├── Mapping
│   ├── Validators
│   ├── Specifications
│   └── Interfaces
│
├── E_Commerce.Domain
│   ├── Entities
│   └── Interfaces
│
└── E_Commerce.Infrastructure
    ├── Data
    │   ├── Configurations
    │   ├── Migrations
    │   └── StoreDbContext
    ├── Identity
    ├── Repositories
    ├── UnitOfWork
    ├── DataSeeding
    └── Payments
```




## API Endpoints


### AuthenticationController

#### POST /api/Authentication/login : Login with credentials.
#### POST /api/Authentication/register : Register a new user.
#### Get /api/Authentication/emailexists : Check For Email Exist
####[Authorized] Get /api/Authentication/currentUser : Check Current User By Token
####[Authorized] Get /api/Authentication/adress : Get User Address By Token 
####[Authorized] Put /api/Authentication/adress : Update Current User Adress

<hr/>

### BasketController

#### Get /api/Basket/{id} : Get Basket With Id
#### Post /api/Basket : Create Or Update Basket
#### Delete /api/Basket : Remove Basket

<hr/>

### Products

#### [Authorized] GET /api/Products : Get All Products
#### [Authorized] GET /api/Products/{Id} : Get With Id
####  GET /api/Products/types  :Get All Types
####  GET /api/Products/brands  :Get All Brands

<hr/>

### OrdersController

#### [Authorized] POST /api/Orders : Create Order
#### [Authorized] GET /api/Orders: Get User Order
#### [Authorized] GET /api/Orders/{Id:guid}: Get User By Id
####  GET /api/Orders/deliveryMethods: Get All Delivery Methods

<hr/>

### PaymentsController

#### 
#### 
#### 
####  

<hr/>


Contributors:
Moataz Mohamed
