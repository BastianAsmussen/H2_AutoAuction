-- Create the Car table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Car')
BEGIN
    CREATE TABLE Car
    (
        
    )
END