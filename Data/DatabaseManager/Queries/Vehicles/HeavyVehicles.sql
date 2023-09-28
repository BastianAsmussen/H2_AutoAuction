USE AutoAuction
GO

-- Create the HeavyVehicles table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'HeavyVehicles')
    BEGIN
        CREATE TABLE HeavyVehicles
        (
            Id                  INT NOT NULL IDENTITY (1,1) PRIMARY KEY,

            VehicleDimensionsId INT NOT NULL FOREIGN KEY REFERENCES Dimensions (Id) ON DELETE CASCADE,
            VehicleId           INT NOT NULL FOREIGN KEY REFERENCES Vehicles (Id) ON DELETE CASCADE,
        )
    END
GO
