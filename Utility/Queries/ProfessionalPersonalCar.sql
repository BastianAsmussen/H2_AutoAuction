USE Auction
GO

-- Create the ProfessionalPersonalCar table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProfessionalPersonalCar')
BEGIN
    CREATE TABLE ProfessionalPersonalCar
    (
        Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

        HasSafetyBar BIT NOT NULL,
        LoadCapacity FLOAT NOT NULL,

        PersonalCarId INT NOT NULL FOREIGN KEY REFERENCES PersonalCar(Id),
    )
END
GO
