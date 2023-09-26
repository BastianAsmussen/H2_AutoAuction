-- Use the AutoAuction database.
USE AutoAuction
GO

-- Create the Users table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
    BEGIN
       CREATE TABLE Users
       (
           Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

           Username NVARCHAR(64) NOT NULL UNIQUE,
           Password NCHAR(60) NOT NULL,
           ZipCode INT NOT NULL,
           Balance DECIMAL NOT NULL,
       )
    END
GO

-- Create the PrivateUser table if it does not exist
IF NOT EXISTS (SELECT * FROM sys.database WHERE name = 'PrivateUser')
    BEGIN
        CREATE TABLE PrivateUser(
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            CPR CHAR(11) NOT NULL UNIQUE,

            UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
        )
    END
GO

-- Create the CorporateUser table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CorporateUser')
    BEGIN
        CREATE TABLE CorporateUser(
            Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),

            CVR INT NOT NULL,
            Credit DECIMAL NOT NULL,

            UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
        )
    END
GO
