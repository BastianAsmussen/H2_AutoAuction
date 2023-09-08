USE Auction
GO

-- Create the FuelType table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FuelType')
BEGIN
    CREATE TABLE FuelType
    (
        Id TINYINT NOT NULL IDENTITY(1,1) PRIMARY KEY,

        Type VARCHAR(2) NOT NULL,
    )
END
GO
