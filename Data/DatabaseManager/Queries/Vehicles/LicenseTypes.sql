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
