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
