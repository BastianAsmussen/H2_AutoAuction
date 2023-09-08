USE Auction
GO

-- Create the LicenseType table if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LicenseType')
BEGIN
    CREATE TABLE LicenseType
    (
        Id TINYINT IDENTITY(1,1) NOT NULL PRIMARY KEY,

        Type varchar(2) NOT NULL,
    )
END
GO
