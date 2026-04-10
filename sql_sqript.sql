IF DB_ID('dbs') IS NOT NULL
    DROP DATABASE dbs;
GO

CREATE DATABASE dbs;
GO

USE dbs;
GO

CREATE TABLE Users (
    UserId INT IDENTITY PRIMARY KEY,
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL
);
GO

INSERT INTO Users (Username, Password)
VALUES ('admin', '1234');
GO

CREATE TABLE Accounts (
    AccountNumber INT PRIMARY KEY,
    FirstName VARCHAR(50),
    Surname VARCHAR(50),
    Email VARCHAR(100),
    Phone VARCHAR(20),
    AddressLine1 VARCHAR(100),
    AddressLine2 VARCHAR(100),
    City VARCHAR(50),
    County VARCHAR(50),
    AccountType VARCHAR(20),
    SortCode INT,
    Balance DECIMAL(10,2),
    OverdraftLimit DECIMAL(10,2),
    IsActive BIT DEFAULT 1
);
GO

CREATE TABLE Transactions (
    TransactionId INT IDENTITY PRIMARY KEY,
    AccountNumber INT,
    TransactionType VARCHAR(20),
    Amount DECIMAL(10,2),
    Date DATETIME DEFAULT GETDATE(),
    ReferenceNumber VARCHAR(50),
    SourceAccountNumber INT,
    DestinationAccountNumber INT NULL
);
GO

CREATE PROCEDURE sp_Login
    @Username VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    SELECT * FROM Users
    WHERE Username = @Username AND Password = @Password;
END
GO

CREATE PROCEDURE sp_AddAccount
    @AccountNumber INT,
    @FirstName VARCHAR(50),
    @Surname VARCHAR(50),
    @Email VARCHAR(100),
    @Phone VARCHAR(20),
    @AddressLine1 VARCHAR(100),
    @AddressLine2 VARCHAR(100),
    @City VARCHAR(50),
    @County VARCHAR(50),
    @AccountType VARCHAR(20),
    @SortCode INT,
    @Balance DECIMAL(10,2),
    @OverdraftLimit DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Accounts
    (
        AccountNumber,
        FirstName,
        Surname,
        Email,
        Phone,
        AddressLine1,
        AddressLine2,
        City,
        County,
        AccountType,
        SortCode,
        Balance,
        OverdraftLimit,
        IsActive
    )
    VALUES
    (
        @AccountNumber,
        @FirstName,
        @Surname,
        @Email,
        @Phone,
        @AddressLine1,
        @AddressLine2,
        @City,
        @County,
        @AccountType,
        @SortCode,
        @Balance,
        @OverdraftLimit,
        1
    );
END
GO

CREATE PROCEDURE sp_GetAccounts
AS
BEGIN
    SELECT * FROM Accounts WHERE IsActive = 1;
END
GO

CREATE PROCEDURE sp_DeactivateAccount
    @AccountNumber INT
AS
BEGIN
    UPDATE Accounts
    SET IsActive = 0
    WHERE AccountNumber = @AccountNumber;
END
GO

CREATE PROCEDURE sp_AddTransaction
    @AccountNumber INT,
    @TransactionType VARCHAR(20),
    @Amount DECIMAL(10,2),
    @ReferenceNumber VARCHAR(50),
    @SourceAccountNumber INT,
    @DestinationAccountNumber INT = NULL
AS
BEGIN
    INSERT INTO Transactions
    (
        AccountNumber,
        TransactionType,
        Amount,
        ReferenceNumber,
        SourceAccountNumber,
        DestinationAccountNumber
    )
    VALUES
    (
        @AccountNumber,
        @TransactionType,
        @Amount,
        @ReferenceNumber,
        @SourceAccountNumber,
        @DestinationAccountNumber
    );
END
GO

CREATE PROCEDURE sp_GetTransactionsByAccount
    @AccountNumber INT
AS
BEGIN
    SELECT * FROM Transactions
    WHERE AccountNumber = @AccountNumber
    ORDER BY Date DESC;
END
GO


