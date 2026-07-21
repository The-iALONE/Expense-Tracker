
# Expense Tracker

A simple Expense Tracker app written in C#.


## Project Goal

The purpose of this project was to learn, practice, and strengthen my C# skills by building a real-world console application. While there are many features and improvements that could be added, they were intentionally left out because the focus of this project was learning software design principles and implementing core concepts, not developing a production-ready application.
## Features

- **Transaction Management**
  - add
  - view
  - edit
  - delete
- Search By:
  - title
  - category
- **Filter Transaction by:**
  - type (Income - Expense)
  - category
  - this month transactions
  - transactions between two dates
  - transactions more than user input amount
- **Financial Reports:**
  - total income
  - total expenses
  - balance
  - total expenses by category
- **Monthly Report**
## Architecture

The project follows a layered structure:

- ConsoleUi: Handles user interaction
- TransactionService: Contains business logic
- ReportService: Generates financial reports
- IStorage: Defines storage contract
- JsonStorage: Handles data persistence
## What I Learned

- Financial Data Modeling
- Working with "decimal" for Monetary Values
- Working with "DateTime"
- Enums ("TransactionType", "TransactionCategory")
- Advanced LINQ
  - "Where"
  - "Sum"
  - "GroupBy"
  - "OrderBy"
- Data Filtering
- Search Functionality
- Monthly Reports
- Financial Reports
- Data Aggregation
- Separation of Business Logic and Presentation
- Generic Interfaces ("IStorage<T>")
- Dependency Injection
- SOLID Principles
- Clean Code & Software Architecture
- JSON Data Persistence
- Validation
- Refactoring
## Technologies

- .NET 10
- Console Application
- Object-Oriented Programing
- File I/O
