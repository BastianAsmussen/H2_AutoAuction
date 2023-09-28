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
