-- Create the Vehicle database if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'User')
BEGIN
    CREATE DATABASE User
END
GO
