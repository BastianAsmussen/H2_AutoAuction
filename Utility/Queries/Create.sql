USE master
GO

-- Drop the Auction database if it exists.
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Auction')
    BEGIN
        ALTER DATABASE Auction SET SINGLE_USER WITH ROLLBACK IMMEDIATE
        DROP DATABASE Auction
    END
GO

-- Create the Auction database if it does not exist.
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
            Id TINYINT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            Type VARCHAR(2) NOT NULL,
        )
    END
GO

-- Create the FuelType table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FuelType')
    BEGIN
        CREATE TABLE FuelType
        (
            Id TINYINT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            Type VARCHAR(2) NOT NULL,
        )
    END
GO


-- Create the EnergyClass table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EnergyClass')
    BEGIN
        CREATE TABLE EnergyClass
        (
            Id TINYINT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            Type VARCHAR(2) NOT NULL,
        )
    END
GO

-- Create the Vehicle table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Vehicle')
    BEGIN
        CREATE TABLE Vehicle
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

            LicenseTypeId TINYINT NOT NULL FOREIGN KEY REFERENCES LicenseType(id),
            FuelTypeId TINYINT NOT NULL FOREIGN KEY REFERENCES FuelType(id),
            EnergyClassId TINYINT NOT NULL FOREIGN KEY REFERENCES EnergyClass(id),
        )
    END
GO

-- Create the trunkDimensions table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Dimensions')
    BEGIN
        CREATE TABLE Dimensions
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            Length FLOAT NOT NULL,
            Width FLOAT NOT NULL,
            Height FLOAT NOT NULL,
        )
    END
GO

-- Create the PersonalCar table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PersonalCar')
    BEGIN
        CREATE TABLE PersonalCar
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            NumberOfSeats TINYINT NOT NULL,

            TrunkDimensions INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
            VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicle(Id),
        )
    END
GO

-- Create the ProfessionalPersonalCar table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProfessionalPersonalCar')
    BEGIN
        CREATE TABLE ProfessionalPersonalCar
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            HasSafetyBar BIT NOT NULL,
            LoadCapacity FLOAT NOT NULL,

            PersonalCarId INT NOT NULL FOREIGN KEY REFERENCES PersonalCar(Id),
        )
    END
GO

-- Create the HeavyVehicle table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HeavyVehicle')
    BEGIN
        CREATE TABLE HeavyVehicle
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            VehicleDimensions INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
            VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicle(Id),
        )
    END
GO


-- Create the PrivatePersonalCar table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PrivatePersonalCar')
    BEGIN
        CREATE TABLE PrivatePersonalCar
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            HasIsofixFittings BIT NOT NULL,

            PersonalCarId INT NOT NULL FOREIGN KEY REFERENCES PersonalCar(Id),
        )
    END
GO

-- Create the HeavyVehicle table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HeavyVehicle')
    BEGIN
        CREATE TABLE HeavyVehicle
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            VehicleDimensions INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
            VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicle(Id),
        )
    END
GO

-- Create the Truck table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Truck')
    BEGIN
        CREATE TABLE Truck
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            HeavyVehicleId INT NOT NULL FOREIGN KEY REFERENCES HeavyVehicle(Id),
        )
    END
GO

-- Create the Bus table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bus')
    BEGIN
        CREATE TABLE Bus
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            NumberOfSeats TINYINT NOT NULL,
            NumberOfWheels TINYINT NOT NULL,
            HasToilet BIT NOT NULL,

            HeavyVehicleId INT NOT NULL FOREIGN KEY REFERENCES HeavyVehicle(Id)
        )
    END
GO
