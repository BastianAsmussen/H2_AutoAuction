USE AutoAuction
GO

-- Create the Bids table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'Bids')
    BEGIN
        CREATE TABLE Bids
        (
            Id        INT      NOT NULL IDENTITY (1,1) PRIMARY KEY,

            Date      DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
            Amount    DECIMAL  NOT NULL,

            BidderId  INT      NOT NULL FOREIGN KEY REFERENCES Users (Id) ON DELETE CASCADE,
            AuctionId INT      NOT NULL FOREIGN KEY REFERENCES Auctions (Id) ON DELETE NO ACTION,
        )
    END
GO
