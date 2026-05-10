CREATE DATABASE FoodOrderingDB;
GO
USE FoodOrderingDB;
GO

CREATE TABLE tbUsers(
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Phone VARCHAR(30),
    PasswordHash VARCHAR(64) NOT NULL,
    Address VARCHAR(250),
    Role VARCHAR(20) NOT NULL DEFAULT 'Customer'
);

CREATE TABLE tbMenuItems(
    MenuItemId INT IDENTITY(1,1) PRIMARY KEY,
    ItemName VARCHAR(100) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Description VARCHAR(250),
    Price DECIMAL(10,2) NOT NULL,
    IsAvailable BIT NOT NULL DEFAULT 1
);

CREATE TABLE tbOrders(
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(10,2) NOT NULL,
    PaymentMethod VARCHAR(50) NOT NULL,
    OrderStatus VARCHAR(30) NOT NULL DEFAULT 'Pending',
    CONSTRAINT FK_tbOrders_tbUsers FOREIGN KEY(UserId) REFERENCES tbUsers(UserId)
);

CREATE TABLE tbOrderDetails(
    OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    MenuItemId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_tbOrderDetails_tbOrders FOREIGN KEY(OrderId) REFERENCES tbOrders(OrderId),
    CONSTRAINT FK_tbOrderDetails_tbMenuItems FOREIGN KEY(MenuItemId) REFERENCES tbMenuItems(MenuItemId)
);

-- Password: admin123 hashed with SHA256
INSERT INTO tbUsers(FullName,Email,Phone,PasswordHash,Address,Role)
VALUES('System Admin','admin@food.com','01000000000','240be518fabd2724d937f9fa69002ba8f2dc192a5762435bf93f2a6cfab2d118','Restaurant Office','Admin');

INSERT INTO tbMenuItems(ItemName,Category,Description,Price,IsAvailable) VALUES
('Chicken Burger','Burgers','Grilled chicken burger with fries',120,1),
('Beef Burger','Burgers','Classic beef burger',150,1),
('Margherita Pizza','Pizza','Cheese and tomato pizza',180,1),
('Chicken Pizza','Pizza','Chicken pizza with vegetables',220,1),
('Cola','Drinks','Cold soft drink',25,1),
('French Fries','Sides','Crispy fries',45,1);
GO
