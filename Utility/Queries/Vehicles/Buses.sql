USE Vehicle
GO

-- Create the Buses table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Buses')
BEGIN
    CREATE TABLE Buses
    (
        Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        
        NumberOfSeats TINYINT NOT NULL,
        NumberOfSleepingSpaces TINYINT NOT NULL,
        HasToilet BIT NOT NULL,
        
        HeavyVehicleId INT NOT NULL FOREIGN KEY REFERENCES HeavyVehicles(Id)
    )
END
GO
