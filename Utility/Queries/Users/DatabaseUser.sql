USE AutoAuction
GO

CREATE PROCEDURE createUser (@username TEXT, @sql TEXT)
AS
BEGIN
    SET @username = 'TestUser'
    SET @sql = 'CREATE LOGIN ' + @username + ' WITH PASSWORD = ''' + @username + ''', DEFAULT_DATABASE = ' + @username + ', CHECK_POLICY = OFF;'
    EXEC (@sql);
END
