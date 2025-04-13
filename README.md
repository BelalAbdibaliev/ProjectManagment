# 📁 ProjectManagment

## 📌 Description
- **Persistence** – db access, `DbContext`, configurations, migrations.
- **Presentation** – client interfaces, including frontend in `wwwroot`.
- **Application** – buisness logic and interfaces.
- **Domain** – entities.

## Running

Before running **you need to change connection string** to database in `appsettings.json`:

```json
"ConnectionStrings": {
  "MsSqlConnection": "Data Source=yourconnectionstring"
}
