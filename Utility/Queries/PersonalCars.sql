USE Auction
GO

-- Create the PersonalCars table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PersonalCars')
    BEGIN
        CREATE TABLE PersonalCars
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            NumberOfSeats TINYINT NOT NULL,

            TrunkDimensionsId INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
            VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicles(Id),
        )
    END
GO
