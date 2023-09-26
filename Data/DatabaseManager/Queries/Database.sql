USE master
GO

-- Create the AutoAuction database if it does not exist.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'AutoAuction')
    BEGIN
        CREATE DATABASE AutoAuction
    END
GO
