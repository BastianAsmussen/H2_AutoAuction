USE 'User'
GO

-- Create the CorporateUser table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CorporateUser')
BEGIN
    CREATE TABLE CorporateUser(
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        
        CVR INT NOT NULL,
        Credit DECIMAL NOT NULL,

        UserId INT FOREIGN KEY NOT NULL REFERENCES Users(Id),
    )
END
