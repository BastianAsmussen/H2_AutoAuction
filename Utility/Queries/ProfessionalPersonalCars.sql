USE Auction
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
