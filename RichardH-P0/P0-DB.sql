--Drop Tables
DROP TABLE Orders;
DROP TABLE Users;
DROP TABLE Inventories;
DROP TABLE Items;
DROP TABLE Locations;



--Create Items Table
CREATE TABLE Items
(
    ItemID INT IDENTITY (1,1) PRIMARY KEY,
    ItemName VARCHAR(100) NOT NULL,
    ItemDescription NVARCHAR(512) NOT NULL
);

--Create Locations Table
CREATE TABLE Locations
(
    LocationID INT IDENTITY (1,1) PRIMARY KEY,
    LocationName NVARCHAR(100) NOT NULL,
    SalePercentage DECIMAL NOT NULL,
);

--Create Users Table
CREATE TABLE Users
(
    UserID INT IDENTITY (1,1) PRIMARY KEY,
    UserName NVARCHAR (200) NOT NULL,
    UserPassword NVARCHAR (32),
    LocationID INT FOREIGN KEY REFERENCES Locations(LocationID) NOT NULL,
    Manager INT,
);

--Create Inventory Table
CREATE TABLE Inventories
(
    InventoryID INT IDENTITY (1,1) PRIMARY KEY,
    ItemID INT FOREIGN KEY REFERENCES Items(ItemID) NOT NULL,
    ItemPrice MONEY NOT NULL,
    LocationID INT FOREIGN KEY REFERENCES Locations(LocationID) NOt NULL,
);

--Cretae Orders Table
CREATE TABLE Orders
(
    OrderID INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    UserID INT FOREIGN KEY REFERENCES Users(UserID) NOT NULL,
    LocationID INT FOREIGN KEY REFERENCES Locations(LocationID) NOT NULL,
    ItemID INT FOREIGN KEY REFERENCES Items(ItemID) NOT NULL,
    CONSTRAINT PK_Orders PRIMARY KEY (UserID, LocationID, ItemID),
);


--Insert data Items
INSERT Items
    (ItemName, ItemDescription)
VALUES
    ('Small Monitor', 'A small monitor, supporting low resolution.'),
    ('Medium Monitor', 'A medium monitor, supporting mid range resolution.'),
    ('Large Monitor', 'A large monitor, with high resolution capabilities.'),
    ('baby-ATX', 'A baby-ATX motherboard, suitable for low performance systems'),
    ('mini-ATX', 'mini-ATX motherboard, capable of operatng an acerage workstation.'),
    ('ATX', 'A full ATX motherboard, designed for the peak of performance.');



--Insert data Locations
INSERT Locations
    (LocationName, SalePercentage)
VALUES
    ('New York', 1),
    ('Dalas', 1),
    ('Miami', 1),
    ('Paris', 1),
    ('London', 1);

--Insert data Users
INSERT Users
    (UserName, UserPassword, LocationID, Manager)
VALUES
    ('Hawkkiller', 'BeoHawk', 1, 1),
    ('User1', 'password', 2, 0),
    ('User2', 'password', 2, 0),
    ('Manager1', 'password', 3, 1);

--Insert data Inventories
INSERT Inventories
    (ItemID, ItemPrice, LocationID)
VALUES
    (1, 74.99, 1),
    (2, 99.99, 1),
    (3, 149.99, 1),
    (4, 99.99, 1),
    (5, 124.99, 1),
    (6, 174.99, 1),
    (1, 74.99, 2),
    (2, 99.99, 2),
    (3, 149.99, 2),
    (4, 99.99, 2),
    (5, 124.99, 2),
    (6, 174.99, 2),
    (1, 74.99, 3),
    (2, 99.99, 3),
    (3, 149.99, 3),
    (4, 99.99, 3),
    (5, 124.99, 3),
    (6, 174.99, 3),
    (1, 74.99, 4),
    (2, 99.99, 4),
    (3, 149.99, 4),
    (4, 99.99, 4),
    (5, 124.99, 4),
    (6, 174.99, 4),
    (1, 74.99, 5),
    (2, 99.99, 5),
    (3, 149.99, 5),
    (4, 99.99, 5),
    (5, 124.99, 5),
    (6, 174.99, 5);

--Insert data Orders
INSERT Orders
    (OrderID, OrderDate, UserID, LocationID, ItemID)
VALUES
    (1, '2022-01-19 12:35:29', 2, 1, 1),
    (1, '2022-01-19 12:35:29', 2, 1, 2),
    (1, '2022-01-19 12:35:29', 2, 1, 3),
    (1, '2022-01-19 12:35:29', 2, 1, 5),
    (2, '2022-01-19 12:35:29', 3, 4, 1),
    (2, '2022-01-19 12:35:29', 3, 4, 5),
    (2, '2022-01-19 12:35:29', 3, 4, 4),
    (2, '2022-01-19 12:35:29', 3, 4, 3),
    (2, '2022-01-19 12:35:29', 3, 4, 2)