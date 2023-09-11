-- Create the Vehicle database if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Vehicle')
BEGIN
    CREATE DATABASE Vehicle
END
GO
