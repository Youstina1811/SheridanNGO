# SheridanNGO Web Platform - 2024/2025

# A Donation & Volunteer Management Platform for Non-Profit Organizations

### Sheridan College Group Project

![SheridanNGO Logo](Logo/NGO_Logo.png)

## Overview

This repository contains the work of our team for the **SheridanNGO Web Platform**, a full-stack web application built at **Sheridan College**.

The platform enables **non-profit organizations (NGOs)** to manage **donations, fundraising campaigns, donors, and volunteers** in a centralized, secure system. It combines a responsive front-end (HTML, CSS, JavaScript) with a robust **ASP.NET Core MVC** backend, **Entity Framework Core** for data access, and optional **Stripe** integration for payment processing.  

This project demonstrates our ability to deliver real-world value through **web application development, database design, authentication, payment integration, and team collaboration**.

## The Team

| Name | Role | GitHub Profile |
| :--- | :--- | :--- |
| Youstina Botros | Frontend / Authentication / Dashboard | [@Youstina1811](https://github.com/Youstina1811) |
| *(Additional classmates)* | Backend / UX / Database | *(Class project team members not listed here)* |

## Website

*(Local project – runs on `https://localhost` during development)*

-----

## Business Case & Objective

**The Problem:**  
Many NGOs still rely on manual processes (spreadsheets, emails, paper forms) to track donations, manage campaigns, and coordinate volunteers. This leads to **poor transparency, high administrative overhead, and a fragmented donor experience**.

**Our Objective:**  
To design and implement a **unified web platform** where:

- Donors can discover campaigns and donate securely  
- NGOs can create and manage fundraising campaigns  
- Volunteers can register their interest and availability  
- Admins can oversee users, campaigns, and donations from a central dashboard  

**Guiding Questions:**

  * **Audience:** Small–to–mid-size NGOs, charity organizations, campus clubs, and school-led initiatives.
  * **Key Question:** *How can we simplify donation and volunteer management while ensuring security, transparency, and a user-friendly experience?*
  * **Business Value:** Providing NGOs with a **low-cost, scalable digital solution** that improves donor trust, operational efficiency, and engagement.

-----

## The Platform & Tech Stack

The application is built using a modern Microsoft web stack:

  * **Backend:** ASP.NET Core MVC (C#)
  * **Data Access:** Entity Framework Core with **SQLite**
  * **Frontend:** HTML, CSS, JavaScript, Razor Views
  * **Authentication:** Cookie-based authentication, claims identity, hashed passwords
  * **Payments (Optional):** Stripe `PaymentIntent` API
  * **Environment:** .NET 8, Visual Studio / Visual Studio Code

**Core Domain Models:**

  * `User` – Donor, NGO, or Admin (with `UserType`)
  * `Donation` – Tracks donor, NGO, amount, and date
  * `Campaign` – NGO-run fundraising campaigns with goals, dates, and categories
  * `NGO` – Organization profile, mission, and website
  * `Receipt` – Donation receipt details (amount, date, donation reference)
  * `Volunteer` – Volunteer registrations
  * `Contact` – Contact form submissions

> *Note: Entity relationships and schema are defined in `DonationDbContext` and migrations under the `Migrations` folder.*

-----

## Repository Structure

We have organized our repository to ensure clean separation of concerns and maintainability:

```text
├── Controllers
│   ├── AccountController.cs        # Authentication, dashboard, profile, admin actions
│   ├── CampaignsController.cs      # CRUD for campaigns
│   ├── PaymentController.cs        # Stripe payment intent creation
│   └── HomeController.cs           # Public-facing pages (Home, SignIn, SignUp, Volunteer, Contact)
│
├── Models
│   ├── User.cs                     # Custom user model with roles (donor, NGO, admin)
│   ├── Donation.cs                 # Donations linked to users and NGOs
│   ├── Campaign.cs                 # Campaigns with goals, dates, categories
│   ├── NGO.cs                      # NGO entity with mission and contact info
│   ├── Receipt.cs                  # Donation receipts
│   ├── Volunteer.cs                # Volunteer registrations
│   ├── Contact.cs                  # Contact form model
│   └── ErrorViewModel.cs           # Error handling model
│
├── Data
│   └── DonationDbContext.cs        # EF Core DbContext with relationships configuration
│
├── Migrations                      # Auto-generated EF Core database migrations
│
├── Views                           # Razor views (HTML + C#)
│   ├── Home                        # Home, SignIn, SignUp, Volunteer, Contact, etc.
│   ├── Account                     # Dashboard, ManageProfile, Admin views, Receipts
│   ├── Campaigns                   # Index, Details, Create, Edit, Delete
│   └── Shared                      # Layout, partials
│
├── wwwroot
│   ├── css                         # Stylesheets
│   ├── js                          # Client-side scripts
│   └── images                      # Static assets
│
├── Properties
│   └── launchSettings.json         # Local HTTP/HTTPS configuration
│
├── appsettings.json                # Connection strings and app configuration
├── Program.cs                      # Application bootstrap
└── README.md                       # Project documentation
```
-----

## Methodology

### 1. MVC Architecture (Model–View–Controller)

The SheridanNGO platform is developed using **ASP.NET Core MVC**, ensuring clean separation of responsibilities:

  * **Models:** Database entities representing Users, NGOs, Campaigns, Donations, Receipts, and Volunteers.  
  * **Views:** Razor-based UI pages built using HTML, CSS, JavaScript, and shared layouts.  
  * **Controllers:** Application logic handling routing, requests, and interaction with the database.

This architecture provides a strong foundation for **scalability, maintainability, and clean code organization**.

### 2. Entity Framework Core

We used **Entity Framework Core** with a **SQLite** database for data persistence.  
Relationships between models were defined using Fluent API inside `OnModelCreating`.

**Foreign Key Mapping:**

  * **User (Donor)** → **Donation**  
  * **NGO** → **Campaigns**  
  * **Donation** → **Receipt**

These relationships ensure referential integrity and support structured querying across entities.

### 3. Authentication Workflow

User authentication follows a **claims-based identity** approach with cookie authentication:

  * User submits login credentials  
  * Password is validated using `PasswordHasher<User>`  
  * Claims identity (email, UserID, UserType) is created  
  * Cookie authentication signs the user in  
  * Redirect based on role:
      * **Admin** → Admin Console  
      * **Donor / NGO** → Dashboard

This system ensures a secure and role-aware access flow.

### 4. Stripe Payment Integration

Payment processing is implemented using **Stripe**:

  * StripeClient initializes secure API communication  
  * Backend creates `PaymentIntent` for each donation  
  * Front-end completes payment  
  * A receipt is generated and stored in the database  

This enables future support for recurring donations, refunds, and full payment logs.

-----

## Key Screens & User Flows

### 🏠 Home Page

  * Mission statement  
  * “Donate Now” call-to-action  
  * Featured fundraising campaigns  
  * Volunteer registration link  

### 💳 Donation Flow

  * User selects a campaign  
  * Enters donation amount  
  * Stripe processes payment  
  * Receipt is generated and stored  

### 📊 Admin Dashboard

  * List of donors, admins, NGOs  
  * Promote or demote users (donor ↔ admin)  
  * Delete accounts  
  * Manage campaigns (create/edit/delete)  
  * View volunteer registrations  

-----

## Project Highlights

  * ✔ Developed full navigation and UI components (homepage, donation form, volunteer page)  
  * ✔ Implemented secure claims-based authentication  
  * ✔ Built admin dashboards and campaign CRUD features  
  * ✔ Designed responsive and accessible UI — praised for clarity and consistency  
  * ✔ Team collaboration using GitHub branches, Pull Requests, and code reviews  

-----

## How to Run the Project

1. **Clone the repository:**

    ```bash
    git clone https://github.com/<your-repo>/SheridanNGO.git
    cd SheridanNGO
    ```

2. **Install dependencies:**

    ```bash
    dotnet restore
    ```

3. **Run the application:**

    ```bash
    dotnet run
    ```

4. **Access the app:**

    ```
    https://localhost:7028
    http://localhost:5072
    ```

-----

## Future Improvements

  * Add donor analytics dashboards  
  * Send email notifications with receipts  
  * Generate PDF receipts  
  * Multi-NGO account and campaign creation  
  * Image upload for campaigns  
  * Admin-level donation and growth reporting  

-----

## Contact

For questions or collaboration opportunities, please reach out:

  * **GitHub:** [@Youstina1811](https://github.com/Youstina1811)

-----



