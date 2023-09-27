USE AutoAuction
GO

-- Create the CorporateUsers table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CorporateUsers')
    BEGIN
        CREATE TABLE CorporateUsers(
            Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),

            CVR NCHAR(11) NOT NULL,
            Credit DECIMAL NOT NULL,

            UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
        )
    END
GO
