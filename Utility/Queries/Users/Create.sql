USE 'Master'
GO

-- Drop the User database if it exists.
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'User')
    BEGIN
        ALTER DATABASE Vehicle SET SINGLE_USER WITH ROLLBACK IMMEDIATE
        DROP DATABASE Vehicle
    END
GO

-- Create the User database if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'User')
    BEGIN
        CREATE DATABASE User
    END
GO
