USE Auction
GO

-- Create the PrivatePersonalCar table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PrivatePersonalCar')
    BEGIN
        CREATE TABLE PrivatePersonalCar
        (
            Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

            HasIsofixFittings BIT NOT NULL,

            PersonalCarId INT NOT NULL FOREIGN KEY REFERENCES PersonalCar(Id),
        )
    END
GO