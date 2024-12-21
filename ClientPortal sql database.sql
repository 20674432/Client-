Use Master 
If Exists (Select * from sys.databases where name = 'ClientPortal') 
DROP DATABASE ClientPortal  
Go 

-- Create the database
CREATE DATABASE ClientPortal;
GO

-- Use the created database
USE ClientPortal;
GO

-- Create Clients table
CREATE TABLE Clients (
    ClientId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Create Products table
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(18,2) NOT NULL,
    StockQuantity INT NOT NULL,
    Image NVARCHAR(8000),
    CreatedAt DATETIME DEFAULT GETDATE()
); 

-- Create OrderStatuses table
CREATE TABLE OrderStatuses (
    StatusId INT PRIMARY KEY IDENTITY(1,1),
    StatusName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255)
);

-- Create Orders table
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    ClientId INT FOREIGN KEY REFERENCES Clients(ClientId),
    ProductId INT FOREIGN KEY REFERENCES Products(ProductId),
    Quantity INT NOT NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    StatusId INT FOREIGN KEY REFERENCES OrderStatuses(StatusId),
    OrderDate DATETIME DEFAULT GETDATE(),
    LastUpdated DATETIME DEFAULT GETDATE()
);



INSERT INTO Clients (Username, PasswordHash, Email, FullName)
VALUES 
('johndoe', 'test123', 'johndoe@example.com', 'John Doe'),
('janedoe', 'mypass', 'janedoe@example.com', 'Jane Doe');

INSERT INTO Products (ProductName, Description, Price, StockQuantity, Image)
VALUES 
('Laptop', 'High-performance laptop with 16GB RAM and 512GB SSD', 12000.99, 50, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT7pkJ_vBex_6z6WBb3GBMFPEWyXW9pgLQpPQ&s'),
('Wireless Mouse', 'Ergonomic wireless mouse with adjustable DPI', 450.99, 150, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcStbn7eFi0VxTqmuTjGfvY0pJ7txOMIF6Pf1w&s'),
('Bluetooth Headphones', 'Noise-canceling Bluetooth headphones with 20-hour battery life', 960.99, 75, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSHvANRg0UybldRXucgQAU1y3KUpC9bc--g9Q&s'),
('USB flesh drive','USB drives are commonly used for storage, data backup, and transferring files between devices.',199.99,100,'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSrxnu1_rjCQ4BUOEHuD_zoP0s71Nl_Yj2X5g&s'),
('Smartphone','Latest model smartphone with 128GB storage and 5G capability',8999.99,100,'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTmuYFAVSzCyyEbVp-wTdYXfSfDDbt_5Hlkaw&s'),
('4K Monitor','27-inch 4K UHD monitor with high color accuracy',13059.99,30,'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRaSXSwmfaB0Zi-RvDZHhutkYuGOIG_XodjaQ&s'),
('Mechanical Keyboard','Gaming mechanical keyboard with RGB backlighting',4599.99,80,'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSYaxz8K-cl1PmoCrHrPH1aoek3K_5UHJEuDA&s'),
('External Hard Drive','2TB external hard drive with fast USB 3.0 connectivity',2099.00,60,'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS34FUeHSTO31HWVPigEG2BTJxcMCYcEGlcQA&s'),
('Smartwatch','Fitness tracking smartwatch with heart rate monitor',499.99,120,'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS8Lx7JoDF_hpytamvjL6zBtDi50Wj4N5Rutw&s');


INSERT INTO OrderStatuses (StatusName, Description)
VALUES 
('Pending', 'Order has been placed but not yet processed'),
('Shipped', 'Order has been shipped'),
('Delivered', 'Order has been delivered'),
('Cancelled', 'Order has been cancelled');


INSERT INTO Orders (ClientId, ProductId, Quantity, TotalPrice, StatusId)
VALUES 
(1, 1, 2, 24000.00, 1),  -- John Doe orders 2 units of Product 1
(2, 2, 1, 450.99, 2);  -- Jane Doe orders 1 unit of Product 2
