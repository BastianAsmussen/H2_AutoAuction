USE Vehicle
GO

-- Create the Trucks table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Trucks')
   BEGIN
       CREATE TABLE Trucks
       (
           Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

           LoadCapacity FLOAT NOT NULL,

           HeavyVehicleId INT NOT NULL FOREIGN KEY REFERENCES HeavyVehicles(Id),
       )
   END
GO
