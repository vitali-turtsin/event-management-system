# Event Management System v1.0

This repository contains the source code for an Event Management Web Application developed using ASP.NET Core Web Api and Entity Framework Core. The application allows users to organize and manage events seamlessly. Users can add new events, view participants, add attendees (both individuals and companies), and manage participant details.

## Architecture

- The application is built using the n-tier architecture pattern. It is divided into three layers: **Presentation Layer**, **Business Logic Layer**, and **Data Access Layer**. The Presentation Layer is responsible for handling user requests and responses. The Business Logic Layer is responsible for processing the business logic and the Data Access Layer is responsible for interacting with the database.

- The application uses **Entity Framework Core** to interact with the database. The database schema is designed using the Code First approach. The database schema consists of the following tables: Events, Organizations, People and PaymentMethods.

- Entities between layers are mapped using AutoMapper with a support for customization. The application uses dependency injection to inject services into controllers. Api versioning is implemented using Asp.Versioning library. Web api tests are implemented using xUnit library. The application is containerized using Docker.

- Frontend is implemented using latest version of the Angular framework.

## ERD

![alt text](erd.png)

## How to run the application

1. After cloning the repo navigate to the backend directory:

```bash
cd .\backend\
```

2. Run the following docker compose command to start the application:

```bash
docker compose up
```

3. To see Swagger documentation navigate to `https://localhost:5001/swagger/index.html`

4. From the repository root navigate to the frontend directory:

```bash
cd .\client\
```

5. Run the following command to install the dependencies:

```bash
npm install
```

6. Run the following command to start the frontend application:

```bash
ng serve
```

7. Open a browser and navigate to `http://localhost:4200/` to access the application.

## How to run the tests

1. After cloning the repo navigate to the backend directory:

```bash
cd .\backend\
```

2. Run dotnet test command to run the tests:

```bash
dotnet test tests
```

### Contributors

- [Vitali Turtsin](https://www.linkedin.com/in/vitali-turtsin/)
