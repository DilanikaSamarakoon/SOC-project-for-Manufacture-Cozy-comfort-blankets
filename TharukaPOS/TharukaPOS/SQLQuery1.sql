CREATE TABLE Dealer (
    DealerId INT PRIMARY KEY, -- NOT IDENTITY
    DealerName NVARCHAR(100) NOT NULL,
    DealerAddress NVARCHAR(200),
    DealerPhone NVARCHAR(15)
);

CREATE TABLE Supplier (
    SupplierId INT PRIMARY KEY,
    SupplierName NVARCHAR(40),
    SupplierAddress NVARCHAR(100),
    SupplierPhone NVARCHAR(15)
);


CREATE TABLE Products (
    product_id INT IDENTITY(1000,1) PRIMARY KEY,
    product_name NVARCHAR(100) NOT NULL,
    weight FLOAT NOT NULL,
    quantity INT NOT NULL,
    buying_price DECIMAL(10,2) NOT NULL,
    selling_price DECIMAL(10,2) NOT NULL
);


CREATE TABLE BulkProduct (
    BulkProductId INT PRIMARY KEY, -- Manually assigned primary key (not auto-increment)
    BulkProductName NVARCHAR(100) NOT NULL,
    SupplierId INT NOT NULL,
    BulkQuantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,

    -- Foreign key constraint
    CONSTRAINT FK_BulkProducts_Suppliers FOREIGN KEY (SupplierId)
        REFERENCES Supplier(SupplierId)
);


CREATE TABLE Sales (
    SaleId INT PRIMARY KEY,
    DealerId INT NOT NULL,
    SaleDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    -- Add other fields as needed, e.g., Discount, GrandTotal
    CONSTRAINT FK_Sales_Dealer FOREIGN KEY (DealerId) REFERENCES Dealer(DealerId)
);


CREATE TABLE SaleDetails (
    SaleDetailId INT IDENTITY(1,1) PRIMARY KEY,
    SaleId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    SellingPrice DECIMAL(10,2) NOT NULL,
    BuyingPriceAtSale DECIMAL(10,2) NOT NULL,
    LineTotal AS ((Quantity * SellingPrice) - 0) PERSISTED, -- You can adjust if discount is involved

    CONSTRAINT FK_SaleDetails_Sales FOREIGN KEY (SaleId) REFERENCES Sales(SaleId)ON DELETE CASCADE,
    CONSTRAINT FK_SaleDetails_Products FOREIGN KEY (ProductId) REFERENCES Products(product_id)
);


-- Create the Roles table
CREATE TABLE UserRoles (
    RoleId INT PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL
);

-- Insert sample data
INSERT INTO UserRoles (RoleId, RoleName) VALUES
(1, 'Admin'),
(2, 'User');



CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),       -- Auto-incremented UserId
    Username NVARCHAR(50) NOT NULL UNIQUE,      -- Unique username
    Password NVARCHAR(255) NOT NULL,            -- Hashed password
    Salt NVARCHAR(255) NOT NULL,                -- Salt used in hashing
    RoleId INT NOT NULL,                        -- Foreign key to Roles table
    CONSTRAINT FK_Users_Role FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);
