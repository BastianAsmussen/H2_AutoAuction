-- Create the Vehicle database if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Auction')
BEGIN
    CREATE DATABASE Auction
END
GO

USE Auction
GO

-- Create the LicenseType table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LicenseType')
BEGIN
    CREATE TABLE LicenseType
    (
        LicenseTypeID TINYINT IDENTITY(1,1) PRIMARY KEY,
    )
END
GO

-- Create the FuelType table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FuelType')
BEGIN
    CREATE TABLE FuelType
    (
        FuelTypeID TINYINT IDENTITY(1,1) PRIMARY KEY,
    )
END
GO

-- Create the EnergyClass table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EnergyClass')
BEGIN
    CREATE TABLE EnergyClass
    (
        EnergyClassID TINYINT IDENTITY(1,1) PRIMARY KEY,
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
        
        LicenseTypeID TINYINT FOREIGN KEY REFERENCES LicenseType(LicenseTypeID),
        FuelTypeID TINYINT FOREIGN KEY REFERENCES FuelType(FuelTypeID),
        EnergyClassID TINYINT FOREIGN KEY REFERENCES EnergyClass(EnergyClassID),
    )
END
GO
