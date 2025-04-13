# 📁 ProjectManagment

## 📌 Description
- **Persistence** – db access, `DbContext`, configurations, migrations.
- **Presentation** – client interfaces, including frontend in `wwwroot`.
- **Application** – buisness logic and interfaces.
- **Domain** – entities.

## Running

- Before running **you need to change connection string** to database in `appsettings.json`:

```json
"ConnectionStrings": {
  "MsSqlConnection": "Data Source=yourconnectionstring"
}
```

## Libraries

- For database manipulations used EF Core.
- Also used AutoMapper for mappind entities and DTOs.
- For testing used NUnit and Moq.
