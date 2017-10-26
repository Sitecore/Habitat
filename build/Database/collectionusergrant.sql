-- :SETVAR DatabasePrefix habitat
-- :SETVAR UserName collectionuser
-- :SETVAR Password Test12345
:SETVAR ShardMapManagerDatabaseNameSuffix _Xdb.Collection.ShardMapManager
:SETVAR Shard0DatabaseNameSuffix _Xdb.Collection.Shard0
:SETVAR Shard1DatabaseNameSuffix _Xdb.Collection.Shard1 
GO

IF(SUSER_ID('$(UserName)') IS NULL) BEGIN
    CREATE LOGIN [$(UserName)] WITH PASSWORD = '$(Password)';
END; 
GO

USE [$(DatabasePrefix)$(ShardMapManagerDatabaseNameSuffix)]

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'$(UserName)') 
BEGIN
    CREATE USER [$(UserName)] FOR LOGIN [$(UserName)]
    GRANT SELECT ON SCHEMA :: __ShardManagement TO [$(UserName)]
END; 
GO

GRANT EXECUTE ON SCHEMA :: __ShardManagement TO [$(UserName)]

USE [$(DatabasePrefix)$(Shard0DatabaseNameSuffix)]

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'$(UserName)') 
BEGIN
    CREATE USER [$(UserName)] FOR LOGIN [$(UserName)]
    EXEC [xdb_collection].[GrantLeastPrivilege] @UserName = '$(UserName)'
END; 
GO

USE [$(DatabasePrefix)$(Shard1DatabaseNameSuffix)]

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'$(UserName)') 
BEGIN
    CREATE USER [$(UserName)] FOR LOGIN [$(UserName)]
    EXEC [xdb_collection].[GrantLeastPrivilege] @UserName = '$(UserName)'
END;
GO
