-- Create the Auction database if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Auction')
BEGIN
    CREATE DATABASE Auction
END
GO
