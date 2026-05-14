# Online Food Ordering System 🍔🍕

A desktop-based **Online Food Ordering System** developed using **C# Windows Forms (.NET Framework)** and **SQL Server (SSMS)** in **Visual Studio Community**.

This project allows users to browse food items, place orders, manage carts, and provides an admin panel for managing menus and customer orders.

---

# 📌 Features

## 👤 User Features

* User Registration & Login
* Browse Food Menu
* Add Items to Cart
* Place Orders
* View Order History
* Simple & User-Friendly Interface

---

## 🔑 Admin Features

* Admin Login
* Manage Food Items
* Manage Categories
* View Customer Orders
* Update Order Status
* Admin Dashboard

---

# 🛠 Technologies Used

| Technology              | Description             |
| ----------------------- | ----------------------- |
| C#                      | Programming Language    |
| Windows Forms           | Desktop GUI             |
| .NET Framework 4.8      | Application Framework   |
| SQL Server (SSMS)       | Database                |
| ADO.NET                 | Database Connectivity   |
| Visual Studio Community | Development Environment |

---

# 📂 Project Structure

```text id="3k1m9q"
OnlineFoodOrderingSystem
│
├── Common
│   ├── AppSession.cs
│   ├── PasswordHasher.cs
│   └── Theme.cs
│
├── DataAccess
│   ├── Database.cs
│   └── Repository.cs
│
├── Forms
│   ├── LoginForm.cs
│   ├── RegisterForm.cs
│   ├── MainForm.cs
│   ├── MenuForm.cs
│   ├── CartForm.cs
│   ├── MyOrdersForm.cs
│   ├── AdminMenuForm.cs
│   ├── AdminOrdersForm.cs
│   └── AdminMainForm.cs
│
├── App.config
├── Program.cs
└── OnlineFoodOrderingSystem.csproj
```

---

# 🗄 Database Tables

The project uses the following tables:

* Users
* Categories
* FoodItems
* Orders
* OrderDetails
* Cart

---

# ⚙️ Database Setup

## 1️⃣ Open SQL Server Management Studio (SSMS)

Connect to:

```text id="q9x4zp"
(localdb)\MSSQLLocalDB
```

---

## 2️⃣ Create Database

```sql id="6p2m8d"
CREATE DATABASE OnlineFoodOrderingDB;
```

---

## 3️⃣ Use Database

```sql id="w1n7qx"
USE OnlineFoodOrderingDB;
```

---

## 4️⃣ Create Tables

Run the SQL script provided in the project.

---

## 5️⃣ Insert Admin Account

```sql id="u5m8te"
INSERT INTO Users
(FullName, Email, Password, Phone, Role)
VALUES
(
    'Admin',
    'admin@food.com',
    'admin123',
    '01000000000',
    'Admin'
);
```

---

# 🔗 Connection String

Inside:

```text id="f2m8qp"
App.config
```

```xml id="7q1mzw"
<connectionStrings>
  <add name="FoodOrderingDB"
       connectionString="Data Source=(localdb)\MSSQLLocalDB;
       Initial Catalog=OnlineFoodOrderingDB;
       Integrated Security=True;"
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

---

# ▶️ How to Run

1. Open the solution in Visual Studio
2. Restore NuGet packages if needed
3. Build Solution
4. Run the project using:

```text id="8m1qzt"
F5
```

---

# 🔐 Default Admin Login

| Email                                   | Password |
| --------------------------------------- | -------- |
| [admin@food.com](mailto:admin@food.com) | admin123 |

---

# 📸 Screenshots

*Add screenshots of the application here.*

---

# 🚀 Future Improvements

* Online Payment Integration
* Food Images Upload
* Delivery Tracking
* Email Notifications
* Responsive UI Design
* Search & Filter System

---

# 👨‍💻 Author

Developed by **Mohamed Khaled**

---

# 📄 License

This project is for educational purposes only.
