CREATE   PROC SystemAlive 

@databaseName varchar(100),
@rowCnt INT OUTPUT

AS

SELECT 
    name 
FROM 
    master.sys.databases d
WHERE 
    d.name = @databaseName

SET @rowCnt = @@ROWCOUNT

SELECT @rowCnt    