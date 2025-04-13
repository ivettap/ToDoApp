# ToDoApp - Console To-Do List with EF Core

## Description

This console application is a simple To-Do list manager where users can view, create, complete, and delete tasks organized by categories. It is developed as a practical example of a basic CRUD application using C# and Entity Framework Core with a SQL Server database.
The project follows a clean object-oriented structure and uses a layered architecture (UI, Logic, Data, Models, and Migrations) to separate concerns.

## Features

- **Task Management**: Add, list, mark as done, undo completion, and delete tasks.
- **Categories**: Tasks can be assigned to predefined categories (e.g., Home, Work, Personal).
- **Database Integration**: All tasks are stored in a SQL Server database via Entity Framework Core.
- **Seeded Data**: Initial category and task data is automatically seeded.

##  How to Set Up the Project

1. Clone the repository to your local machine.
 ```bash
   git clone https://github.com/your-username/ToDoApp.git
  ``` 

2. Open the project in your preferred IDE (e.g., Visual Studio).

3. Inside the project folder, locate the `appsettings.example.json` file.

4. Rename `appsettings.example.json` to `appsettings.json`.

5. Open the `appsettings.json` file and set your **SQL Server** connection string. For example:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ToDoAppDb;Trusted_Connection=True;"
  }
}
```
## Restore NuGet Packages

1. Open the project in Visual Studio.

2. Right-click on the solution in Solution Explorer and click Restore NuGet Packages.

3. Alternatively, you can run this in the Package Manager Console:
```bash
dotnet restore
```

## Apply migrations and Update the Database

If the migrations haven’t been applied yet, you can update the database schema:

1. Open the **Package Manager Console** in Visual Studio and run:
```bash
Update-Database
```

2. Alternatively, use the .NET CLI:
```bash
dotnet ef database update
```



