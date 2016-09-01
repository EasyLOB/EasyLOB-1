/*
USE EasyLOBSecurity;
*/

/*
-- DROP

DROP TABLE AspNetActivityRoles;
DROP TABLE AspNetActivity;
*/

/*
-- DROP

DROP TABLE AspNetUserClaims;
DROP TABLE AspNetUserLogins;
DROP TABLE AspNetUserRoles;
DROP TABLE AspNetUsers;

DROP TABLE AspNetActivityRoles;
DROP TABLE AspNetActivity;

DROP TABLE AspNetRoles;

DROP TABLE __MigrationHistory;
*/

CREATE TABLE AspNetActivity
(
	Id varchar(128) NOT NULL
	,Name varchar(255) NOT NULL
	,CONSTRAINT PK_AspNetActivity PRIMARY KEY (Id)
);
CREATE INDEX IX_AspNetActivity_01 ON AspNetActivity(Name);

CREATE TABLE AspNetActivityRoles
(
	ActivityId varchar(128) NOT NULL
	,RoleId varchar(128) NOT NULL
	,Operations varchar(255) NULL
	,CONSTRAINT PK_AspNetActivityRoles PRIMARY KEY (ActivityId, RoleId)
);
ALTER TABLE AspNetActivityRoles ADD CONSTRAINT FK_AspNetActivityRoles_01
    FOREIGN KEY(ActivityId) REFERENCES AspNetActivity(Id) ON UPDATE CASCADE;
ALTER TABLE AspNetActivityRoles ADD CONSTRAINT FK_AspNetActivityRoles_02
    FOREIGN KEY(RoleId) REFERENCES AspNetRoles(Id) ON UPDATE CASCADE;
