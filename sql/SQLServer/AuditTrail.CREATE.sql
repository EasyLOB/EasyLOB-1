/*
USE EasyLOBAuditTrail
GO
*/

/*
-- DROP

DROP TABLE AuditTrailConfiguration
DROP TABLE AuditTrailLog
*/

CREATE TABLE AuditTrailConfiguration
(
	AuditTrailConfigurationId int IDENTITY(1,1) NOT NULL
	,Domain varchar(256) NOT NULL
	,Entity varchar(256) NOT NULL
	,LogOperations varchar(256)
	,LogMode varchar(1) -- (N)one | (K)ey | (E)ntity
    ,CONSTRAINT PK_AuditTrailConfiguration PRIMARY KEY (AuditTrailConfigurationId)
)
ALTER TABLE AuditTrailConfiguration ADD CONSTRAINT UN_AuditTrailConfiguration_01
    UNIQUE (Domain, Entity)
CREATE INDEX IX_AuditTrailConfiguration_01 ON AuditTrailConfiguration(Domain)
CREATE INDEX IX_AuditTrailConfiguration_02 ON AuditTrailConfiguration(Entity)

CREATE TABLE AuditTrailLog
(
	AuditTrailLogId int IDENTITY(1,1) NOT NULL
	,LogDate datetime
	,LogTime datetime
	,LogUserName varchar(256)
	,LogDomain varchar(256) NOT NULL
	,LogEntity varchar(256) NOT NULL
	,LogOperation varchar(1) -- (C)reate | (R)ead | (U)pdate | (D)elete
	,LogId varchar(4096) -- { JSON } C R U D
	,LogEntityBefore varchar(4096) -- { JSON } - R U D
	,LogEntityAfter varchar(4096) -- { JSON } C - U -
    ,CONSTRAINT PK_AuditTrailLog PRIMARY KEY (AuditTrailLogId)
)
CREATE INDEX IX_AuditTrailLog_01 ON AuditTrailLog(LogDate, LogTime)
CREATE INDEX IX_AuditTrailLog_02 ON AuditTrailLog(LogUserName)
CREATE INDEX IX_AuditTrailLog_03 ON AuditTrailLog(LogDomain)
CREATE INDEX IX_AuditTrailLog_04 ON AuditTrailLog(LogEntity)
CREATE INDEX IX_AuditTrailLog_05 ON AuditTrailLog(LogOperation)
