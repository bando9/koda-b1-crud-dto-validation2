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
