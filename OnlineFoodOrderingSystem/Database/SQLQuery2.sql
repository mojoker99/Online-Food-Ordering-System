USE master;
GO

IF DB_ID('OnlineFoodOrderingDB') IS NOT NULL
BEGIN
    ALTER DATABASE OnlineFoodOrderingDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE OnlineFoodOrderingDB;
END
GO

CREATE DATABASE OnlineFoodOrderingDB;
GO

USE OnlineFoodOrderingDB;
GO

CREATE TABLE tbUsers (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL
);
GO

CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE tbMenuItems (
    MenuItemId INT IDENTITY(1,1) PRIMARY KEY,
    ItemName NVARCHAR(100) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    Price DECIMAL(10,2) NOT NULL,
    IsAvailable BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE tbOrders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(10,2) NOT NULL,
    PaymentMethod NVARCHAR(50) NOT NULL,
    OrderStatus NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    CONSTRAINT FK_tbOrders_tbUsers FOREIGN KEY (UserId) REFERENCES tbUsers(UserId)
);
GO

CREATE TABLE tbOrderDetails (
    OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    MenuItemId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_tbOrderDetails_tbOrders FOREIGN KEY (OrderId) REFERENCES tbOrders(OrderId),
    CONSTRAINT FK_tbOrderDetails_tbMenuItems FOREIGN KEY (MenuItemId) REFERENCES tbMenuItems(MenuItemId)
);
GO

CREATE TABLE tbCart (
    CartId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    MenuItemId INT NOT NULL,
    Quantity INT NOT NULL,
    CONSTRAINT FK_tbCart_tbUsers FOREIGN KEY (UserId) REFERENCES tbUsers(UserId),
    CONSTRAINT FK_tbCart_tbMenuItems FOREIGN KEY (MenuItemId) REFERENCES tbMenuItems(MenuItemId)
);
GO

INSERT INTO tbUsers (FullName, Email, PasswordHash, Role)
VALUES
('System Admin', 'admin@food.com', 'admin123', 'Admin'),
('Test Customer', 'customer@food.com', 'customer123', 'Customer');
GO

INSERT INTO Categories (CategoryName)
VALUES
('Pizza'),
('Burger'),
('Drinks'),
('Dessert');
GO

INSERT INTO tbMenuItems (ItemName, Category, Description, Price, IsAvailable)
VALUES
('Margherita Pizza', 'Pizza', 'Classic pizza with cheese and tomato sauce', 120.00, 1),
('Chicken Burger', 'Burger', 'Grilled chicken burger with fries', 95.00, 1),
('Cola', 'Drinks', 'Cold soft drink', 25.00, 1),
('Chocolate Cake', 'Dessert', 'Fresh chocolate cake slice', 60.00, 1);
GO