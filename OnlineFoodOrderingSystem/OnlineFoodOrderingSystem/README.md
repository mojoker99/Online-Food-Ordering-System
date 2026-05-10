# Online Food Ordering System - C# WinForms

This project is a desktop Online Food Ordering System built with:
- C#
- Windows Forms (.NET Framework 4.8)
- SQL Server / LocalDB
- ADO.NET
- Layered folder structure

## Main Features
- Customer registration and login
- Admin login
- Browse/search menu items
- Add items to cart
- Checkout with Cash on Delivery or Online Payment option
- Customer order tracking
- Admin menu CRUD operations
- Admin order status updates: Pending, Preparing, Out for Delivery, Delivered

## How to Run
1. Open SQL Server Management Studio.
2. Run `Database/FoodOrderingDB.sql`.
3. Open `OnlineFoodOrderingSystem.sln` in Visual Studio.
4. Build the solution.
5. Run the project.

## Default Admin Login
Email: admin@food.com
Password: admin123

## Connection String
The connection string is inside:
`OnlineFoodOrderingSystem/App.config`

Default:
`Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FoodOrderingDB;Integrated Security=True;`

If you use SQL Server Express, change it to something like:
`Data Source=.\SQLEXPRESS;Initial Catalog=FoodOrderingDB;Integrated Security=True;`

## Architecture
- Forms: Presentation Layer
- DataAccess: SQL connection and repositories
- Common: session, theme, password hashing
- Database: SQL script
