USE Auction
GO

-- Create the Truck table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Trucks')
   BEGIN
       CREATE TABLE Trucks
       (
           Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

           HeavyVehicleId INT NOT NULL FOREIGN KEY REFERENCES HeavyVehicles(Id),
       )
   END
GO
