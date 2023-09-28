USE AutoAuction
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
