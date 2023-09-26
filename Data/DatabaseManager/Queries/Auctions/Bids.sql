USE AutoAuction
GO

-- Create the Bids table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bids')
BEGIN
   CREATE TABLE Bids
   (
       Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

       Date DATETIME NOT NULL,
       Amount DECIMAL NOT NULL,

       BidderId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
       AuctionId INT NOT NULL FOREIGN KEY REFERENCES Auctions(Id),
   )
END
GO
