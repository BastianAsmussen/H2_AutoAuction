USE 'User'
GO

-- Create the Users table if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
   CREATE TABLE Users
   (
       Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

       Username VARCHAR(64) NOT NULL UNIQUE,
       IsCorporate BIT NOT NULL,
       Balance DECIMAL NOT NULL,
   )
END
GO
