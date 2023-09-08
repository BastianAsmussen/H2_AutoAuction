USE Auction
GO

-- Create the Car table if it doesn't exist
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

-- Create the ProfessionalPersonalCar table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProfessionalPersonalCar')
BEGIN
    CREATE TABLE ProfessionalPersonalCar
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        HasSafetyBar BIT NOT NULL,
        LoadCapacity FLOAT NOT NULL,
        CarId INT NOT NULL FOREIGN KEY REFERENCES Car(Id),
    )
END
GO 



