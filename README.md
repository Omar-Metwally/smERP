# .NET Core API ERP Project

This repository contains a small ERP (Enterprise Resource Planning) system built using .NET Core API. The project is currently under development and serves as a platform for exploring advanced software development concepts.

## Project Overview

This ERP system is designed to provide a centralized solution for managing various business processes. It's built as a single project with a layered architecture to ensure separation of concerns and maintainability. Beyond its practical applications, this project is a vehicle for venturing into advanced topics in software development.

## Learning Objectives

This project aims to explore and implement:

- Design patterns
- Clean code principles
- Best practices in .NET development
- Utilization of popular and widely-known .NET NuGet packages

## Technologies Used

- **.NET Core API**: The main framework used for building the API.
- **Fluent Validation**: For implementing robust input validation.
- **ImageSharp**: Used for image processing capabilities.
- **Serilog**: Implemented for centralized logging across the application.

## Architecture

The project follows a layered architecture:

1. **Repository Layer**: Implements the Unit of Work pattern for data access.
2. **Service Layer**: Contains the business logic of the application.
3. **Controller Layer**: Handles HTTP requests and responses.

## Features (In Progress)

1. **Multi-Company Support**: 
   - Ability to manage multiple companies within the same ERP system.
   - Each company can have multiple branches.
   - Each branch can have multiple storage locations.

2. **Flexible Organizational Structure**:
   - Recognition and management of various company structures.
   - Customizable hierarchy to fit different business models.

3. **Data Encryption**:
   - Encryption of sensitive files such as sales orders and purchase orders.
   - Enhanced security for critical business data.

4. **Inventory Management**:
   - Tracking of inventory across multiple storage locations.
   - Real-time inventory updates and reporting.

5. **Order Processing**:
   - Handling of sales orders and purchase orders.
   - Secure storage and retrieval of order information.

6. [Additional features to be added as development progresses]

## Design Patterns and Principles

This project serves as a practical application of various design patterns and clean code principles. Some of the concepts being explored include:

- Repository Pattern and Unit of Work
- Dependency Injection
- SOLID Principles
- [Other patterns and principles to be added as they are implemented]
