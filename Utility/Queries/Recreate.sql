USE master
GO

-- Drop the AutoAuction database if it exists.
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'AutoAuction')
    BEGIN
        ALTER DATABASE AutoAuction SET SINGLE_USER WITH ROLLBACK IMMEDIATE
        DROP DATABASE AutoAuction
    END
GO

-- Create the AutoAuction database if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'AutoAuction')
    BEGIN
        CREATE DATABASE AutoAuction
    END
GO

-- Use the AutoAuction database.
USE AutoAuction
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

            Type VARCHAR(8) NOT NULL,
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

            TrunkDimensionsId INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
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

            VehicleDimensionsId INT NOT NULL FOREIGN KEY REFERENCES Dimensions(Id),
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

            LoadCapacity FLOAT NOT NULL,

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
            NumberOfSleepingSpaces TINYINT NOT NULL,
            HasToilet BIT NOT NULL,

            HeavyVehicleId INT NOT NULL FOREIGN KEY REFERENCES HeavyVehicles(Id)
        )
    END
GO


-- Create the Users table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
    BEGIN
        CREATE TABLE Users
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            Username NVARCHAR(64) NOT NULL UNIQUE,
            Password NCHAR(60) NOT NULL,
            ZipCode INT NOT NULL,
            Balance DECIMAL NOT NULL,
        )
    END
GO

-- Create the PrivateUser table if it does not exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PrivateUser')
    BEGIN
        CREATE TABLE PrivateUser(
            Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,

            CPR CHAR(11) UNIQUE NOT NULL,

            UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
        )
    END
GO

-- Create the CorporateUser table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CorporateUser')
    BEGIN
        CREATE TABLE CorporateUser(
            Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,

            CVR INT NOT NULL,
            Credit DECIMAL NOT NULL,

            UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
        )
    END
GO

-- Create the Auctions table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Auctions')
    BEGIN
        CREATE TABLE Auctions
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            MinimumPrice DECIMAL NOT NULL,
            StartingBid DECIMAL NOT NULL,

            VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicles(Id),
            SellerId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
            BuyerId INT FOREIGN KEY REFERENCES Users(Id), -- Null if there's no buyer yet.
        )
    END
GO

-- Create the Bids table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bids')
    BEGIN
        CREATE TABLE Bids
        (
            Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

            Date DATETIME NOT NULL,
            Amount DECIMAL NOT NULL,

            BidderId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
            AuctionId INT NOT NULL FOREIGN KEY REFERENCES Auctions(Id),
        )
    END
GO
