## CarRental - Car Rental Application

## Project Description

CarRental is a C# application based on a basic microservices architecture. It consists of a backend built with Web API and a frontend using MVC. The application provides car rental functionalities such as booking, browsing available vehicles, and user and vehicle management for administrators.

## Status
I'm plannig to add Docker to the application

## Technologies Used

The project is built using the following technologies:

## Backend:

- ASP.NET Core Web API

- Entity Framework Core

- MySQL

- JWT Authentication

- MediatR

- Fluent Validation

- AutoMapper

- xUnit (Unit Testing)

- Moq for unit tests

- Toastr for notifications

## Frontend:

- ASP.NET Core MVC

- HTML
- CSS
- Bootstrap

## User Functions:

- Registration and login (JWT Authentication)

- Browse available vehicles

- Rent a car

- View personal reservations

## Administrator Functions:

- Add Cars
  
- Delete Cars
  
- Edit Cars
  
- Display All Cars

- View all users and their reservations

- Delete users

## Some screenshots from application
![CARRENTAL1](https://github.com/user-attachments/assets/e3d45cc6-f829-4cb5-9996-f9e03fa2dd77)
![CARRENTAL2](https://github.com/user-attachments/assets/c497bbd0-a8be-4e8f-b8b2-21399f03c6c0)
![CARRENTAL3](https://github.com/user-attachments/assets/e1785223-7f12-4edb-846e-2e4aa4373c13)
![CARRENTAL4](https://github.com/user-attachments/assets/e71f6d33-57f5-4e6e-9e20-429f00e55334)
![CARRENTAL5](https://github.com/user-attachments/assets/3b375ed8-2f80-4ae5-b2d6-25af10eb25ef)
![CARRENTAL6](https://github.com/user-attachments/assets/46f67211-d271-46a8-81d7-e9b886d50a85)
![CARRENTAL7](https://github.com/user-attachments/assets/548b951e-9948-48ef-933f-4172b9e98a7e)
![CARRENTAL8](https://github.com/user-attachments/assets/50bfde42-56c0-4de6-a482-fc0c6801638e)






## Database

The application uses MySQL to store data about users, cars, and reservations.

## Requirements:

- .NET 8

- MySQL Server

## Step 1: Clone the Repository
```cmd
git clone https://github.com/your-username/carrental.git
cd carrental
```

## Step 2: Configure Database Connection

The MySQL connection string is stored in the application’s secret settings. To configure it, set it using User Secrets:
```cmd
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;database=CarRentalDB;user=root;password=yourpassword;"
```

Alternatively, you can set it as an environment variable.

## Step 3: Apply Database Migrations
```cmd
dotnet ef database update
```
## Step 4: Run the Application
```cmd
dotnet run
```
The application should be accessible at http://localhost:7090/.


Author

Patryk Rekiel;

