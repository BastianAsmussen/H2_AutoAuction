USE Auction
GO

-- Create the Dimensions table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Dimensions')
BEGIN
    CREATE TABLE Dimensions
    (
        Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

        Length FLOAT NOT NULL,
        Width FLOAT NOT NULL,
        Height FLOAT NOT NULL,
    )
END
GO
