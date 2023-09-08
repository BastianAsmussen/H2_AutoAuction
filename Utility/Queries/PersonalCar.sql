USE Auction
GO

-- Create the PersonalCar table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PersonalCar')
    BEGIN
        CREATE TABLE PersonalCar
        (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            NumberOfSeats TINYINT NOT NULL,
            TrunkDimensions INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
            VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicle(Id),
        )
    END
GO

-- Create the PrivatePersonalCar table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PrivatePersonalCar')
BEGIN
    CREATE TABLE PrivatePersonalCar
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        HasIsofixFittings BIT NOT NULL,
        CarId INT NOT NULL FOREIGN KEY REFERENCES Car(Id),
    )
END
GO

