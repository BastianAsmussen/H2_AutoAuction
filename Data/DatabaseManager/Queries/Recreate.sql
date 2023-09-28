USE master
GO

-- Drop the AutoAuction database if it exists.
IF EXISTS (SELECT *
           FROM sys.databases
           WHERE name = 'AutoAuction')
    BEGIN
        ALTER DATABASE AutoAuction SET SINGLE_USER WITH ROLLBACK IMMEDIATE
        DROP DATABASE AutoAuction
    END
GO

-- Create the AutoAuction database if it does not exist.
IF NOT EXISTS (SELECT *
               FROM sys.databases
               WHERE name = 'AutoAuction')
    BEGIN
        CREATE DATABASE AutoAuction
    END
GO

-- Use the AutoAuction database.
USE AutoAuction
GO

-- Create the Vehicles table if it does not exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'Vehicles')
    BEGIN
        CREATE TABLE Vehicles
        (
            Id                 INT           NOT NULL IDENTITY (1,1) PRIMARY KEY,

            Name               NVARCHAR(128) NOT NULL,
            Km                 FLOAT         NOT NULL,
            RegistrationNumber NVARCHAR(7)   NOT NULL,
            Year               SMALLINT      NOT NULL,
            NewPrice           DECIMAL       NOT NULL,
            HasTowbar          BIT           NOT NULL,
            EngineSize         FLOAT         NOT NULL,
            KmPerLiter         FLOAT         NOT NULL,

            LicenseTypeId      TINYINT       NOT NULL,
            FuelTypeId         TINYINT       NOT NULL,
            EnergyTypeId       TINYINT       NOT NULL,
        )
    END
GO

-- Create the trunkDimensions table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'Dimensions')
    BEGIN
        CREATE TABLE Dimensions
        (
            Id     INT   NOT NULL IDENTITY (1,1) PRIMARY KEY,

            Length FLOAT NOT NULL,
            Width  FLOAT NOT NULL,
            Height FLOAT NOT NULL,
        )
    END
GO

-- Create the PersonalCars table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'PersonalCars')
    BEGIN
        CREATE TABLE PersonalCars
        (
            Id                INT     NOT NULL IDENTITY (1,1) PRIMARY KEY,

            NumberOfSeats     TINYINT NOT NULL,

            TrunkDimensionsId INT     NOT NULL FOREIGN KEY REFERENCES Dimensions (Id) ON DELETE CASCADE,
            VehicleId         INT     NOT NULL FOREIGN KEY REFERENCES Vehicles (Id) ON DELETE CASCADE,
        )
    END
GO

-- Create the ProfessionalPersonalCars table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'ProfessionalPersonalCars')
    BEGIN
        CREATE TABLE ProfessionalPersonalCars
        (
            Id            INT   NOT NULL IDENTITY (1,1) PRIMARY KEY,

            HasSafetyBar  BIT   NOT NULL,
            LoadCapacity  FLOAT NOT NULL,

            PersonalCarId INT   NOT NULL FOREIGN KEY REFERENCES PersonalCars (Id) ON DELETE CASCADE,
        )
    END
GO

-- Create the PrivatePersonalCars table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'PrivatePersonalCars')
    BEGIN
        CREATE TABLE PrivatePersonalCars
        (
            Id                INT NOT NULL IDENTITY (1,1) PRIMARY KEY,

            HasIsofixFittings BIT NOT NULL,

            PersonalCarId     INT NOT NULL FOREIGN KEY REFERENCES PersonalCars (Id) ON DELETE CASCADE,
        )
    END
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

-- Create the Trucks table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'Trucks')
    BEGIN
        CREATE TABLE Trucks
        (
            Id             INT   NOT NULL IDENTITY (1,1) PRIMARY KEY,

            LoadCapacity   FLOAT NOT NULL,

            HeavyVehicleId INT   NOT NULL FOREIGN KEY REFERENCES HeavyVehicles (Id) ON DELETE CASCADE,
        )
    END
GO

-- Create the Buses table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'Buses')
    BEGIN
        CREATE TABLE Buses
        (
            Id                     INT     NOT NULL IDENTITY (1,1) PRIMARY KEY,

            NumberOfSeats          TINYINT NOT NULL,
            NumberOfSleepingSpaces TINYINT NOT NULL,
            HasToilet              BIT     NOT NULL,

            HeavyVehicleId         INT     NOT NULL FOREIGN KEY REFERENCES HeavyVehicles (Id) ON DELETE CASCADE,
        )
    END
GO

-- Create the Users table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'Users')
    BEGIN
        CREATE TABLE Users
        (
            Id       INT          NOT NULL IDENTITY (1,1) PRIMARY KEY,

            Username NVARCHAR(64) NOT NULL UNIQUE,
            Password NCHAR(60)    NOT NULL,
            Zipcode  NVARCHAR(5)  NOT NULL,
            Balance  DECIMAL      NOT NULL,
        )
    END
GO

-- Create the PrivateUsers table if it does not exist
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'PrivateUsers')
    BEGIN
        CREATE TABLE PrivateUsers
        (
            Id     INT       NOT NULL IDENTITY (1,1) PRIMARY KEY,

            CPR    NCHAR(11) NOT NULL UNIQUE,

            UserId INT       NOT NULL FOREIGN KEY REFERENCES Users (Id) ON DELETE CASCADE,
        )
    END
GO

-- Create the CorporateUsers table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'CorporateUsers')
    BEGIN
        CREATE TABLE CorporateUsers
        (
            Id     INT       NOT NULL PRIMARY KEY IDENTITY (1,1),

            CVR    NCHAR(11) NOT NULL,
            Credit DECIMAL   NOT NULL,

            UserId INT       NOT NULL FOREIGN KEY REFERENCES Users (Id) ON DELETE CASCADE,
        )
    END
GO

-- Create the Auctions table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'Auctions')
    BEGIN
        CREATE TABLE Auctions
        (
            Id           INT      NOT NULL IDENTITY (1,1) PRIMARY KEY,

            MinimumPrice DECIMAL  NOT NULL,
            StartingBid  DECIMAL  NOT NULL,

            StartDate    DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
            EndDate      DATETIME NOT NULL,

            VehicleId    INT      NOT NULL FOREIGN KEY REFERENCES Vehicles (Id) ON DELETE CASCADE,
            SellerId     INT      NOT NULL FOREIGN KEY REFERENCES Users (Id) ON DELETE CASCADE,
            BuyerId      INT FOREIGN KEY REFERENCES Users (Id) ON DELETE NO ACTION, -- Null if there's no buyer yet.
        )
    END
GO

-- Create the Bids table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'Bids')
    BEGIN
        CREATE TABLE Bids
        (
            Id        INT      NOT NULL IDENTITY (1,1) PRIMARY KEY,

            Date      DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
            Amount    DECIMAL  NOT NULL,

            BidderId  INT      NOT NULL FOREIGN KEY REFERENCES Users (Id) ON DELETE CASCADE,
            AuctionId INT      NOT NULL FOREIGN KEY REFERENCES Auctions (Id) ON DELETE NO ACTION,
        )
    END
GO
