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

-- Create the LicenseTypes table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LicenseTypes')
    BEGIN
        CREATE TABLE LicenseTypes
        (
            Id TINYINT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            Type VARCHAR(2) NOT NULL,
        )
    END
GO

-- Create the FuelTypes table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FuelTypes')
    BEGIN
        CREATE TABLE FuelTypes
        (
            Id TINYINT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            Type VARCHAR(2) NOT NULL,
        )
    END
GO


-- Create the EnergyTypes table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EnergyTypes')
    BEGIN
        CREATE TABLE EnergyTypes
        (
            Id TINYINT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            Type VARCHAR(2) NOT NULL,
        )
    END
GO

-- Create the Vehicles table if it does not exist.
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

-- Create the PersonalCars table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PersonalCars')
    BEGIN
        CREATE TABLE PersonalCars
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            NumberOfSeats TINYINT NOT NULL,

            TrunkDimensions INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
            VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicles(Id),
        )
    END
GO

-- Create the ProfessionalPersonalCars table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProfessionalPersonalCars')
    BEGIN
        CREATE TABLE ProfessionalPersonalCars
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            HasSafetyBar BIT NOT NULL,
            LoadCapacity FLOAT NOT NULL,

            PersonalCarId INT NOT NULL FOREIGN KEY REFERENCES PersonalCars(Id),
        )
    END
GO

-- Create the HeavyVehicles table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HeavyVehicles')
    BEGIN
        CREATE TABLE HeavyVehicles
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            VehicleDimensions INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
            VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicles(Id),
        )
    END
GO


-- Create the PrivatePersonalCars table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PrivatePersonalCars')
    BEGIN
        CREATE TABLE PrivatePersonalCars
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            HasIsofixFittings BIT NOT NULL,

            PersonalCarId INT NOT NULL FOREIGN KEY REFERENCES PersonalCars(Id),
        )
    END
GO

-- Create the HeavyVehicles table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HeavyVehicles')
    BEGIN
        CREATE TABLE HeavyVehicles
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            VehicleDimensions INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
            VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicles(Id),
        )
    END
GO

-- Create the Trucks table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Trucks')
    BEGIN
        CREATE TABLE Trucks
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            HeavyVehicleId INT NOT NULL FOREIGN KEY REFERENCES HeavyVehicles(Id),
        )
    END
GO

-- Create the Buses table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Buses')
    BEGIN
        CREATE TABLE Buses
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            NumberOfSeats TINYINT NOT NULL,
            NumberOfWheels TINYINT NOT NULL,
            HasToilet BIT NOT NULL,

            HeavyVehicleId INT NOT NULL FOREIGN KEY REFERENCES HeavyVehicles(Id)
        )
    END
GO
