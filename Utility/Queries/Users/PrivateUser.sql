USE AutoAuction
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
