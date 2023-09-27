USE AutoAuction
GO

-- Create the Auctions table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Auctions')
BEGIN
   CREATE TABLE Auctions
   (
       Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

       CurrentPrice DECIMAL NOT NULL,
       StartingBid DECIMAL NOT NULL,

       StartDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
       EndDate DATETIME NOT NULL,

       VehicleId INT NOT NULL FOREIGN KEY REFERENCES Vehicles(Id),
       SellerId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
       BuyerId INT FOREIGN KEY REFERENCES Users(Id), -- Null if there's no buyer yet.
   )
END
GO
