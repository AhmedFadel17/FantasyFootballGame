# FantasyFootballGame API

FantasyFootballGame is a fantasy football backend system built with **ASP.NET Core 9 Web API**. It enables users to manage fantasy teams, perform player transfers, track performance by gameweek, and simulate the fantasy league system. This project uses **Entity Framework Core** with data seeding from files to populate real football data.

---

## ✨ Features

- 🔐 **User Authentication** (via JWT)
- ⚽ **Fantasy Team Creation** & Management
- 🔁 **Player Transfers** with validation
- 📅 **Gameweek System** with dynamic player scoring
- 🧠 **Rules Engine** via FluentValidation
- 📂 **Seeder** for importing initial data from files
- 📊 **Gameweek Points Tracking**

---

## 🛠️ Tech Stack

- **ASP.NET Core 9**
- **Entity Framework Core**
- **SQL Server**
- **FluentValidation**
- **AutoMapper**
- **JWT Authentication**

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- (Optional) [Postman](https://www.postman.com/) for testing endpoints

---

### 📥 Clone the Repository

```bash
git clone https://github.com/AhmedFadel17/FantasyFootballGame.git
cd FantasyFootballGame
```
---
## Setup Identity Server
### 1- Update the connection string in appsettings.json:
```c#
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SQL_SERVER;Database=FantasyFootballISDB;Trusted_Connection=True;"
}
```
---

### 2- Apply Migrations & Seed the Database
```bash
dotnet ef database update
```
---

### 3- Run the API
```bash
dotnet run
```
The Identity server will now be running at:
```bash
https://localhost:7245
```
---

## Setup API
### 1- Update the connection string in appsettings.json:
```c#
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SQL_SERVER;Database=FantasyFootballDB;Trusted_Connection=True;"
}
```
---

### 2- Apply Migrations & Seed the Database
```bash
dotnet ef database update
```
---

### 3- Run the API
```bash
dotnet run
```
The API will now be running at:
```bash
https://localhost:7057
```
---

