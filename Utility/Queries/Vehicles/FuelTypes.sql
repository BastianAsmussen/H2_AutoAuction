USE Vehicle
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
