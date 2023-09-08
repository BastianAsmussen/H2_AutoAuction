USE Auction
GO

-- Create the FuelType table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FuelType')
BEGIN
    CREATE TABLE FuelType
    (
        Id TINYINT IDENTITY(1,1) PRIMARY KEY,
        Type varchar(2) NOT NULL,
    )
END
GO

-- Create the EnergyClass table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EnergyClass')
BEGIN
    CREATE TABLE EnergyClass
    (
        Id TINYINT IDENTITY(1,1) PRIMARY KEY,
        Type varchar(2) NOT NULL,
    )
END
GO

-- Create the Vehicle table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Vehicle')
BEGIN
    CREATE TABLE Vehicle
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(128) NOT NULL,
        Km FLOAT NOT NULL,
        RegistrationNumber VARCHAR(7) NOT NULL,
        Year SMALLINT NOT NULL,
        NewPrice DECIMAL NOT NULL ,
        HasTowbar BIT NOT NULL,
        EngineSize FLOAT NOT NULL,
        KmPerLiter FLOAT NOT NULL,
        
        LicenseTypeId TINYINT FOREIGN KEY REFERENCES LicenseType(id),
        FuelTypeId TINYINT FOREIGN KEY REFERENCES FuelType(id),
        EnergyClassId TINYINT FOREIGN KEY REFERENCES EnergyClass(id),
    )
END
GO
