USE Auction
GO

-- Create the PersonalCar table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PersonalCar')
    BEGIN
        CREATE TABLE PersonalCar
        (
            Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

            NumberOfSeats TINYINT NOT NULL,

            TrunkDimensions INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
            VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicle(Id),
        )
    END
GO
