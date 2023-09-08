USE Auction
GO

-- Create the Bus table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bus')
BEGIN
    CREATE TABLE Bus
    (
        Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        
        NumberOfSeats TINYINT NOT NULL,
        NumberOfWheels TINYINT NOT NULL,
        HasToilet BIT NOT NULL,
        
        HeavyVehicleId INT NOT NULL FOREIGN KEY REFERENCES HeavyVehicle(Id)
    )
END
GO
