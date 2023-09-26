USE 'User'
GO 

-- Create the PrivateUser table if it does not exist
IF NOT EXISTS (SELECT * FROM sys.database WHERE name = 'PrivateUser')
BEGIN
    CREATE TABLE PrivateUser(
        Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
        
        CPR CHAR(11) UNIQUE NOT NULL,

        UserId INT FOREIGN KEY NOT NULL REFERENCES Users(Id),
    )
END
