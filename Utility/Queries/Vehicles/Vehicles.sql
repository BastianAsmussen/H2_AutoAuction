USE Vehicle
GO

-- Create the Vehicle table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Vehicles')
BEGIN
    CREATE TABLE Vehicles
    (
        Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

        Name VARCHAR(128) NOT NULL,
        Km FLOAT NOT NULL,
        RegistrationNumber VARCHAR(7) NOT NULL,
        Year SMALLINT NOT NULL,
        NewPrice DECIMAL NOT NULL ,
        HasTowbar BIT NOT NULL,
        EngineSize FLOAT NOT NULL,
        KmPerLiter FLOAT NOT NULL,
        
        LicenseTypeId TINYINT NOT NULL FOREIGN KEY REFERENCES LicenseTypes(id),
        FuelTypeId TINYINT NOT NULL FOREIGN KEY REFERENCES FuelTypes(id),
        EnergyTypeId TINYINT NOT NULL FOREIGN KEY REFERENCES EnergyTypes(id),
    )
END
GO
