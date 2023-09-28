USE AutoAuction
GO

-- Create the Users table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'Users')
    BEGIN
        CREATE TABLE Users
        (
            Id       INT          NOT NULL IDENTITY (1,1) PRIMARY KEY,

            Username NVARCHAR(64) NOT NULL UNIQUE,
            Password NCHAR(60)    NOT NULL,
            Zipcode  NVARCHAR(5)  NOT NULL,
            Balance  DECIMAL      NOT NULL,
        )
    END
GO

-- Create the PrivateUsers table if it does not exist
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'PrivateUsers')
    BEGIN
        CREATE TABLE PrivateUsers
        (
            Id     INT       NOT NULL IDENTITY (1,1) PRIMARY KEY,

            CPR    NCHAR(11) NOT NULL UNIQUE,

            UserId INT       NOT NULL FOREIGN KEY REFERENCES Users (Id) ON DELETE CASCADE,
        )
    END
GO

-- Create the CorporateUsers table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'CorporateUsers')
    BEGIN
        CREATE TABLE CorporateUsers
        (
            Id     INT       NOT NULL PRIMARY KEY IDENTITY (1,1),

            CVR    NCHAR(11) NOT NULL,
            Credit DECIMAL   NOT NULL,

            UserId INT       NOT NULL FOREIGN KEY REFERENCES Users (Id) ON DELETE CASCADE,
        )
    END
GO
