USE Auction
GO

-- Create the FuelType table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FuelType')
BEGIN
    CREATE TABLE FuelType
    (
        Id TINYINT IDENTITY(1,1) NOT NULL PRIMARY KEY,

        Type varchar(2) NOT NULL,
    )
END
GO
