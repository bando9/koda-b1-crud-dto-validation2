-- CREATE DATABASE ecommerce;
-- SELECT name FROM sys.databases;

-- CREATE DATABASE ecommercedb;

-- CREATE TABLE dbo.Product(
--     ProductId INT identity(1,1) PRIMARY KEY,
--     ProductCode NVARCHAR(20) NOT NULL UNIQUE,
--     Name NVARCHAR(100) NOT NULL,
--     Price DECIMAL(18,2) NOT NULL,
--     Category NVARCHAR(50) NOT NULL,
--     Stock INT NOT NULL DEFAULT 0,
--     IsActive BIT NOT NULL DEFAULT 1,

--     CONSTRAINT CK_Products_Price CHECK(Price >= 0),
--     CONSTRAINT CK_Products_Stock CHECK(Stock >= 0)
-- )
-- GO;

--     ,


-- INSERT INTO dbo.Product(ProductCode, Name, Price, Stock, Category) VALUES 
-- ('PR-001', 'Minyak goreng', 21000, 10, 'Sembako');

SELECT * FROM dbo.Product;


create table dbo.Customer(
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL UNIQUE,
    Email NVARCHAR(200),
    Phone NVARCHAR(150) UNIQUE,
    Address NVARCHAR(500),
);

ALTER table dbo.Product add CustomerId int CONSTRAINT fk_customer FOREIGN KEY(CustomerId) REFERENCES dbo.Customer(CustomerId);

insert into dbo.Customer (Name, Email, Phone, Address) values ('Bando Mega', 'test@gmail.com', '0891213', 'Bogor');

SELECT * FROM dbo.Customer;

INSERT INTO dbo.Product(ProductCode, Name, Price, Stock, Category, CustomerId) VALUES 
('PR-002', 'Minyak Tanah', 15000, 10, 'Sembako',1);
INSERT INTO dbo.Product(ProductCode, Name, Price, Stock, Category, CustomerId) VALUES 
('PR-003', 'Minyak Bimoli 2L', 22000, 10, 'Sembako',1);

select *
from dbo.Product p join dbo.Customer c on p.CustomerId = c.CustomerId;