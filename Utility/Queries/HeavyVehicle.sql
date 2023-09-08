USE Auction
GO

-- Create the HeavyVehicle table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HeavyVehicle')
BEGIN
    CREATE TABLE HeavyVehicle
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        
        VehicleDimensions INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
        VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicle(Id),
    )
END
GO
