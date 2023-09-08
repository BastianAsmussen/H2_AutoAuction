USE Auction
GO

-- Create the EnergyClass table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EnergyClass')
BEGIN
    CREATE TABLE EnergyClass
    (
        Id TINYINT NOT NULL IDENTITY(1,1) PRIMARY KEY,

        Type VARCHAR(2) NOT NULL,
    )
END
GO
