USE AutoAuction
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
