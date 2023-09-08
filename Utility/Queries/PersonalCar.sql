-- Create the PersonalCar table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PersonalCar')
BEGIN
    CREATE TABLE PersonalCar
    (

    )
END
GO
