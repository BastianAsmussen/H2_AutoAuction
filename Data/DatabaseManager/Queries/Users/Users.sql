USE AutoAuction
GO

-- Create the Users table if it doesn't exist.
IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'Users')
    BEGIN
        CREATE TABLE Users
        (
            Id       INT          NOT NULL IDENTITY (1,1) PRIMARY KEY,

            Username NVARCHAR(64) NOT NULL UNIQUE,
            Password NCHAR(60)    NOT NULL,
            Zipcode  NVARCHAR(5)  NOT NULL,
            Balance  DECIMAL      NOT NULL,
        )
    END
GO
